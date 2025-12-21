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
// Non-static JSON parsing and drawing helpers for vehicle builder
// =============================================================================

#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;

namespace ChronoVehicleBuilder
{

    public class VehicleJSONParser
    {
        private VehicleBuilderCore builderCore;
        private VehicleMapEditor mapEditor;
    private readonly Dictionary<string, bool> foldoutStates = new Dictionary<string, bool>();

        public class MapInfo
        {
            public string path;
            public string label;
            public JArray data;
        }

        public VehicleJSONParser(VehicleBuilderCore core, VehicleMapEditor mapEdit)
        {
            builderCore = core;
            mapEditor = mapEdit;
        }

        // Auto-detect all maps in a JSON object - ALWAYS SCAN FRESH
        public List<MapInfo> DetectAllMaps(JObject root, string jsonKey = "")
        {
            if (root == null)
                return new List<MapInfo>();

            var maps = new List<MapInfo>();
            
            var sw = Stopwatch.StartNew();
            try
            {
                // ALWAYS scan fresh - no caching to avoid stale reference issues
                ScanForMaps(root, "", maps, 0);
            }
            catch (System.Exception ex)
            {
                UnityEngine.Debug.LogError($"Error scanning for maps in {jsonKey}: {ex.Message}\n{ex.StackTrace}");
                return new List<MapInfo>();
            }
            finally
            {
                sw.Stop();
                if (sw.ElapsedMilliseconds > 50)
                {
                    UnityEngine.Debug.LogWarning($"DetectAllMaps[{jsonKey}] took {sw.ElapsedMilliseconds} ms");
                }
            }

            return maps;
        }

