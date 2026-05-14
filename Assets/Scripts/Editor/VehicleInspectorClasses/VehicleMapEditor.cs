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
// Vehicle map editor - integrates Plot2D with JSON data binding for mapping
// torque curves, capacity factors, etc
// =============================================================================

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using ChronoVehicleBuilder.Plotting;

namespace ChronoVehicleBuilder
{

    public class VehicleMapEditor
    {
        private Dictionary<string, Plot2D> plots = new Dictionary<string, Plot2D>();
        
        /// <summary>
        /// Clear all map states
        /// </summary>
        public void ClearAllMapStates()
        {
            plots.Clear();
        }
        
        /// <summary>
        /// Draw numeric list of [x,y] pairs with add/remove buttons
        /// </summary>
        public void DrawPairs(JArray mapArray, string label = "Map")
        {
            EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
            
            if (mapArray == null || mapArray.Count == 0)
            {
                EditorGUILayout.HelpBox("No data in map.", UnityEditor.MessageType.Info);
                return;
            }
            
            try
            {
                for (int i = 0; i < mapArray.Count; i++)
                {
                    if (mapArray[i] is JArray arr && arr.Count == 2)
                    {
                        float oldX = arr[0].ToObject<float>();
                        float oldY = arr[1].ToObject<float>();
                        
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField($"[{i}] X:", GUILayout.Width(60));
                        float newX = EditorGUILayout.FloatField(oldX, GUILayout.Width(80));
                        EditorGUILayout.LabelField("Y:", GUILayout.Width(20));
                        float newY = EditorGUILayout.FloatField(oldY, GUILayout.Width(80));
                        
                        if (GUILayout.Button("-", GUILayout.Width(25)))
                        {
                            mapArray.RemoveAt(i);
                            EditorGUILayout.EndHorizontal();
                            return;
                        }
                        EditorGUILayout.EndHorizontal();
                        
                        if (!Mathf.Approximately(newX, oldX) || !Mathf.Approximately(newY, oldY))
                        {
                            mapArray[i] = new JArray(newX, newY);
                        }
                    }
                }
                
                if (GUILayout.Button("Add Point", GUILayout.Width(100)))
                {
                    mapArray.Add(new JArray(0f, 0f));
                }
            }
            catch (System.Exception)
            {
                EditorGUILayout.HelpBox("Map data invalidated.", UnityEditor.MessageType.Warning);
            }
        }
        
        /// <summary>
        /// Draw side-by-side view: numeric pairs on left, graph on right
        /// </summary>
        public void DrawCompactMapWithPairs(JArray mapArray, string label = "Map")
        {
            if (mapArray == null)
            {
                EditorGUILayout.HelpBox("Map array is null.", UnityEditor.MessageType.Warning);
                return;
            }
            
            EditorGUILayout.BeginHorizontal();
            
            // Left side: numeric pairs
            EditorGUILayout.BeginVertical(GUILayout.Width(250));
            DrawPairs(mapArray, label);
            EditorGUILayout.EndVertical();
            
            // Right side: graph
            EditorGUILayout.BeginVertical();
            DrawGraph(mapArray, label, 400, 300);
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.EndHorizontal();
        }
        
