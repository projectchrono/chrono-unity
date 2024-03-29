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

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PathFollowTrigger))]
public class PathFollowTriggerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PathFollowTrigger script = (PathFollowTrigger)target;

        // Dropdown for path following option
        script.pathFollowingOption = (PathFollowingOption)EditorGUILayout.EnumPopup("Path Following Option", script.pathFollowingOption);

        if (script.pathFollowingOption == PathFollowingOption.EnablePathFollowing)
        {
            // Show properties related to path following
            script.pathFollowerObject = (UChVehiclePath)EditorGUILayout.ObjectField("Path Follower Object", script.pathFollowerObject, typeof(UChVehiclePath), true);
            script.pathTargetSpeed = EditorGUILayout.DoubleField("Path Target Speed", script.pathTargetSpeed);
            script.lookAhead = EditorGUILayout.DoubleField("Look Ahead", script.lookAhead);
            script.steeringKp = EditorGUILayout.DoubleField("Steering Kp", script.steeringKp);
            script.steeringKd = EditorGUILayout.DoubleField("Steering Kd", script.steeringKd);
            script.steeringKi = EditorGUILayout.DoubleField("Steering Ki", script.steeringKi);
            script.speedKp = EditorGUILayout.DoubleField("Speed Kp", script.speedKp);
            script.speedKd = EditorGUILayout.DoubleField("Speed Kd", script.speedKd);
            script.speedKi = EditorGUILayout.DoubleField("Speed Ki", script.speedKi);
        }
        else if (script.pathFollowingOption == PathFollowingOption.Restore)
        {
            // No properties are shown in this mode
        }

        // Save changes and repaint if any properties are changed
        if (GUI.changed)
        {
            EditorUtility.SetDirty(script);
        }
    }
}
