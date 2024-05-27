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
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UChRigidTerrainManager))]
public class UChRigidTerrainManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default inspector
        DrawDefaultInspector();

        UChRigidTerrainManager manager = (UChRigidTerrainManager)target;
    }

    // Navigate all descendants of a GameObject to apply the material
    void ApplyMaterialToChildren(GameObject obj, Material mat)
    {
        foreach (Transform child in obj.transform)
        {
            MeshRenderer renderer = child.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.material = mat; // Apply the material
            }
            // Apply the material recursively to the grandchildren
            ApplyMaterialToChildren(child.gameObject, mat);
        }
    }
}
