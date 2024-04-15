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

[CustomEditor(typeof(UChBodyCylinder))]
public class UChBodyCylinderEditor : UChBodyEditor
{
    public override void OnInspectorGUI()
    {
        UChBodyCylinder body = (UChBodyCylinder)target;

        base.OnInspectorGUI();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(body);
        }
    }
}
