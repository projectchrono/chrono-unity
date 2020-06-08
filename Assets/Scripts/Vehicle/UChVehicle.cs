using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UChVehicle : MonoBehaviour, IAdvance
{
    protected DriverInputs inputs;
    protected double speed;

    public UChVehicle()
    {
        ChWorldFrame.SetYUP();
        //ChWorldFrame.Set(new ChMatrix33D(1));

        inputs = new DriverInputs();
        speed = 0;
    }

    void Start()
    {
        // Register with the Chrono system (for Advance).
        UChSystem system = (UChSystem)FindObjectOfType(typeof(UChSystem));
        system.Register(gameObject.name, this);

        OnStart();
    }

    public void Advance(double step)
    {
        speed = GetChVehicle().GetVehicleSpeed();
        OnAdvance(step);
    }

    public void SetDriverInputs(double steering, double throttle, double braking)
    {
        inputs.m_steering = steering;
        inputs.m_throttle = throttle;
        inputs.m_braking = braking;
    }

    public double GetSpeed() { return speed; }
    public double GetSteeringInput() { return inputs.m_steering; }
    public double GetThrottleInput() { return inputs.m_throttle; }
    public double GetBrakingInput() { return inputs.m_braking; }

    public abstract double GetMaxSpeed();
    public abstract ChVehicle GetChVehicle();
    public abstract ChPowertrain GetChPowertrain();

    protected abstract void OnStart();
    protected abstract void OnAdvance(double step);
}