        /// <summary>
        /// Draw interactive graph with full editing capabilities
        /// </summary>
        public void DrawGraph(JArray mapArray, string label = "Graph", int width = 0, int height = 300)
        {
            if (mapArray == null)
            {
                EditorGUILayout.HelpBox("Map array is null.", UnityEditor.MessageType.Warning);
                return;
            }
            
            // Get or create plot for this map
            if (!plots.TryGetValue(label, out Plot2D plot) || plot == null)
            {
                plot = new Plot2D(label);
                plots[label] = plot;
            }

            if (plot != null)
            {
                if (width > 0)
                {
                    plot.Width = width;
                }

                if (height > 0)
                {
                    plot.Height = height;
                }
            }
            
            // Convert JSON array to points
            List<Vector2> points = ExtractPoints(mapArray);
            
            // ALWAYS rebuild series to ensure data is fresh
            // This ensures any external JSON changes are reflected immediately
            PlotSeries seriesRef = null;
            if (plot.GetSeriesCount() == 0)
            {
                if (points.Count > 0)
                {
                    seriesRef = plot.AddSeries(label, points, Color.green);
                    seriesRef.IsEditable = true;
                }
            }
            else
            {
                seriesRef = plot.GetSeries(0);
                if (seriesRef != null)
                {
                    if (points.Count > 0)
                    {
                        bool needsUpdate = seriesRef.PointCount != points.Count;

                        if (!needsUpdate)
                        {
                            for (int i = 0; i < points.Count; i++)
                            {
                                if (seriesRef.Points[i] != points[i])
                                {
                                    needsUpdate = true;
                                    break;
                                }
                            }
                        }

                        if (needsUpdate)
                        {
                            seriesRef.SetPoints(points);
                        }

                        seriesRef.IsEditable = true;
                    }
                    else
                    {
                        plot.ClearSeries();
                        seriesRef = null;
                    }
                }
            }
            
            // Draw plot (this handles interactions)
            plot.Draw();
            
            // Sync changes back to JSON after any interaction
            // GUI.changed will be true if the plot modified the data
            if (plot.GetSeriesCount() > 0)
            {
                var series = plot.GetSeries(0);
                if (series != null && series.Points.Count > 0)
                {
                    // Check if points actually changed by comparing count or content
                    bool needsSync = (series.Points.Count != points.Count);
                    if (!needsSync && series.Points.Count == points.Count)
                    {
                        for (int i = 0; i < points.Count; i++)
                        {
                            if (series.Points[i] != points[i])
                            {
                                needsSync = true;
                                break;
                            }
                        }
                    }
                    
                    if (needsSync)
                    {
                        // Don't sort while dragging - this would change which point is being dragged!
                        bool allowSort = !series.IsDragging;
                        SyncPointsToJSON(mapArray, series.Points, allowSort);
                    }
                }
            }
        }
        
        private List<Vector2> ExtractPoints(JArray mapArray)
        {
            List<Vector2> points = new List<Vector2>();
            
            if (mapArray == null)
                return points;
            
            try
            {
                for (int i = 0; i < mapArray.Count; i++)
                {
                    if (mapArray[i] == null)
                        continue;
                        
                    if (mapArray[i] is JArray arr && arr.Count >= 2)
                    {
                        try
                        {
                            // Validate that values are numeric
                            if (arr[0] == null || arr[1] == null)
                                continue;
                                
                            float px = arr[0].ToObject<float>();
                            float py = arr[1].ToObject<float>();
                            
                            // Skip invalid values (NaN, Infinity)
                            if (float.IsNaN(px) || float.IsNaN(py) || 
                                float.IsInfinity(px) || float.IsInfinity(py))
                                continue;
                            
                            points.Add(new Vector2(px, py));
                        }
                        catch (System.Exception)
                        {
                            // Skip malformed entry
                            continue;
                        }
                    }
                }
            }
            catch (System.Exception)
            {
                // JArray might have been invalidated during JSON transition
                points.Clear();
            }
            
            return points;
        }
        
        private void SyncPointsToJSON(JArray mapArray, List<Vector2> points, bool allowSort = true)
        {
            if (mapArray == null || points == null)
                return;
                
            try
            {
                // Validate points before syncing
                List<Vector2> validPoints = new List<Vector2>();
                foreach (var pt in points)
                {
                    if (!float.IsNaN(pt.x) && !float.IsNaN(pt.y) &&
                        !float.IsInfinity(pt.x) && !float.IsInfinity(pt.y))
                    {
                        validPoints.Add(pt);
                    }
                }
                
                // Only sort when NOT dragging to prevent index shifting
                // Sorting during drag causes the dragged point index to change mid-drag!
                if (allowSort)
                {
                    validPoints.Sort((a, b) => a.x.CompareTo(b.x));
                }

                // Update in-memory series list to match sorted/validated data
                points.Clear();
                points.AddRange(validPoints);
                
                // Remove extra entries
                while (mapArray.Count > validPoints.Count)
                {
                    mapArray.RemoveAt(mapArray.Count - 1);
                }
                
                // Update existing and add new
                for (int i = 0; i < validPoints.Count; i++)
                {
                    if (i < mapArray.Count)
                    {
                        // Update existing
                        if (mapArray[i] is JArray arr && arr.Count >= 2)
                        {
                            arr[0] = validPoints[i].x;
                            arr[1] = validPoints[i].y;
                        }
                        else
                        {
                            mapArray[i] = new JArray(validPoints[i].x, validPoints[i].y);
                        }
                    }
                    else
                    {
                        // Add new
                        mapArray.Add(new JArray(validPoints[i].x, validPoints[i].y));
                    }
                }
            }
            catch (System.Exception ex)
            {
                // JArray might have been invalidated
                UnityEngine.Debug.LogWarning($"Failed to sync points to JSON: {ex.Message}");
            }
        }
    }
}
#endif
