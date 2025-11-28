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
// Authors: Bocheng Zou, Json Zhou
// =============================================================================

using UnityEngine;
using System;
using System.Collections.Generic;

[DefaultExecutionOrder(0)]
public class UChSCMTerrain : MonoBehaviour
{
    public Patch patch;
    private ChContactMaterial mat;
    public enum PatchType
    {
        boxPatch,
        unityTerrainPatch
    }

    public SCMTerrain chronoTerrain { get; protected set; }
    private Terrain sourceTerrain;

    private List<Terrain> terrainGrid = new List<Terrain>();
    private float chunkSize;
    private int chunkRes;
    private double delta;

    [SerializeField] private int terrainSplit;

    [SerializeField]
    public PatchType patchType; // Field to set the type of patch in the Unity Editor


    // Initialize SCM Terrain in Chrono
    public void AddHeightMapPatchTerrain()
    {
        var terrainData = sourceTerrain.terrainData;

        // Determine the 'Chrono' origin of the terrain path. that is the central 0,0 (as opposed to Unity terrain's bottom right point.
        Vector3 localCenter = terrainData.size * 0.5f;
        localCenter.y = 0;
        Vector3 centralTerrainPoint = this.transform.TransformPoint(localCenter);

        // Position and rotate the terrain to suit a Y-Up world
        var rot = Utils.ToChronoFlip(transform.rotation); // flip Z axis
        var pos = Utils.ToChronoFlip(centralTerrainPoint); 

        ChQuaterniond qAxis = new ChQuaterniond(chrono.QuatFromAngleX( -chrono.CH_PI_2)); // Rotate SCM Terrain from Z-up to Y-up
        ChQuaterniond qRotationToChrono = new ChQuaterniond();
        qRotationToChrono.Cross(rot, qAxis); // order is paramount

        chronoTerrain = new SCMTerrain(UChSystem.chrono_system);
        chronoTerrain.SetReferenceFrame(new ChCoordsysd(pos, qRotationToChrono));
        chronoTerrain.SetBoundary(new ChAABB(new ChVector3d(-terrainData.size.x / 2.0, -terrainData.size.z / 2.0, 0),
                                            new ChVector3d( terrainData.size.x / 2.0,  terrainData.size.z / 2.0, 0))); // Z doesn't matter here
        // chronoTerrain.SetReferenceFrame(new ChCoordsysd(new ChVector3d(0,0,0),qAxis));

        var chTriangleTerrain = ConvertTerrainToChTriangleMesh(sourceTerrain);

        chronoTerrain.Initialize(chTriangleTerrain, delta);
    }

    private ChTriangleMeshConnected ConvertTerrainToChTriangleMesh(Terrain terrain)
    {
        ChTriangleMeshConnected mesh = new ChTriangleMeshConnected();

        TerrainData terrainData = terrain.terrainData;
        int resolution = terrainData.heightmapResolution;
        float[,] heights = terrainData.GetHeights(0, 0, resolution, resolution);

        double xScale = terrainData.size.x / (resolution - 1);
        double zScale = terrainData.size.z / (resolution - 1);
        double yScale = terrainData.size.y;

        // Generate triangles
        for (int x = 0; x < resolution - 1; x++)
        {
            for (int z = 0; z < resolution - 1; z++)
            {

                // Vertices for the current quad
                var v0 = new ChVector3d(
                    (x * xScale) - (terrainData.size.x / 2.0),
                    (z * zScale) - (terrainData.size.z / 2.0),
                    heights[z, x] * yScale
                );

                var v1 = new ChVector3d(
                    ((x + 1) * xScale) - (terrainData.size.x / 2.0),
                    (z * zScale) - (terrainData.size.z / 2.0),
                    heights[z, x + 1] * yScale
                );

                var v2 = new ChVector3d(
                    (x * xScale) - (terrainData.size.x / 2.0),
                    ((z + 1) * zScale) - (terrainData.size.z / 2.0),
                    heights[z + 1, x] * yScale
                );

                var v3 = new ChVector3d(
                    ((x + 1) * xScale) - (terrainData.size.x / 2.0),
                    ((z + 1) * zScale) - (terrainData.size.z / 2.0),
                    heights[z + 1, x + 1] * yScale
                );

                // Create triangles and add to mesh
                var triangle1 = new ChTriangle(v0, v1, v2);
                var triangle2 = new ChTriangle(v2, v1, v3);

                mesh.AddTriangle(triangle1);
                mesh.AddTriangle(triangle2);
            }
        }

        return mesh;
    }


