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
                var funConstant = new ChFunctionConst(constant_val);
                fun = funConstant;
                break;
            case Type.Ramp:
                var funRamp = new ChFunctionRamp(ramp_y0, ramp_ang);
                fun = funRamp;
                break;
            case Type.Sigma:
                var funSigma = new ChFunctionPoly23(sigma_amp, sigma_start, sigma_end);
                fun = funSigma;
                break;
            case Type.Sine:
                var funSine = new ChFunctionSine(sine_phase, sine_freq, sine_amp);
                fun = funSine;
                break;
        }
    }

    void Start()
    {
        switch (type)
        {
            case Type.Constant:
                ((ChFunctionConst)fun).SetConstant(constant_val);
                break;
            case Type.Ramp:
                ((ChFunctionRamp)fun).SetStartVal(ramp_y0);
                ((ChFunctionRamp)fun).SetAngularCoeff(ramp_ang);
                break;
            case Type.Sigma:
                ((ChFunctionPoly23)fun).SetStartArg(sigma_start);
                ((ChFunctionPoly23)fun).SetEndArg(sigma_end);
                ((ChFunctionPoly23)fun).SetAmplitude(sigma_amp);
                break;
            case Type.Sine:
                ((ChFunctionSine)fun).SetAmplitude(sine_amp);
                ((ChFunctionSine)fun).SetFrequency(sine_freq);
                ((ChFunctionSine)fun).SetPhase(sine_phase);
                break;
        }
    }

    void Update()
    {
        value = fun.GetVal(UChSystem.chrono_system.GetChTime());
        ////Debug.Log("  t = " + UChSystem.chrono_system.GetChTime() + " val = " + value);
    }
}
