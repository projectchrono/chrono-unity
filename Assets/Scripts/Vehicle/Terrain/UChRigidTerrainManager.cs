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

using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using System;
using UnityEngine.UIElements;

// Add Terrain prior to the UChVehicle (which is at -900)
[DefaultExecutionOrder(-950)]
public class UChRigidTerrainManager : UChTerrainManager
{
    void Start()
    {
        // Verify the Chrono system exists
        if (UChSystem.chrono_system == null)
        {
            Debug.LogError($"[{gameObject.name}] UChSystem.chrono_system is null. Ensure a UChSystem component exists and initializes before the terrain manager.");
            return;
        }
        
        // Set the Terrain to the main system (which the vehicle/s are also a part of)
        RigidTerrain chronoRigidTerrain = new RigidTerrain(UChSystem.chrono_system);

        // Find all TerrainPatch components and add them as patches
        var patches = UnityEngine.Object.FindObjectsByType<UChRigidTerrainPatch>(FindObjectsSortMode.None);
        foreach (var patch in patches)
        {
            if (patch.patchType == UChRigidTerrainPatch.PatchType.boxPatch)
            {
                // Check for a UChBody component on the patch GameObject
                var bodyScript = patch.GetComponent<UChBody>();
                if (bodyScript != null)
                {
                    Debug.LogError($"UChBody script found on {gameObject.name}. Remove it from the terrain patch object to avoid conflicts.");
                }

                patch.AddBoxPatchTerrain(chronoRigidTerrain);
            }
            else if (patch.patchType == UChRigidTerrainPatch.PatchType.unityTerrainPatch)
            {
                patch.AddHeightMapPatchTerrain(chronoRigidTerrain);
            }
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
            var defaultFlatTerrain = chronoRigidTerrain.AddPatch(defaultMat, new ChCoordsysd(), 100.0, 100.0, 0.5, false, 1, false);

        }
        */

        if (patches == null || patches.Length == 0)
        {
            Debug.LogWarning("No UChRigidTerrainPatch objects found. Add a terrain patch as a child of the terrain manager.");
        }
        // Initialize the terrain
        chronoRigidTerrain.Initialize();
        UChTerrainManager.chronoTerrain = chronoRigidTerrain;


        ///int count = 0;
        ///foreach (var item in chronoRigidTerrain.GetPatches())
        ///{
        ///    Debug.Log("Patch terrain added. Type:" + item.ToString() + " designation: " + count);
        ///    count++;
        ///}
    }

    void FixedUpdate()
    {
        if (chronoTerrain == null) return;
        
        chronoTerrain.Synchronize(UChSystem.chrono_system.GetChTime());
        chronoTerrain.Advance(UChSystem.chrono_system.GetStep());
    }

    void OnDisable()
    {
        // Reset the static reference when disabled (e.g., exiting play mode or scene change)
        // so we dont get 'stuck' with a stale ref
        chronoTerrain = null;
    }

}
