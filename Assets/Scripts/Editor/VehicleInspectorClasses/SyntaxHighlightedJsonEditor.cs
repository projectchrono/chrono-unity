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
// JSON editor for vehicle inspector using UChRichTextEditorCore with syntax 
// highlighting, bracket matching, line numbers, undo/redo, etc.
// =============================================================================

using UnityEditor;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ChronoVehicleBuilder;
using System;
using System.Linq;

namespace VehicleBuilder.Editor
{

    public class SyntaxHighlightedJsonEditor
    {
        private UChRichTextEditorCore editorCore;
        private Vector2 scrollPosition = Vector2.zero;
        private GUIStyle lineNumberStyle;
        private GUIStyle textStyle;
        private GUIStyle measurementStyle; // For measuring text width without rich text
        private bool stylesInitialized = false;
        private float lineHeight = 16f;
        
        // Callback to request repaint from the host window
        private System.Action onRequestRepaint;
        
        // Track if we need to handle keyboard input
        private bool hadFocusLastFrame = false;
        
        public SyntaxHighlightedJsonEditor(System.Action repaintCallback = null)
        {
            editorCore = new UChRichTextEditorCore();
            onRequestRepaint = repaintCallback;
        }
        
        public void SetJson(JObject jsonObject)
        {
            if (jsonObject != null)
            {
                string formattedJson = jsonObject.ToString(Formatting.Indented);
                
                // Use InlineNumericArrays to make arrays like [1, 2, 3] stay on one line
                formattedJson = editorCore.InlineNumericArrays(formattedJson);
                
                editorCore.InitializeWithJson("vehicle", "", formattedJson);
            }
        }
        
        public JObject GetJson()
        {
            string[] lines = editorCore.GetLines();
            string fullText = string.Join("\n", lines);
            
            try
            {
                return JsonCommentHandling.ParseJObject(fullText ?? string.Empty);
            }
            catch (JsonException)
            {
                return null;
            }
        }
        
        public void Draw(float height = 500f)
        {
            InitializeStyles();
            HandleKeyboardInput();
            
            EditorGUILayout.BeginVertical("box");
            {
                DrawToolbar();
                DrawErrorDisplay();
                DrawEditorArea(height);
                DrawStatusBar();
            }
            EditorGUILayout.EndVertical();
        }
        
        private void InitializeStyles()
        {
            if (stylesInitialized) return;
            
            // Guard against calling before EditorStyles is initialised
            if (EditorStyles.label == null)
                return;
            
            lineNumberStyle = new GUIStyle(EditorStyles.label);
            string[] osFontNames = Font.GetOSInstalledFontNames();
            if (osFontNames.Contains("Consolas"))
                lineNumberStyle.font = Font.CreateDynamicFontFromOSFont("Consolas", 12);
            else if (osFontNames.Contains("Courier New"))
                lineNumberStyle.font = Font.CreateDynamicFontFromOSFont("Courier New", 12);
            lineNumberStyle.normal.textColor = new Color(0.5f, 0.5f, 0.5f);
            lineNumberStyle.alignment = TextAnchor.UpperRight;
            lineNumberStyle.padding = new RectOffset(0, 5, 0, 0);
            
            textStyle = new GUIStyle(EditorStyles.label);
            textStyle.font = lineNumberStyle.font;
            textStyle.richText = true;
            textStyle.wordWrap = false;
            textStyle.alignment = TextAnchor.UpperLeft;
            
            // Measurement style without rich text for accurate width calculation
            measurementStyle = new GUIStyle(EditorStyles.label);
            measurementStyle.font = lineNumberStyle.font;
            measurementStyle.richText = false;
            measurementStyle.wordWrap = false;
            measurementStyle.alignment = TextAnchor.UpperLeft;
            
            stylesInitialized = true;
        }
        
        private void DrawToolbar()
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
            {
                if (GUILayout.Button("Format", EditorStyles.toolbarButton, GUILayout.Width(60)))
                {
                    FormatJson();
                }
                
                if (GUILayout.Button("Validate", EditorStyles.toolbarButton, GUILayout.Width(60)))
                {
                    editorCore.ValidateJson();
                }
                
                if (GUILayout.Button("Undo", EditorStyles.toolbarButton, GUILayout.Width(50)))
                {
                    editorCore.DoUndo();
                }
                
                if (GUILayout.Button("Redo", EditorStyles.toolbarButton, GUILayout.Width(50)))
                {
                    editorCore.DoRedo();
                }
                
                GUILayout.FlexibleSpace();
            }
            EditorGUILayout.EndHorizontal();
        }
        
