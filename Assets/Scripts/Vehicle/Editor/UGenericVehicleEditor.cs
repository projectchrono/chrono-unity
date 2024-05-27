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

using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq;
using System;

[CustomEditor(typeof(UGenericVehicle))]
public class UGenericVehicleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default inspector options
        DrawDefaultInspector();

        UGenericVehicle vehicleScript = (UGenericVehicle)target;
        SerializedProperty selectedConfigurationProp = serializedObject.FindProperty("selectedConfiguration");

        // Ensure any changes are drawn and updated in the custom inspector
        serializedObject.Update();

        // Manually set the locations for the files to the data dir
        string configurationsPath = (Application.streamingAssetsPath + UChSystem.vehicleDataLocation + "generic/vehicle/");
        DirectoryInfo dirInfo = new DirectoryInfo(configurationsPath);
        FileInfo[] fileInfo = dirInfo.Exists ? dirInfo.GetFiles("*.json") : new FileInfo[0];
        string[] configurations = fileInfo.Select(file => file.Name).ToArray();

        // Handling no configurations found
        if (!configurations.Any())
        {
            EditorGUILayout.HelpBox("No configurations found in the specified path.", MessageType.Warning);
            return;
        }

        // Get current selected index based on the full path stored
        int selectedIndex = Array.IndexOf(configurations, Path.GetFileName(selectedConfigurationProp.stringValue));
        selectedIndex = Mathf.Max(0, selectedIndex); // Ensure non-negative index

        // Create and handle configuration dropdown
        int newIndex = EditorGUILayout.Popup("Configuration", selectedIndex, configurations);

        // Update if changed
        if (newIndex != selectedIndex && newIndex >= 0)
        {
            string newPath = Path.Combine(configurationsPath, configurations[newIndex]);
            selectedConfigurationProp.stringValue = newPath;
            serializedObject.ApplyModifiedProperties(); // Apply and save changes


            // Debug log for verification
            Debug.Log("Selected Configuration Full Path: " + selectedConfigurationProp.stringValue);
        }
    }
}
