﻿// =============================================================================
// PROJECT CHRONO - http://projectchrono.org
//
// Copyright (c) 2024 projectchrono.org
// All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found
// in the LICENSE file at the top level of the distribution.
//
// =============================================================================
// Authors: Radu Serban
// =============================================================================

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UChBodyBox))]
public class UChBodyBoxEditor : UChBodyEditor
{
    public override void OnInspectorGUI()
    {
        UChBodyBox body = (UChBodyBox)target;

        // Check if UChMaterialSurface component is attached to the object, assign one if not
        UChMaterialSurface matSurface = body.GetComponent<UChMaterialSurface>();
        if (matSurface == null)
        {
            // If not, add the UChMaterialSurface component to the object
            matSurface = body.gameObject.AddComponent<UChMaterialSurface>();
        }

        base.OnInspectorGUI();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(body);
        }
    }
}
