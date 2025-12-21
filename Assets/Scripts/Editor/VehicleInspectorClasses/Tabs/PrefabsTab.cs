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
// Prefabs tab - handles visual prefab assignment for chassis, wheels
// and other components
// =============================================================================

using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using ChronoVehicleBuilder;

namespace VehicleBuilder.Editor
{

    public class PrefabsTab : InspectorTab
    {
        // Persistent storage for user-selected prefabs (overrides auto-detected)
        private GameObject userSelectedChassis;
        
        public PrefabsTab(VehicleInspectorContext context) : base(context)
        {
        }
        
        public override void DrawTab()
        {
            DrawSectionHeader("Vehicle Prefabs & GameObjects");
            
            // Get complete vehicle setup
            int numAxles = Context.Vehicle.axleData?.Count ?? 2;
            var vehicleSetup = Context.PrefabScanner.GetCompleteVehicleSetup(Context.CurrentVehicleType, numAxles);
            
            // Show prefab scanner info
            DrawPrefabScannerInfo();
            
            EditorGUILayout.Space();
            
            // Show detected setup status
            DrawSetupStatus(vehicleSetup);
            
            EditorGUILayout.Space();
            
            // Bulk assignment button
            if (vehicleSetup.HasChassis || vehicleSetup.HasAllWheels)
            {
                DrawBulkAssignmentButtons(vehicleSetup);
            }
            
            DrawDivider();
            
            // Chassis assignment
            DrawChassisAssignment(vehicleSetup);
            
            EditorGUILayout.Space();
            
            // Wheel assignments
            DrawWheelAssignments(numAxles, vehicleSetup);
        }
        
        private void DrawPrefabScannerInfo()
        {
            var counts = Context.PrefabScanner.GetPrefabCountsByType();
            int totalFound = Context.PrefabScanner.GetTotalPrefabsFound();
            
            if (totalFound > 0)
            {
                EditorGUILayout.BeginVertical("box");
                EditorGUILayout.LabelField($"Found {totalFound} vehicle prefabs:", EditorStyles.boldLabel);
                foreach (var kvp in counts)
                {
                    EditorGUILayout.LabelField($"  • {kvp.Key}: {kvp.Value} prefab(s)", EditorStyles.miniLabel);
                }
                EditorGUILayout.EndVertical();
            }
            else
            {
                EditorGUILayout.HelpBox("No vehicle prefabs found. Prefabs can be manually assigned below.", UnityEditor.MessageType.Warning);
            }
        }
        
        private void DrawSetupStatus(VehiclePrefabScanner.VehicleSetup vehicleSetup)
        {
            EditorGUILayout.BeginHorizontal("box");
            EditorGUILayout.LabelField("Auto-detected Setup:", EditorStyles.boldLabel, GUILayout.Width(150));
            if (vehicleSetup.IsComplete)
            {
                EditorGUILayout.LabelField("✓ Complete", EditorStyles.boldLabel);
            }
            else
            {
                if (!vehicleSetup.HasChassis)
                    EditorGUILayout.LabelField("⚠ Chassis not found", EditorStyles.miniLabel);
                if (!vehicleSetup.HasAllWheels)
                    EditorGUILayout.LabelField("⚠ Some wheels not found", EditorStyles.miniLabel);
            }
            EditorGUILayout.EndHorizontal();
        }
        
        private void DrawBulkAssignmentButtons(VehiclePrefabScanner.VehicleSetup vehicleSetup)
        {
            EditorGUILayout.BeginHorizontal();
            
            if (GUILayout.Button($"Auto-Assign Complete {Context.CurrentVehicleType.ToUpper()} Setup", GUILayout.Height(35)))
            {
                EditorApplication.delayCall += () => ApplyCompleteVehicleSetup(vehicleSetup);
            }
            
            if (GUILayout.Button("Refresh Prefabs", GUILayout.Width(120), GUILayout.Height(35)))
            {
                EditorApplication.delayCall += () => {
                    Context.PrefabScanner.ScanForPrefabs();
                    EditorUtility.DisplayDialog("Refresh Complete", "Prefab cache has been rebuilt.", "OK");
                };
            }
            
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
        }
        
