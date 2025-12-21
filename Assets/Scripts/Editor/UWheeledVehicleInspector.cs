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
// Custom vehicle inspector for UWheeledVehicle - uses tab system to pull
// chrono jsons and develop/edit/create a vehicle. Allows for easy editing and
// switching and handling json data
// =============================================================================

using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using ChronoVehicleBuilder;
using VehicleBuilder.Editor;
using Debug = UnityEngine.Debug;
using Stopwatch = System.Diagnostics.Stopwatch;

[CustomEditor(typeof(UWheeledVehicle))]
public class UWheeledVehicleInspector : Editor
{
    // ========== INSPECTOR STATE ==========
    private VehicleJsonState jsonState = new VehicleJsonState();
    
    private UWheeledVehicle vehicle;
    
    // Builder tools
    private VehicleBuilderCore builderCore;
    private VehicleMapEditor mapEditor;
    private VehicleJSONParser jsonParser;
    private VehiclePrefabScanner prefabScanner;
    private SyntaxHighlightedJsonEditor textEditor;
    
    // Tab management
    private int selectedTab = 0;
    private readonly string[] tabNames = new string[] { "Overview", "Vehicle", "Engine", "Transmission", "Tires", "Prefabs", "Raw JSON" };
    
    // NEW: OOP Tab System
    private VehicleBuilder.Editor.VehicleInspectorContext inspectorContext;
    private Dictionary<int, VehicleBuilder.Editor.InspectorTab> tabs;
    private int previousTab = -1;
    
    // Scroll positions for different tabs
    private Vector2 scrollPosition = Vector2.zero;
    
    // JSON data holders (loaded on-demand, not every frame)
    private JObject vehicleJsonData;
    private JObject engineJsonData;
    private JObject transmissionJsonData;
    private JObject tireJsonData;
    
    // Parsed vehicle data structure (with axles, etc.)
    private VehicleDataModel.VehicleData parsedVehicleData;
    
    // JSON loaded flags to prevent reloading
    private bool vehicleJsonLoaded = false;
    
    // Vehicle type selection
    private string[] availableVehicleTypes;
    private int selectedVehicleTypeIndex = 0;
    private string currentVehicleType = "hmmwv";
    
    // Cached file lists
    private List<string> vehicleJsonFiles = new List<string>();
    private List<string> engineJsonFiles = new List<string>();
    private List<string> transmissionJsonFiles = new List<string>();
    private List<string> tireJsonFiles = new List<string>();
    
    // Last paths to detect changes
    private string lastEngineJsonPath = "";
    private string lastTransJsonPath = "";
    private string lastTireJsonPath = "";

    private void OnEnable()
    {
        vehicle = (UWheeledVehicle)target;
        
        // Initialize builder tools
        builderCore = new VehicleBuilderCore();
        mapEditor = new VehicleMapEditor();
        jsonParser = new VehicleJSONParser(builderCore, mapEditor);
        prefabScanner = new VehiclePrefabScanner();
        textEditor = new SyntaxHighlightedJsonEditor(() => Repaint());
        
        // Get available vehicle types
        availableVehicleTypes = builderCore.GetVehicleTypes().ToArray();
        
        // Detect current vehicle type from JSON path
        DetectVehicleType();
        
        // Sync jsonState with current vehicle JSON paths
        SyncJsonStateFromVehicle();
        
        // Refresh file lists
        RefreshFileLists();
        
        // Force invalidate all cached data on enable (script reload, selection change, etc.)
        InvalidateJsonCache();
        
        // NEW: Initialize OOP tab system
        InitializeTabSystem();
        
        // Don't load JSON data here - do it on-demand per tab
    }
    
