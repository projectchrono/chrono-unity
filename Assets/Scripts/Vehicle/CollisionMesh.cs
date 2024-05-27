// =============================================================================
// PROJECT CHRONO - http://projectchrono.org
//
// Copyright (c) 2024 projectchrono.org
// All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found
// in the LICENSE file at the top level of the distribution.
//
// =============================================================================
// Authors: Josh Diyn
// =============================================================================
// Used for rendering the col shapes of vehicles. However, this and the
// associated compute script are only working on one vehicle at a time.
// =============================================================================

using System.Collections.Generic;
using UnityEngine;
// ensure executes after all other necessary initialisations in chrono so that there's an object to call upon
[DefaultExecutionOrder(500)]
public class CollisionMesh : MonoBehaviour
{
    private Material lineMaterial;
    public ComputeShader transformShader;
    private ComputeBuffer vertexBuffer, transformedVertexBuffer, localTransformsBuffer;
    private int kernelHandle;
    private ChBodyAuxRef chassisBody;
    private bool buffersSet = false; // used to ensure buffers will be cleared at the end

    void Start()
    {
        // Setup visual shader (for wireframe line drawings)
        Shader shader = Shader.Find("Custom/DrawCollisionShape");
        if (shader == null)
        {
            Debug.LogError("'DrawCollisionShape' shader not found.");
            return;
        }
        lineMaterial = new Material(shader);

        transformShader = (ComputeShader)Resources.Load("VertexTransform");
        if (transformShader == null)
        {
            Debug.LogError("'VertexTransform' shader not found.");
            return;
        }

        // connect the parent object (vehicle). This script could be expanded for wider use.
        chassisBody = GetComponentInParent<UChVehicle>().GetChVehicle().GetChassisBody();
        if (chassisBody.GetCollisionModel() == null)
        {
            Debug.Log("No collision model to draw");
            this.GetComponent<CollisionMesh>().enabled = false;
            return;
        }
        GenerateCollisionMeshes();
        InitializeComputeBuffers();
        buffersSet = true; // buffers have been created
    }