        private void ScanForMaps(JToken token, string path, List<MapInfo> maps, int depth)
        {
            // Prevent infinite recursion or stack overflow
            const int MAX_DEPTH = 50;
                if (depth > MAX_DEPTH)
                {
                    UnityEngine.Debug.LogWarning($"Max recursion depth {MAX_DEPTH} reached at path: {path}");
                return;
            }

            // Guard against null token
            if (token == null)
                return;

            try
            {
                if (token is JObject obj)
                {
                    // Prevent hanging on corrupt objects
                    int propCount = 0;
                    const int MAX_PROPERTIES = 1000;
                    
                    foreach (var kvp in obj)
                    {
                        if (++propCount > MAX_PROPERTIES)
                        {
                            UnityEngine.Debug.LogWarning($"Too many properties ({MAX_PROPERTIES}) at path: {path}");
                            break;
                        }

                        if (kvp.Value == null)
                            continue;

                        string newPath = string.IsNullOrEmpty(path) ? kvp.Key : $"{path}.{kvp.Key}";
                        ScanForMaps(kvp.Value, newPath, maps, depth + 1);
                    }
                }
                else if (token is JArray arr)
                {
                    // Prevent hanging on huge arrays
                    const int MAX_ARRAY_SIZE = 10000;
                    if (arr.Count > MAX_ARRAY_SIZE)
                    {
                        UnityEngine.Debug.LogWarning($"Array too large ({arr.Count}) at path: {path}");
                        return;
                    }

                    // Check if this is a map (array of [x,y] pairs)
                    // BUT exclude simple transmission shift point arrays (these are gear ranges, not plots)
                    // don't currenlty plot multi-series. that's a bit of overkill for just a few gear shift points
                    bool isShiftPointMap = path.Contains("Shift Points Map RPM") || path.EndsWith("Shift Points");
                    
                    if (IsMapArray(arr) && !isShiftPointMap)
                    {
                        maps.Add(new MapInfo
                        {
                            path = path,
                            label = GetFriendlyMapName(path),
                            data = arr
                        });
                        return;  // Don't recurse into map arrays
                    }
                    else
                    {
                        // Check nested arrays
                        for (int i = 0; i < arr.Count; i++)
                        {
                            if (arr[i] == null)
                                continue;
                                
                            ScanForMaps(arr[i], $"{path}[{i}]", maps, depth + 1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogError($"Error scanning token at path '{path}': {ex.Message}");
                // Continue scanning other paths even if one fails
            }
        }

        private bool IsMapArray(JArray arr)
        {
            // CRITICAL: Guard against null or empty array
            if (arr == null || arr.Count == 0) 
                return false;
            
            // Arrays with only 1 element can't be a map
            if (arr.Count == 1)
                return false;
            
            try
            {
                // Check if all elements are [x,y] pairs - sample first 3
                int samplesToCheck = Mathf.Min(3, arr.Count);
                
                for (int i = 0; i < samplesToCheck; i++)
                {
                    // Must be an array
                    if (!(arr[i] is JArray subArr))
                        return false;
                    
                    // Must have exactly 2 elements
                    if (subArr.Count != 2)
                        return false;
                    
                    // Both elements must be numeric
                    JTokenType type0 = subArr[0]?.Type ?? JTokenType.None;
                    JTokenType type1 = subArr[1]?.Type ?? JTokenType.None;
                    
                    if (type0 != JTokenType.Float && type0 != JTokenType.Integer)
                        return false;
                    if (type1 != JTokenType.Float && type1 != JTokenType.Integer)
                        return false;
                }
                
                return true;
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogWarning($"Error checking if array is map: {ex.Message}");
                return false;
            }
        }

        private string GetFriendlyMapName(string path)
        {
            string[] parts = path.Split('.');
            return parts[parts.Length - 1];
        }

        // Draw detected maps in a compact, organized way
        public void DrawDetectedMaps(JObject root, string jsonKey = "", List<MapInfo> precomputedMaps = null)
        {
            // CRITICAL: Guard against null root during JSON transitions
            if (root == null)
            {
                EditorGUILayout.HelpBox("JSON data not loaded.", UnityEditor.MessageType.Info);
                return;
            }

            // Lazy validation ensures maps are fresh for current root
            List<MapInfo> maps = precomputedMaps ?? DetectAllMaps(root, jsonKey);
            
            if (maps == null || maps.Count == 0)
            {
                EditorGUILayout.HelpBox("No maps/curves detected in this JSON.", UnityEditor.MessageType.Info);
                return;
            }

            EditorGUILayout.LabelField($"Detected {maps.Count} Map(s)/Curve(s)", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            foreach (var map in maps)
            {
                // Skip maps with null data (shouldn't happen with lazy validation)
                if (map?.data == null)
                    continue;
                    
                DrawCompactMap(map, jsonKey);
                EditorGUILayout.Space(5);
            }
        }

        private void DrawCompactMap(MapInfo map, string jsonKey)
        {
            // Always show graphs expanded - no foldout
            EditorGUILayout.Space(10);
            
            // Draw the interactive graph directly
            mapEditor.DrawGraph(map.data, map.label);
            
            EditorGUILayout.Space(5);
        }

        public void DrawJsonObjectEditor(JObject root, List<MapInfo> mapInfos, IEnumerable<string> additionalSkipPaths = null)
        {
            if (root == null)
            {
                EditorGUILayout.HelpBox("JSON data not loaded.", UnityEditor.MessageType.Info);
                return;
            }

            HashSet<string> skipPaths = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            if (mapInfos != null)
            {
                foreach (var map in mapInfos)
                {
                    if (!string.IsNullOrEmpty(map?.path))
                        skipPaths.Add(map.path);
                }
            }

            if (additionalSkipPaths != null)
            {
                foreach (var path in additionalSkipPaths)
                {
                    if (!string.IsNullOrEmpty(path))
                        skipPaths.Add(path);
                }
            }

            EditorGUILayout.LabelField("Parameters", EditorStyles.boldLabel);
            EditorGUILayout.Space(3f);

            EditorGUILayout.BeginVertical("box");
            foreach (var property in root.Properties())
            {
                string path = property.Name;
                if (skipPaths.Contains(path))
                    continue;

                DrawEditableToken(property.Value, path, property.Name, skipPaths);
            }
            EditorGUILayout.EndVertical();
        }

        private void DrawEditableToken(JToken token, string path, string label, HashSet<string> skipPaths)
        {
            if (token == null)
                return;

            switch (token.Type)
            {
                case JTokenType.Object:
                    DrawEditableObject(token as JObject, path, label, skipPaths);
                    break;
                case JTokenType.Array:
                    DrawEditableArray(token as JArray, path, label, skipPaths);
                    break;
                case JTokenType.Integer:
                case JTokenType.Float:
                case JTokenType.Boolean:
                case JTokenType.String:
                case JTokenType.Null:
                    DrawEditableValue(token as JValue, label);
                    break;
                default:
                    EditorGUILayout.LabelField(label, token.ToString());
                    break;
            }
        }

        private void DrawEditableObject(JObject obj, string path, string label, HashSet<string> skipPaths)
        {
            if (obj == null)
                return;

            EditorGUILayout.BeginVertical("box");
            bool expanded = GetFoldoutState(path, label, true);
            if (expanded)
            {
                EditorGUI.indentLevel++;
                foreach (var property in obj.Properties())
                {
                    string childPath = string.IsNullOrEmpty(path) ? property.Name : $"{path}.{property.Name}";
                    if (skipPaths.Contains(childPath))
                        continue;

                    DrawEditableToken(property.Value, childPath, property.Name, skipPaths);
                }
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndVertical();
        }

        private void DrawEditableArray(JArray array, string path, string label, HashSet<string> skipPaths)
        {
            if (array == null || skipPaths.Contains(path))
                return;

            EditorGUILayout.BeginVertical("box");
            string header = $"{label} [{array.Count}]";
            bool expanded = GetFoldoutState(path, header, array.Count <= 6);
            if (expanded)
            {
                EditorGUI.indentLevel++;
                JToken sample = array.FirstOrDefault(t => t != null && t.Type != JTokenType.Null);
                if (sample == null)
                {
                    EditorGUILayout.HelpBox("Array is empty. Choose a value type to add.", UnityEditor.MessageType.Info);
                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("Add Number", GUILayout.Width(110f)))
                    {
                        array.Add(0.0);
                        GUI.changed = true;
                    }
                    if (GUILayout.Button("Add Integer", GUILayout.Width(110f)))
                    {
                        array.Add(0);
                        GUI.changed = true;
                    }
                    if (GUILayout.Button("Add Bool", GUILayout.Width(110f)))
                    {
                        array.Add(false);
                        GUI.changed = true;
                    }
                    if (GUILayout.Button("Add String", GUILayout.Width(110f)))
                    {
                        array.Add(string.Empty);
                        GUI.changed = true;
                    }
                    if (GUILayout.Button("Add Object", GUILayout.Width(110f)))
                    {
                        array.Add(new JObject());
                        GUI.changed = true;
                    }
                    EditorGUILayout.EndHorizontal();
                }
                else
                {
                    switch (sample.Type)
                    {
                        case JTokenType.Integer:
                            DrawNumericArray(array, true);
                            break;
                        case JTokenType.Float:
                            DrawNumericArray(array, false);
                            break;
                        case JTokenType.Boolean:
                            DrawBoolArray(array);
                            break;
                        case JTokenType.String:
                            DrawStringArray(array);
                            break;
                        case JTokenType.Object:
                            for (int i = 0; i < array.Count; i++)
                            {
                                string childPath = $"{path}[{i}]";
                                DrawEditableObject(array[i] as JObject, childPath, $"{label} [{i}]", skipPaths);
                            }
                            break;
                        case JTokenType.Array:
                            for (int i = 0; i < array.Count; i++)
                            {
                                string childPath = $"{path}[{i}]";
                                DrawEditableArray(array[i] as JArray, childPath, $"{label} [{i}]", skipPaths);
                            }
                            break;
                        default:
                            for (int i = 0; i < array.Count; i++)
                            {
                                EditorGUILayout.LabelField($"[{i}]", array[i]?.ToString() ?? "null");
                            }
                            break;
                    }
                }
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndVertical();
        }

        private void DrawNumericArray(JArray array, bool isInteger)
        {
            if (array == null)
                return;

            for (int i = 0; i < array.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                JToken element = array[i];
                if (isInteger)
                {
                    long value = element != null && element.Type != JTokenType.Null ? element.Value<long>() : 0L;
                    long newValue = EditorGUILayout.LongField($"[{i}]", value);
                    if (newValue != value)
                    {
                        array[i] = newValue;
                        GUI.changed = true;
                    }
                }
                else
                {
                    double value = element != null && element.Type != JTokenType.Null ? element.Value<double>() : 0.0;
                    double newValue = EditorGUILayout.DoubleField($"[{i}]", value);
                    if (Math.Abs(newValue - value) > 1e-9)
                    {
                        array[i] = newValue;
                        GUI.changed = true;
                    }
                }
                bool remove = GUILayout.Button("-", GUILayout.Width(24f));
                EditorGUILayout.EndHorizontal();
                if (remove)
                {
                    array.RemoveAt(i);
                    GUI.changed = true;
                    i--;
                }
            }

            if (GUILayout.Button("Add Entry", GUILayout.Width(120f)))
            {
                array.Add(isInteger ? (JToken)0 : 0.0);
                GUI.changed = true;
            }
        }

        private void DrawBoolArray(JArray array)
        {
            if (array == null)
                return;

            for (int i = 0; i < array.Count; i++)
            {
                JToken element = array[i];
                bool value = element != null && element.Type != JTokenType.Null && element.Value<bool>();
                bool newValue = EditorGUILayout.Toggle($"[{i}]", value);
                if (newValue != value)
                {
                    array[i] = newValue;
                    GUI.changed = true;
                }
            }

            if (GUILayout.Button("Add Entry", GUILayout.Width(120f)))
            {
                array.Add(false);
                GUI.changed = true;
            }
        }

        private void DrawStringArray(JArray array)
        {
            if (array == null)
                return;

            for (int i = 0; i < array.Count; i++)
            {
                string value = array[i]?.Value<string>() ?? string.Empty;
                string newValue = EditorGUILayout.TextField($"[{i}]", value);
                if (!string.Equals(newValue, value, StringComparison.Ordinal))
                {
                    array[i] = newValue;
                    GUI.changed = true;
                }
            }

            if (GUILayout.Button("Add Entry", GUILayout.Width(120f)))
            {
                array.Add(string.Empty);
                GUI.changed = true;
            }
        }

        private void DrawEditableValue(JValue value, string label)
        {
            if (value == null)
                return;

            float inspectorWidth = EditorGUIUtility.currentViewWidth - 32f;
            float maxFieldWidth = Mathf.Max(100f, inspectorWidth - EditorGUIUtility.labelWidth - 20f);

            switch (value.Type)
            {
                case JTokenType.Integer:
                {
                    long current = value.Value<long>();
                    EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(inspectorWidth));
                    long updated = EditorGUILayout.LongField(label, current, GUILayout.MaxWidth(inspectorWidth));
                    EditorGUILayout.EndHorizontal();
                    if (updated != current)
                    {
                        value.Value = updated;
                        GUI.changed = true;
                    }
                    break;
                }
                case JTokenType.Float:
                {
                    double current = value.Value<double>();
                    EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(inspectorWidth));
                    double updated = EditorGUILayout.DoubleField(label, current, GUILayout.MaxWidth(inspectorWidth));
                    EditorGUILayout.EndHorizontal();
                    if (Math.Abs(updated - current) > 1e-9)
                    {
                        value.Value = updated;
                        GUI.changed = true;
                    }
                    break;
                }
                case JTokenType.Boolean:
                {
                    bool current = value.Value<bool>();
                    EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(inspectorWidth));
                    bool updated = EditorGUILayout.Toggle(label, current);
                    EditorGUILayout.EndHorizontal();
                    if (updated != current)
                    {
                        value.Value = updated;
                        GUI.changed = true;
                    }
                    break;
                }
                case JTokenType.String:
                {
                    string current = value.Value<string>();
                    EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(inspectorWidth));
                    string updated = EditorGUILayout.TextField(label, current ?? string.Empty, GUILayout.MaxWidth(inspectorWidth));
                    EditorGUILayout.EndHorizontal();
                    if (!string.Equals(updated, current, StringComparison.Ordinal))
                    {
                        value.Value = updated;
                        GUI.changed = true;
                    }
                    break;
                }
                case JTokenType.Null:
                    EditorGUILayout.LabelField(label, "null");
                    break;
                default:
                    EditorGUILayout.LabelField(label, value.ToString());
                    break;
            }
        }

        private bool GetFoldoutState(string path, string label, bool defaultState)
        {
            string key = !string.IsNullOrEmpty(path)
                ? path
                : (!string.IsNullOrEmpty(label) ? label : "<root>");

            if (!foldoutStates.TryGetValue(key, out bool state))
            {
                state = defaultState;
                foldoutStates[key] = state;
            }

            bool newState = EditorGUILayout.Foldout(state, label, true);
            if (newState != state)
            {
                foldoutStates[key] = newState;
            }

            return newState;
        }

        // Clear cached data when JSON changes - NO-OP now since we don't cache
        public void ClearCache(string jsonKey = "")
        {
            // No caching anymore - always scan fresh
        }

        // Draw the full JSON object
        public void DrawFullJsonObject(JObject root)
        {
            if (root == null)
            {
                EditorGUILayout.HelpBox("No JSON root provided.", UnityEditor.MessageType.Warning);
                return;
            }

            DrawGenericJToken(root, "", "");
        }

        // Universal switch for JObject / JArray / scalar
        private void DrawGenericJToken(JToken token, string prefix, string propertyName)
        {
            // Guard against null token during JSON transitions
            if (token == null)
                return;

            if (token is JObject obj)
            {
                DrawJObject(obj, prefix, propertyName);
            }
            else if (token is JArray arr)
            {
                if (!string.IsNullOrEmpty(propertyName) &&
                    (propertyName.Contains("Map", StringComparison.OrdinalIgnoreCase) ||
                     propertyName.Contains("Curve Data", StringComparison.OrdinalIgnoreCase)))
                {
                    DrawMapArray(arr, prefix, propertyName);
                }
                else if (IsLikelyVector3(arr))
                {
                    DrawVector3Inline(arr, prefix, propertyName);
                }
                else if (arr != null && arr.Count == 4 && propertyName.Equals("Orientation", StringComparison.OrdinalIgnoreCase))
                {
                    DrawQuaternionArrayField(prefix + propertyName, arr);
                }
                else
                {
                    DrawJArray(arr, prefix, propertyName);
                }
            }
            else
            {
                EditorGUILayout.LabelField($"{prefix}{token}");
            }
        }

        private void DrawMapArray(JArray mapArray, string prefix, string label)
        {
            // Guard against null mapArray during JSON transitions
            if (mapArray == null)
            {
                EditorGUILayout.HelpBox("Map data is null.", UnityEditor.MessageType.Warning);
                return;
            }
            
            // Just draw the interactive graph - no text columns, no extra wrapping
            mapEditor.DrawGraph(mapArray, label);
        }

        private void DrawJObject(JObject obj, string prefix, string parentKey)
        {
            // Guard against null object during JSON transitions
            if (obj == null)
                return;
                
            foreach (var kvp in obj)
            {
                string key = kvp.Key;
                JToken value = kvp.Value;

                EditorGUILayout.LabelField($"{prefix}{key}:", EditorStyles.boldLabel);
                EditorGUI.indentLevel++;
                DrawGenericJToken(value, prefix + "  ", key);
                EditorGUI.indentLevel--;
            }
        }

        private void DrawJArray(JArray arr, string prefix, string parentKey)
        {
            // Guard against null array during JSON transitions
            if (arr == null)
                return;
                
            try
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    EditorGUILayout.LabelField($"{prefix}[{i}]:", EditorStyles.boldLabel);
                    EditorGUI.indentLevel++;
                    DrawGenericJToken(arr[i], prefix + "  ", $"{parentKey}[{i}]");
                    EditorGUI.indentLevel--;
                }
            }
            catch (System.Exception)
            {
                // JArray might have been invalidated during JSON transition
                EditorGUILayout.HelpBox("Array data invalidated.", UnityEditor.MessageType.Warning);
            }
        }