    // NEW: Initialize the OOP tab system
    private void InitializeTabSystem()
    {
        // Create context with all shared dependencies
        inspectorContext = new VehicleBuilder.Editor.VehicleInspectorContext(
            vehicle,
            jsonState,
            builderCore,
            jsonParser,
            prefabScanner,
            textEditor,
            mapEditor
        );
        
        inspectorContext.CurrentVehicleType = currentVehicleType;
        inspectorContext.VehicleJsonFiles = vehicleJsonFiles;
        inspectorContext.EngineJsonFiles = engineJsonFiles;
        inspectorContext.TransmissionJsonFiles = transmissionJsonFiles;
        inspectorContext.TireJsonFiles = tireJsonFiles;
        inspectorContext.ParsedVehicleData = parsedVehicleData;
        
        // Wire up callbacks for inspector-level operations
        inspectorContext.OnVehicleJsonChanged = HandleVehicleJsonChanged;
    inspectorContext.OnVehicleStructureChanged = HandleVehicleStructureChanged;
        
        // Create all tab instances
        tabs = new Dictionary<int, VehicleBuilder.Editor.InspectorTab>
        {
            { 0, new VehicleBuilder.Editor.OverviewTab(inspectorContext) },
            { 1, new VehicleBuilder.Editor.VehicleTab(inspectorContext) },
            { 2, new VehicleBuilder.Editor.EngineTab(inspectorContext) },
            { 3, new VehicleBuilder.Editor.TransmissionTab(inspectorContext) },
            { 4, new VehicleBuilder.Editor.TiresTab(inspectorContext) },
            { 5, new VehicleBuilder.Editor.PrefabsTab(inspectorContext) },
            { 6, new VehicleBuilder.Editor.RawJsonTab(inspectorContext) }
        };
        
        // Call OnTabEnter for the initially selected tab
        tabs[selectedTab]?.OnTabEnter();
    }
    
    // Sync the central JSON state from the vehicle object
    private void SyncJsonStateFromVehicle()
    {
        jsonState.SetVehiclePath(vehicle.topLevelVehicleJSON ?? "");
        jsonState.SetEnginePath(vehicle.engineJSON ?? "");
        jsonState.SetTransmissionPath(vehicle.transJSON ?? "");
        jsonState.SetTirePath(vehicle.tireJSON ?? "");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        DrawNewInspectorGUI();
        
        serializedObject.ApplyModifiedProperties();
        
        // Apply changes and mark dirty
        if (GUI.changed)
        {
            EditorUtility.SetDirty(vehicle);
        }
    }
    
    // NEW: Clean OOP-based inspector GUI (~30 lines!)
    private void DrawNewInspectorGUI()
    {
        // Safety check - if context isn't initialized, show error and return
        if (inspectorContext == null)
        {
            EditorGUILayout.HelpBox("Inspector context not initialized. This may happen during script compilation. Try selecting the object again.", UnityEditor.MessageType.Error);
            if (GUILayout.Button("Reinitialize"))
            {
                OnEnable();
            }
            return;
        }
        
        // Update context with latest state
        inspectorContext.CurrentVehicleType = currentVehicleType;
        inspectorContext.ParsedVehicleData = parsedVehicleData;
        inspectorContext.VehicleJsonFiles = vehicleJsonFiles;
        inspectorContext.EngineJsonFiles = engineJsonFiles;
        inspectorContext.TransmissionJsonFiles = transmissionJsonFiles;
        inspectorContext.TireJsonFiles = tireJsonFiles;
        
        // Header with vehicle type
        DrawInspectorHeader();
        
        EditorGUILayout.Space(5);
        
        // Tab toolbar
        int newTab = GUILayout.Toolbar(selectedTab, tabNames);
        
        // Handle tab switching
        if (newTab != selectedTab)
        {
            tabs[selectedTab]?.OnTabExit();
            selectedTab = newTab;
            tabs[selectedTab]?.OnTabEnter();
            previousTab = selectedTab;
            
            // Clear UI focus and map editor state
            GUI.FocusControl(null);
            mapEditor?.ClearAllMapStates();
            
            // Force repaint to ensure plots and other interactive elements update immediately
            Repaint();
        }
        
        EditorGUILayout.Space(5);
        
        // Draw current tab (delegated to tab class)
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        try
        {
            tabs[selectedTab]?.DrawTab();
        }
        catch (ExitGUIException)
        {
            // ExitGUI is thrown by Unity when ObjectFields open pickers - this is normal, just rethrow
            throw;
        }
        catch (System.Exception ex)
        {
            EditorGUILayout.HelpBox($"Error rendering tab: {ex.Message}", UnityEditor.MessageType.Error);
            Debug.LogException(ex);
        }
        finally
        {
            EditorGUILayout.EndScrollView();
        }
    }
    
