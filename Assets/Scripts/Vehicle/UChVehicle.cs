using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UChVehicle : MonoBehaviour
{
    protected DriverInputs inputs;
    protected double speed;

    public UChVehicle()
    {
        inputs = new DriverInputs();
        speed = 0;
    }

    public void SetDriverInputs(double steering, double throttle, double braking)
    {
        inputs.m_steering = steering;
        inputs.m_throttle = throttle;
        inputs.m_braking = braking;
    }

    public double GetSpeed() { return speed; }

    public abstract double GetMaxSpeed();
    public abstract ChVehicle GetChVehicle();
    public abstract ChPowertrain GetPowertrain();
}
