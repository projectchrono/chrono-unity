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
// Overview tab - provides summary, vehicle type selection, and other main
// vehicel settings
// =============================================================================

using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace VehicleBuilder.Editor
{

    public class OverviewTab : InspectorTab
    {
        public OverviewTab(VehicleInspectorContext context) : base(context)
        {
        }
        
        public override void DrawTab()
        {
            Context.EnsureVehicleJsonLoaded();
            DrawSectionHeader("Vehicle Overview");
            
            // JSON file selectors
            DrawJsonFileSelectors();
            
            DrawDivider();
            
            // Basic vehicle settings
            DrawVehicleSettings();
        }
        
        private void DrawJsonFileSelectors()
        {
            DrawSectionHeader("JSON Configuration Files");
            EditorGUILayout.BeginVertical("box");
            
            DrawQuickJsonSelector("Vehicle:", Context.Vehicle.topLevelVehicleJSON, Context.VehicleJsonFiles, (newPath) => {
                Context.Vehicle.topLevelVehicleJSON = newPath;
                Context.JsonState.SetVehiclePath(newPath);
                EditorUtility.SetDirty(Context.Vehicle);
                
                // Trigger prefab regeneration when vehicle JSON changes
                Context.OnVehicleJsonChanged?.Invoke();
            });
            
            DrawQuickJsonSelector("Engine:", Context.Vehicle.engineJSON, Context.EngineJsonFiles, (newPath) => {
                Context.Vehicle.engineJSON = newPath;
                Context.JsonState.SetEnginePath(newPath);
                Context.JsonState.LoadEngine(null);  // Invalidate cached engine data
                EditorUtility.SetDirty(Context.Vehicle);
            });
            
            DrawQuickJsonSelector("Transmission:", Context.Vehicle.transJSON, Context.TransmissionJsonFiles, (newPath) => {
                Context.Vehicle.transJSON = newPath;
                Context.JsonState.SetTransmissionPath(newPath);
                Context.JsonState.LoadTransmission(null);  // Invalidate cached transmission data
                EditorUtility.SetDirty(Context.Vehicle);
            });
            
            // Tire selector
            bool newUseSingleTireFile = EditorGUILayout.Toggle("Single Tire for All:", Context.Vehicle.useSingleTireFile);
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
            
            if (Context.Vehicle.useSingleTireFile)
            {
                DrawQuickJsonSelector("Tire:", Context.Vehicle.tireJSON, Context.TireJsonFiles, (newPath) => {
                    Context.Vehicle.tireJSON = newPath;
                    Context.JsonState.SetTirePath(newPath);
                    Context.JsonState.LoadTire(null);  // Invalidate cached tire data
                    EditorUtility.SetDirty(Context.Vehicle);
                });
            }
            else
            {
                DrawPerAxleTireOverviewSection();
            }
            
            EditorGUILayout.EndVertical();
        }
        
        private void DrawAxleConfiguration()
        {
            DrawSectionHeader("Axle Configuration");
            
            var vehicleData = Context.JsonState.VehicleData as JObject;
            var axlesArray = vehicleData?["Axles"] as JArray;
            int axleCount = axlesArray?.Count ?? (Context.Vehicle.axleData?.Count ?? 0);
            EditorGUILayout.LabelField($"Number of Axles: {axleCount}", EditorStyles.boldLabel);
            
            if (axleCount > 0)
            {
                int totalWheels = CountTotalWheels();
                EditorGUILayout.LabelField($"Total Wheels: {totalWheels}");
            }
            else
            {
                EditorGUILayout.HelpBox("Load a vehicle JSON to configure axles.", UnityEditor.MessageType.Info);
            }
        }
        
        private void AutoSetDefaultJsonFiles()
        {
            // Invalidate cache before setting new paths
            Context.InvalidateJsonCache();
            
            // Set first file from each category when vehicle type changes
            if (Context.VehicleJsonFiles.Count > 0)
            {
                Context.Vehicle.topLevelVehicleJSON = Context.VehicleJsonFiles[0];
                Context.JsonState.SetVehiclePath(Context.VehicleJsonFiles[0]);
            }
            
            if (Context.EngineJsonFiles.Count > 0)
            {
                Context.Vehicle.engineJSON = Context.EngineJsonFiles[0];
                Context.JsonState.SetEnginePath(Context.EngineJsonFiles[0]);
            }
            
            if (Context.TransmissionJsonFiles.Count > 0)
            {
                Context.Vehicle.transJSON = Context.TransmissionJsonFiles[0];
                Context.JsonState.SetTransmissionPath(Context.TransmissionJsonFiles[0]);
            }
            
            if (Context.TireJsonFiles.Count > 0)
            {
                Context.Vehicle.tireJSON = Context.TireJsonFiles[0];
                Context.JsonState.SetTirePath(Context.TireJsonFiles[0]);
            }
            
            Debug.Log($"Auto-set default JSON files for {Context.CurrentVehicleType}");
        }

        // use this for applying tire jsons to the wheels/gameobjects etc
        private int CountTotalWheels()
        {
            int count = 0;
            if (Context.Vehicle.axleData != null)
            {
                foreach (var axle in Context.Vehicle.axleData)
                {
                    if (axle.visualGameobjects != null)
                        count += axle.visualGameobjects.Count;
                }
            }
            return count;
        }
        
        private void DrawVehicleSettings()
        {
            DrawSectionHeader("Vehicle Settings");
            EditorGUILayout.BeginVertical("box");
            
            // Tire Step Size
            EditorGUI.BeginChangeCheck();
            float newTireStepSize = EditorGUILayout.FloatField("Tire Step Size", Context.Vehicle.tireStepSize);
            if (EditorGUI.EndChangeCheck())
            {
                Context.Vehicle.tireStepSize = newTireStepSize;
                EditorUtility.SetDirty(Context.Vehicle);
            }
            
            // Tire Collision Type
            EditorGUI.BeginChangeCheck();
            var newCollisionType = (ChTire.CollisionType)EditorGUILayout.EnumPopup("Tire Collision Type", Context.Vehicle.tireCollisionType);
            if (EditorGUI.EndChangeCheck())
            {
                Context.Vehicle.tireCollisionType = newCollisionType;
                EditorUtility.SetDirty(Context.Vehicle);
            }
            
            // Brake Locking
            EditorGUI.BeginChangeCheck();
            bool newBrakeLocking = EditorGUILayout.Toggle("Enable Brake Locking", Context.Vehicle.brakeLocking);
            if (EditorGUI.EndChangeCheck())
            {
                Context.Vehicle.brakeLocking = newBrakeLocking;
                EditorUtility.SetDirty(Context.Vehicle);
            }
            
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("Initial Conditions", EditorStyles.boldLabel);
            
            // Initial Forward Velocity
            EditorGUI.BeginChangeCheck();
            double newInitFwdVel = EditorGUILayout.DoubleField("Init Forward Velocity", Context.Vehicle.initForwardVel);
            if (EditorGUI.EndChangeCheck())
            {
                Context.Vehicle.initForwardVel = newInitFwdVel;
                EditorUtility.SetDirty(Context.Vehicle);
            }
            
            // Initial Wheel Angular Velocity
            EditorGUI.BeginChangeCheck();
            double newInitWheelAngVel = EditorGUILayout.DoubleField("Init Wheel Angular Vel", Context.Vehicle.initWheelAngVel);
            if (EditorGUI.EndChangeCheck())
            {
                Context.Vehicle.initWheelAngVel = newInitWheelAngVel;
                EditorUtility.SetDirty(Context.Vehicle);
            }
            
            EditorGUILayout.EndVertical();
        }

        private void DrawPerAxleTireOverviewSection()
        {
            int axleCount = Context.Vehicle.axleData?.Count ?? 0;
            if (axleCount == 0)
            {
                EditorGUILayout.HelpBox("No axles detected. Load a vehicle JSON to configure per-axle tires.", UnityEditor.MessageType.Info);
                return;
            }

            Context.Vehicle.RefreshTireOverridesFromInspector(forceReset: false);

            for (int i = 0; i < axleCount; i++)
            {
                int axleIndex = i;
                DrawQuickJsonSelector($"Axle {i} Tire:",
                    Context.Vehicle.perAxleTireSpec.Count > axleIndex ? Context.Vehicle.perAxleTireSpec[axleIndex] : string.Empty,
                    Context.TireJsonFiles,
                    (newPath) => {
                        EnsurePerAxleTireList(axleIndex + 1);
                        Context.Vehicle.perAxleTireSpec[axleIndex] = newPath;
                        Context.JsonState.SetTirePath(newPath);
                        Context.JsonState.LoadTire(null);
                        EditorUtility.SetDirty(Context.Vehicle);
                    });
            }
        }

        private void EnsurePerAxleTireList(int requiredEntries)
        {
            while (Context.Vehicle.perAxleTireSpec.Count < requiredEntries)
            {
                Context.Vehicle.perAxleTireSpec.Add(Context.Vehicle.tireJSON);
            }
        }
        
        private void DrawQuickJsonSelector(string label, string currentPath, List<string> files, System.Action<string> onChanged)
        {
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
    }
}