    // Header bar with title and vehicle type selector
    private void DrawInspectorHeader()
    {
        float inspectorWidth = EditorGUIUtility.currentViewWidth - 32f;
        EditorGUILayout.BeginHorizontal("box", GUILayout.MaxWidth(inspectorWidth));
        
        float titleWidth = inspectorWidth > 250f ? 180f : Mathf.Max(80f, inspectorWidth - 150f);
        EditorGUILayout.LabelField("Chrono Vehicle Builder", EditorStyles.boldLabel, GUILayout.MaxWidth(titleWidth));
        GUILayout.FlexibleSpace();
        
        // Vehicle type quick selector
        if (inspectorWidth > 180f)
        {
            EditorGUILayout.LabelField("Type:", GUILayout.Width(35));
            float popupWidth = Mathf.Min(100f, inspectorWidth - 130f);
            int newTypeIndex = EditorGUILayout.Popup(selectedVehicleTypeIndex, availableVehicleTypes, GUILayout.Width(popupWidth));
            if (newTypeIndex != selectedVehicleTypeIndex)
            {
                selectedVehicleTypeIndex = newTypeIndex;
                currentVehicleType = availableVehicleTypes[selectedVehicleTypeIndex];
                inspectorContext.CurrentVehicleType = currentVehicleType;
                RefreshFileLists();
                InvalidateJsonCache();
                AutoSetDefaultJsonFiles();
                AutoAssignVehiclePrefabs();  // This loads JSON and creates axles
                SyncPerAxleTireList(forceReset: true);       // Sync tire list after axles are created
                
                // Notify current tab to reload its data
                tabs[selectedTab]?.OnTabEnter();
                
                // Force repaint to show updated data
                Repaint();
            }
        }
        
        EditorGUILayout.EndHorizontal();
    }
    
    // ========== HELPER METHODS ==========
    
    private void HandleVehicleJsonChanged()
    {
        // Called when tabs change the vehicle JSON file
        // This triggers the same flow as changing vehicle type in the header
        Debug.Log("[HandleVehicleJsonChanged] Vehicle JSON changed, regenerating prefabs and parsing data");
        
        InvalidateJsonCache();
        AutoAssignVehiclePrefabs();  // This will load fresh JSON and regenerate prefabs
    SyncPerAxleTireList(forceReset: true);       // Sync tire list after axles are created
    }

    private void HandleVehicleStructureChanged()
    {
        parsedVehicleData = inspectorContext?.ParsedVehicleData;
        if (parsedVehicleData != null)
        {
            SyncAxleDataWithParsedData();
            Repaint();
        }
    }
    
    private void InvalidateJsonCache()
    {
        vehicleJsonLoaded = false;
        vehicleJsonData = null;
        engineJsonData = null;
        transmissionJsonData = null;
        tireJsonData = null;
        jsonParser.ClearCache();
        mapEditor?.ClearAllMapStates();
        
        // Also invalidate the new JsonState system
        jsonState.InvalidateAll();
    }

    // ========== OLD TAB METHODS DELETED - NOW USING OOP TAB CLASSES ==========
    // All Draw*Tab() methods have been moved to separate classes in VehicleInspector/Tabs/
    // See: OverviewTab.cs, VehicleJsonTab.cs, EngineTab.cs, TransmissionTab.cs, 
    //      TiresAndWheelsTab.cs, PrefabsTab.cs, RawJsonTab.cs
    
    
    private void AutoAssignAllWheels()
    {
        VehicleSetupUtility.AutoAssignAllWheels(vehicle);
    }
    