        private bool IsLikelyVector3(JArray arr)
        {
            if (arr == null || arr.Count != 3) return false;
            for (int i = 0; i < 3; i++)
            {
                if (arr[i].Type != JTokenType.Float && arr[i].Type != JTokenType.Integer)
                    return false;
            }
            return true;
        }

        private void DrawVector3Inline(JArray arr, string prefix, string propertyName)
        {
            // CRITICAL: Guard against null or invalidated array during JSON transitions
            if (arr == null || arr.Count < 3) 
                return;

            try
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField($"{prefix}{propertyName}:", GUILayout.Width(150));
                
                float x = arr[0].ToObject<float>();
                float y = arr[1].ToObject<float>();
                float z = arr[2].ToObject<float>();

                EditorGUILayout.LabelField("X:", GUILayout.Width(15));
                float newX = EditorGUILayout.FloatField(x, GUILayout.Width(60));
                EditorGUILayout.LabelField("Y:", GUILayout.Width(15));
                float newY = EditorGUILayout.FloatField(y, GUILayout.Width(60));
                EditorGUILayout.LabelField("Z:", GUILayout.Width(15));
                float newZ = EditorGUILayout.FloatField(z, GUILayout.Width(60));

                if (!Mathf.Approximately(newX, x)) arr[0] = newX;
                if (!Mathf.Approximately(newY, y)) arr[1] = newY;
                if (!Mathf.Approximately(newZ, z)) arr[2] = newZ;

                EditorGUILayout.EndHorizontal();
            }
            catch (System.Exception)
            {
                // JArray might have been invalidated during JSON transition
                EditorGUILayout.LabelField($"{prefix}{propertyName}: [Invalid data]");
            }
        }

