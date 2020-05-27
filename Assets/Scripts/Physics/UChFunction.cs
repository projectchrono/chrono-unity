using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;

public class UChFunction : MonoBehaviour
{
    public enum Type { Constant, Ramp, Sigma, Sine }
    public Type type;

    // Constant function 
    //   y(t) = constant_value
    public double constant_val = 0;

    // Ramp function
    //   y(t) = y0 + ang * t
    public double ramp_y0 = 0;
    public double ramp_ang = 1;

    // Sigma function 
    //   t < start => y(t) = 0
    //   t > end   => y(t) = amp
    //   otherwise => y(t) = cubic polynomial between 0 and amp
    public double sigma_start = 0;
    public double sigma_end = 1;
    public double sigma_amp = 1;

    // Sine function
    //   y(t) = amp * sin (phase + 2 * pi * freq * t)
    public double sine_phase = 0;
    public double sine_amp = 1;
    public double sine_freq = 1;

    public double value;

    private ChFunction fun;

    public UChFunction()
    {
        type = Type.Constant;
    }

    public ChFunction GetChFunction()
    {
        return fun;
    }

    void Awake()
    {
        switch (type)
        {
            case Type.Constant:
            default:
                var funConstant = new ChFunction_Const(constant_val);
                fun = funConstant;
                break;
            case Type.Ramp:
                var funRamp = new ChFunction_Ramp(ramp_y0, ramp_ang);
                fun = funRamp;
                break;
            case Type.Sigma:
                var funSigma = new ChFunction_Sigma(sigma_amp, sigma_start, sigma_end);
                fun = funSigma;
                break;
            case Type.Sine:
                var funSine = new ChFunction_Sine(sine_phase, sine_freq, sine_amp);
                fun = funSine;
                break;
        }
    }

    void Start()
    {
        switch (type)
        {
            case Type.Constant:
                ((ChFunction_Const)fun).Set_yconst(constant_val);
                break;
            case Type.Ramp:
                ((ChFunction_Ramp)fun).Set_y0(ramp_y0);
                ((ChFunction_Ramp)fun).Set_ang(ramp_ang);
                break;
            case Type.Sigma:
                ((ChFunction_Sigma)fun).Set_start(sigma_start);
                ((ChFunction_Sigma)fun).Set_end(sigma_end);
                ((ChFunction_Sigma)fun).Set_amp(sigma_amp);
                break;
            case Type.Sine:
                ((ChFunction_Sine)fun).Set_amp(sine_amp);
                ((ChFunction_Sine)fun).Set_freq(sine_freq);
                ((ChFunction_Sine)fun).Set_phase(sine_phase);
                break;
        }
    }

    void Update()
    {
        value = fun.Get_y(UChSystem.chrono_system.GetChTime());
        ////Debug.Log("  t = " + UChSystem.chrono_system.GetChTime() + " val = " + value);
    }
}

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