    private void ApplyCompleteVehicleSetup(VehiclePrefabScanner.VehicleSetup setup)
    {
        // Ensure vehicle JSON is loaded to get axle data
        if (!vehicleJsonLoaded && !string.IsNullOrEmpty(vehicle.topLevelVehicleJSON))
        {
            LoadVehicleJsonData();
        }
        
        // CRITICAL: Clean up ALL existing axles and wheels before creating new ones
        CleanupExistingAxlesAndWheels();
        
        int assignedCount = 0;
        
        // Assign chassis
        if (setup.HasChassis)
        {
            VehicleSetupUtility.AssignChassisPrefab(vehicle, setup.Chassis);
            assignedCount++;
        }
        
        // Assign all wheels with proper positioning
        int numAxles = vehicle.axleData?.Count ?? setup.Wheels.Count;
        for (int axleIdx = 0; axleIdx < numAxles && axleIdx < setup.Wheels.Count; axleIdx++)
        {
            var axle = vehicle.axleData[axleIdx];
            if (axle.visualGameobjects == null)
                axle.visualGameobjects = new List<GameObject>();
            
            while (axle.visualGameobjects.Count < 2)
                axle.visualGameobjects.Add(null);
            
            var wheelPair = setup.Wheels[axleIdx];
            
            // Get axle position and spacing from parsed data
            Vector3 axlePosition = Vector3.zero;
            float halfTrack = 1.0f;
            
            if (parsedVehicleData != null && parsedVehicleData.Axles != null && axleIdx < parsedVehicleData.Axles.Count)
            {
                var axleData = parsedVehicleData.Axles[axleIdx];
                axlePosition = new Vector3(
                    axleData.SuspensionLocation.x,
                    axleData.SuspensionLocation.z,
                    axleData.SuspensionLocation.y
                );
                
                if (!string.IsNullOrEmpty(axleData.Suspension))
                {
                    halfTrack = VehicleSetupUtility.GetSuspensionHalfTrack(builderCore, axleData.Suspension);
                }
            }
            
            // Create or update axle container
            Transform axleContainer = vehicle.transform.Find($"Axle_{axleIdx}");
            if (axleContainer == null)
            {
                GameObject axleGO = new GameObject($"Axle_{axleIdx}");
                axleGO.transform.SetParent(vehicle.transform);
                axleGO.transform.localPosition = axlePosition;
                axleContainer = axleGO.transform;
                
                // Create axle visualization
                VehicleSetupUtility.CreateAxleVisualization(axleContainer, halfTrack);
            }
            else
            {
                axleContainer.localPosition = axlePosition;
                // Update axle visualization
                VehicleSetupUtility.CreateAxleVisualization(axleContainer, halfTrack);
            }
            
            if (wheelPair.Left != null)
            {
                axle.visualGameobjects[0] = VehicleSetupUtility.AssignWheel(wheelPair.Left, axleContainer, "Left", halfTrack);
                assignedCount++;
            }
            
            if (wheelPair.Right != null)
            {
                axle.visualGameobjects[1] = VehicleSetupUtility.AssignWheel(wheelPair.Right, axleContainer, "Right", -halfTrack);
                assignedCount++;
            }
        }
        
        EditorUtility.SetDirty(vehicle);
        EditorUtility.DisplayDialog("Complete Setup Applied", 
            $"Successfully assigned {assignedCount} component(s) for {currentVehicleType.ToUpper()} vehicle:\n" +
            $"• Chassis: {(setup.HasChassis ? "✓" : "✗")}\n" +
            $"• Wheels: {(setup.HasAllWheels ? "✓ All axles" : "⚠ Partial")}", "OK");
    }
    
