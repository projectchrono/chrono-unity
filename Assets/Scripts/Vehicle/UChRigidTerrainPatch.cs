using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.Animations;
using UnityEngine.XR;
using UnityEngine.Timeline;

[DefaultExecutionOrder(0)]
public class UChRigidTerrainPatch : MonoBehaviour
{

    public enum PatchType
    {
        boxPatch,
        unityTerrainPatch
    }
    [SerializeField]
    public PatchType patchType; // Field to set the type of patch in the Unity Editor
    
    public Patch patch;
    private ChMaterialSurface mat;
    public bool tiled = false; // Tile terrain if it is large
    public int numberOfTiles = 1;

        // Variables for the Mesh Patch
    public double smoothingFactor = 0.1; // Taubin smoothing use
    private bool useInterpolatedPoints = false; // include every intermediate interpolated point into the cloud. Default off. Mostly useful for error/accuracy checking.

    public double refineGreaterThanAngle = 15.0; // sets the max angle for neighbouring grid blocks to be considered similar enough to simplify
    public double coarseMeshGridSize = 2.0; // in meters, size of the coarse mesh

    // Create a Box patch Terrain
    public void AddBoxPatchTerrain(RigidTerrain chronoRigidTerrain)
    {

        Vector3 centralTerrainPoint = this.transform.position;
        centralTerrainPoint.z *= -1; // ensure the LHF is converted to RHF position!
        //var rot = Utils.ToChrono(transform.rotation);
        var pos = Utils.ToChrono(centralTerrainPoint);
        var rot = Utils.ToChrono(transform.rotation);

        // Unity scale to Chrono size
        var size = transform.localScale;

        // Create the material for the patch
        var mat_component = this.GetComponent<UChMaterialSurface>();
        mat = mat_component.mat_info.CreateMaterial(mat_component.contact_method);

        // Add the patch to Chrono's RigidTerrain
        patch = chronoRigidTerrain.AddPatch(mat, new ChCoordsysD(pos, rot), size.x, size.z, size.y, tiled, (tiled ? numberOfTiles : 1), false /* pass false visualisation so chrono doesn't create unnecessary visual objects */);
        
        // Debugging information
        var patchPosition = patch.GetGroundBody().GetPos();
        var patchRotation = patch.GetGroundBody().GetRot();
        Debug.Log($"Patch added with Position: {patchPosition.x}, {patchPosition.y}, {patchPosition.z},  Rotation: {patchRotation.Q_to_Euler123().x * chrono.CH_C_RAD_TO_DEG}, {patchRotation.Q_to_Euler123().y * chrono.CH_C_RAD_TO_DEG}, {patchRotation.Q_to_Euler123().z * chrono.CH_C_RAD_TO_DEG}");
        Debug.Log($"Unity values send to Chrono. Position: {pos.x}, {pos.y}, {pos.z},  Rotation: {rot.Q_to_Euler123().x * chrono.CH_C_RAD_TO_DEG}, {rot.Q_to_Euler123().y * chrono.CH_C_RAD_TO_DEG}, {rot.Q_to_Euler123().z * chrono.CH_C_RAD_TO_DEG}");
    }

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
        centralTerrainPoint.z *= -1;

        var rot = Utils.ToChrono(transform.rotation);
        var pos = Utils.ToChrono(centralTerrainPoint);

        var mat_component = GetComponent<UChMaterialSurface>();
        var mat = mat_component.mat_info.CreateMaterial(mat_component.contact_method);

        // Extract height data from the Terrain component and convert to ChVector list
        var pointCloud = TerrainToPointCloud(terrain);

        // set the starting alternating triangle mesh resolution. balance speed and accuracy here
        int coarseMeshResolution = (int)(terrain.terrainData.size.x / coarseMeshGridSize); // if coarseMeshGridSize = 2, then 2m basic grid size starting mesh by divinding the width by 2.0m to get the number of grid squares

        // Add the patch to Chrono's RigidTerrain using the extracted height vectors
        var patch = chronoRigidTerrain.AddPatch(
            mat,
            new ChCoordsysD(pos, rot),
            pointCloud,
            terrain.terrainData.size.x,  // Length of the terrain patch
            terrain.terrainData.size.z,  // Width of the terrain patch
            coarseMeshResolution, // Starting mesh.
            (int)terrain.terrainData.heightmapResolution, // Grid Subdivision, convert to int from float
            3, // number of iterations
            refineGreaterThanAngle,
            smoothingFactor,
            1.0, // max LEPP edge length in refinement
            sweepSphereRadius,
            false
        );
        
        // Debugging information
        ////var patchPosition = patch.GetGroundBody().GetPos();
        ////var patchRotation = patch.GetGroundBody().GetRot();
        ////Debug.Log($"Mesh patch added with Position: {patchPosition.x}, {patchPosition.y}, {patchPosition.z}, Rotation: {patchRotation.Q_to_Euler123().x * chrono.CH_C_RAD_TO_DEG}, {patchRotation.Q_to_Euler123().y * chrono.CH_C_RAD_TO_DEG}, {patchRotation.Q_to_Euler123().z * chrono.CH_C_RAD_TO_DEG}");
        ////Debug.Log($"Mesh Path size: {terrain.unityHeightmap.size.x}, by {terrain.unityHeightmap.size.z} and a height of {terrain.unityHeightmap.size.y}");
        ////Debug.Log($"Direct details of patch terrain Ground Body xyz: {patch.GetGroundBody().GetPos().x} {patch.GetGroundBody().GetPos().y} {patch.GetGroundBody().GetPos().z}");
    }



    /// This method uses the direct height map and alternating (in between) access of the interpolated data used by unity
    private vector_ChVectorD TerrainToPointCloud(Terrain terrain)
    {
        vector_ChVectorD pointCloud = new vector_ChVectorD();
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
                ChVectorD chronoPoint = new ChVectorD(worldX, worldZ, worldY);
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
                            ChVectorD interpChronoPoint = new ChVectorD(interpWorldX, interpWorldZ, interpWorldY);
                            pointCloud.Add(interpChronoPoint);
                        }
                    }
                }
            }
        }

        return pointCloud;
    }

}