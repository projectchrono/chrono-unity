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

[CustomEditor(typeof(UChVehiclePath))]
public class UChVehiclePathEditor : Editor
{
    override public void OnInspectorGUI()
    {
        UChVehiclePath path = (UChVehiclePath)target;

        string[] options = new string[] { "Straight Line", "Circle", "Double Lane Change", "Custom", "File" };
        path.type = (UChVehiclePath.Type)EditorGUILayout.Popup("Type", (int)path.type, options, EditorStyles.popup);

        EditorGUI.indentLevel++;
        switch (path.type)
        {
            case UChVehiclePath.Type.StraightLine:
                path.lineStart = EditorGUILayout.Vector3Field("Start point", path.lineStart);
                path.lineEnd = EditorGUILayout.Vector3Field("End point", path.lineEnd);
                path.lineNumPoints = (uint)EditorGUILayout.IntField("Num. Intermediate", (int)path.lineNumPoints);
                break;
            case UChVehiclePath.Type.Circle:
                path.circleStart = EditorGUILayout.Vector3Field("Start point", path.circleStart);
                path.circleRadius = EditorGUILayout.DoubleField("Radius", path.circleRadius);
                path.circleRun = EditorGUILayout.DoubleField("Run", path.circleRun);
                path.circleNumTurns = EditorGUILayout.IntField("Num. Turns", path.circleNumTurns);
                path.circleLeftTurn = EditorGUILayout.Toggle("Left Turn", path.circleLeftTurn);
                break;
            case UChVehiclePath.Type.DoubleLaneChange:
                path.dlcStart = EditorGUILayout.Vector3Field("Start point", path.dlcStart);
                path.dlcRamp = EditorGUILayout.DoubleField("Ramp", path.dlcRamp);
                path.dlcWidth = EditorGUILayout.DoubleField("Width", path.dlcWidth);
                path.dlcLength = EditorGUILayout.DoubleField("Length", path.dlcLength);
                path.dlcRun = EditorGUILayout.DoubleField("Run", path.dlcRun);
                path.dlcLeftTurn = EditorGUILayout.Toggle("Left Turn", path.dlcLeftTurn);
                break;
            case UChVehiclePath.Type.Custom:
                int size = EditorGUILayout.DelayedIntField("Number of Points", path.points.Count);
                while (size < path.points.Count)
                    path.points.RemoveAt(path.points.Count - 1);
                while (size > path.points.Count)
                    path.points.Add(new Vector3());

                for (int i = 0; i < size; i++)
                {
                    path.points[i] = EditorGUILayout.Vector3Field("Point " + (i + 1), path.points[i]);
                }
                break;
            case UChVehiclePath.Type.File:
                path.bezierCurveFile = EditorGUILayout.TextField("File Name", path.bezierCurveFile);
                break;
        }

        EditorGUI.indentLevel--;

        path.numRenderPoints = EditorGUILayout.IntField("Num. Render Points", path.numRenderPoints);

        path.hidePath = EditorGUILayout.Toggle("Hide Path", path.hidePath);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(path);
            path.UpdateLine();
        }
    }
}