    private void AutoSetDefaultJsonFiles()
    {
        // Auto-select the first available JSON file for each category when vehicle type changes
        // Debug.Log($"Auto-setting default JSON files for vehicle type: {currentVehicleType}");
        
        // Set vehicle JSON (prefer first one in the list)
        if (vehicleJsonFiles != null && vehicleJsonFiles.Count > 0)
        {
            string oldPath = vehicle.topLevelVehicleJSON;
            vehicle.topLevelVehicleJSON = vehicleJsonFiles[0];
            jsonState.SetVehiclePath(vehicle.topLevelVehicleJSON);
            // Debug.Log($"Auto-set vehicle JSON: {oldPath} -> {vehicle.topLevelVehicleJSON}");
        }
        
        // Set engine JSON
        if (engineJsonFiles != null && engineJsonFiles.Count > 0)
        {
            string oldPath = vehicle.engineJSON;
            vehicle.engineJSON = engineJsonFiles[0];
            jsonState.SetEnginePath(vehicle.engineJSON);
            // Debug.Log($"Auto-set engine JSON: {oldPath} -> {vehicle.engineJSON}");
        }
        
        // Set transmission JSON
        if (transmissionJsonFiles != null && transmissionJsonFiles.Count > 0)
        {
            string oldPath = vehicle.transJSON;
            vehicle.transJSON = transmissionJsonFiles[0];
            jsonState.SetTransmissionPath(vehicle.transJSON);
            // Debug.Log($"Auto-set transmission JSON: {oldPath} -> {vehicle.transJSON}");
        }
        
        // Set tire JSON
        if (tireJsonFiles != null && tireJsonFiles.Count > 0)
        {
            string oldPath = vehicle.tireJSON;
            vehicle.tireJSON = tireJsonFiles[0];
            jsonState.SetTirePath(vehicle.tireJSON);
            // Debug.Log($"Auto-set tire JSON: {oldPath} -> {vehicle.tireJSON}");
        }
        
        EditorUtility.SetDirty(vehicle);
    }
    
    private void SyncPerAxleTireList(bool forceReset)
    {
        vehicle.RefreshTireOverridesFromInspector(forceReset);
        if (forceReset)
        {
            inspectorContext?.TryAssignFrontRearTireDefaults(vehicle.axleData?.Count ?? 0);
        }
        EditorUtility.SetDirty(vehicle);
        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(vehicle.gameObject.scene);
    }
    
    private string GetSmartTireAssignment(int axleIndex, int totalAxles)
    {
        // Try to intelligently assign front/rear tires based on axle position and available files
        if (tireJsonFiles == null || tireJsonFiles.Count == 0)
        {
            return vehicle.tireJSON;
        }
        
        // Check if we have files with "Front" and "Rear" in the name
        var frontTires = tireJsonFiles.Where(f => f.ToLower().Contains("front")).ToList();
        var rearTires = tireJsonFiles.Where(f => f.ToLower().Contains("rear")).ToList();
        
        if (frontTires.Count > 0 && rearTires.Count > 0)
        {
            // We have both front and rear tires available
            // Assume first axle(s) are front, last axle(s) are rear
            if (totalAxles == 2)
            {
                // Simple case: 2 axles = front + rear
                return axleIndex == 0 ? frontTires[0] : rearTires[0];
            }
            else if (totalAxles > 2)
            {
                // Multiple axles: first is front, rest are rear
                return axleIndex == 0 ? frontTires[0] : rearTires[0];
            }
        }
        
        // Fallback to default tire
        return vehicle.tireJSON;
    }
    
    private void CleanupExistingAxlesAndWheels()
    {
        // Find and destroy ALL existing Axle_N GameObjects
        List<Transform> toDestroy = new List<Transform>();
        foreach (Transform child in vehicle.transform)
        {
            if (child.name.StartsWith("Axle_") || child.name == "Chassis")
            {
                toDestroy.Add(child);
            }
        }
        
        foreach (Transform obj in toDestroy)
        {
            DestroyImmediate(obj.gameObject);
        }
        
        // Clear the axleData references (they'll be repopulated)
        if (vehicle.axleData != null)
        {
            foreach (var axle in vehicle.axleData)
            {
                if (axle.visualGameobjects != null)
                {
                    axle.visualGameobjects.Clear();
                }
            }
        }
        
        Debug.Log($"Cleaned up {toDestroy.Count} existing components (axles and chassis)");
    }
    
    
    private void AutoAssignVehiclePrefabs()
    {
        // CRITICAL: Load and parse vehicle JSON FIRST to ensure fresh data
        if (!vehicleJsonLoaded && !string.IsNullOrEmpty(vehicle.topLevelVehicleJSON))
        {
            LoadVehicleJsonData();
        }
        
        VehicleSetupUtility.AutoAssignVehiclePrefabs(
            vehicle,
            currentVehicleType,
            prefabScanner,
            builderCore,
            parsedVehicleData,  // Now this is fresh data
            vehicleJsonLoaded,
            LoadVehicleJsonData,
            CleanupExistingAxlesAndWheels
        );
    }

