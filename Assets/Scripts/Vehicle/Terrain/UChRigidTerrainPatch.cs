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

using UnityEngine;
using System;

[DefaultExecutionOrder(0)]
public class UChRigidTerrainPatch : MonoBehaviour
{
    public Patch patch;
    private ChContactMaterial mat;
    public enum PatchType
    {
        boxPatch,
        unityTerrainPatch
    }
    [SerializeField]
    public PatchType patchType; // Field to set the type of patch in the Unity Editor
    
    // Variables for the box patch
    public bool tiled = false; // Tile terrain if it is large
    [Tooltip("Partition a large patch into a number of tiles")]
    public int numberOfTiles = 1;

    // Variables for the Mesh Patch
    private bool useInterpolatedPoints = false; // include every intermediate interpolated point into the cloud. Default off. Mostly useful for error/accuracy checking.
    [Tooltip("Amount of Taubin smoothing to apply to each vertex (0-1.0)")]
    public double smoothingFactor = 0.1; // Taubin smoothing use. Sensitive factor
    [Tooltip("Algorithm will mark for refinement any triangles whose normals differ by greater than this angle")]
    public double refineGreaterThanAngle = 15.0; // sets the max angle for neighbouring grid blocks to be considered similar enough to simplify
    [Tooltip("Starting grid size in meters")]
    public double coarseMeshGridSize = 2.0; // in meters, size of the coarse mesh
    [Tooltip("Number of iterations to perform")]
    public int numberOfRefinements = 3; // number of iterative passes over mesh to split triangles greater than the normal angle
    [Tooltip("Maximum length of refined triangles in meters")]
    public double maxTriangleEdgeLength = 1.0; // point at which to stop refining once reached


    // Create a Box patch Terrain
    public void AddBoxPatchTerrain(RigidTerrain chronoRigidTerrain)
    {
        Vector3 centralTerrainPoint = this.transform.position;
        centralTerrainPoint.z *= -1; // ensure the LHF is converted to RHF position!
        //var rot = Utils.ToChrono(transform.rotation);
        var pos = Utils.ToChrono(centralTerrainPoint);
        var rot = Utils.ToChrono(transform.rotation);
        // Rotation to Unity's YUP world
        // Rotate the terrain object for Unity's Y-Up world
        ChQuaterniond rotateQ = new ChQuaterniond(chrono.QuatFromAngleX(-90 * chrono.CH_DEG_TO_RAD));
        ChQuaterniond qRotationToChrono = new ChQuaterniond();
        qRotationToChrono.Cross(rot, rotateQ); // order is paramount

        // Unity scale to Chrono size
        var size = transform.localScale;

        // Create the material for the patch
        var mat_component = this.GetComponent<UChMaterialSurface>();
        mat = mat_component.mat_info.CreateMaterial(mat_component.contact_method);

        // Add the patch to Chrono's RigidTerrain
        patch = chronoRigidTerrain.AddPatch(mat, new ChCoordsysd(pos, qRotationToChrono), size.x, size.z, size.y, tiled, (tiled ? numberOfTiles : 1), false /* pass false visualisation so chrono doesn't create unnecessary visual objects */);
        
        // Debugging information
        var patchPosition = patch.GetGroundBody().GetPos();
        var patchRotation = patch.GetGroundBody().GetRot();                                
        ///Debug.Log($"Patch added with Position: {patchPosition.x}, {patchPosition.y}, {patchPosition.z},  Rotation: {patchRotation.GetAxisX().x * chrono.CH_DEG_TO_RAD}, {patchRotation.GetCardanAnglesXYZ().y * chrono.CH_DEG_TO_RAD}, {patchRotation.GetCardanAnglesXYZ().z * chrono.CH_DEG_TO_RAD}");
        ///Debug.Log($"Unity values send to Chrono. Position: {pos.x}, {pos.y}, {pos.z},  Rotation: {rot.GetCardanAnglesXYZ().x * chrono.CH_DEG_TO_RAD}, {rot.GetCardanAnglesXYZ().y * chrono.CH_DEG_TO_RAD}, {rot.GetCardanAnglesXYZ().z * chrono.CH_DEG_TO_RAD}");
    }

