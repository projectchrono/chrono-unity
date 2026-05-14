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
// details vehicle json breakdown (driveline, steering, axles) with enums and
// a save/save as to concrete changes (otherwise changes won't be applied in
// play mode, since chrono loads teh jsons.. not effecitly whats in the editor)
// =============================================================================

using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace VehicleBuilder.Editor
{

    public class VehicleTab : InspectorTab
    {
        public VehicleTab(VehicleInspectorContext context) : base(context)
        {
        }
        
        public override void DrawTab()
        {
            Context.EnsureVehicleJsonLoaded();
            DrawSectionHeader("Vehicle Configuration");
            
            DrawDrivelineAndSteeringSelectors();
            
            DrawDivider();
            
            // Axle configuration
            DrawAxleConfiguration();
            
            DrawDivider();
            
            // Save buttons
            DrawSaveButtons();
        }
        
        private void DrawDrivelineAndSteeringSelectors()
        {
            if (Context.JsonState.VehicleData == null)
            {
                EditorGUILayout.HelpBox("No vehicle JSON loaded.", UnityEditor.MessageType.Info);
                return;
            }

            var vehicleData = Context.JsonState.VehicleData;

            DrawSectionHeader("Driveline & Steering");
            
            string drivelinePath = vehicleData["Driveline"]?["Input File"]?.ToString() ?? string.Empty;
            DrawComponentSelector("Driveline:", drivelinePath, "Driveline", (newPath) => UpdateVehicleComponent("Driveline", newPath));

            string steeringPath = vehicleData["Steering"]?["Input File"]?.ToString() ?? string.Empty;
            DrawComponentSelector("Steering:", steeringPath, "Steering", (newPath) => UpdateVehicleComponent("Steering", newPath));
        }
        
        private void DrawAxleConfiguration()
        {
            DrawSectionHeader("Axle Configuration");

            if (Context.JsonState.VehicleData == null)
            {
                EditorGUILayout.HelpBox("No vehicle JSON loaded.", UnityEditor.MessageType.Info);
                return;
            }

            var vehicleData = Context.JsonState.VehicleData;
            var axles = vehicleData["Axles"] as JArray;

            if (axles == null || axles.Count == 0)
            {
                EditorGUILayout.HelpBox("No axles defined in vehicle JSON.", UnityEditor.MessageType.Info);
                return;
            }

            var parsedVehicleData = Context.JsonState.ParsedVehicleData;
            var parsedAxles = parsedVehicleData?.Axles;

            for (int i = 0; i < axles.Count; i++)
            {
                var axleObj = axles[i] as JObject;
                if (axleObj == null) continue;

                int axleIndex = i;

                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField($"Axle {i}", EditorStyles.boldLabel);

                // Editable Suspension Location
                if (parsedAxles != null && axleIndex < parsedAxles.Count)
                {
                    var currentLoc = parsedAxles[axleIndex].SuspensionLocation;
                    EditorGUI.BeginChangeCheck();
                    Vector3 newLoc = EditorGUILayout.Vector3Field("Suspension Location", currentLoc);
                    if (EditorGUI.EndChangeCheck())
                    {
                        UpdateAxleSuspensionLocation(axleIndex, newLoc);
                    }
                }

                DrawComponentSelector("Suspension:", axleObj?["Suspension Input File"]?.ToString() ?? string.Empty, "Suspension",
                    (newPath) => UpdateAxleField(axleIndex, "Suspension Input File", newPath));

                DrawComponentSelector("Left Wheel:", axleObj?["Left Wheel Input File"]?.ToString() ?? string.Empty, "Wheel",
                    (newPath) => UpdateAxleField(axleIndex, "Left Wheel Input File", newPath));

                DrawComponentSelector("Right Wheel:", axleObj?["Right Wheel Input File"]?.ToString() ?? string.Empty, "Wheel",
                    (newPath) => UpdateAxleField(axleIndex, "Right Wheel Input File", newPath));

                DrawComponentSelector("Left Brake:", axleObj?["Left Brake Input File"]?.ToString() ?? string.Empty, "Brake",
                    (newPath) => UpdateAxleField(axleIndex, "Left Brake Input File", newPath));

                DrawComponentSelector("Right Brake:", axleObj?["Right Brake Input File"]?.ToString() ?? string.Empty, "Brake",
                    (newPath) => UpdateAxleField(axleIndex, "Right Brake Input File", newPath));

                EditorGUILayout.EndVertical();
            }
        }
        
        private void DrawComponentSelector(string label, string currentPath, string componentType, System.Action<string> onChanged)
        {
            var files = Context.GetFilesForComponentType(componentType);
            if (files == null || files.Count == 0)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(label, GUILayout.Width(120));
                EditorGUILayout.LabelField("[No files found]");
                EditorGUILayout.EndHorizontal();
                return;
            }

            Context.BuilderCore.BuildDisplayListFromFilenames(files, out List<string> displayNames, out List<string> actualPaths);

            int selectedIndex = actualPaths.IndexOf(currentPath);
            if (selectedIndex < 0 && actualPaths.Count > 0)
                selectedIndex = 0;

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, GUILayout.Width(120));
            int newIndex = EditorGUILayout.Popup(selectedIndex, displayNames.ToArray());
            EditorGUILayout.EndHorizontal();

            if (newIndex >= 0 && newIndex < actualPaths.Count && newIndex != selectedIndex)
            {
                onChanged?.Invoke(actualPaths[newIndex]);
            }
        }
        
        private void UpdateVehicleComponent(string componentName, string newPath)
        {
            if (Context.JsonState.VehicleData == null)
                return;

            var vehicleData = Context.JsonState.VehicleData;
            
            if (vehicleData[componentName] == null)
            {
                vehicleData[componentName] = new JObject();
            }

            vehicleData[componentName]["Input File"] = newPath;
            // Mark JSON as modified without triggering Unity undo
            GUI.changed = true;
        }
        
        private void UpdateAxleField(int axleIndex, string fieldName, string newValue)
        {
            if (Context.JsonState.VehicleData == null)
                return;

            var vehicleData = Context.JsonState.VehicleData;
            var axles = vehicleData["Axles"] as JArray;

            if (axles != null && axleIndex < axles.Count)
            {
                axles[axleIndex][fieldName] = newValue;
                // Mark JSON as modified without triggering Unity undo
                GUI.changed = true;
            }
        }
        
        private void UpdateAxleSuspensionLocation(int axleIndex, Vector3 newLocation)
        {
            if (Context.JsonState.VehicleData == null)
                return;

            var vehicleData = Context.JsonState.VehicleData;
            var axles = vehicleData["Axles"] as JArray;

            if (axles != null && axleIndex < axles.Count)
            {
                var axleObj = axles[axleIndex] as JObject;
                if (axleObj != null)
                {
                    // Update the Location array in the JSON
                    axleObj["Location"] = new JArray(newLocation.x, newLocation.y, newLocation.z);
                    
                    // Refresh the parsed data so the UI updates
                    Context.RefreshParsedVehicleData();
                    
                    // Mark JSON as modified without triggering Unity undo
                    GUI.changed = true;
                }
            }
        }
        
        private void DrawSaveButtons()
        {
            DrawSectionHeader("Save Vehicle JSON");
            
            EditorGUILayout.BeginVertical("box");
            
            EditorGUILayout.BeginHorizontal();
            
            // Save button
            if (GUILayout.Button("Save", GUILayout.Height(30), GUILayout.Width(80)))
            {
                EditorApplication.delayCall += () => SaveVehicleJson(false);
            }
            
            // Save As button
            if (GUILayout.Button("Save As...", GUILayout.Height(30), GUILayout.Width(100)))
            {
                EditorApplication.delayCall += () => SaveVehicleJson(true);
            }
            
            GUILayout.FlexibleSpace();
            
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.EndVertical();
        }
        
        private void SaveVehicleJson(bool saveAs)
        {
            if (Context.JsonState.VehicleData == null)
            {
                EditorUtility.DisplayDialog("Error", "No vehicle JSON data loaded.", "OK");
                return;
            }
            
            string targetPath = Context.JsonState.VehiclePath;
            
            if (saveAs || string.IsNullOrEmpty(targetPath))
            {
                // Show save file dialog
                string directory = string.IsNullOrEmpty(targetPath) 
                    ? Context.BuilderCore.ChronoVehicleDataRoot 
                    : System.IO.Path.GetDirectoryName(targetPath);
                    
                string filename = string.IsNullOrEmpty(targetPath) 
                    ? "vehicle.json" 
                    : System.IO.Path.GetFileName(targetPath);
                
                targetPath = EditorUtility.SaveFilePanel(
                    "Save Vehicle JSON As",
                    directory,
                    filename,
                    "json"
                );
                
                if (string.IsNullOrEmpty(targetPath))
                {
                    return; // User cancelled
                }
                
                // Convert to relative path if within Chrono data directory
                string relativePath = Context.BuilderCore.ToRelativePath(targetPath);
                if (!string.IsNullOrEmpty(relativePath))
                {
                    targetPath = relativePath;
                }
            }
            
            // Confirmation dialog
            if (!JsonSaveUtility.ConfirmSave("vehicle JSON", targetPath))
            {
                return;
            }
            
            // Save the file
            try
            {
                Context.BuilderCore.SaveJson(Context.JsonState.VehicleData, targetPath);
                
                // Update the path if it changed (Save As scenario)
                if (targetPath != Context.JsonState.VehiclePath)
                {
                    Context.Vehicle.topLevelVehicleJSON = targetPath;
                    Context.JsonState.SetVehiclePath(targetPath);
                    EditorUtility.SetDirty(Context.Vehicle);
                }
                
                EditorUtility.DisplayDialog("Success", $"Vehicle JSON saved to:\n{targetPath}", "OK");
            }
            catch (System.Exception ex)
            {
                EditorUtility.DisplayDialog("Error", $"Failed to save vehicle JSON:\n{ex.Message}", "OK");
                Debug.LogError($"[VehicleTab] Failed to save vehicle JSON: {ex}");
            }
        }
    }
}
