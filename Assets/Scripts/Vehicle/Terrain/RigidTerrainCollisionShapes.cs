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
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(800)]
public class RigidTerrainCollisionShapes : MonoBehaviour
{
    public RigidTerrain chronoRigidTerrain;
    private Material lineMaterial;
    private List<Vector3> vertices = new List<Vector3>();
    private ComputeBuffer vertexBuffer;

    void Start()
    {
        // Setup the material for line rendering
        Shader shader = Shader.Find("Custom/DrawCollisionShape");
        if (shader == null)
        {
            Debug.LogError("'DrawCollisionShape' shader not found.");
            return;
        }
        lineMaterial = new Material(shader);

        // Fetching the RigidTerrain component
        chronoRigidTerrain = chrono_vehicle.CastToRigidTerrain(UChRigidTerrainManager.chronoTerrain);
        if (chronoRigidTerrain == null)
        {
            Debug.LogError("ChronoRigidTerrain reference is not set.");
            return;
        }

        vertices = new List<Vector3>();
        GenerateTerrainMeshes();
        InitializeVertexBuffer();
    }

    void GenerateTerrainMeshes()
    {
        foreach (var patch in chronoRigidTerrain.GetPatches())
        {
            ChBody groundBody = patch.GetGroundBody();
            var numShapes = groundBody.GetCollisionModel().GetNumShapes();
            for (int shapeIndex = 0; shapeIndex < numShapes; shapeIndex++)
            {
                var shapeInstance = groundBody.GetCollisionModel().GetShapeInstance(shapeIndex);
                ChFramed localFrame = shapeInstance.frame; // Get local frame
                ChFramed worldFrame = groundBody.GetFrameRefToAbs();
                ChCollisionShape.Type shapeType = shapeInstance.shape.GetType();

                switch (shapeType)
                {
                    // Most rigid terrain is either box patches or trianglemesh
                    case ChCollisionShape.Type.TRIANGLEMESH:
                        var chronoMesh = chrono.CastToChCollisionShapeTriangleMesh(shapeInstance.shape);
                        for (uint i = 0; i < chronoMesh.GetMesh().GetNumTriangles(); i++)
                        {
                            var triangle = chronoMesh.GetMesh().GetTriangle(i);
                            // Transform each vertex by the local frame transformation
                            Vector3 v1 = Utils.FromChronoFlip(worldFrame.TransformPointLocalToParent(localFrame.TransformPointLocalToParent(triangle.p1)));
                            Vector3 v2 = Utils.FromChronoFlip(worldFrame.TransformPointLocalToParent(localFrame.TransformPointLocalToParent(triangle.p2)));
                            Vector3 v3 = Utils.FromChronoFlip(worldFrame.TransformPointLocalToParent(localFrame.TransformPointLocalToParent(triangle.p3)));

                            // Line renderer shader requires instructing the order of vertices for the creation of a triangle
                            vertices.Add(v1); vertices.Add(v2); // Edge 1
                            vertices.Add(v2); vertices.Add(v3); // Edge 2
                            vertices.Add(v3); vertices.Add(v1); // Edge 3
                            // This is not particularly efficient, as it doubles the vertices required, however this is suitable for tolerance checking of geometry
                        }
                    break;
                    
                    // Primitives usually make use of just boxes
                    case ChCollisionShape.Type.BOX:
                        var chronoBox = chrono.CastToChCollisionShapeBox(shapeInstance.shape);
                        ChVector3d halfExtents = chronoBox.GetHalflengths();
                        var center = new ChVector3d(0, 0, 0); // (assuming easybox with centre at the local origin)

                        ChVector3d[] corners = new ChVector3d[8];
                        // Generate corners programmatically
                        for (int i = 0; i < 8; i++)
                        {
                            corners[i] = new ChVector3d(
                                ((i & 1) == 0 ? -halfExtents.x : halfExtents.x),  // Check the least significant bit
                                ((i & 2) == 0 ? -halfExtents.y : halfExtents.y),  // Check the second least significant bit
                                ((i & 4) == 0 ? -halfExtents.z : halfExtents.z)); // Check the third least significant bit
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
                            Vector3 v1 = Utils.FromChronoFlip(worldFrame.TransformPointLocalToParent(localFrame.TransformPointLocalToParent(corners[edge[0]])));
                            Vector3 v2 = Utils.FromChronoFlip(worldFrame.TransformPointLocalToParent(localFrame.TransformPointLocalToParent(corners[edge[1]])));
                            vertices.Add(v1);
                            vertices.Add(v2);
                        }
                        break;

                    default:
                        // If unknown. Could get bounding box and provides those vertices if required.
                        Debug.LogWarning("Cannot draw collision shape. Unsupported shape type: " + shapeType.ToString());
                        break;
                }
            }
        }
    }


    void InitializeVertexBuffer()
    {
        vertexBuffer = new ComputeBuffer(vertices.Count, sizeof(float) * 3, ComputeBufferType.Default);
        vertexBuffer.SetData(vertices);
    }

    void OnRenderObject()
    {
        if (lineMaterial != null)
        {
            lineMaterial.SetPass(0);
            lineMaterial.SetBuffer("vertexPositions", vertexBuffer);
            Graphics.DrawProceduralNow(MeshTopology.Lines, vertexBuffer.count, 1);
        }
    }

    void OnDestroy()
    {
        if (vertexBuffer != null)
        {
            vertexBuffer.Release();
        }
    }
}