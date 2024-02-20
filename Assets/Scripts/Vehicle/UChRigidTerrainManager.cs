using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using System;
using UnityEngine.UIElements;

// Add Terrain prior to the UChVehicle (which is at -900)
[DefaultExecutionOrder(-950)]
public class UChRigidTerrainManager : MonoBehaviour
{
    public static RigidTerrain chronoRigidTerrain;

    void Start()
    {
        // Set the Terrain to the main system (which the vehicle/s are also a part of)
        //ChVehicle theVehicle = FindObjectOfType<UChVehicle>().GetChVehicle();
        chronoRigidTerrain = new RigidTerrain(UChSystem.chrono_system);

        // Find all TerrainPatch components and add them as patches
        var patches = FindObjectsOfType<UChRigidTerrainPatch>();
        foreach (var patch in patches)
        {
            if (patch.patchType == UChRigidTerrainPatch.PatchType.boxPatch)
            {

                patch.AddBoxPatchTerrain(chronoRigidTerrain);
            }
            else if (patch.patchType == UChRigidTerrainPatch.PatchType.unityTerrainPatch)
            {
                patch.AddHeightMapPatchTerrain(chronoRigidTerrain);
            }

            // TODO add the flat terrain here as an option

        }
        /*
        // default if no terrain patches present, create a flat terrain
        if (patches == null)
        {
            ChContactMaterialData minfo = new ChContactMaterialData();
            minfo.mu = 0.9f;
            minfo.cr = 0.1f;
            minfo.Y = 2e7f;
            var defaultMat = minfo.CreateMaterial(ChContactMethod.NSC);
            var defaultFlatTerrain = chronoRigidTerrain.AddPatch(defaultMat, new ChCoordsysD(), 100.0, 100.0, 0.5, false, 1, false);

        }
        */

        if (patches == null) { Debug.Log("No patches added"); }
        // Initialize the terrain
        chronoRigidTerrain.Initialize();


        ///int count = 0;
        ///foreach (var item in chronoRigidTerrain.GetPatches())
        ///{
        ///    Debug.Log("Patch terrain added. Type:" + item.ToString() + " designation: " + count);
        ///    count++;
        ///}
    }

    void FixedUpdate()
    {
        // Note:: Synchronize and Advance moved to UChVehicle //
        //        chronoRigidTerrain.Synchronize(UChSystem.chrono_system.GetChTime());
        //         chronoRigidTerrain.Advance(UChSystem.chrono_system.GetStep());
    }

}
