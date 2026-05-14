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
//  Engine tab - handles engine JSON editing and map visualization
// =============================================================================

using UnityEditor;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace VehicleBuilder.Editor
{

    public class EngineTab : InspectorTab
    {
        private bool engineJsonLoaded;
        private string lastEngineJsonPath;
        
        public EngineTab(VehicleInspectorContext context) : base(context)
        {
        }
        
        public override void OnTabEnter()
        {         
            // Check if path changed and force reload if it did
            bool pathChanged = Context.Vehicle.engineJSON != lastEngineJsonPath;
            
            // Also check if data was invalidated (by a vehicle type changed)
            bool dataInvalidated = Context.JsonState.EngineData == null;
            
            // Auto-load engine JSON when entering the tab if path changed OR data was invalidated
            if (pathChanged || dataInvalidated)
            {
                Context.EnsureEngineJsonLoaded(forceReload: true);
                
                // Sync local flag
                engineJsonLoaded = Context.JsonState.EngineData != null;
                lastEngineJsonPath = Context.Vehicle.engineJSON;
            }
        }
        
        public override void DrawTab()
        {
            DrawSectionHeader("Engine Configuration");
            
            // File selector
            DrawQuickJsonSelector(
                "Engine File:",
                Context.Vehicle.engineJSON,
                Context.EngineJsonFiles,
                (newPath) => {
                    Context.Vehicle.engineJSON = newPath;
                    Context.JsonState.SetEnginePath(newPath);
                    Context.JsonState.LoadEngine(null);  // Invalidate cache
                    engineJsonLoaded = false;
                    lastEngineJsonPath = "";
                    
                    // Immediately load the new JSON
                    OnTabEnter();
                });
            
            EditorGUILayout.Space();
            DrawDivider();
            
            // Display engine data (auto-loads in OnTabEnter when path changes)
            if (Context.JsonState.EngineData != null)
            {
                var engineMaps = Context.JsonParser.DetectAllMaps(Context.JsonState.EngineData, "engine");
                
                // Draw editable parameters
                Context.JsonParser.DrawJsonObjectEditor(Context.JsonState.EngineData, engineMaps);
                
                DrawDivider();
                
                // Draw maps/plots
                Context.JsonParser.DrawDetectedMaps(Context.JsonState.EngineData, "engine", engineMaps);
                
                EditorGUILayout.Space(10);
                
                // Save buttons
                DrawButtonRow(
                    ("Save", SaveEngineJsonData),
                    ("Save As...", SaveEngineJsonDataAs)
                );
            }
            else if (string.IsNullOrEmpty(Context.Vehicle.engineJSON))
            {
                EditorGUILayout.HelpBox("No engine JSON file assigned. Select a file above.", UnityEditor.MessageType.Info);
            }
            else
            {
                EditorGUILayout.HelpBox("Engine JSON could not be loaded. Check console for errors.", UnityEditor.MessageType.Warning);
            }
        }
        
        private void SaveEngineJsonData()
        {
            if (Context.JsonState.EngineData != null && !string.IsNullOrEmpty(Context.JsonState.EnginePath))
            {
                if (!JsonSaveUtility.ConfirmSave("engine JSON", Context.JsonState.EnginePath))
                    return;

                Context.BuilderCore.SaveJson(Context.JsonState.EngineData, Context.JsonState.EnginePath);
                EditorUtility.DisplayDialog("Saved", $"Engine JSON saved to:\n{Context.JsonState.EnginePath}", "OK");
            }
        }
        // does what it says        
        private void SaveEngineJsonDataAs()
        {
            if (Context.JsonState.EngineData == null)
            {
                EditorUtility.DisplayDialog("Error", "No engine JSON data to save.", "OK");
                return;
            }
            
            string directory = string.IsNullOrEmpty(Context.JsonState.EnginePath) ? 
                Application.dataPath : System.IO.Path.GetDirectoryName(Context.JsonState.EnginePath);
            string filename = string.IsNullOrEmpty(Context.JsonState.EnginePath) ? 
                "NewEngine.json" : System.IO.Path.GetFileName(Context.JsonState.EnginePath);
            
            string path = EditorUtility.SaveFilePanel("Save Engine JSON As", directory, filename, "json");
            if (!string.IsNullOrEmpty(path))
            {
                if (!JsonSaveUtility.ConfirmSaveAs("engine JSON", path))
                    return;

                Context.BuilderCore.SaveJson(Context.JsonState.EngineData, path);
                Context.Vehicle.engineJSON = path;
                Context.JsonState.SetEnginePath(path);
                EditorUtility.DisplayDialog("Saved", $"Engine JSON saved to:\n{path}", "OK");
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
