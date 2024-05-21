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
        chronoRigidTerrain.Synchronize(UChSystem.chrono_system.GetChTime());
        chronoRigidTerrain.Advance(UChSystem.chrono_system.GetStep());
    }

}
