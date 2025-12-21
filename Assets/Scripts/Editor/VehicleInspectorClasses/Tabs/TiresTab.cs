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
// Tires and Wheels tab - handles tire JSON configuration
// =============================================================================

using UnityEditor;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace VehicleBuilder.Editor
{

    public class TiresTab : InspectorTab
    {
        private bool tireJsonLoaded;
        private string lastTireJsonPath;
        private int tireDataGeneration = 0;
        
        public TiresTab(VehicleInspectorContext context) : base(context)
        {
        }
        
        public override void OnTabEnter()
        {
            // Get current tire path
            string currentTirePath = Context.Vehicle.useSingleTireFile ? Context.Vehicle.tireJSON : 
                                    (Context.Vehicle.perAxleTireSpec.Count > 0 ? Context.Vehicle.perAxleTireSpec[0] : "");
            
            // Check if path changed - force reload if it did
            bool pathChanged = currentTirePath != lastTireJsonPath;
            
            // Also check if data was invalidated (e.g., vehicle type changed)
            bool dataInvalidated = Context.JsonState.TireData == null;
            
            if (pathChanged || dataInvalidated)
            {
                Debug.Log($"[TiresTab] Reloading tire data (pathChanged={pathChanged}, dataInvalidated={dataInvalidated})");
                tireDataGeneration++;
                
                // Auto-load tire JSON when entering the tab (this ensures we're always up to date - don't rely on the unity.dirty)
                Context.EnsureTireJsonLoaded(forceReload: true);
                
                // Sync local state
                tireJsonLoaded = Context.JsonState.TireData != null;
                lastTireJsonPath = currentTirePath;
            }
        }
        
        public override void DrawTab()
        {
            DrawTireConfiguration();
            DrawDivider();
            DrawWheelConfiguration();
        }
        
        private void DrawTireConfiguration()
        {
            DrawSectionHeader("Tire Configuration");
            
            // Tire parameters
            EditorGUILayout.BeginVertical("box");
            Context.Vehicle.tireStepSize = EditorGUILayout.FloatField("Tire Step Size", Context.Vehicle.tireStepSize);
            Context.Vehicle.tireCollisionType = (ChTire.CollisionType)EditorGUILayout.EnumPopup("Collision Type", Context.Vehicle.tireCollisionType);
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.Space();
            
            // Single or multiple tire files
            bool newUseSingleTireFile = EditorGUILayout.Toggle("Use Single Tire File?", Context.Vehicle.useSingleTireFile);
            if (newUseSingleTireFile != Context.Vehicle.useSingleTireFile)
            {
                Context.Vehicle.useSingleTireFile = newUseSingleTireFile;
                Context.Vehicle.RefreshTireOverridesFromInspector(forceReset: true);
                if (!Context.Vehicle.useSingleTireFile)
                {
                    Context.TryAssignFrontRearTireDefaults(Context.Vehicle.axleData?.Count ?? 0);
                }
                EditorUtility.SetDirty(Context.Vehicle);
            }
            EditorGUILayout.Space();
            
            if (Context.Vehicle.useSingleTireFile)
            {
                DrawSingleTireSelector();
            }
            else
            {
                DrawPerAxleTireSelectors();
            }
            
            EditorGUILayout.Space();
            
            // Display tire data (no auto-load in DrawTab - happens in OnTabEnter)
            string currentTirePath = GetCurrentTirePath();
            
            if (Context.JsonState.TireData != null)
            {
                DrawSectionHeader("Tire Characteristics & Maps");
                Context.JsonParser.DrawDetectedMaps(Context.JsonState.TireData, "tire");
                
                EditorGUILayout.Space(10);
                
                DrawButtonRow(
                    ("Save", () => SaveTireJsonData(currentTirePath)),
                    ("Save As...", () => SaveTireJsonDataAs(currentTirePath))
                );
            }
            else if (string.IsNullOrEmpty(currentTirePath))
            {
                EditorGUILayout.HelpBox("Select a tire JSON file from the dropdown above.", UnityEditor.MessageType.Info);
            }
        }
        
        private void DrawSingleTireSelector()
        {
            DrawQuickJsonSelector(
                "Tire JSON:",
                Context.Vehicle.tireJSON,
                Context.TireJsonFiles,
                (newPath) => {
                    Context.Vehicle.tireJSON = newPath;
                    Context.JsonState.SetTirePath(newPath);
                    tireJsonLoaded = false;
                    lastTireJsonPath = newPath;
                    tireDataGeneration++;
                    Context.JsonParser.ClearCache("tire");
                    EditorUtility.SetDirty(Context.Vehicle); // Mark vehicle as changed for save
                });
        }
        
        private void DrawPerAxleTireSelectors()
        {
            int axleCount = Context.Vehicle.axleData?.Count ?? 0;
            
            if (axleCount == 0)
            {
                EditorGUILayout.HelpBox("No axles detected. Please load a vehicle JSON first.", UnityEditor.MessageType.Info);
                return;
            }
            
            EditorGUILayout.LabelField($"Configure tire for each of {axleCount} axles:");
            
            Context.Vehicle.RefreshTireOverridesFromInspector(forceReset: false);
            
            for (int i = 0; i < axleCount; i++)
            {
                // Safety check - ensure index exists
                if (i >= Context.Vehicle.perAxleTireSpec.Count)
                {
                    Debug.LogError($"[TiresTab] Missing tire spec for axle {i}! perAxleTireSpec.Count={Context.Vehicle.perAxleTireSpec.Count}");
                    break;
                }
                
                int axleIndex = i; // Capture for lambda
                DrawQuickJsonSelector(
                    $"Axle {i}:",
                    Context.Vehicle.perAxleTireSpec[i],
                    Context.TireJsonFiles,
                    (newPath) => {
                        Context.Vehicle.perAxleTireSpec[axleIndex] = newPath;
                        if (axleIndex == 0) // Update first axle in state
                        {
                            Context.JsonState.SetTirePath(newPath);
                        }
                        tireJsonLoaded = false;
                        tireDataGeneration++;
                        Context.JsonParser.ClearCache("tire");
                        EditorUtility.SetDirty(Context.Vehicle); // Mark vehicle as changed for save
                    });
            }
        }
        
        private void DrawWheelConfiguration()
        {
            DrawSectionHeader("Wheel Visual GameObjects");
            
            int numAxles = Context.Vehicle.axleData?.Count ?? 0;
            
            if (numAxles == 0)
            {
                EditorGUILayout.HelpBox("No axles detected. Please load a vehicle JSON first.", UnityEditor.MessageType.Info);
                return;
            }
            
            EditorGUILayout.LabelField($"Total Axles: {numAxles}", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            
            for (int i = 0; i < numAxles; i++)
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField($"Axle {i}", EditorStyles.boldLabel);
                
                if (Context.Vehicle.axleData[i].visualGameobjects != null)
                {
                    for (int w = 0; w < Context.Vehicle.axleData[i].visualGameobjects.Count; w++)
                    {
                        Context.Vehicle.axleData[i].visualGameobjects[w] = (GameObject)EditorGUILayout.ObjectField(
                            $"Wheel {w}:",
                            Context.Vehicle.axleData[i].visualGameobjects[w],
                            typeof(GameObject),
                            true
                        );
                    }
                }
                
                EditorGUILayout.EndVertical();
                EditorGUILayout.Space();
            }
        }
        
        private string GetCurrentTirePath()
        {
            return Context.Vehicle.useSingleTireFile ? Context.Vehicle.tireJSON : 
                   (Context.Vehicle.perAxleTireSpec.Count > 0 ? Context.Vehicle.perAxleTireSpec[0] : "");
        }
        
        private void LoadTireJsonData(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                try
                {
                    JObject data = Context.BuilderCore.LoadJson(path);
                    Context.JsonState.LoadTire(data);
                    tireJsonLoaded = data != null;
                    lastTireJsonPath = path;
                    
                    if (data != null)
                    {
                        Debug.Log($"Loaded tire JSON: {path}");
                    }
                }
                catch (System.Exception ex)
                {
                    Debug.LogError($"Failed to load tire JSON: {ex.Message}");
                    Context.JsonState.LoadTire(null);
                    tireJsonLoaded = false;
                }
            }
        }
        
        private void SaveTireJsonData(string path)
        {
            if (Context.JsonState.TireData != null && !string.IsNullOrEmpty(path))
            {
                if (!JsonSaveUtility.ConfirmSave("tire JSON", path))
                    return;

                Context.BuilderCore.SaveJson(Context.JsonState.TireData, path);
                EditorUtility.DisplayDialog("Saved", $"Tire JSON saved to:\n{path}", "OK");
            }
        }
        
        private void SaveTireJsonDataAs(string currentPath)
        {
            if (Context.JsonState.TireData == null)
            {
                EditorUtility.DisplayDialog("Error", "No tire JSON data to save.", "OK");
                return;
            }
            
            string directory = string.IsNullOrEmpty(currentPath) ? 
                Application.dataPath : System.IO.Path.GetDirectoryName(currentPath);
            string filename = string.IsNullOrEmpty(currentPath) ? 
                "NewTire.json" : System.IO.Path.GetFileName(currentPath);
            
            string path = EditorUtility.SaveFilePanel("Save Tire JSON As", directory, filename, "json");
            if (!string.IsNullOrEmpty(path))
            {
                if (!JsonSaveUtility.ConfirmSaveAs("tire JSON", path))
                    return;

                Context.BuilderCore.SaveJson(Context.JsonState.TireData, path);
                
                if (Context.Vehicle.useSingleTireFile)
                {
                    Context.Vehicle.tireJSON = path;
                    Context.JsonState.SetTirePath(path);
                }
                else if (Context.Vehicle.perAxleTireSpec.Count > 0)
                {
                    Context.Vehicle.perAxleTireSpec[0] = path;
                    Context.JsonState.SetTirePath(path);
                }
                
                lastTireJsonPath = path;
                EditorUtility.DisplayDialog("Saved", $"Tire JSON saved to:\n{path}", "OK");
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
