using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UChVehicle : MonoBehaviour
{
    protected DriverInputs inputs;

    public UChVehicle()
    {
        inputs = new DriverInputs();
    }

    public void SetDriverInputs(double steering, double throttle, double braking)
    {
        inputs.m_steering = steering;
        inputs.m_throttle = throttle;
        inputs.m_braking = braking;
    }
}
