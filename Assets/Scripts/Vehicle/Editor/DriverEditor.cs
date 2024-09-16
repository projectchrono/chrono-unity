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

using System;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Driver))]
public class DriverEditor : Editor
{
    override public void OnInspectorGUI()
    {
        //// TODO: expose more parameters?

        ////EditorGUILayout.HelpBox("Test help", MessageType.Info);

        ////GUIStyle myStyle = GUI.skin.GetStyle("HelpBox");
        ////myStyle.richText = true;
        ////EditorGUILayout.TextArea("This is my text <b>AND IT IS BOLD</b>", myStyle);

        Driver driver = (Driver)target;

        // Steering control mode
        string[] steering_options = new string[] { "Wheel Control", "Path Follower" };
        driver.steeringMode = (Driver.SteeringMode)EditorGUILayout.Popup("Steering Control Mode", (int)driver.steeringMode, steering_options, EditorStyles.popup);

        if (driver.steeringMode == Driver.SteeringMode.PathFollower)
        {
            EditorGUI.indentLevel++;
            driver.path = (UChVehiclePath)EditorGUILayout.ObjectField("Path", driver.path, typeof(UChVehiclePath), true);

            driver.lookAhead = EditorGUILayout.DoubleField("Look-ahead Distance", driver.lookAhead);

            driver.steeringKp = EditorGUILayout.DoubleField(new GUIContent("Kp", "Gain for PID proportional term"), driver.steeringKp);
            driver.steeringKd = EditorGUILayout.DoubleField(new GUIContent("Kd", "Gain for PID derivative term"), driver.steeringKd);
            driver.steeringKi = EditorGUILayout.DoubleField(new GUIContent("Ki", "Gain for PID integral term"), driver.steeringKi);

            driver.gizmoRadius = EditorGUILayout.FloatField("Gizmo Radius", driver.gizmoRadius);

            EditorGUI.indentLevel--;
        }

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        // Speed control mode
        string[] speed_options = new string[] { "Pedal Control", "Cruise Control" };
        driver.speedMode = (Driver.SpeedMode)EditorGUILayout.Popup("Speed Control Mode", (int)driver.speedMode, speed_options, EditorStyles.popup);

        if (driver.speedMode == Driver.SpeedMode.CruiseControl)
        {
            EditorGUI.indentLevel++;

            double KPH = Math.Round(3.6 * driver.targetSpeed * 10) / 10;
            KPH = EditorGUILayout.DoubleField("Target Speed (km/h)", KPH);
            driver.targetSpeed = KPH / 3.6;

            driver.speedKp = EditorGUILayout.DoubleField(new GUIContent("Kp", "Gain for PID proportional term"), driver.speedKp);
            driver.speedKd = EditorGUILayout.DoubleField(new GUIContent("Kd", "Gain for PID derivative term"), driver.speedKd);
            driver.speedKi = EditorGUILayout.DoubleField(new GUIContent("Ki", "Gain for PID integral term"), driver.speedKi);

            EditorGUI.indentLevel--;
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(driver);
        }
    }
}