        private void DrawErrorDisplay()
        {
            if (!string.IsNullOrEmpty(editorCore.ParseError))
            {
                EditorGUILayout.HelpBox($"JSON Parse Error: {editorCore.ParseError}", UnityEditor.MessageType.Error);
            }
        }
        
        private void DrawEditorArea(float height)
        {
            string[] lines = editorCore.GetLines();
            
            // Create a scroll view
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(height));
            {
                EditorGUILayout.BeginHorizontal();
                {
                    // Line numbers column
                    DrawLineNumbers(lines);
                    
                    // Editor content column
                    DrawEditorContent(lines);
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();
        }
        
        private void DrawLineNumbers(string[] lines)
        {
            EditorGUILayout.BeginVertical(GUILayout.Width(40));
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    GUILayout.Label((i + 1).ToString(), lineNumberStyle, GUILayout.Height(lineHeight));
                }
            }
            EditorGUILayout.EndVertical();
        }
        
        private void DrawEditorContent(string[] lines)
        {
            EditorGUILayout.BeginVertical();
            {
                Event e = Event.current;
                
                for (int i = 0; i < lines.Length; i++)
                {
                    DrawEditorLine(i, lines[i], e);
                }
                
                // Add empty space at bottom for clicking
                GUILayout.Space(20);
            }
            EditorGUILayout.EndVertical();
        }
        
        private void DrawEditorLine(int lineIndex, string rawLine, Event e)
        {
            Rect lineRect = GUILayoutUtility.GetRect(GUIContent.none, textStyle, GUILayout.Height(lineHeight));
            
            // Handle mouse clicks for caret positioning
            if (e.type == EventType.MouseDown && lineRect.Contains(e.mousePosition))
            {
                editorCore.SetFocus(true);
                
                // Calculate relative X position within this line rect
                float relativeX = e.mousePosition.x - lineRect.x;
                
                // Use MeasureColumn which handles all the logic (including beyond-text detection)
                // Important to use use measurementStyle (richText=false) for accurate measurement
                // and not measure off the wrong text
                GUIStyle styleForMeasurement = measurementStyle ?? textStyle;
                int column = editorCore.MeasureColumn(lineIndex, relativeX, styleForMeasurement);
                
                editorCore.SetCaretLineColumn(lineIndex, column);
                
                if (e.shift)
                {
                    editorCore.UpdateSelectionEnd();
                }
                else
                {
                    editorCore.ClearSelection();
                }
                
                e.Use();
                GUI.changed = true;
            }
            
            // Handle mouse drag for selection
            if (e.type == EventType.MouseDrag && lineRect.Contains(e.mousePosition) && editorCore.HasFocus)
            {
                if (!editorCore.IsDraggingSelection)
                {
                    editorCore.BeginDragSelection();
                }
                
                // Calculate relative X position within this line rect
                float relativeX = e.mousePosition.x - lineRect.x;
                
                GUIStyle styleForMeasurement = measurementStyle ?? textStyle;
                int column = editorCore.MeasureColumn(lineIndex, relativeX, styleForMeasurement);
                
                editorCore.SetCaretLineColumn(lineIndex, column);
                editorCore.UpdateSelectionEnd();
                e.Use();
                GUI.changed = true;
            }
            
            if (e.type == EventType.MouseUp)
            {
                editorCore.EndDragSelection();
            }
            
            // Draw selection highlight
            if (editorCore.HasSelection && editorCore.LineInSelectionRange(lineIndex))
            {
                EditorGUI.DrawRect(lineRect, new Color(0.3f, 0.5f, 0.8f, 0.3f));
            }
            
            // Draw line with syntax highlighting
            string colorizedLine = editorCore.ColorizeJSONLine(rawLine, lineIndex);
            
            // Draw caret if this is the caret line and editor has focus
            if (lineIndex == editorCore.CaretLine && editorCore.HasFocus)
            {
                DrawCaret(lineRect, rawLine);
            }
            
            // Draw the text
            GUI.Label(lineRect, colorizedLine, textStyle);
        }
        
        private void DrawCaret(Rect lineRect, string lineText)
        {
            // Measure up to caret column to get x position (use measurement style without rich text)
            string beforeCaret = lineText.Substring(0, Mathf.Min(editorCore.CaretColumn, lineText.Length));
            float caretX = (measurementStyle ?? textStyle).CalcSize(new GUIContent(beforeCaret)).x;
            
            Rect caretRect = new Rect(lineRect.x + caretX, lineRect.y, 2, lineHeight);
            EditorGUI.DrawRect(caretRect, new Color(1f, 1f, 1f, 0.8f));
        }
        
        private void HandleKeyboardInput()
        {
            Event e = Event.current;
            
            // Track focus changes
            bool hasFocusNow = editorCore.HasFocus || GUIUtility.keyboardControl != 0;
            if (!hadFocusLastFrame && hasFocusNow)
            {
                editorCore.SetFocus(true);
            }
            hadFocusLastFrame = hasFocusNow;
            
            if (!editorCore.HasFocus)
                return;
            
            if (e.type != EventType.KeyDown)
                return;
            
            // Handle keyboard shortcuts
            bool ctrl = e.control || e.command;
            
            if (ctrl && e.keyCode == KeyCode.Z)
            {
                editorCore.DoUndo();
                e.Use();
                GUI.changed = true;
                return;
            }
            
            if (ctrl && e.keyCode == KeyCode.Y)
            {
                editorCore.DoRedo();
                e.Use();
                GUI.changed = true;
                return;
            }
            
            if (ctrl && e.keyCode == KeyCode.X)
            {
                editorCore.DoCut();
                e.Use();
                GUI.changed = true;
                return;
            }
            
            if (ctrl && e.keyCode == KeyCode.C)
            {
                editorCore.DoCopy();
                e.Use();
                return;
            }
            
            if (ctrl && e.keyCode == KeyCode.V)
            {
                editorCore.DoPaste();
                e.Use();
                GUI.changed = true;
                return;
            }
            
            // Arrow keys
            if (e.keyCode == KeyCode.UpArrow)
            {
                editorCore.MoveCaretLineUp();
                if (e.shift)
                    editorCore.UpdateSelectionEnd();
                else
                    editorCore.ClearSelection();
                e.Use();
                GUI.changed = true;
                return;
            }
            
            if (e.keyCode == KeyCode.DownArrow)
            {
                editorCore.MoveCaretLineDown();
                if (e.shift)
                    editorCore.UpdateSelectionEnd();
                else
                    editorCore.ClearSelection();
                e.Use();
                GUI.changed = true;
                return;
            }
            
            if (e.keyCode == KeyCode.LeftArrow)
            {
                editorCore.MoveCaretLeft();
                if (e.shift)
                    editorCore.UpdateSelectionEnd();
                else
                    editorCore.ClearSelection();
                e.Use();
                GUI.changed = true;
                return;
            }
            
            if (e.keyCode == KeyCode.RightArrow)
            {
                editorCore.MoveCaretRight();
                if (e.shift)
                    editorCore.UpdateSelectionEnd();
                else
                    editorCore.ClearSelection();
                e.Use();
                GUI.changed = true;
                return;
            }
            
            // Backspace and Delete
            if (e.keyCode == KeyCode.Backspace)
            {
                editorCore.DoBackspace();
                e.Use();
                GUI.changed = true;
                return;
            }
            
            if (e.keyCode == KeyCode.Delete)
            {
                editorCore.DoDelete();
                e.Use();
                GUI.changed = true;
                return;
            }
            
            // Enter/Return
            if (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter)
            {
                editorCore.InsertNewLineWithIndent();
                e.Use();
                GUI.changed = true;
                return;
            }
            
            // Regular character input
            if (e.character != '\0' && !char.IsControl(e.character))
            {
                editorCore.InsertText(e.character.ToString());
                e.Use();
                GUI.changed = true;
            }
        }
        
        private void DrawStatusBar()
        {
            string[] lines = editorCore.GetLines();
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"Line {editorCore.CaretLine + 1}, Col {editorCore.CaretColumn + 1}", EditorStyles.miniLabel, GUILayout.Width(120));
            EditorGUILayout.LabelField($"Lines: {lines.Length}", EditorStyles.miniLabel, GUILayout.Width(80));
            
            int totalChars = 0;
            foreach (var line in lines)
                totalChars += line.Length;
            EditorGUILayout.LabelField($"Chars: {totalChars}", EditorStyles.miniLabel, GUILayout.Width(80));
            
            if (string.IsNullOrEmpty(editorCore.ParseError))
            {
                EditorGUILayout.LabelField("✓ Valid JSON", EditorStyles.miniLabel);
            }
            else
            {
                EditorGUILayout.LabelField("✗ Invalid JSON", EditorStyles.miniLabel);
            }
            EditorGUILayout.EndHorizontal();
        }
        
        private void FormatJson()
        {
            JObject obj = GetJson();
            if (obj != null)
            {
                SetJson(obj);
            }
        }
    }
}
