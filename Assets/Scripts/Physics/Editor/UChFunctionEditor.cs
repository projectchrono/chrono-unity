// =============================================================================
// PROJECT CHRONO - http://projectchrono.org
//
// Copyright (c) 2024 projectchrono.org
// All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found
// in the LICENSE file at the top level of the distribution and at
// http://projectchrono.org/license-chrono.txt.
//
// =============================================================================
// Authors: Radu Serban
// =============================================================================

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UChFunction))]
public class UChFunctionEditor : Editor
{
    override public void OnInspectorGUI()
    {
        UChFunction fun = (UChFunction)target;

        string[] options = new string[] { "Constant", "Ramp", "Sigma", "Sine"};
        fun.type = (UChFunction.Type)EditorGUILayout.Popup("Type", (int)fun.type, options, EditorStyles.popup);

        EditorGUI.indentLevel++;
        switch (fun.type)
        {
            case UChFunction.Type.Constant:
                fun.constant_val = EditorGUILayout.DoubleField("Constant value", fun.constant_val);
                break;
            case UChFunction.Type.Ramp:
                fun.ramp_y0 = EditorGUILayout.DoubleField("Start", fun.ramp_y0);
                fun.ramp_ang = EditorGUILayout.DoubleField("Slope", fun.ramp_ang);
                break;
            case UChFunction.Type.Sigma:
                fun.sigma_start = EditorGUILayout.DoubleField("Start", fun.sigma_start);
                fun.sigma_end = EditorGUILayout.DoubleField("End", fun.sigma_end);
                fun.sigma_amp = EditorGUILayout.DoubleField("Amplitude", fun.sigma_amp);
                break;
            case UChFunction.Type.Sine:
                fun.sine_phase = EditorGUILayout.DoubleField("Phase", fun.sine_phase);
                fun.sine_freq = EditorGUILayout.DoubleField("Frequency", fun.sine_freq);
                fun.sine_amp = EditorGUILayout.DoubleField("Amplitude", fun.sine_amp);
                break;
        }
        EditorGUI.indentLevel--;

        EditorGUILayout.LabelField("Value", fun.value.ToString());

        if (GUI.changed)
        {
            EditorUtility.SetDirty(fun);
        }
    }
}
