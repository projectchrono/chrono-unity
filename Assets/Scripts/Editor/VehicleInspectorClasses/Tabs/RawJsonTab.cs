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
// Raw JSON tab - provides text editor for direct JSON manipulation
// TODO - this is a bit monolithic, and in future could do with partitioning up
// =============================================================================

using UnityEditor;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace VehicleBuilder.Editor
{

    public class RawJsonTab : InspectorTab
    {
        private class TargetFileOption
        {
            public string Label;
            public string Path;
            public int Index;
        }

        private string currentTarget = "vehicle";
        private string lastTarget = "";
        private string lastTargetPath = "";
        private bool showEditor = false;
        private int selectedTireOption = 0;
        private List<TargetFileOption> cachedTireOptions = new List<TargetFileOption>();
        
        // Per-axle component selections
        private int selectedSuspensionOption = 0;
        private List<TargetFileOption> cachedSuspensionOptions = new List<TargetFileOption>();
        private int selectedBrakeOption = 0;
        private List<TargetFileOption> cachedBrakeOptions = new List<TargetFileOption>();
        private int selectedWheelOption = 0;
        private List<TargetFileOption> cachedWheelOptions = new List<TargetFileOption>();
        
        // Cached component data
        private JObject cachedDrivelineData = null;
        private string cachedDrivelinePath = "";
        private JObject cachedChassisData = null;
        private string cachedChassisPath = "";
        private JObject cachedSuspensionData = null;
        private string cachedSuspensionPath = "";
        private JObject cachedBrakeData = null;
        private string cachedBrakePath = "";
        private JObject cachedWheelData = null;
        private string cachedWheelPath = "";
        
        public RawJsonTab(VehicleInspectorContext context) : base(context)
        {
        }
        
        public override void OnTabEnter()
        {
            // Reset editor state
            showEditor = false;
            lastTarget = "";
            lastTargetPath = "";
            selectedTireOption = 0;
            cachedTireOptions.Clear();
            selectedSuspensionOption = 0;
            cachedSuspensionOptions.Clear();
            selectedBrakeOption = 0;
            cachedBrakeOptions.Clear();
            selectedWheelOption = 0;
            cachedWheelOptions.Clear();
            
            // Clear cached component data
            cachedDrivelineData = null;
            cachedDrivelinePath = "";
            cachedChassisData = null;
            cachedChassisPath = "";
            cachedSuspensionData = null;
            cachedSuspensionPath = "";
            cachedBrakeData = null;
            cachedBrakePath = "";
            cachedWheelData = null;
            cachedWheelPath = "";

            if (Context.Vehicle != null)
            {
                Context.Vehicle.RefreshTireOverridesFromInspector(forceReset: false);
            }
        }
        
        public override void DrawTab()
        {
            DrawSectionHeader("Raw JSON Editor");
            
            // Target selector - responsive layout
            EditorGUILayout.LabelField("Edit:");
            
            string[] targets = new string[] { "Vehicle", "Chassis", "Engine", "Transmission", "Tire", "Driveline", "Suspension", "Brake", "Wheel" };
            string[] targetsLower = new string[] { "vehicle", "chassis", "engine", "transmission", "tire", "driveline", "suspension", "brake", "wheel" };
            int targetIndex = System.Array.IndexOf(targetsLower, currentTarget);
            if (targetIndex < 0) targetIndex = 0;
            
            // Calculate columns based on inspector width
            float inspectorWidth = GetInspectorWidth();
            float buttonWidth = 125f;
            float margin = 20f; // Safety margin
            int maxColumns = Mathf.Max(1, Mathf.FloorToInt((inspectorWidth - margin) / buttonWidth));
            int columns = Mathf.Min(maxColumns, targets.Length);
            
            // Draw selection grid with MaxWidth constraint to prevent overflow
            EditorGUILayout.BeginHorizontal();
            int newTargetIndex = GUILayout.SelectionGrid(
                targetIndex, 
                targets, 
                columns, 
                GUILayout.MaxWidth(inspectorWidth - margin)
            );
            EditorGUILayout.EndHorizontal();
            
            if (newTargetIndex != targetIndex)
            {
                currentTarget = targetsLower[newTargetIndex];
                Context.MapEditor?.ClearAllMapStates();
            }
            
            EditorGUILayout.Space();
            
            // Load and display appropriate JSON
            JObject targetJson = null;
            string targetPath = "";
            string displayLabel = string.Empty;
            
            switch (currentTarget)
            {
                case "vehicle":
                    targetPath = Context.JsonState.VehiclePath;
                    if (!Context.JsonState.VehicleLoaded && !string.IsNullOrEmpty(targetPath))
                    {
                        LoadVehicleAsync();
                        return;
                    }
                    targetJson = Context.JsonState.VehicleData;
                    break;
                    
                case "engine":
                    targetPath = Context.JsonState.EnginePath;
                    if (!Context.JsonState.EngineLoaded && !string.IsNullOrEmpty(targetPath))
                    {
                        LoadEngineAsync();
                        return;
                    }
                    targetJson = Context.JsonState.EngineData;
                    break;
                    
                case "transmission":
                    targetPath = Context.JsonState.TransmissionPath;
                    if (!Context.JsonState.TransmissionLoaded && !string.IsNullOrEmpty(targetPath))
                    {
                        LoadTransmissionAsync();
                        return;
                    }
                    targetJson = Context.JsonState.TransmissionData;
                    break;
                    
                case "tire":
                    EnsureTireOptionSelection();
                    targetPath = Context.JsonState.TirePath;

                    // Load if we have a path but no data
                    if (!string.IsNullOrEmpty(targetPath) && Context.JsonState.TireData == null)
                    {
                        LoadTireAsync();
                        return;
                    }

                    targetJson = Context.JsonState.TireData;
                    displayLabel = GetCurrentTireSelectionLabel();
                    break;
                    
                case "chassis":
                    targetPath = GetChassisPath();
                    if (!string.IsNullOrEmpty(targetPath))
                    {
                        if (cachedChassisData == null || cachedChassisPath != targetPath)
                        {
                            LoadComponentAsync(targetPath, (data) => {
                                cachedChassisData = data;
                                cachedChassisPath = targetPath;
                            });
                            return;
                        }
                        targetJson = cachedChassisData;
                    }
                    break;
                    
                case "driveline":
                    targetPath = GetDrivelinePath();
                    if (!string.IsNullOrEmpty(targetPath))
                    {
                        if (cachedDrivelineData == null || cachedDrivelinePath != targetPath)
                        {
                            LoadComponentAsync(targetPath, (data) => {
                                cachedDrivelineData = data;
                                cachedDrivelinePath = targetPath;
                            });
                            return;
                        }
                        targetJson = cachedDrivelineData;
                    }
                    break;
                    
                case "suspension":
                    EnsureSuspensionOptionSelection();
                    targetPath = GetSuspensionPath();
                    if (!string.IsNullOrEmpty(targetPath))
                    {
                        if (cachedSuspensionData == null || cachedSuspensionPath != targetPath)
                        {
                            LoadComponentAsync(targetPath, (data) => {
                                cachedSuspensionData = data;
                                cachedSuspensionPath = targetPath;
                            });
                            return;
                        }
                        targetJson = cachedSuspensionData;
                        displayLabel = GetCurrentSuspensionSelectionLabel();
                    }
                    break;
                    
                case "brake":
                    EnsureBrakeOptionSelection();
                    targetPath = GetBrakePath();
                    if (!string.IsNullOrEmpty(targetPath))
                    {
                        if (cachedBrakeData == null || cachedBrakePath != targetPath)
                        {
                            LoadComponentAsync(targetPath, (data) => {
                                cachedBrakeData = data;
                                cachedBrakePath = targetPath;
                            });
                            return;
                        }
                        targetJson = cachedBrakeData;
                        displayLabel = GetCurrentBrakeSelectionLabel();
                    }
                    break;
                    
                case "wheel":
                    EnsureWheelOptionSelection();
                    targetPath = GetWheelPath();
                    if (!string.IsNullOrEmpty(targetPath))
                    {
                        if (cachedWheelData == null || cachedWheelPath != targetPath)
                        {
                            LoadComponentAsync(targetPath, (data) => {
                                cachedWheelData = data;
                                cachedWheelPath = targetPath;
                            });
                            return;
                        }
                        targetJson = cachedWheelData;
                        displayLabel = GetCurrentWheelSelectionLabel();
                    }
                    break;
            }
            
            if (targetJson != null)
            {
                string labelSuffix = string.IsNullOrEmpty(displayLabel) ? targetPath : $"{displayLabel} — {targetPath}";
                EditorGUILayout.LabelField($"Editing: {labelSuffix}", EditorStyles.miniLabel);
                
                // Draw selection dropdowns for per-axle/per-wheel components
                if (currentTarget == "tire")
                {
                    DrawTireSelectionDropdown();
                }
                else if (currentTarget == "suspension")
                {
                    DrawSuspensionSelectionDropdown();
                }
                else if (currentTarget == "brake")
                {
                    DrawBrakeSelectionDropdown();
                }
                else if (currentTarget == "wheel")
                {
                    DrawWheelSelectionDropdown();
                }
                
                EditorGUILayout.Space();
                
                // Initialize or reinitialize text editor when target changes OR when path changes
                if (!showEditor || lastTarget != currentTarget || lastTargetPath != targetPath)
                {
                    Context.TextEditor.SetJson(targetJson);
                    showEditor = true;
                    lastTarget = currentTarget;
                    lastTargetPath = targetPath;
                }
                
                // Draw text editor
                Context.TextEditor.Draw(500f);
                
                EditorGUILayout.Space();
                
                // Save buttons - left justified
                EditorGUILayout.BeginHorizontal();
                
                if (GUILayout.Button("Save", GUILayout.Height(30), GUILayout.Width(80)))
                {
                    EditorApplication.delayCall += () => SaveCurrentJson(false);
                }
                
                if (GUILayout.Button("Save As...", GUILayout.Height(30), GUILayout.Width(100)))
                {
                    EditorApplication.delayCall += () => SaveCurrentJson(true);
                }
                
                GUILayout.FlexibleSpace();
                
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                EditorGUILayout.HelpBox($"No {currentTarget} JSON loaded. Select a file in the Overview tab.", UnityEditor.MessageType.Info);
            }
        }
        
        private void LoadVehicleAsync()
        {
            EditorGUILayout.HelpBox("Loading vehicle JSON...", UnityEditor.MessageType.Info);
            EditorApplication.delayCall += () => {
                if (Context.Vehicle != null)
                {
                    try
                    {
                        JObject data = Context.BuilderCore.LoadJson(Context.JsonState.VehiclePath);
                        Context.JsonState.LoadVehicle(data, null);
                    }
                    catch (System.Exception ex)
                    {
                        Debug.LogError($"Failed to load vehicle JSON: {ex.Message}");
                    }
                    finally
                    {
                        EditorUtility.SetDirty(Context.Vehicle);
                    }
                }
            };
        }
        
        private void LoadEngineAsync()
        {
            EditorGUILayout.HelpBox("Loading engine JSON...", UnityEditor.MessageType.Info);
            EditorApplication.delayCall += () => {
                if (Context.Vehicle != null)
                {
                    try
                    {
                        JObject data = Context.BuilderCore.LoadJson(Context.JsonState.EnginePath);
                        Context.JsonState.LoadEngine(data);
                    }
                    catch (System.Exception ex)
                    {
                        Debug.LogError($"Failed to load engine JSON: {ex.Message}");
                    }
                    finally
                    {
                        EditorUtility.SetDirty(Context.Vehicle);
                    }
                }
            };
        }
        
        private void LoadTransmissionAsync()
        {
            EditorGUILayout.HelpBox("Loading transmission JSON...", UnityEditor.MessageType.Info);
            EditorApplication.delayCall += () => {
                if (Context.Vehicle != null)
                {
                    try
                    {
                        JObject data = Context.BuilderCore.LoadJson(Context.JsonState.TransmissionPath);
                        Context.JsonState.LoadTransmission(data);
                    }
                    catch (System.Exception ex)
                    {
                        Debug.LogError($"Failed to load transmission JSON: {ex.Message}");
                    }
                    finally
                    {
                        EditorUtility.SetDirty(Context.Vehicle);
                    }
                }
            };
        }
        
        private void LoadTireAsync()
        {
            EditorGUILayout.HelpBox("Loading tire JSON...", UnityEditor.MessageType.Info);
            EditorApplication.delayCall += () => {
                if (Context.Vehicle != null)
                {
                    try
                    {
                        JObject data = Context.BuilderCore.LoadJson(Context.JsonState.TirePath);
                        Context.JsonState.LoadTire(data);
                    }
                    catch (System.Exception ex)
                    {
                        Debug.LogError($"Failed to load tire JSON: {ex.Message}");
                    }
                    finally
                    {
                        EditorUtility.SetDirty(Context.Vehicle);
                    }
                }
            };
        }
        
        private void SaveCurrentJson(bool saveAs)
        {
            JObject updated = Context.TextEditor.GetJson();
            if (updated == null)
            {
                EditorUtility.DisplayDialog("Error", "JSON has errors. Please fix before saving.", "OK");
                return;
            }
            
            switch (currentTarget)
            {
                case "vehicle":
                    SaveJsonFile(saveAs, "vehicle JSON", 
                        Context.JsonState.VehiclePath, 
                        updated,
                        (path) => {
                            Context.JsonState.LoadVehicle(updated, null);
                            if (path != Context.JsonState.VehiclePath)
                            {
                                Context.Vehicle.topLevelVehicleJSON = path;
                                Context.JsonState.SetVehiclePath(path);
                                EditorUtility.SetDirty(Context.Vehicle);
                            }
                        });
                    break;
                    
                case "engine":
                    SaveJsonFile(saveAs, "engine JSON", 
                        Context.JsonState.EnginePath, 
                        updated,
                        (path) => {
                            Context.JsonState.LoadEngine(updated);
                            if (path != Context.JsonState.EnginePath)
                            {
                                Context.Vehicle.engineJSON = path;
                                Context.JsonState.SetEnginePath(path);
                                EditorUtility.SetDirty(Context.Vehicle);
                            }
                        });
                    break;
                    
                case "transmission":
                    SaveJsonFile(saveAs, "transmission JSON", 
                        Context.JsonState.TransmissionPath, 
                        updated,
                        (path) => {
                            Context.JsonState.LoadTransmission(updated);
                            if (path != Context.JsonState.TransmissionPath)
                            {
                                Context.Vehicle.transJSON = path;
                                Context.JsonState.SetTransmissionPath(path);
                                EditorUtility.SetDirty(Context.Vehicle);
                            }
                        });
                    break;
                    
                case "tire":
                    if (string.IsNullOrEmpty(Context.JsonState.TirePath))
                    {
                        EditorUtility.DisplayDialog("No File", "Select a tire JSON entry before saving.", "OK");
                        return;
                    }
                    
                    SaveJsonFile(saveAs, "tire JSON", 
                        Context.JsonState.TirePath, 
                        updated,
                        (path) => {
                            Context.JsonState.LoadTire(updated);
                            if (path != Context.JsonState.TirePath)
                            {
                                Context.JsonState.SetTirePath(path);
                                UpdateTirePathInVehicle(path);
                            }
                        });
                    break;
                    
                case "chassis":
                    if (string.IsNullOrEmpty(cachedChassisPath))
                    {
                        EditorUtility.DisplayDialog("No File", "No chassis JSON file loaded.", "OK");
                        return;
                    }
                    
                    SaveJsonFile(saveAs, "chassis JSON", 
                        cachedChassisPath, 
                        updated,
                        (path) => {
                            cachedChassisData = updated;
                            cachedChassisPath = path;
                            if (saveAs && Context.JsonState.VehicleData != null)
                            {
                                Context.JsonState.VehicleData["Chassis"]["Input File"] = path;
                            }
                        });
                    break;
                    
                case "driveline":
                    if (string.IsNullOrEmpty(cachedDrivelinePath))
                    {
                        EditorUtility.DisplayDialog("No File", "No driveline JSON file loaded.", "OK");
                        return;
                    }
                    
                    SaveJsonFile(saveAs, "driveline JSON", 
                        cachedDrivelinePath, 
                        updated,
                        (path) => {
                            cachedDrivelineData = updated;
                            cachedDrivelinePath = path;
                            if (saveAs && Context.JsonState.VehicleData != null)
                            {
                                Context.JsonState.VehicleData["Driveline"]["Input File"] = path;
                            }
                        });
                    break;
                    
                case "suspension":
                    if (string.IsNullOrEmpty(cachedSuspensionPath))
                    {
                        EditorUtility.DisplayDialog("No File", "No suspension JSON file loaded.", "OK");
                        return;
                    }
                    
                    SaveJsonFile(saveAs, "suspension JSON", 
                        cachedSuspensionPath, 
                        updated,
                        (path) => {
                            cachedSuspensionData = updated;
                            cachedSuspensionPath = path;
                            if (saveAs && cachedSuspensionOptions.Count > 0 && selectedSuspensionOption < cachedSuspensionOptions.Count)
                            {
                                var option = cachedSuspensionOptions[selectedSuspensionOption];
                                UpdateAxleSuspensionPath(option.Index, path);
                            }
                        });
                    break;
                    
                case "brake":
                    if (string.IsNullOrEmpty(cachedBrakePath))
                    {
                        EditorUtility.DisplayDialog("No File", "No brake JSON file loaded.", "OK");
                        return;
                    }
                    
                    SaveJsonFile(saveAs, "brake JSON", 
                        cachedBrakePath, 
                        updated,
                        (path) => {
                            cachedBrakeData = updated;
                            cachedBrakePath = path;
                            if (saveAs && cachedBrakeOptions.Count > 0 && selectedBrakeOption < cachedBrakeOptions.Count)
                            {
                                var option = cachedBrakeOptions[selectedBrakeOption];
                                UpdateAxleBrakePath(option.Index, path);
                            }
                        });
                    break;
                    
                case "wheel":
                    if (string.IsNullOrEmpty(cachedWheelPath))
                    {
                        EditorUtility.DisplayDialog("No File", "No wheel JSON file loaded.", "OK");
                        return;
                    }
                    
                    SaveJsonFile(saveAs, "wheel JSON", 
                        cachedWheelPath, 
                        updated,
                        (path) => {
                            cachedWheelData = updated;
                            cachedWheelPath = path;
                            if (saveAs && cachedWheelOptions.Count > 0 && selectedWheelOption < cachedWheelOptions.Count)
                            {
                                var option = cachedWheelOptions[selectedWheelOption];
                                UpdateAxleWheelPath(option.Index, path);
                            }
                        });
                    break;
            }
        }
        
        private void SaveJsonFile(bool saveAs, string fileType, string currentPath, JObject data, System.Action<string> onSaved)
        {
            string targetPath = currentPath;
            
            if (saveAs || string.IsNullOrEmpty(targetPath))
            {
                // Show save file dialog
                string directory = string.IsNullOrEmpty(targetPath) 
                    ? Context.BuilderCore.ChronoVehicleDataRoot 
                    : System.IO.Path.GetDirectoryName(targetPath);
                    
                string filename = string.IsNullOrEmpty(targetPath) 
                    ? $"{currentTarget}.json" 
                    : System.IO.Path.GetFileName(targetPath);
                
                targetPath = EditorUtility.SaveFilePanel(
                    $"Save {fileType} As",
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
            if (!JsonSaveUtility.ConfirmSave(fileType, targetPath))
            {
                return;
            }
            
            // Save the file
            try
            {
                Context.BuilderCore.SaveJson(data, targetPath);
                onSaved?.Invoke(targetPath);
                EditorUtility.DisplayDialog("Success", $"{fileType} saved to:\n{targetPath}", "OK");
                
                // Update editor if path changed
                if (targetPath != currentPath)
                {
                    lastTargetPath = targetPath;
                }
            }
            catch (System.Exception ex)
            {
                EditorUtility.DisplayDialog("Error", $"Failed to save {fileType}:\n{ex.Message}", "OK");
                Debug.LogError($"[RawJsonTab] Failed to save {fileType}: {ex}");
            }
        }
        
        private void UpdateTirePathInVehicle(string newPath)
        {
            if (cachedTireOptions.Count == 0 || selectedTireOption >= cachedTireOptions.Count)
                return;
                
            var option = cachedTireOptions[selectedTireOption];
            
            if (option.Index == -1)
            {
                // Single tire file mode
                Context.Vehicle.tireJSON = newPath;
            }
            else
            {
                // Per-axle/per-wheel mode
                if (option.Index < Context.Vehicle.perAxleTireSpec.Count)
                {
                    Context.Vehicle.perAxleTireSpec[option.Index] = newPath;
                }
            }
            
            EditorUtility.SetDirty(Context.Vehicle);
        }
        
        private void UpdateAxleSuspensionPath(int axleIndex, string newPath)
        {
            if (Context.JsonState.VehicleData == null)
                return;
                
            var axles = Context.JsonState.VehicleData["Axles"] as JArray;
            if (axles != null && axleIndex < axles.Count)
            {
                axles[axleIndex]["Suspension Input File"] = newPath;
            }
        }
        
        private void UpdateAxleBrakePath(int optionIndex, string newPath)
        {
            if (Context.JsonState.VehicleData == null)
                return;
                
            var axles = Context.JsonState.VehicleData["Axles"] as JArray;
            if (axles == null)
                return;
                
            int axleIndex = optionIndex / 2;
            bool isLeft = (optionIndex % 2) == 0;
            
            if (axleIndex < axles.Count)
            {
                string fieldName = isLeft ? "Left Brake Input File" : "Right Brake Input File";
                axles[axleIndex][fieldName] = newPath;
            }
        }
        
        private void UpdateAxleWheelPath(int optionIndex, string newPath)
        {
            if (Context.JsonState.VehicleData == null)
                return;
                
            var axles = Context.JsonState.VehicleData["Axles"] as JArray;
            if (axles == null)
                return;
                
            int axleIndex = optionIndex / 2;
            bool isLeft = (optionIndex % 2) == 0;
            
            if (axleIndex < axles.Count)
            {
                string fieldName = isLeft ? "Left Wheel Input File" : "Right Wheel Input File";
                axles[axleIndex][fieldName] = newPath;
            }
        }

        private void DrawTireSelectionDropdown()
        {
            EnsureTireOptionSelection();
            if (cachedTireOptions.Count <= 1)
            {
                return;
            }

            string[] labels = cachedTireOptions.Select(o => o.Label).ToArray();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Tire Entry:", GUILayout.Width(80));
            int newIndex = EditorGUILayout.Popup(selectedTireOption, labels);
            EditorGUILayout.EndHorizontal();

            if (newIndex != selectedTireOption)
            {
                selectedTireOption = newIndex;
                ApplyTireOption(cachedTireOptions[selectedTireOption]);
            }
        }

        private void EnsureTireOptionSelection()
        {
            if (Context.Vehicle != null)
            {
                Context.Vehicle.RefreshTireOverridesFromInspector(forceReset: false);
            }

            var newOptions = BuildTireOptions();
            bool reloadList = newOptions.Count != cachedTireOptions.Count ||
                              newOptions.Where((t, i) => cachedTireOptions.Count <= i || cachedTireOptions[i].Path != t.Path).Any();

            if (reloadList)
            {
                cachedTireOptions = newOptions;

                string currentPath = Context.JsonState.TirePath;
                int matchIndex = cachedTireOptions.FindIndex(o => o.Path == currentPath);
                selectedTireOption = matchIndex >= 0 ? matchIndex : 0;

                if (cachedTireOptions.Count > 0)
                {
                    // Force load the tire JSON immediately
                    var selectedOption = cachedTireOptions[selectedTireOption];
                    Context.JsonState.SetTirePath(selectedOption.Path);
                    
                    // Load the JSON synchronously instead of waiting for ApplyTireOption
                    if (!string.IsNullOrEmpty(selectedOption.Path))
                    {
                        try
                        {
                            var data = Context.BuilderCore.LoadJson(selectedOption.Path);
                            Context.JsonState.LoadTire(data);
                        }
                        catch (System.Exception ex)
                        {
                            Debug.LogError($"[RawJsonTab] Failed to auto-load tire JSON: {ex.Message}");
                        }
                    }
                }
            }
        }

        private List<TargetFileOption> BuildTireOptions()
        {
            var options = new List<TargetFileOption>();

            if (Context.Vehicle == null)
            {
                return options;
            }

            if (Context.Vehicle.useSingleTireFile || Context.Vehicle.tireAssignmentMode == UWheeledVehicle.TireAssignmentMode.SingleFile)
            {
                options.Add(new TargetFileOption
                {
                    Label = "All Tires",
                    Path = Context.Vehicle.tireJSON,
                    Index = -1
                });
                return options;
            }

            var entries = Context.Vehicle.perAxleTireSpec;
            if (entries == null || entries.Count == 0)
            {
                return options;
            }

            for (int i = 0; i < entries.Count; i++)
            {
                string entryPath = entries[i];
                if (string.IsNullOrWhiteSpace(entryPath))
                {
                    entryPath = Context.Vehicle.tireJSON;
                }

                if (!string.IsNullOrWhiteSpace(entryPath) && Path.IsPathRooted(entryPath))
                {
                    string relative = Context.BuilderCore.ToRelativePath(entryPath);
                    if (!string.IsNullOrEmpty(relative))
                    {
                        entryPath = relative;
                        entries[i] = relative;
                        EditorUtility.SetDirty(Context.Vehicle);
                    }
                }

                string label = Context.Vehicle.tireAssignmentMode == UWheeledVehicle.TireAssignmentMode.PerWheelList
                    ? $"Wheel {i + 1}"
                    : $"Axle {i}";

                options.Add(new TargetFileOption
                {
                    Label = label,
                    Path = entryPath,
                    Index = i
                });
            }

            return options;
        }

        private void ApplyTireOption(TargetFileOption option, bool forceReload = true)
        {
            if (option == null)
                return;

            string path = option.Path;
            Context.JsonState.SetTirePath(path);
            Context.JsonState.LoadTire(null);
            if (forceReload)
            {
                showEditor = false;
                lastTargetPath = string.Empty;
                Context.MapEditor?.ClearAllMapStates();
            }
        }

        private string GetCurrentTireSelectionLabel()
        {
            if (cachedTireOptions == null || cachedTireOptions.Count == 0)
                return string.Empty;

            if (selectedTireOption >= 0 && selectedTireOption < cachedTireOptions.Count)
            {
                return cachedTireOptions[selectedTireOption].Label;
            }

            return string.Empty;
        }
        
        // ===== Driveline Methods =====
        
        private string GetDrivelinePath()
        {
            if (Context.JsonState.VehicleData == null)
                return string.Empty;
                
            return Context.JsonState.VehicleData["Driveline"]?["Input File"]?.ToString() ?? string.Empty;
        }
        
        private string GetChassisPath()
        {
            if (Context.JsonState.VehicleData == null)
                return string.Empty;
                
            return Context.JsonState.VehicleData["Chassis"]?["Input File"]?.ToString() ?? string.Empty;
        }
        
        // ===== Suspension Methods =====
        
        private void EnsureSuspensionOptionSelection()
        {
            if (cachedSuspensionOptions.Count == 0)
            {
                cachedSuspensionOptions = BuildSuspensionOptions();
                if (cachedSuspensionOptions.Count > 0 && selectedSuspensionOption >= cachedSuspensionOptions.Count)
                {
                    selectedSuspensionOption = 0;
                }
            }
        }
        
        private List<TargetFileOption> BuildSuspensionOptions()
        {
            var options = new List<TargetFileOption>();
            
            if (Context.JsonState.VehicleData == null)
                return options;
                
            var axles = Context.JsonState.VehicleData["Axles"] as JArray;
            if (axles == null)
                return options;
                
            for (int i = 0; i < axles.Count; i++)
            {
                string path = axles[i]?["Suspension Input File"]?.ToString() ?? string.Empty;
                if (!string.IsNullOrEmpty(path))
                {
                    options.Add(new TargetFileOption
                    {
                        Label = $"Axle {i}",
                        Path = path,
                        Index = i
                    });
                }
            }
            
            return options;
        }
        
        private string GetSuspensionPath()
        {
            if (cachedSuspensionOptions.Count == 0)
                return string.Empty;
                
            if (selectedSuspensionOption >= 0 && selectedSuspensionOption < cachedSuspensionOptions.Count)
            {
                return cachedSuspensionOptions[selectedSuspensionOption].Path;
            }
            
            return string.Empty;
        }
        
        private string GetCurrentSuspensionSelectionLabel()
        {
            if (cachedSuspensionOptions.Count == 0)
                return string.Empty;
                
            if (selectedSuspensionOption >= 0 && selectedSuspensionOption < cachedSuspensionOptions.Count)
            {
                return cachedSuspensionOptions[selectedSuspensionOption].Label;
            }
            
            return string.Empty;
        }
        
        private void DrawSuspensionSelectionDropdown()
        {
            if (cachedSuspensionOptions.Count <= 1)
                return;
                
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Select Suspension:", GUILayout.Width(120));
            
            string[] labels = cachedSuspensionOptions.Select(o => o.Label).ToArray();
            int newSelection = EditorGUILayout.Popup(selectedSuspensionOption, labels);
            
            if (newSelection != selectedSuspensionOption)
            {
                selectedSuspensionOption = newSelection;
                cachedSuspensionData = null;
                cachedSuspensionPath = "";
                showEditor = false;
            }
            
            EditorGUILayout.EndHorizontal();
        }
        
        // ===== Brake Methods =====
        
        private void EnsureBrakeOptionSelection()
        {
            if (cachedBrakeOptions.Count == 0)
            {
                cachedBrakeOptions = BuildBrakeOptions();
                if (cachedBrakeOptions.Count > 0 && selectedBrakeOption >= cachedBrakeOptions.Count)
                {
                    selectedBrakeOption = 0;
                }
            }
        }
        
        private List<TargetFileOption> BuildBrakeOptions()
        {
            var options = new List<TargetFileOption>();
            
            if (Context.JsonState.VehicleData == null)
                return options;
                
            var axles = Context.JsonState.VehicleData["Axles"] as JArray;
            if (axles == null)
                return options;
                
            for (int i = 0; i < axles.Count; i++)
            {
                string leftPath = axles[i]?["Left Brake Input File"]?.ToString() ?? string.Empty;
                string rightPath = axles[i]?["Right Brake Input File"]?.ToString() ?? string.Empty;
                
                if (!string.IsNullOrEmpty(leftPath))
                {
                    options.Add(new TargetFileOption
                    {
                        Label = $"Axle {i} - Left",
                        Path = leftPath,
                        Index = i * 2
                    });
                }
                
                if (!string.IsNullOrEmpty(rightPath))
                {
                    options.Add(new TargetFileOption
                    {
                        Label = $"Axle {i} - Right",
                        Path = rightPath,
                        Index = i * 2 + 1
                    });
                }
            }
            
            return options;
        }
        
        private string GetBrakePath()
        {
            if (cachedBrakeOptions.Count == 0)
                return string.Empty;
                
            if (selectedBrakeOption >= 0 && selectedBrakeOption < cachedBrakeOptions.Count)
            {
                return cachedBrakeOptions[selectedBrakeOption].Path;
            }
            
            return string.Empty;
        }
        
        private string GetCurrentBrakeSelectionLabel()
        {
            if (cachedBrakeOptions.Count == 0)
                return string.Empty;
                
            if (selectedBrakeOption >= 0 && selectedBrakeOption < cachedBrakeOptions.Count)
            {
                return cachedBrakeOptions[selectedBrakeOption].Label;
            }
            
            return string.Empty;
        }
        
        private void DrawBrakeSelectionDropdown()
        {
            if (cachedBrakeOptions.Count <= 1)
                return;
                
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Select Brake:", GUILayout.Width(120));
            
            string[] labels = cachedBrakeOptions.Select(o => o.Label).ToArray();
            int newSelection = EditorGUILayout.Popup(selectedBrakeOption, labels);
            
            if (newSelection != selectedBrakeOption)
            {
                selectedBrakeOption = newSelection;
                cachedBrakeData = null;
                cachedBrakePath = "";
                showEditor = false;
            }
            
            EditorGUILayout.EndHorizontal();
        }
        
        // ===== Wheel Methods =====
        
        private void EnsureWheelOptionSelection()
        {
            if (cachedWheelOptions.Count == 0)
            {
                cachedWheelOptions = BuildWheelOptions();
                if (cachedWheelOptions.Count > 0 && selectedWheelOption >= cachedWheelOptions.Count)
                {
                    selectedWheelOption = 0;
                }
            }
        }
        
        private List<TargetFileOption> BuildWheelOptions()
        {
            var options = new List<TargetFileOption>();
            
            if (Context.JsonState.VehicleData == null)
                return options;
                
            var axles = Context.JsonState.VehicleData["Axles"] as JArray;
            if (axles == null)
                return options;
                
            for (int i = 0; i < axles.Count; i++)
            {
                string leftPath = axles[i]?["Left Wheel Input File"]?.ToString() ?? string.Empty;
                string rightPath = axles[i]?["Right Wheel Input File"]?.ToString() ?? string.Empty;
                
                if (!string.IsNullOrEmpty(leftPath))
                {
                    options.Add(new TargetFileOption
                    {
                        Label = $"Axle {i} - Left",
                        Path = leftPath,
                        Index = i * 2
                    });
                }
                
                if (!string.IsNullOrEmpty(rightPath))
                {
                    options.Add(new TargetFileOption
                    {
                        Label = $"Axle {i} - Right",
                        Path = rightPath,
                        Index = i * 2 + 1
                    });
                }
            }
            
            return options;
        }
        
        private string GetWheelPath()
        {
            if (cachedWheelOptions.Count == 0)
                return string.Empty;
                
            if (selectedWheelOption >= 0 && selectedWheelOption < cachedWheelOptions.Count)
            {
                return cachedWheelOptions[selectedWheelOption].Path;
            }
            
            return string.Empty;
        }
        
        private string GetCurrentWheelSelectionLabel()
        {
            if (cachedWheelOptions.Count == 0)
                return string.Empty;
                
            if (selectedWheelOption >= 0 && selectedWheelOption < cachedWheelOptions.Count)
            {
                return cachedWheelOptions[selectedWheelOption].Label;
            }
            
            return string.Empty;
        }
        
        private void DrawWheelSelectionDropdown()
        {
            if (cachedWheelOptions.Count <= 1)
                return;
                
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Select Wheel:", GUILayout.Width(120));
            
            string[] labels = cachedWheelOptions.Select(o => o.Label).ToArray();
            int newSelection = EditorGUILayout.Popup(selectedWheelOption, labels);
            
            if (newSelection != selectedWheelOption)
            {
                selectedWheelOption = newSelection;
                cachedWheelData = null;
                cachedWheelPath = "";
                showEditor = false;
            }
            
            EditorGUILayout.EndHorizontal();
        }
        
        // ===== Generic Component Loading =====
        
        private async void LoadComponentAsync(string path, System.Action<JObject> callback)
        {
            if (string.IsNullOrEmpty(path))
            {
                callback?.Invoke(null);
                return;
            }

            try
            {
                var data = await System.Threading.Tasks.Task.Run(() => Context.BuilderCore.LoadJson(path));
                callback?.Invoke(data);
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"[RawJsonTab] Failed to load component JSON from {path}: {ex.Message}");
                callback?.Invoke(null);
            }
        }
    }
}