        private void DrawChassisAssignment(VehiclePrefabScanner.VehicleSetup vehicleSetup)
        {
            EditorGUILayout.LabelField("Chassis Assignment", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("box");
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Select Chassis:", GUILayout.Width(100));
            
            // Show auto-detected suggestion
            if (vehicleSetup.Chassis != null)
            {
                EditorGUILayout.LabelField($"[Auto: {vehicleSetup.Chassis.name}]", EditorStyles.miniLabel, GUILayout.Width(150));
            }
            
            // Use user selection if set, otherwise fall back to auto-detected
            if (userSelectedChassis == null)
            {
                userSelectedChassis = vehicleSetup.Chassis;
            }
            
            // Editable ObjectField for user selection
            GameObject newSelection = (GameObject)EditorGUILayout.ObjectField(
                userSelectedChassis, 
                typeof(GameObject), 
                false);
            
            if (newSelection != userSelectedChassis)
            {
                userSelectedChassis = newSelection;
            }
            
            if (userSelectedChassis != null && GUILayout.Button("Assign", GUILayout.Width(60)))
            {
                GameObject chassisToAssign = userSelectedChassis;
                EditorApplication.delayCall += () => {
                    VehicleSetupUtility.AssignChassisPrefab(Context.Vehicle, chassisToAssign);
                    EditorUtility.SetDirty(Context.Vehicle);
                };
            }
            else
            {
                EditorGUILayout.LabelField("(Chassis prefab not found)", EditorStyles.miniLabel);
            }
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.Space(3);
            EditorGUILayout.LabelField("Note: Chassis will be instantiated as child of this vehicle at runtime", EditorStyles.wordWrappedMiniLabel);
            
            EditorGUILayout.EndVertical();
        }
        
        private void DrawWheelAssignments(int numAxles, VehiclePrefabScanner.VehicleSetup vehicleSetup)
        {
            EditorGUILayout.LabelField("Wheel Assignments", EditorStyles.boldLabel);
            
            if (numAxles == 0)
            {
                EditorGUILayout.HelpBox("Load a vehicle JSON first to configure wheels.", UnityEditor.MessageType.Info);
                return;
            }
            
            for (int axleIdx = 0; axleIdx < numAxles; axleIdx++)
            {
                DrawAxleWheelAssignment(axleIdx, vehicleSetup);
            }
        }
        
        private void DrawAxleWheelAssignment(int axleIdx, VehiclePrefabScanner.VehicleSetup vehicleSetup)
        {
            EditorGUILayout.BeginVertical("box");
            
            // Header with axle info
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"Axle {axleIdx}", EditorStyles.boldLabel);
            
            var axle = Context.Vehicle.axleData[axleIdx];
            if (axle.visualGameobjects == null)
                axle.visualGameobjects = new List<GameObject>();
            
            // Ensure we have at least 2 wheels (left/right)
            while (axle.visualGameobjects.Count < 2)
                axle.visualGameobjects.Add(null);
            
            // Quick assign for this axle
            if (axleIdx < vehicleSetup.Wheels.Count)
            {
                var wheelPair = vehicleSetup.Wheels[axleIdx];
                if (wheelPair.Left != null && wheelPair.Right != null)
                {
                    if (GUILayout.Button("Auto-assign Axle", GUILayout.Width(120)))
                    {
                        int capturedIdx = axleIdx;
                        EditorApplication.delayCall += () => {
                            axle.visualGameobjects[0] = wheelPair.Left;
                            axle.visualGameobjects[1] = wheelPair.Right;
                            EditorUtility.SetDirty(Context.Vehicle);
                        };
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.Space(3);
            
            // Left wheel
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Left:", GUILayout.Width(50));
            GameObject autoLeft = axleIdx < vehicleSetup.Wheels.Count ? vehicleSetup.Wheels[axleIdx].Left : null;
            
            if (autoLeft != null)
            {
                EditorGUILayout.LabelField($"[Auto: {autoLeft.name}]", EditorStyles.miniLabel, GUILayout.Width(150));
            }
            
            GameObject newLeft = (GameObject)EditorGUILayout.ObjectField(axle.visualGameobjects[0], typeof(GameObject), false);
            if (newLeft != axle.visualGameobjects[0])
            {
                axle.visualGameobjects[0] = newLeft;
                EditorUtility.SetDirty(Context.Vehicle);
            }
            EditorGUILayout.EndHorizontal();
            
            // Right wheel
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Right:", GUILayout.Width(50));
            GameObject autoRight = axleIdx < vehicleSetup.Wheels.Count ? vehicleSetup.Wheels[axleIdx].Right : null;
            
            if (autoRight != null)
            {
                EditorGUILayout.LabelField($"[Auto: {autoRight.name}]", EditorStyles.miniLabel, GUILayout.Width(150));
            }
            
            GameObject newRight = (GameObject)EditorGUILayout.ObjectField(axle.visualGameobjects[1], typeof(GameObject), false);
            if (newRight != axle.visualGameobjects[1])
            {
                axle.visualGameobjects[1] = newRight;
                EditorUtility.SetDirty(Context.Vehicle);
            }
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(5);
        }
        
        private void ApplyCompleteVehicleSetup(VehiclePrefabScanner.VehicleSetup vehicleSetup)
        {
            // Ensure vehicle JSON is loaded AND parsed to get correct axle count
            if (!Context.JsonState.VehicleLoaded || Context.ParsedVehicleData == null || Context.ParsedVehicleData.Axles == null)
            {
                if (!string.IsNullOrEmpty(Context.Vehicle.topLevelVehicleJSON))
                {
                    LoadAndParseVehicleData();
                }
            }
            
            // Get axle count from PARSED JSON data (not from old axleData!)
            int numAxles = Context.ParsedVehicleData?.Axles?.Count ?? 0;
            if (numAxles == 0)
            {
                EditorUtility.DisplayDialog("Error", 
                    "Cannot apply vehicle setup: No axle data found in vehicle JSON.\n\n" +
                    "Please ensure a valid vehicle JSON file is loaded first.", "OK");
                return;
            }
            
            Debug.Log($"ApplyCompleteVehicleSetup: Creating {numAxles} axles from JSON");
            
            // Clean up ALL existing axles, wheels, and chassis before creating new ones
            CleanupExistingComponents();
            
            // Ensure axleData list matches the parsed axle count
            if (Context.Vehicle.axleData == null)
                Context.Vehicle.axleData = new List<WheelGameobjects>();
            
            Context.Vehicle.axleData.Clear();
            for (int i = 0; i < numAxles; i++)
            {
                Context.Vehicle.axleData.Add(new WheelGameobjects { visualGameobjects = new List<GameObject>() });
            }
            
            int assignedCount = 0;
            
            // Assign chassis (at runtime, Chrono positions it at the COM)
            // The chassis visual mesh offset is handled internally by Chrono - but the user might need to adjust
            // if local COM/REF position is different from the visual mesh origin
            if (vehicleSetup.HasChassis)
            {
                VehicleSetupUtility.AssignChassisPrefab(Context.Vehicle, vehicleSetup.Chassis);
                assignedCount++;
            }
            
            // Assign all wheels with proper positioning - use axle count from JSON
            // Note: Suspension locations in JSON are relative to chassis COM (and only for estimate not in runtime)
            for (int axleIdx = 0; axleIdx < numAxles; axleIdx++)
            {
                var axle = Context.Vehicle.axleData[axleIdx];
                if (axle.visualGameobjects == null)
                    axle.visualGameobjects = new List<GameObject>();
                
                while (axle.visualGameobjects.Count < 2)
                    axle.visualGameobjects.Add(null);
                
                // Check if we have wheel prefabs for this axle
                if (axleIdx >= vehicleSetup.Wheels.Count)
                {
                    Debug.LogWarning($"No wheel prefabs found for axle {axleIdx}. Skipping.");
                    continue;
                }
                
                var wheelPair = vehicleSetup.Wheels[axleIdx];
                
                // Get axle position and spacing from parsed data
                Vector3 axlePosition = Vector3.zero;
                float halfTrack = 1.0f;
                
                if (Context.ParsedVehicleData != null && Context.ParsedVehicleData.Axles != null && axleIdx < Context.ParsedVehicleData.Axles.Count)
                {
                    var axleData = Context.ParsedVehicleData.Axles[axleIdx];
                    // Convert from Chrono coords - positions in JSON are relative to chassis COM
                    axlePosition = Utils.FromChronoFlip(new ChVector3d(
                        axleData.SuspensionLocation.x, axleData.SuspensionLocation.y, axleData.SuspensionLocation.z
                    ));
                    
                    if (!string.IsNullOrEmpty(axleData.Suspension))
                    {
                        halfTrack = VehicleSetupUtility.GetSuspensionHalfTrack(Context.BuilderCore, axleData.Suspension);
                    }
                }
                
                // Create axle container
                GameObject axleGO = new GameObject($"Axle_{axleIdx}");
                axleGO.transform.SetParent(Context.Vehicle.transform);
                axleGO.transform.localPosition = axlePosition;
                Transform axleContainer = axleGO.transform;
                
                // Create axle visualization
                VehicleSetupUtility.CreateAxleVisualization(axleContainer, halfTrack);
                
                // Assign wheels
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
            
            // Important - sync per-axle tire list to match the axle count
            SyncPerAxleTireList(numAxles, forceReset: true);
            
            EditorUtility.SetDirty(Context.Vehicle);
            EditorUtility.DisplayDialog("Complete Setup Applied", 
                $"Successfully assigned {assignedCount} component(s) for {Context.CurrentVehicleType.ToUpper()} vehicle:\n" +
                $"• Chassis: {(vehicleSetup.HasChassis ? "✓" : "✗")}\n" +
                $"• Wheels: {(vehicleSetup.HasAllWheels ? "✓ All axles" : "⚠ Partial")}", "OK");
        }
        
        private void CleanupExistingComponents()
        {
            // Find and destroy ALL existing Axle_N GameObjects and Chassis
            // this ensures we dont end up with multiple chassis objects or left over axles when changing from 8wd to 4wd
            List<Transform> toDestroy = new List<Transform>();
            foreach (Transform child in Context.Vehicle.transform)
            {
                if (child.name.StartsWith("Axle_") || child.name == "Chassis")
                {
                    toDestroy.Add(child);
                }
            }
            
            foreach (Transform obj in toDestroy)
            {
                Debug.Log($"Destroying existing component: {obj.name}");
                Object.DestroyImmediate(obj.gameObject);
            }
            
            // Clear the axleData references (they'll be repopulated)
            if (Context.Vehicle.axleData != null)
            {
                foreach (var axle in Context.Vehicle.axleData)
                {
                    if (axle.visualGameobjects != null)
                    {
                        axle.visualGameobjects.Clear();
                    }
                }
            }
            
            Debug.Log($"Cleaned up {toDestroy.Count} existing components (axles and chassis)");
        }
        
        private void LoadAndParseVehicleData()
        {
            try
            {
                var data = Context.BuilderCore.LoadJson(Context.JsonState.VehiclePath);
                if (data == null)
                {
                    Debug.LogError($"Failed to load vehicle JSON from: {Context.JsonState.VehiclePath}");
                    return;
                }
                
                // Load the JSON data into state
                Context.JsonState.LoadVehicle(data, null);
                
                // Parse axles from JSON
                if (data["Axles"] is Newtonsoft.Json.Linq.JArray axlesArray)
                {
                    int axleCount = axlesArray.Count;
                    
                    // Create ParsedVehicleData structure
                    if (Context.ParsedVehicleData == null)
                    {
                        Context.ParsedVehicleData = new VehicleDataModel.VehicleData
                        {
                            Axles = new System.Collections.Generic.List<VehicleDataModel.AxleEntry>()
                        };
                    }
                    
                    // Parse each axle
                    Context.ParsedVehicleData.Axles = new System.Collections.Generic.List<VehicleDataModel.AxleEntry>();
                    foreach (var axToken in axlesArray)
                    {
                        if (axToken is Newtonsoft.Json.Linq.JObject axObj)
                        {
                            var axle = new VehicleDataModel.AxleEntry
                            {
                                Suspension = axObj["Suspension Input File"]?.ToString(),
                                SuspensionLocation = ParseVector3(axObj["Suspension Location"]),
                                SteeringIndex = axObj["Steering Index"]?.ToObject<int>() ?? 0,
                                LeftWheel = axObj["Left Wheel Input File"]?.ToString(),
                                RightWheel = axObj["Right Wheel Input File"]?.ToString()
                            };
                            Context.ParsedVehicleData.Axles.Add(axle);
                        }
                    }
                    
                    // Sync vehicle.axleData count with parsed axles
                    if (Context.Vehicle.axleData == null)
                        Context.Vehicle.axleData = new List<WheelGameobjects>();
                    
                    // IMPORTANT: Clear old axle data and create new entries matching JSON
                    Context.Vehicle.axleData.Clear();
                    for (int i = 0; i < axleCount; i++)
                    {
                        Context.Vehicle.axleData.Add(new WheelGameobjects { visualGameobjects = new List<GameObject>() });
                    }
                    
                    Debug.Log($"LoadAndParseVehicleData: Parsed {axleCount} axles from JSON");
                }
                else
                {
                    Debug.LogError("No 'Axles' array found in vehicle JSON!");
                }
                
                EditorUtility.SetDirty(Context.Vehicle);
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Failed to parse vehicle data: {ex.Message}\n{ex.StackTrace}");
            }
        }
        
        private Vector3 ParseVector3(Newtonsoft.Json.Linq.JToken token)
        {
            if (token is Newtonsoft.Json.Linq.JArray arr && arr.Count >= 3)
            {
                return new Vector3(
                    arr[0]?.ToObject<float>() ?? 0f,
                    arr[1]?.ToObject<float>() ?? 0f,
                    arr[2]?.ToObject<float>() ?? 0f
                );
            }
            return Vector3.zero;
        }
        
        private void SyncPerAxleTireList(int axleCount, bool forceReset)
        {
            Context.Vehicle.RefreshTireOverridesFromInspector(forceReset);
            if (forceReset)
            {
                Context.TryAssignFrontRearTireDefaults(axleCount);
            }
        }
    }
}
