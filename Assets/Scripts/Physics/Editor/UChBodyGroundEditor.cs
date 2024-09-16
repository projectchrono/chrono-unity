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
// Authors: Radu Serban
// =============================================================================

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UChBodyGround))]
public class UChBodyGroundEditor : Editor
{
    override public void OnInspectorGUI()
    {
        UChBodyGround ground = (UChBodyGround)target;
        // Check if UChMaterialSurface component is attached to the object, assign one if not
        UChMaterialSurface matSurface = ground.GetComponent<UChMaterialSurface>();
        if (matSurface == null)
        {
            // If not, add the UChMaterialSurface component to the object
            matSurface = ground.gameObject.AddComponent<UChMaterialSurface>();
        }

        ground.showFrameGizmo = EditorGUILayout.Toggle("Show Frame Gizmo", ground.showFrameGizmo);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(ground);
        }
    }
}
