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
// Authors: Radu Serban
// =============================================================================

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UChMaterialSurface))]
public class UChMaterialSurfaceEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var mat = target as UChMaterialSurface;
        string[] options = new string[] { "NSC", "SCM" };

        mat.contact_method = (ChContactMethod)EditorGUILayout.Popup("Contact Method", (int)mat.contact_method, options, EditorStyles.popup);

        mat.mat_info.mu = EditorGUILayout.FloatField("Friction", mat.mat_info.mu);  
        mat.mat_info.mu_r = EditorGUILayout.FloatField("Rolling Friction", mat.mat_info.mu_r);
        mat.mat_info.mu_s = EditorGUILayout.FloatField("Spinning Friction", mat.mat_info.mu_s);
        mat.mat_info.cr = EditorGUILayout.FloatField("Restitution", mat.mat_info.cr);
        mat.mat_info.coh = EditorGUILayout.FloatField("Cohesion", mat.mat_info.coh);

        using (new EditorGUI.DisabledScope(mat.contact_method == 0))
        {
            mat.mat_info.Y = EditorGUILayout.FloatField("Young Modulus", mat.mat_info.Y);
            mat.mat_info.nu = EditorGUILayout.FloatField("Poisson Ratio", mat.mat_info.nu);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(mat);
        }
    }
}