    void Awake()
    {
        sourceTerrain = GetComponent<Terrain>();
        if (sourceTerrain == null)
        {
            Debug.LogError("Terrain component not found.");
            return;
        }
        delta = sourceTerrain.terrainData.heightmapScale[0];

        AddHeightMapPatchTerrain();

        if (terrainSplit <= 0)
        {
            Debug.LogError("terrainSplit must be >= 1");
            enabled = false;
            return;
        }

        var hmRes = GetComponent<Terrain>().terrainData.heightmapResolution;
        if (terrainSplit > hmRes - 1)
        {
            Debug.LogError($"terrainSplit too large. Need <= {hmRes - 1}, got {terrainSplit}");
            enabled = false;
            return;
        }

        SplitTerrain(sourceTerrain, terrainSplit, terrainSplit);

        if (chunkRes <= 1)
        {
            Debug.LogError($"Invalid chunkRes={chunkRes}. Would cause division by zero in FixedUpdate.");
            enabled = false;
            return;
        }

        // sourceTerrain.SetActive(false);
    }

    void FixedUpdate()
    {
        chronoTerrain.Synchronize(UChSystem.chrono_system.GetChTime());
        chronoTerrain.Advance(UChSystem.chrono_system.GetStep());

        ChVector2i test;
        ChSCMTerrainNodeLevelList modifiedNodes = chronoTerrain.GetModifiedNodes(false);

        TerrainData tData = sourceTerrain.terrainData;
        int hmRes = tData.heightmapResolution;
        double sizeX = tData.size.x;  // total width
        double sizeZ = tData.size.z;  // total length
        double sizeY = tData.size.y;  // total height range

        // Dictionary to store per-chunk heightmaps
        // Key = (chunkIndexX, chunkIndexZ), Value = chunkHeights array
        Dictionary<Vector2Int, float[,]> updatedChunkMaps = new Dictionary<Vector2Int, float[,]>();

        // Also store references to chunk if you like, or you can re-derive them later
        Dictionary<Vector2Int, Terrain> chunkReference = new Dictionary<Vector2Int, Terrain>();

        foreach (var node in modifiedNodes)
        {
            ChVector2i vec = node.first;

            // Coordinate in Chrono (local to SCM Terrain)
            double x = vec.x * delta;
            double z = vec.y * delta;
            double y = node.second;

            // Coordinate in Unity
            int xIndex = (int)(x / sizeX  * (hmRes - 1) + (hmRes / 2));
            int zIndex = (int)(z / sizeZ  * (hmRes - 1) + (hmRes / 2));

            xIndex = Math.Clamp(xIndex, 0, hmRes - 1);
            zIndex = Math.Clamp(zIndex, 0, hmRes - 1);

            // int xIndex = (int)(x + (hmRes / 2));
            // int zIndex = (int)(z + (hmRes / 2));

            float normalizedHeight = (float)(y / sizeY);

            // Get tile index

            int chunkIndexX = xIndex / (chunkRes - 1);
            int chunkIndexZ = zIndex / (chunkRes - 1);

            Vector2Int chunkKey = new Vector2Int(chunkIndexX, chunkIndexZ);

            // Get height array of that tile index from terrainGrid
            if (!updatedChunkMaps.ContainsKey(chunkKey))
            {
                Terrain chunk = terrainGrid[chunkIndexX * terrainSplit + chunkIndexZ];
                float[,] chunkHeights = chunk.terrainData.GetHeights(0, 0, chunkRes, chunkRes);

                updatedChunkMaps[chunkKey] = chunkHeights;
                chunkReference[chunkKey] = chunk;
            }

            // Get the index of the height in the tile

            int localX = xIndex % (chunkRes - 1);
            int localZ = zIndex % (chunkRes - 1);

            updatedChunkMaps[chunkKey][localZ, localX] = normalizedHeight;
        }

        foreach (var kvp in updatedChunkMaps)
        {
            Vector2Int key = kvp.Key;
            float[,] newHeights = kvp.Value;

            // Retrieve the chunk Terrain
            Terrain chunk = chunkReference[key];

            // Set the updated heightmap
            chunk.terrainData.SetHeights(0, 0, newHeights);
        }

    }
    void SplitTerrain(Terrain terrain, int splitCountX, int splitCountZ)
    {
        TerrainData originalData = terrain.terrainData;
        int originalHeightmapResolution = originalData.heightmapResolution;
        Vector3 originalSize = originalData.size;

        // Each new chunk's terrain resolution (minus 1, because e.g. a 513x513 heightmap
        // splits into two chunks of 257x257 each, which is 513/2 + 1).
        int newHeightmapResolution = (originalHeightmapResolution - 1) / splitCountX + 1;
        float chunkWidth = originalSize.x / splitCountX;
        float chunkLength = originalSize.z / splitCountZ;

        chunkSize = chunkWidth;
        chunkRes = newHeightmapResolution;

        // --- 1) Obtain alphaMap info from original terrain ---
        int alphaMapResolution = originalData.alphamapResolution;
        int numAlphaLayers = originalData.alphamapLayers;

        // For details:
        int detailResolution = originalData.detailResolution;

        // For each "tile" in splitX x splitZ
        for (int x = 0; x < splitCountX; x++)
        {
            for (int z = 0; z < splitCountZ; z++)
            {
                // 1) Create the new TerrainData
                TerrainData chunkData = new TerrainData();
                chunkData.detailPrototypes = originalData.detailPrototypes;

                // Heightmap resolution for the chunk
                chunkData.heightmapResolution = newHeightmapResolution;
                // Keep the same vertical height
                chunkData.size = new Vector3(chunkWidth, originalSize.y, chunkLength);

                // 2) Copy heights from the source
                float[,] chunkHeights = GetChunkHeights(originalData, x, z, splitCountX, splitCountZ);
                chunkData.SetHeights(0, 0, chunkHeights);

                // 3) Copy alphamaps
                //    We need to slice the alphamap from the original. 
                //    The slicing logic is analogous, but be aware that
                //    the alphamap resolution might differ from the heightmap resolution.
                float[,,] chunkAlphaMaps = GetChunkAlphaMaps(originalData, x, z, splitCountX, splitCountZ);
                chunkData.alphamapResolution = chunkAlphaMaps.GetLength(0);
                // ^ or set it explicitly if you prefer a guaranteed size
                chunkData.SetAlphamaps(0, 0, chunkAlphaMaps);

                // 4) Copy detail/grass layers
                //    If the original terrain has multiple detail layers, 
                //    iterate them and slice each layer into chunkData.
                int detailLayers = originalData.detailPrototypes.Length;
                chunkData.SetDetailResolution(detailResolution / splitCountX, 8);
                //   ^ note: set the resolution and pixelError (8 is a typical base). 
                //     Adjust as needed for your project.

                chunkData.terrainLayers = originalData.terrainLayers;

                for (int layer = 0; layer < detailLayers; layer++)
                {
                    int[,] chunkDetailMap = GetChunkDetailMap(originalData, x, z, splitCountX, splitCountZ, layer);
                    chunkData.SetDetailLayer(0, 0, layer, chunkDetailMap);
                }

                // 5) Create new Terrain GameObject
                GameObject chunkGO = Terrain.CreateTerrainGameObject(chunkData);
                chunkGO.name = $"Terrain_Chunk_{x}_{z}";
                chunkGO.GetComponent<Terrain>().materialTemplate = terrain.materialTemplate;

                // 6) Position the chunk in the correct place
                // The original Terrain might be offset in the scene (terrain.transform.position).
                Vector3 terrainPosition = terrain.GetPosition();
                float posX = terrainPosition.x + x * chunkWidth;
                float posZ = terrainPosition.z + z * chunkLength;
                chunkGO.transform.position = new Vector3(posX, terrainPosition.y, posZ);

                // 7) Store the newly created terrain for reference
                terrainGrid.Add(chunkGO.GetComponent<Terrain>());
            }
        }

        // Disable the original big terrain so it doesn’t overlap
        GetComponent<Terrain>().enabled = false;
    }

