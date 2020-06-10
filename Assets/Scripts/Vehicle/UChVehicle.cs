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
        // Set Chrono::Vehicle world frame to y-up (still a RHF)
        ChWorldFrame.SetYUP();

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

        ////Vector3 accel = GetAccelerationLocal(Vector3.zero);
        ////Debug.Log(inputs.m_steering + "     " + accel.x + "  " + accel.y + "  " + accel.z);
        ////Vector3 omg = GetWvelLocal();
        ////Debug.Log(inputs.m_steering + "     " + omg.x + "  " + omg.y + "  " + omg.z);

        OnAdvance(step);
    }

    public void SetDriverInputs(double steering, double throttle, double braking)
    {
        inputs.m_steering = steering;
        inputs.m_throttle = throttle;
        inputs.m_braking = braking;
    }

    // NOTE: Chrono::Vehicle functions currently return vector quantities in the ISO frame (z-up RHF).
    //       Convert the return vectors to Unity y-up LHF (flip y & z).

    public Vector3 GetAccelerationGlobal(Vector3 loc)
    {
        ChVectorD loc_chrono = new ChVectorD(loc.x, -loc.z, loc.y);
        ChVectorD accL_chrono = GetChVehicle().GetVehiclePointAcceleration(loc_chrono);
        ChVectorD accG_chrono = GetChVehicle().GetChassisBody().Dir_Body2World(accL_chrono);
        return new Vector3((float)accG_chrono.x, (float)accG_chrono.z, (float)accG_chrono.y);
    }

    public Vector3 GetAccelerationLocal(Vector3 loc)
    {
        ChVectorD loc_chrono = new ChVectorD(loc.x, -loc.z, loc.y);
        ChVectorD accL_chrono = GetChVehicle().GetVehiclePointAcceleration(loc_chrono);
        return new Vector3((float)accL_chrono.x, (float)accL_chrono.z, (float)accL_chrono.y);
    }

    public Vector3 GetWvelGlobal()
    {
        ChVectorD wvelG_chrono = GetChVehicle().GetChassisBody().GetWvel_par();
        return new Vector3((float)wvelG_chrono.x, (float)wvelG_chrono.z, (float)wvelG_chrono.y);
    }

    public Vector3 GetWvelLocal()
    {
        ChVectorD wvelL_chrono = GetChVehicle().GetChassisBody().GetWvel_loc();
        return new Vector3((float)wvelL_chrono.x, (float)wvelL_chrono.z, (float)wvelL_chrono.y);
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
