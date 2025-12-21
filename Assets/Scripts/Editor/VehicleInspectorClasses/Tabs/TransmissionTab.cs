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
// Transmission tab - handles transmission, plots for this and shift points
// =============================================================================

using UnityEditor;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace VehicleBuilder.Editor
{

    public class TransmissionTab : InspectorTab
    {
        private bool transmissionJsonLoaded;
        private string lastTransJsonPath;
        private int transmissionDataGeneration = 0;
        
        public TransmissionTab(VehicleInspectorContext context) : base(context)
        {
        }
        
        public override void OnTabEnter()
        {           
            // Check if path changed - force reload if it did
            bool pathChanged = Context.Vehicle.transJSON != lastTransJsonPath;
            
            // Also check if data was invalidated (e.g., vehicle type changed)
            bool dataInvalidated = Context.JsonState.TransmissionData == null;
            
            if (pathChanged || dataInvalidated)
            {
                transmissionDataGeneration++;
                Context.JsonParser.ClearCache("transmission");
                
                // Auto-load transmission JSON when entering the tab
                Context.EnsureTransmissionJsonLoaded(forceReload: true);
                
                // Sync local flag
                transmissionJsonLoaded = Context.JsonState.TransmissionData != null;
                lastTransJsonPath = Context.Vehicle.transJSON;
            }
        }
        
        public override void DrawTab()
        {
            DrawSectionHeader("Transmission Configuration");
            
            // File selector
            DrawQuickJsonSelector(
                "Transmission File:",
                Context.Vehicle.transJSON,
                Context.TransmissionJsonFiles,
                (newPath) => {
                    Context.Vehicle.transJSON = newPath;
                    Context.JsonState.SetTransmissionPath(newPath);
                    Context.JsonState.LoadTransmission(null);  // Invalidate cache
                    transmissionJsonLoaded = false;
                    lastTransJsonPath = "";
                    transmissionDataGeneration++;
                    Context.JsonParser.ClearCache("transmission");
                    
                    // Immediately load the new JSON
                    OnTabEnter();
                });
            
            EditorGUILayout.Space();
            DrawDivider();
            
            // Display transmission data (no auto-load in DrawTab - happens in OnTabEnter)
            if (Context.JsonState.TransmissionData != null)
            {
                bool simpleTransmission = IsSimpleAutomaticTransmission(Context.JsonState.TransmissionData);
                var transmissionMaps = Context.JsonParser.DetectAllMaps(Context.JsonState.TransmissionData, "transmission");

                // Draw editable parameters
                Context.JsonParser.DrawJsonObjectEditor(
                    Context.JsonState.TransmissionData,
                    transmissionMaps,
                    simpleTransmission ? new[] { "Gear Box" } : null);
                
                if (simpleTransmission)
                {
                    DrawDivider();
                    DrawSectionHeader("Gear Box Configuration");
                    DrawSimpleTransmissionEditor(Context.JsonState.TransmissionData);
                }
                
                DrawDivider();
                
                // Draw maps/plots
                Context.JsonParser.DrawDetectedMaps(Context.JsonState.TransmissionData, "transmission", transmissionMaps);
                
                EditorGUILayout.Space(10);
                
                // Save buttons
                DrawButtonRow(
                    ("Save", SaveTransmissionJsonData),
                    ("Save As...", SaveTransmissionJsonDataAs)
                );
            }
            else if (string.IsNullOrEmpty(Context.Vehicle.transJSON))
            {
                EditorGUILayout.HelpBox("Select a transmission JSON file from the dropdown above.", UnityEditor.MessageType.Info);
            }
            else
            {
                EditorGUILayout.HelpBox("Failed to load transmission JSON. Check the file path.", UnityEditor.MessageType.Warning);
            }
        }
        
        private void LoadTransmissionJsonData()
        {
            if (!string.IsNullOrEmpty(Context.JsonState.TransmissionPath))
            {
                try
                {
                    JObject data = Context.BuilderCore.LoadJson(Context.JsonState.TransmissionPath);
                    Context.JsonState.LoadTransmission(data);
                    transmissionJsonLoaded = data != null;
                    
                    if (data != null)
                    {
                        Debug.Log($"Loaded transmission JSON: {Context.JsonState.TransmissionPath}");
                    }
                }
                catch (System.Exception ex)
                {
                    Debug.LogError($"Failed to load transmission JSON: {ex.Message}");
                    Context.JsonState.LoadTransmission(null);
                    transmissionJsonLoaded = false;
                }
            }
        }
        
        private void SaveTransmissionJsonData()
        {
            if (Context.JsonState.TransmissionData != null && !string.IsNullOrEmpty(Context.JsonState.TransmissionPath))
            {
                if (!JsonSaveUtility.ConfirmSave("transmission JSON", Context.JsonState.TransmissionPath))
                    return;

                Context.BuilderCore.SaveJson(Context.JsonState.TransmissionData, Context.JsonState.TransmissionPath);
                EditorUtility.DisplayDialog("Saved", $"Transmission JSON saved to:\n{Context.JsonState.TransmissionPath}", "OK");
            }
        }
        
        private void SaveTransmissionJsonDataAs()
        {
            if (Context.JsonState.TransmissionData == null)
            {
                EditorUtility.DisplayDialog("Error", "No transmission JSON data to save.", "OK");
                return;
            }
            
            string directory = string.IsNullOrEmpty(Context.JsonState.TransmissionPath) ? 
                Application.dataPath : System.IO.Path.GetDirectoryName(Context.JsonState.TransmissionPath);
            string filename = string.IsNullOrEmpty(Context.JsonState.TransmissionPath) ? 
                "NewTransmission.json" : System.IO.Path.GetFileName(Context.JsonState.TransmissionPath);
            
            string path = EditorUtility.SaveFilePanel("Save Transmission JSON As", directory, filename, "json");
            if (!string.IsNullOrEmpty(path))
            {
                if (!JsonSaveUtility.ConfirmSaveAs("transmission JSON", path))
                    return;

                Context.BuilderCore.SaveJson(Context.JsonState.TransmissionData, path);
                Context.Vehicle.transJSON = path;
                Context.JsonState.SetTransmissionPath(path);
                EditorUtility.DisplayDialog("Saved", $"Transmission JSON saved to:\n{path}", "OK");
            }
        }
        
        private bool IsSimpleAutomaticTransmission(JObject transmissionData)
        {
            if (transmissionData == null) return false;
            
            string type = transmissionData["Type"]?.ToString();
            if (string.IsNullOrEmpty(type)) return false;
            
            return type.Contains("SimpleAutomatic") || type.Contains("AutomaticSimple");
        }
        
        private void DrawSimpleTransmissionEditor(JObject transmissionData)
        {
            if (transmissionData?["Gear Box"] is JObject gearBox)
            {
                // Forward gears
                if (gearBox["Forward Gear Ratios"] is JArray forwardGears)
                {
                    EditorGUILayout.LabelField("Forward Gear Ratios:", EditorStyles.miniBoldLabel);
                    for (int i = 0; i < forwardGears.Count; i++)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField($"  Gear {i + 1}:", GUILayout.Width(60));
                        float value = forwardGears[i].ToObject<float>();
                        float newValue = EditorGUILayout.FloatField(value);
                        if (newValue != value)
                        {
                            forwardGears[i] = newValue;
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                    EditorGUILayout.Space(5);
                }
                
                // Reverse gear
                if (gearBox["Reverse Gear Ratio"] != null)
                {
                    float reverseRatio = gearBox["Reverse Gear Ratio"].ToObject<float>();
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Reverse Gear:", EditorStyles.miniBoldLabel, GUILayout.Width(120));
                    float newReverse = EditorGUILayout.FloatField(reverseRatio);
                    if (newReverse != reverseRatio)
                    {
                        gearBox["Reverse Gear Ratio"] = newReverse;
                    }
                    EditorGUILayout.EndHorizontal();
                }
                
                // Shift points
                if (gearBox["Shift Points (RPM)"] is JArray shiftPoints)
                {
                    EditorGUILayout.Space(5);
                    EditorGUILayout.LabelField("Shift Points (RPM):", EditorStyles.miniBoldLabel);
                    for (int i = 0; i < shiftPoints.Count; i++)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField($"  {i + 1} → {i + 2}:", GUILayout.Width(60));
                        float value = shiftPoints[i].ToObject<float>();
                        float newValue = EditorGUILayout.FloatField(value);
                        if (newValue != value)
                        {
                            shiftPoints[i] = newValue;
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                }
            }
        }
        
        private void DrawQuickJsonSelector(string label, string currentPath, List<string> files, System.Action<string> onChanged)
        {
            if (files.Count == 0)
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
    }
}
