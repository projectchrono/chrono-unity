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

//// TODO:  Double-check check proper implementation of joint limits.
////        Must take into account body order, orientation, etc.

[CustomEditor(typeof(UChJointRevolute))]
public class UChJointRevoluteEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var joint = target as UChJointRevolute;

        joint.body1 = (UChBody)EditorGUILayout.ObjectField("Body 1", joint.body1, typeof(UChBody), true);
        joint.body2 = (UChBody)EditorGUILayout.ObjectField("Body 2", joint.body2, typeof(UChBody), true);

        joint.enableLimits = EditorGUILayout.Toggle("Joint Limits", joint.enableLimits);

        if (joint.enableLimits)
        {
            EditorGUI.indentLevel++;
            joint.minAngle = EditorGUILayout.DoubleField("Min Angle", joint.minAngle);
            joint.maxAngle = EditorGUILayout.DoubleField("Max Angle", joint.maxAngle);
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.LabelField("Angle", joint.angle.ToString());

        if (GUI.changed)
        {
            EditorUtility.SetDirty(joint);
        }
    }
}