        private void DrawQuaternionArrayField(string label, JArray arr)
        {
            // Guard against null or invalidated array during JSON transitions
            if (arr == null || arr.Count < 4) 
                return;

            try
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(label + " (e0,e1,e2,e3):", GUILayout.Width(200));

                float e0 = arr[0].ToObject<float>();
                float e1 = arr[1].ToObject<float>();
                float e2 = arr[2].ToObject<float>();
                float e3 = arr[3].ToObject<float>();

                float newE0 = EditorGUILayout.FloatField(e0, GUILayout.Width(50));
                float newE1 = EditorGUILayout.FloatField(e1, GUILayout.Width(50));
                float newE2 = EditorGUILayout.FloatField(e2, GUILayout.Width(50));
                float newE3 = EditorGUILayout.FloatField(e3, GUILayout.Width(50));

                if (!Mathf.Approximately(newE0, e0)) arr[0] = newE0;
                if (!Mathf.Approximately(newE1, e1)) arr[1] = newE1;
                if (!Mathf.Approximately(newE2, e2)) arr[2] = newE2;
                if (!Mathf.Approximately(newE3, e3)) arr[3] = newE3;

                EditorGUILayout.EndHorizontal();
            }
            catch (System.Exception)
            {
                // JArray might have been invalidated during JSON transition
                EditorGUILayout.LabelField(label + ": [Invalid data]");
            }
        }

        // Draw JSON reference dropdown
        public string DrawJsonReferenceDropdown(
            string label,
            string currentPath,
            float labelWidth,
            float fieldWidth,
            string forcedType,
            string forcedSubfolder)
        {
            List<string> files = builderCore.GetFilesForSubfolderAndType(forcedSubfolder, forcedType);

            if (files.Count == 0)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(label, GUILayout.Width(labelWidth));
                EditorGUILayout.LabelField("[No files found]", GUILayout.Width(fieldWidth));
                EditorGUILayout.EndHorizontal();
                return currentPath;
            }

            builderCore.BuildDisplayListFromFilenames(files, out List<string> displayNames, out List<string> actualPaths);

            int selectedIndex = actualPaths.IndexOf(currentPath);
            if (selectedIndex < 0 && actualPaths.Count > 0)
            {
                selectedIndex = 0;
                currentPath = actualPaths[0];
            }

            float inspectorWidth = EditorGUIUtility.currentViewWidth - 32f;
            float actualFieldWidth = Mathf.Min(fieldWidth, inspectorWidth - labelWidth - 10f);
            
            EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(inspectorWidth));
            EditorGUILayout.LabelField(label, GUILayout.Width(labelWidth));
            int newIndex = EditorGUILayout.Popup(selectedIndex, displayNames.ToArray(), GUILayout.MaxWidth(actualFieldWidth));
            EditorGUILayout.EndHorizontal();

            if (newIndex >= 0 && newIndex < actualPaths.Count)
            {
                return actualPaths[newIndex];
            }

            return currentPath;
        }
    }
}
#endif