    // Create a point cloud based terrain from a Unity terrain object
    public void AddHeightMapPatchTerrain(RigidTerrain chronoRigidTerrain)
    {
        Terrain terrain = GetComponent<Terrain>();
        if (terrain == null)
        {
            Debug.LogError("Terrain component not found.");
            return;
        }

        double sweepSphereRadius = -0.001; // thickness of terrain

        // Determine the 'Chrono' origin of the terrain path. that is the central 0,0 (as opposed to Unity terrain's bottom right point.
        Vector3 centralTerrainPoint = this.transform.position;
        centralTerrainPoint.x += terrain.terrainData.size.x / 2;
        centralTerrainPoint.z += terrain.terrainData.size.z / 2;
        centralTerrainPoint.z *= -1; // Ensure RHF - LHF is correct

        // Position and rotate the terrain to suit a Y-Up world
        var rot = Utils.ToChrono(transform.rotation);
        var pos = Utils.ToChrono(centralTerrainPoint);
        ChQuaterniond rotateQ = new ChQuaterniond(chrono.QuatFromAngleX(-90 * chrono.CH_DEG_TO_RAD));
        ChQuaterniond qRotationToChrono = new ChQuaterniond();
        qRotationToChrono.Cross(rot, rotateQ); // order is paramount

        // Apply materials
        var mat_component = GetComponent<UChMaterialSurface>();
        var mat = mat_component.mat_info.CreateMaterial(mat_component.contact_method);

        // Extract height data from the Terrain component and convert to ChVector list
        var pointCloud = TerrainToPointCloud(terrain);

        // set the starting alternating triangle mesh resolution. balance speed and accuracy here
        int coarseMeshResolution = (int)(terrain.terrainData.size.x / coarseMeshGridSize); // if coarseMeshGridSize = 2, then 2m basic grid size starting mesh by divinding the width by 2.0m to get the number of grid squares

        Debug.Log("coarse mesh resolution = " + coarseMeshResolution);

        // Add the patch to Chrono's RigidTerrain using the extracted height vectors
        var patch = chronoRigidTerrain.AddPatch(
            mat,
            new ChCoordsysd(pos, rotateQ),
            pointCloud,
            terrain.terrainData.size.x,                         // Length of the terrain patch
            terrain.terrainData.size.z,                         // Width of the terrain patch
            coarseMeshResolution,                               // Starting mesh.
            (int)terrain.terrainData.heightmapResolution - 1,   // Point cloud grid resolution, ensure int
            numberOfRefinements,                                // number of iterations
            refineGreaterThanAngle,                             // angle limit
            smoothingFactor,                                    // Taubin smoothing factor
            maxTriangleEdgeLength,                              // max LEPP edge length in refinement
            sweepSphereRadius,                                  // 'thickness' of terrain object
            false                                               // no visual asset necessary
        );

        
        // Debugging information
        ////var patchPosition = patch.GetGroundBody().GetPos();
        ////var patchRotation = patch.GetGroundBody().GetRot();
        ////Debug.Log($"Mesh patch added with Position: {patchPosition.x}, {patchPosition.y}, {patchPosition.z}, Rotation: {patchRotation.Q_to_Euler123().x * chrono.CH_C_RAD_TO_DEG}, {patchRotation.Q_to_Euler123().y * chrono.CH_C_RAD_TO_DEG}, {patchRotation.Q_to_Euler123().z * chrono.CH_C_RAD_TO_DEG}");
        ////Debug.Log($"Mesh Path size: {terrain.unityHeightmap.size.x}, by {terrain.unityHeightmap.size.z} and a height of {terrain.unityHeightmap.size.y}");
        ////Debug.Log($"Direct details of patch terrain Ground Body xyz: {patch.GetGroundBody().GetPos().x} {patch.GetGroundBody().GetPos().y} {patch.GetGroundBody().GetPos().z}");
    }


    /// This method uses the direct height map and alternating (in between) access of the interpolated data used by unity (if set to true)
    private vector_ChVector3d TerrainToPointCloud(Terrain terrain)
    {
        vector_ChVector3d pointCloud = new vector_ChVector3d();
        TerrainData unityHeightmap = terrain.terrainData;
        ///
        /// Direct Heightmap values sent to the point cloud
        ///
        // Calculate the scale factors for real-world size
        double xScale = unityHeightmap.size.x / (unityHeightmap.heightmapResolution - 1);
        double zScale = unityHeightmap.size.z / (unityHeightmap.heightmapResolution - 1);

        // Loop for direct heightmap points
        for (int x = 0; x < unityHeightmap.heightmapResolution; x++)
        {
            for (int z = 0; z < unityHeightmap.heightmapResolution; z++)
            {
                // Adjust world coordinates to align with the center of heightmap cells
                double worldX = (x * xScale - unityHeightmap.size.x / 2.0) + (xScale / 2.0);
                double worldZ = (z * zScale - unityHeightmap.size.z / 2.0) + (zScale / 2.0);
                double worldY = unityHeightmap.GetHeight(x, z);

                // Create the Chrono point
                ChVector3d chronoPoint = new ChVector3d(worldX, worldZ, worldY);
                pointCloud.Add(chronoPoint);
            }
        }
        ///
        /// Interpolated approach
        /// (can be computationally expensive, default is off)
        // Loop for interpolated heightmap points, if used. Rarely necessary; better to increase heightmap resolution itelf.
        if (useInterpolatedPoints)
        {
            double quarter = 0.25;
            double threeQuarters = 0.75;

            for (int x = 0; x < unityHeightmap.heightmapResolution - 1; x++)
            {
                for (int z = 0; z < unityHeightmap.heightmapResolution - 1; z++)
                {
                    for (double offsetX = quarter; offsetX <= threeQuarters; offsetX += quarter)
                    {
                        for (double offsetZ = quarter; offsetZ <= threeQuarters; offsetZ += quarter)
                        {
                            // Calculate normalized coordinates for interpolated points
                            double interpX = (x + offsetX) / (unityHeightmap.heightmapResolution - 1);
                            double interpZ = (z + offsetZ) / (unityHeightmap.heightmapResolution - 1);

                            // Calculate world coordinates for interpolated points
                            double interpWorldX = interpX * unityHeightmap.size.x - unityHeightmap.size.x / 2.0;
                            double interpWorldZ = interpZ * unityHeightmap.size.z - unityHeightmap.size.z / 2.0;
                            double interpWorldY = unityHeightmap.GetInterpolatedHeight((float)interpX, (float)interpZ);

                            // Create the interpolated Chrono point
                            ChVector3d interpChronoPoint = new ChVector3d(interpWorldX, interpWorldZ, interpWorldY);
                            pointCloud.Add(interpChronoPoint);
                        }
                    }
                }
            }
        }

        return pointCloud;
    }

}