    // Helper methods
    private void DetectVehicleType()
    {
        if (string.IsNullOrEmpty(vehicle.topLevelVehicleJSON))
            return;
        
        string lower = vehicle.topLevelVehicleJSON.ToLower();
        for (int i = 0; i < availableVehicleTypes.Length; i++)
        {
            if (lower.Contains(availableVehicleTypes[i].ToLower()))
            {
                selectedVehicleTypeIndex = i;
                currentVehicleType = availableVehicleTypes[i];
                return;
            }
        }
    }

    private void RefreshFileLists()
    {
        // Use the same filtering logic as the old vehicle generator tool
        vehicleJsonFiles = builderCore.GetFilesForVehicleTypeAndType(currentVehicleType, "Vehicle");
        engineJsonFiles = builderCore.GetFilesForVehicleTypeAndType(currentVehicleType, "Engine");
        transmissionJsonFiles = builderCore.GetFilesForVehicleTypeAndType(currentVehicleType, "Transmission");
        tireJsonFiles = builderCore.GetFilesForVehicleTypeAndType(currentVehicleType, "Tire");
        
        // Also update the inspector context file lists (used by new tab system)
        // Note: inspectorContext may be null during OnEnable before InitializeTabSystem is called
        if (inspectorContext != null)
        {
            inspectorContext.RefreshFileLists();
        }
    }