    // Switch approach to build the mesh depending on what's pulled from the col model.
    void GenerateCollisionMeshes()
    {        
        var numShapes = chassisBody.GetCollisionModel().GetNumShapes();
        List<Vector3> vertices = new List<Vector3>();
        List<Matrix4x4> localTransforms = new List<Matrix4x4>();

        for (int shapeIndex = 0; shapeIndex < numShapes; shapeIndex++)
        {
            var shapeInstance = chassisBody.GetCollisionModel().GetShapeInstance(shapeIndex);
            ChFramed localFrame = shapeInstance.second;
            Matrix4x4 localMatrix = ConvertFrameToMatrix(localFrame);
            localTransforms.Add(localMatrix);

            ChCollisionShape.Type shapeType = shapeInstance.first.GetType();

            switch (shapeType)
            {
                // build the vertex list of the convex hull collision model
                case ChCollisionShape.Type.CONVEXHULL:
                    var chronoHull = chrono.CastToChCollisionShapeConvexHull(shapeInstance.first);
                    foreach (var point in chronoHull.GetPoints())
                    {
                        // these are not in order, so will not generate a clear set of points for triangles
                        Vector3 localPoint = Utils.FromChronoFlip(point);
                        vertices.Add(localPoint);
                    }
                    break;
                // in the case of a triangular mesh
                case ChCollisionShape.Type.TRIANGLEMESH:
                    var chronoMesh = chrono.CastToChCollisionShapeTriangleMesh(shapeInstance.first);
                    for (uint i = 0; i < chronoMesh.GetMesh().GetNumTriangles(); i++)
                    {
                        var triangle = chronoMesh.GetMesh().GetTriangle(i);
                        // Add the triangle points to the vertices list
                        // the shader needs to be instructed about the order to generate triangles
                        // which leads to more vertices than are in the shape
                        // this could be optimised.
                        vertices.Add(Utils.FromChronoFlip(triangle.p1)); vertices.Add(Utils.FromChronoFlip(triangle.p2));
                        vertices.Add(Utils.FromChronoFlip(triangle.p2)); vertices.Add(Utils.FromChronoFlip(triangle.p3));
                        vertices.Add(Utils.FromChronoFlip(triangle.p3)); vertices.Add(Utils.FromChronoFlip(triangle.p1));
                    }
                    break;
                // Primitives usually make use of just boxes
                case ChCollisionShape.Type.BOX:
                    var chronoBox = chrono.CastToChCollisionShapeBox(shapeInstance.first);
                    Vector3 halfExtents = Utils.FromChronoFlip(chronoBox.GetHalflengths());
                    var center = new Vector3(0, 0, 0); // (assuming easybox with centre at the local origin)

                    Vector3[] corners = new Vector3[8];
                    // Generate corners programmatically
                    for (int i = 0; i < 8; i++)
                    {
                        corners[i] = center + new Vector3(
                            (i & 1) == 0 ? -halfExtents.x : halfExtents.x, // Check the least significant bit
                            (i & 2) == 0 ? -halfExtents.y : halfExtents.y, // Check the second least significant bit
                            (i & 4) == 0 ? -halfExtents.z : halfExtents.z  // Check the third least significant bit
                        );
                    }

                    // Define edges of the box using the corners to properly order the drawing of the box.
                    int[][] edges = {
                        new int[] { 0, 1 }, new int[] { 1, 3 }, new int[] { 3, 2 }, new int[] { 2, 0 }, // Bottom face
                        new int[] { 4, 5 }, new int[] { 5, 7 }, new int[] { 7, 6 }, new int[] { 6, 4 }, // Top face
                        new int[] { 0, 4 }, new int[] { 1, 5 }, new int[] { 2, 6 }, new int[] { 3, 7 }  // Verticals
                    };
                    // Add edges based on the corner indices
                    foreach (int[] edge in edges)
                    {
                        vertices.Add(corners[edge[0]]);
                        vertices.Add(corners[edge[1]]);
                    }
                    break;
                default:
                    // If unknown. Could get bounding box and provides those vertices if required.
                    Debug.LogWarning("Cannot draw collision shape. No code to render a: " + shapeType.ToString());
                    break;
            }
        }
        // Send to the buffers for computing on the gpu
        vertexBuffer = new ComputeBuffer(vertices.Count, sizeof(float) * 3);
        vertexBuffer.SetData(vertices);
        localTransformsBuffer = new ComputeBuffer(localTransforms.Count, sizeof(float) * 16);
        localTransformsBuffer.SetData(localTransforms);
    }

    // Set up the compute buffers to process the transforms
    void InitializeComputeBuffers()
    {
        transformedVertexBuffer = new ComputeBuffer(vertexBuffer.count, sizeof(float) * 3);
        kernelHandle = transformShader.FindKernel("CSMain");
        transformShader.SetBuffer(kernelHandle, "vertices", vertexBuffer);
        transformShader.SetBuffer(kernelHandle, "transformedVertices", transformedVertexBuffer);
        transformShader.SetBuffer(kernelHandle, "localTransforms", localTransformsBuffer);
    }

    void Update()
    {
        Matrix4x4 worldTransform = ConvertFrameToMatrix(chassisBody.GetFrameRefToAbs());
        transformShader.SetMatrix("transformationMatrix", worldTransform);
        transformShader.Dispatch(kernelHandle, vertexBuffer.count / 256 + 1, 1, 1);
    }

    void OnRenderObject()
    {
        lineMaterial.SetPass(0);
        lineMaterial.SetBuffer("vertexPositions", transformedVertexBuffer);
        Graphics.DrawProceduralNow(MeshTopology.Lines, transformedVertexBuffer.count, 1);
    }

    void OnDestroy()
    {
        // Release the buffers, if there's buffers to release
        if (buffersSet)
        {
            vertexBuffer.Release();
            transformedVertexBuffer.Release();
            localTransformsBuffer.Release();
        }
    }

    Matrix4x4 ConvertFrameToMatrix(ChFramed frame)
    {
        // helper to generate a matrix from the ChFrame which can be used in the compute shader
        Vector3 position = Utils.FromChronoFlip(frame.GetPos());
        Quaternion rotation = Utils.FromChronoFlip(frame.GetRot());
        return Matrix4x4.TRS(position, rotation, Vector3.one);
    }
}
