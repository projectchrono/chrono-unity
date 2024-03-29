// =============================================================================
// PROJECT CHRONO - http://projectchrono.org
//
// Copyright (c) 2024 projectchrono.org
// All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found
// in the LICENSE file at the top level of the distribution and at
// http://projectchrono.org/license-chrono.txt.
//
// =============================================================================
// Authors: Josh Diyn
// =============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Delay this until most other scripts have executed
[DefaultExecutionOrder(800)]
public class Terrain_Mesh_Viewer : MonoBehaviour
{

    public RigidTerrain chronoRigidTerrain;
    public float gridSize = 50.0f;
    public float interval = 0.2f;
    private Mesh mesh;
    private Vector3[] vertices;
    private bool meshCreated = false; // Flag to check if mesh is already created
    private int chunkSize = 5; // Size of each chunk

    void Start()
    {
        // Find the UChRigidTerrainManager component in the parent and access its chronoRigidTerrain
        UChRigidTerrainManager terrainManager = FindAnyObjectByType<UChRigidTerrainManager>();

        if (terrainManager != null)
        {
            chronoRigidTerrain = UChRigidTerrainManager.chronoRigidTerrain;
            Debug.Log("Rigid Terrain Found: " + chronoRigidTerrain.ToString());
        }
        else
        {
            Debug.LogError("UChRigidTerrainManager component not found in parent GameObjects.");
        }
    }

    // Chunk mesh creation
    public void GenerateMesh()
    {
        if (chronoRigidTerrain == null)
        {
            Debug.LogError("ChronoRigidTerrain reference is not set.");
            return;
        }

        int numChunksX = Mathf.CeilToInt(gridSize / chunkSize);
        int numChunksZ = Mathf.CeilToInt(gridSize / chunkSize);
        ClearExistingChunks(); // Clear old chunks
        for (int chunkX = 0; chunkX < numChunksX; chunkX++)
        {
            for (int chunkZ = 0; chunkZ < numChunksZ; chunkZ++)
            {
                // Calculate actual size of this chunk (might be smaller if at the edge of the grid)
                float actualChunkSizeX = Mathf.Min(chunkSize, gridSize - chunkX * chunkSize);
                float actualChunkSizeZ = Mathf.Min(chunkSize, gridSize - chunkZ * chunkSize);

                CreateChunk(chunkX, chunkZ, actualChunkSizeX, actualChunkSizeZ);
            }
        }
    }

    private void ClearExistingChunks()
    {
        // Iterate through all child GameObjects (chunks) and destroy them
        while (transform.childCount > 0)
        {
            Transform child = transform.GetChild(0);
            DestroyImmediate(child.gameObject); // Use Destroy() in a non-editor script
        }
    }

    private void CreateChunk(int chunkX, int chunkZ, float actualChunkSizeX, float actualChunkSizeZ)
    {
        Vector3 gridCentre = this.gameObject.transform.position;
        double halfGridSize = gridSize / 2.0;
        int numPointsX = Mathf.CeilToInt(chunkSize / interval) + 1;
        int numPointsZ = Mathf.CeilToInt(chunkSize / interval) + 1;

        Vector3[] chunkVertices = new Vector3[numPointsX * numPointsZ];
        List<int> triangles = new List<int>();

        // Create vertices for this chunk
        for (int x = 0; x < numPointsX; x++)
        {
            for (int z = 0; z < numPointsZ; z++)
            {
                double localX = (chunkX * chunkSize + x * interval) - halfGridSize;
                double localZ = (chunkZ * chunkSize + z * interval) - halfGridSize;
                int index = z * numPointsX + x;
                chunkVertices[index] = new Vector3((float)localX, 0, (float)localZ) + gridCentre;
            }
        }

        // Create triangles
        for (int x = 0; x < numPointsX - 1; x++)
        {
            for (int z = 0; z < numPointsZ - 1; z++)
            {
                int topLeft = z * numPointsX + x;
                int topRight = topLeft + 1;
                int bottomLeft = topLeft + numPointsX;
                int bottomRight = bottomLeft + 1;

                // First triangle of quad
                triangles.Add(topLeft);
                triangles.Add(bottomLeft);
                triangles.Add(topRight);

                // Second triangle of quad
                triangles.Add(topRight);
                triangles.Add(bottomLeft);
                triangles.Add(bottomRight);
            }
        }

        // Create and set up the mesh for this chunk
        Mesh chunkMesh = new Mesh();
        chunkMesh.vertices = chunkVertices;
        chunkMesh.triangles = triangles.ToArray();
        chunkMesh.RecalculateNormals();
        chunkMesh.RecalculateBounds();

        // Create a GameObject for this chunk
        GameObject chunkObject = new GameObject("Chunk_" + chunkX + "_" + chunkZ);
        chunkObject.transform.parent = this.transform;

        // Assign mesh to MeshFilter
        MeshFilter meshFilter = chunkObject.AddComponent<MeshFilter>();
        meshFilter.mesh = chunkMesh;

        // Assign material from parent to MeshRenderer
        MeshRenderer meshRenderer = chunkObject.AddComponent<MeshRenderer>();
        MeshRenderer parentRenderer = GetComponent<MeshRenderer>();
        if (parentRenderer != null)
        {
            meshRenderer.sharedMaterial = parentRenderer.sharedMaterial;
        }
        else
        {
            meshRenderer.sharedMaterial = new Material(Shader.Find("Standard")); // Fallback material
        }
    }

    public void UpdateMeshHeights()
    {
        if (chronoRigidTerrain == null)
        {
            Debug.LogError("ChronoRigidTerrain reference is not set.");
            return;
        }

        // Iterate through all child GameObjects (chunks)
        foreach (Transform child in transform)
        {
            MeshFilter meshFilter = child.GetComponent<MeshFilter>();
            if (meshFilter != null && meshFilter.mesh != null)
            {
                Mesh chunkMesh = meshFilter.mesh;
                Vector3[] vertices = chunkMesh.vertices;

                for (int i = 0; i < vertices.Length; i++)
                {
                    // Transforming the local vertex position to world space
                    Vector3 worldVertex = child.TransformPoint(vertices[i]);

                    // Getting the height at the world space position
                    ChVector3d vectorLoc = new ChVector3d(worldVertex.x, 100, -worldVertex.z);

                    float height = (float)chronoRigidTerrain.GetHeight(vectorLoc);
                    vertices[i].y = height;
                }

                chunkMesh.vertices = vertices;
                chunkMesh.RecalculateNormals();
                chunkMesh.RecalculateBounds();
            }
        }
    }
}