    private void LoadVehicleJsonData()
    {
        if (!string.IsNullOrEmpty(jsonState.VehiclePath))
        {
            try
            {
                JObject data = builderCore.LoadJson(jsonState.VehiclePath);
                if (data != null)
                {
                    // Debug.Log($"Loaded vehicle JSON: {jsonState.VehiclePath}");

                    parsedVehicleData = VehicleJsonDataParser.ParseVehicleData(data);
                    jsonState.LoadVehicle(data, parsedVehicleData);
                    inspectorContext.ParsedVehicleData = parsedVehicleData;

                    // Sync axleData count with parsed data
                    SyncAxleDataWithParsedData();
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Failed to load vehicle JSON: {ex.Message}");
                jsonState.LoadVehicle(null, null);
                parsedVehicleData = null;
            }
        }
    }
    
    private void SyncAxleDataWithParsedData()
    {
        if (parsedVehicleData == null || parsedVehicleData.Axles == null)
            return;
        
        // Ensure vehicle.axleData has the same count as parsed axles
        if (vehicle.axleData == null)
            vehicle.axleData = new List<WheelGameobjects>();
        
        int targetCount = parsedVehicleData.Axles.Count;
        
        // Remove excess axles if we have too many
        while (vehicle.axleData.Count > targetCount)
        {
            vehicle.axleData.RemoveAt(vehicle.axleData.Count - 1);
        }
        
        // Add missing axles if we have too few
        while (vehicle.axleData.Count < targetCount)
        {
            vehicle.axleData.Add(new WheelGameobjects { visualGameobjects = new List<GameObject>() });
        }
        
        // Initialize each axle's wheel list (2 wheels per axle)
        for (int i = 0; i < vehicle.axleData.Count; i++)
        {
            if (vehicle.axleData[i].visualGameobjects == null)
                vehicle.axleData[i].visualGameobjects = new List<GameObject>();
            
            while (vehicle.axleData[i].visualGameobjects.Count < 2)
                vehicle.axleData[i].visualGameobjects.Add(null);
        }
        
        EditorUtility.SetDirty(vehicle);
    }

    private void LoadEngineJsonData()
    {
        if (!string.IsNullOrEmpty(jsonState.EnginePath))
        {
            try
            {
                JObject data = builderCore.LoadJson(jsonState.EnginePath);
                jsonState.LoadEngine(data);
                if (data != null)
                    Debug.Log($"Loaded engine JSON: {jsonState.EnginePath}");
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Failed to load engine JSON: {ex.Message}");
                jsonState.LoadEngine(null);
            }
        }
    }

    private void LoadTransmissionJsonData()
    {
        if (!string.IsNullOrEmpty(jsonState.TransmissionPath))
        {
            try
            {
                JObject data = builderCore.LoadJson(jsonState.TransmissionPath);
                jsonState.LoadTransmission(data);
                if (data != null)
                    Debug.Log($"Loaded transmission JSON: {jsonState.TransmissionPath}");
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Failed to load transmission JSON: {ex.Message}");
                jsonState.LoadTransmission(null);
            }
        }
    }

    private void LoadTireJsonData()
    {
        if (!string.IsNullOrEmpty(jsonState.TirePath))
        {
            try
            {
                JObject data = builderCore.LoadJson(jsonState.TirePath);
                jsonState.LoadTire(data);
                if (data != null)
                    Debug.Log($"Loaded tire JSON: {jsonState.TirePath}");
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Failed to load tire JSON: {ex.Message}");
                jsonState.LoadTire(null);
            }
        }
    }

    private void SaveVehicleJsonData()
    {
        if (jsonState.VehicleData != null && !string.IsNullOrEmpty(jsonState.VehiclePath))
        {
            if (!JsonSaveUtility.ConfirmSave("vehicle JSON", jsonState.VehiclePath))
                return;

            builderCore.SaveJson(jsonState.VehicleData, jsonState.VehiclePath);
            EditorUtility.DisplayDialog("Saved", $"Vehicle JSON saved to:\n{jsonState.VehiclePath}", "OK");
        }
    }

    private void SaveEngineJsonData()
    {
        if (jsonState.EngineData != null && !string.IsNullOrEmpty(jsonState.EnginePath))
        {
            if (!JsonSaveUtility.ConfirmSave("engine JSON", jsonState.EnginePath))
                return;

            builderCore.SaveJson(jsonState.EngineData, jsonState.EnginePath);
            EditorUtility.DisplayDialog("Saved", $"Engine JSON saved to:\n{jsonState.EnginePath}", "OK");
        }
    }
    
    private void SaveEngineJsonDataAs()
    {
        if (engineJsonData == null)
        {
            EditorUtility.DisplayDialog("Error", "No engine JSON data to save.", "OK");
            return;
        }
        
        string directory = string.IsNullOrEmpty(vehicle.engineJSON) ? 
            Application.dataPath : System.IO.Path.GetDirectoryName(vehicle.engineJSON);
        string filename = string.IsNullOrEmpty(vehicle.engineJSON) ? 
            "NewEngine.json" : System.IO.Path.GetFileName(vehicle.engineJSON);
        
        string path = EditorUtility.SaveFilePanel("Save Engine JSON As", directory, filename, "json");
        if (!string.IsNullOrEmpty(path))
        {
            if (!JsonSaveUtility.ConfirmSaveAs("engine JSON", path))
                return;

            builderCore.SaveJson(engineJsonData, path);
            vehicle.engineJSON = path;
            lastEngineJsonPath = path;
            EditorUtility.DisplayDialog("Saved", $"Engine JSON saved to:\n{path}", "OK");
        }
    }

    private void SaveTransmissionJsonData()
    {
        if (jsonState.TransmissionData != null && !string.IsNullOrEmpty(jsonState.TransmissionPath))
        {
            if (!JsonSaveUtility.ConfirmSave("transmission JSON", jsonState.TransmissionPath))
                return;

            builderCore.SaveJson(jsonState.TransmissionData, jsonState.TransmissionPath);
            EditorUtility.DisplayDialog("Saved", $"Transmission JSON saved to:\n{jsonState.TransmissionPath}", "OK");
        }
    }
    
    private void SaveTransmissionJsonDataAs()
    {
        if (transmissionJsonData == null)
        {
            EditorUtility.DisplayDialog("Error", "No transmission JSON data to save.", "OK");
            return;
        }
        
        string directory = string.IsNullOrEmpty(vehicle.transJSON) ? 
            Application.dataPath : System.IO.Path.GetDirectoryName(vehicle.transJSON);
        string filename = string.IsNullOrEmpty(vehicle.transJSON) ? 
            "NewTransmission.json" : System.IO.Path.GetFileName(vehicle.transJSON);
        
        string path = EditorUtility.SaveFilePanel("Save Transmission JSON As", directory, filename, "json");
        if (!string.IsNullOrEmpty(path))
        {
            if (!JsonSaveUtility.ConfirmSaveAs("transmission JSON", path))
                return;

            builderCore.SaveJson(transmissionJsonData, path);
            vehicle.transJSON = path;
            lastTransJsonPath = path;
            EditorUtility.DisplayDialog("Saved", $"Transmission JSON saved to:\n{path}", "OK");
        }
    }

    private void SaveTireJsonData()
    {
        if (jsonState.TireData != null && !string.IsNullOrEmpty(jsonState.TirePath))
        {
            if (!JsonSaveUtility.ConfirmSave("tire JSON", jsonState.TirePath))
                return;

            builderCore.SaveJson(jsonState.TireData, jsonState.TirePath);
            EditorUtility.DisplayDialog("Saved", $"Tire JSON saved to:\n{jsonState.TirePath}", "OK");
        }
    }
    
    private void SaveTireJsonDataAs()
    {
        if (tireJsonData == null)
        {
            EditorUtility.DisplayDialog("Error", "No tire JSON data to save.", "OK");
            return;
        }
        
        string currentTireFile = vehicle.useSingleTireFile ? vehicle.tireJSON : 
                                (vehicle.perAxleTireSpec.Count > 0 ? vehicle.perAxleTireSpec[0] : "");
        
        string directory = string.IsNullOrEmpty(currentTireFile) ? 
            Application.dataPath : System.IO.Path.GetDirectoryName(currentTireFile);
        string filename = string.IsNullOrEmpty(currentTireFile) ? 
            "NewTire.json" : System.IO.Path.GetFileName(currentTireFile);
        
        string path = EditorUtility.SaveFilePanel("Save Tire JSON As", directory, filename, "json");
        if (!string.IsNullOrEmpty(path))
        {
            if (!JsonSaveUtility.ConfirmSaveAs("tire JSON", path))
                return;

            builderCore.SaveJson(tireJsonData, path);
            
            // Update the appropriate reference
            if (vehicle.useSingleTireFile)
            {
                vehicle.tireJSON = path;
            }
            else if (vehicle.perAxleTireSpec.Count > 0)
            {
                vehicle.perAxleTireSpec[0] = path;
            }
            
            lastTireJsonPath = path;
            EditorUtility.DisplayDialog("Saved", $"Tire JSON saved to:\n{path}", "OK");
        }
    }

    private void SaveVehicleJsonDataAs()
    {
        if (vehicleJsonData == null)
        {
            EditorUtility.DisplayDialog("Error", "No JSON data to save.", "OK");
            return;
        }
        
        string path = EditorUtility.SaveFilePanel("Save Vehicle JSON", builderCore.ChronoVehicleDataRoot, "NewVehicle.json", "json");
        if (!string.IsNullOrEmpty(path))
        {
            string relativePath = builderCore.ToRelativePath(path);
            if (!string.IsNullOrEmpty(relativePath))
            {
                if (!JsonSaveUtility.ConfirmSaveAs("vehicle JSON", relativePath))
                    return;

                builderCore.SaveJson(vehicleJsonData, relativePath);
                vehicle.topLevelVehicleJSON = relativePath;
                builderCore.RebuildCache();
                RefreshFileLists();
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
        
        builderCore.BuildDisplayListFromFilenames(files, out List<string> displayNames, out List<string> actualPaths);
        
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

    private int CountTotalWheels()
    {
        int count = 0;
        if (vehicle.axleData != null)
        {
            foreach (var axle in vehicle.axleData)
            {
                if (axle.visualGameobjects != null)
                    count += axle.visualGameobjects.Count;
            }
        }
        return count;
    }
}
