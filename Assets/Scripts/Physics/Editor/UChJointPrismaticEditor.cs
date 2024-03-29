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

[CustomEditor(typeof(UChJointPrismatic))]
public class UChJointPrismaticEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var joint = target as UChJointPrismatic;

        joint.body1 = (UChBody)EditorGUILayout.ObjectField("Body 1", joint.body1, typeof(UChBody), true);
        joint.body2 = (UChBody)EditorGUILayout.ObjectField("Body 2", joint.body2, typeof(UChBody), true);

        joint.enableLimits = EditorGUILayout.Toggle("Joint Limits", joint.enableLimits);

        if (joint.enableLimits)
        {
            EditorGUI.indentLevel++;
            joint.minDisplacement = EditorGUILayout.DoubleField("Min Displacement", joint.minDisplacement);
            joint.maxDisplacement = EditorGUILayout.DoubleField("Max Displacement", joint.maxDisplacement);
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.LabelField("Displ. (pos1 - pos2)", joint.displacement.ToString());

        if (GUI.changed)
        {
            EditorUtility.SetDirty(joint);
        }
    }
}