    private float[,,] GetChunkAlphaMaps(TerrainData originalData, int chunkX, int chunkZ, int splitCountX, int splitCountZ)
    {
        int alphaMapWidth = originalData.alphamapWidth;
        int alphaMapHeight = originalData.alphamapHeight;
        int numAlphaLayers = originalData.alphamapLayers;

        // The resolution for each chunk's alphamap.
        // This may or may not match the chunk's heightmap logic exactly.
        // Adjust if you want a different scheme.
        int chunkAlphaWidth = alphaMapWidth / splitCountX;
        int chunkAlphaHeight = alphaMapHeight / splitCountZ;

        // Calculate the starting index in the original alphamap
        int startX = chunkAlphaWidth * chunkX;
        int startZ = chunkAlphaHeight * chunkZ;

        // Get the alpha slice
        float[,,] alphaSlice = originalData.GetAlphamaps(startX, startZ, chunkAlphaWidth, chunkAlphaHeight);
        return alphaSlice;
    }

    private int[,] GetChunkDetailMap(TerrainData originalData, int chunkX, int chunkZ, int splitCountX, int splitCountZ, int layer)
    {
        int detailResolution = originalData.detailResolution;
        int chunkDetailResolutionX = detailResolution / splitCountX;
        int chunkDetailResolutionZ = detailResolution / splitCountZ;

        int startX = chunkDetailResolutionX * chunkX;
        int startZ = chunkDetailResolutionZ * chunkZ;

        // Slice out the detail (grass) data for this layer
        int[,] detailSlice = originalData.GetDetailLayer(startX, startZ, chunkDetailResolutionX, chunkDetailResolutionZ, layer);
        return detailSlice;
    }

    private float[,] GetChunkHeights(TerrainData originalData, int chunkX, int chunkZ, int splitCountX, int splitCountZ)
    {
        int originalResolution = originalData.heightmapResolution;
        // The resolution of each chunk’s heightmap
        int newResolutionX = (originalResolution - 1) / splitCountX + 1;
        int newResolutionZ = (originalResolution - 1) / splitCountZ + 1;

        // Where to start reading the original heightmap
        int startX = (newResolutionX - 1) * chunkX;
        int startZ = (newResolutionZ - 1) * chunkZ;

        // Extract the heights from the original data
        float[,] chunkHeights = originalData.GetHeights(startX, startZ, newResolutionX, newResolutionZ);
        return chunkHeights;
    }
}