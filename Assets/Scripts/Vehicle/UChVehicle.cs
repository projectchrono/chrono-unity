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
// Authors: Radu Serban, Josh Diyn
// =============================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

// Execute after Chrono system (-999) and Terrain (-950)
[DefaultExecutionOrder(-900)]
public abstract class UChVehicle : MonoBehaviour, IAdvance
{
    protected DriverInputs inputs;
    public ChTerrain chTerrain;
    
    public UChVehicle()
    {
        // Set Chrono::Vehicle world frame to y-up (still a RHF)       
        ChWorldFrame.SetYUP();
        
        inputs = new DriverInputs();
        inputs.m_steering = 0;
        inputs.m_braking = 0;
        inputs.m_throttle = 0;
    }

    void Awake()
    {
        ///var rot = ChWorldFrame.Rotation();
        ///Debug.Log($"World Frame \n {rot.getitem(0, 0)}, {rot.getitem(0, 1)}, {rot.getitem(0, 2)}] \n {rot.getitem(1, 0)}, {rot.getitem(1, 1)}, {rot.getitem(1, 2)} \n [{rot.getitem(2, 0)}, {rot.getitem(2, 1)}, {rot.getitem(2, 2)}]");
        ///Debug.Log($"Vertical direction: {ChWorldFrame.Vertical().x}, {ChWorldFrame.Vertical().y}, {ChWorldFrame.Vertical().z}");
        ///Debug.Log($"Forward direction: {ChWorldFrame.Forward().x}, {ChWorldFrame.Forward().y}, {ChWorldFrame.Forward().z}");
    }

    void Start()
    {
        // Register with the Chrono system (for Advance).
        UChSystem system = UnityEngine.Object.FindFirstObjectByType<UChSystem>();
        if (system == null)
        {
            Debug.LogError($"[{gameObject.name}] No UChSystem found in the scene. Add a UChSystem component to initialize the Chrono simulation.");
            return;
        }
        system.Register(gameObject.name, this);

        OnStart(); // Call the vehicle's onstart.
        
        // Find terrain in system and set to chTerrain
        chTerrain = UChTerrainManager.chronoTerrain;
        
        if (chTerrain == null)
        {
            // Check if a terrain manager exists but has no patches
            var terrainManager = UnityEngine.Object.FindFirstObjectByType<UChRigidTerrainManager>();
            if (terrainManager == null)
            {
                Debug.LogError($"[{gameObject.name}] No UChRigidTerrainManager found in the scene. Add a terrain manager with UChRigidTerrainPatch child objects.");
            }
            else
            {
                Debug.LogError($"[{gameObject.name}] UChRigidTerrainManager exists but terrain is not initialized. Ensure it has at least one UChRigidTerrainPatch child object.");
            }
        }
    }
        
    public void Advance(double step)
    {
        ////Vector3 accel = GetAccelerationLocal(Vector3.zero);
        ////Debug.Log(inputs.m_steering + "     " + accel.x + "  " + accel.y + "  " + accel.z);
        ////Vector3 omg = GetWvelLocal();
        ////Debug.Log(inputs.m_steering + "     " + omg.x + "  " + omg.y + "  " + omg.z);
        
        OnAdvance(step);
    }

    // Set the current values of the driver inputs for this vehicle (invoked by driver subsystems).
    public void SetDriverInputs(double steering, double throttle, double braking)
    {
        inputs.m_steering = steering;
        inputs.m_throttle = throttle;
        inputs.m_braking = braking;
    }

    // Get the current vehicle speed (forward component of vehicle velocity expressed in local frame).
    public double GetSpeed()
    {
        ChVector3d velG_chrono = GetChVehicle().GetPointVelocity(new ChVector3d(0, 0, 0));
        ChVector3d velL_chrono = GetChVehicle().GetTransform().TransformDirectionParentToLocal(velG_chrono);

        ////Debug.Log(velL_chrono.x + " " + velL_chrono.y + " " + velL_chrono.z);

        return velL_chrono.x;
    }

    /*
    // Return the vehicle linear acceleration, expressed in the global frame.
    // The location is assumed to be provided in the local vehicle ISO frame. 
    // This needs some massaging related to what frame things are expressed in.
    public Vector3 GetAccelerationGlobal(Vector3 loc)
    {
        ChVector3d accL_chrono = GetChVehicle().GetVehiclePointAcceleration(Utils.ToChrono(loc));
        ChVector3d accG_chrono = GetChVehicle().GetChassisBody().Dir_Body2World(accL_chrono);
        return new Vector3((float)accG_chrono.x, (float)accG_chrono.z, (float)accG_chrono.y);
    }
    */

    // Return the vehicle linear acceleration, expressed in a local ISO frame (x fwd, y left, z up).
    // The location is assumed to be provided in the local vehicle ISO frame. 
    // Note that this is what Chrono::Vehicle currently returns from GetVehiclePointAcceleration,
    // regardless of the world frame orientation.
    public Vector3 GetAccelerationLocal(Vector3 loc)
    {
        ChVector3d accL_chrono = GetChVehicle().GetPointAcceleration(Utils.ToChrono(loc));
        return Utils.FromChrono(accL_chrono);
    }

    // Return the vehicle angular velocity, expressed in the global frame.
    // This needs some massaging related to what frame things are expressed in.
    public Vector3 GetWvelGlobal()
    {
        ChVector3d wvelG_chrono = GetChVehicle().GetChassisBody().GetAngVelParent();
        //return new Vector3((float)wvelG_chrono.x, (float)wvelG_chrono.z, (float)wvelG_chrono.y);
        if (ChWorldFrame.Vertical().Equals(new ChVector3d(0, 1, 0)))
        { 
            return new Vector3((float)wvelG_chrono.x, (float)wvelG_chrono.y, -(float)wvelG_chrono.z);
        } else
        {
            return new Vector3(0,0,0);
        }
    }

    // Return the vehicle angular velocity, expressed in a local ISO frame (x fwd, y left, z up).
    public Vector3 GetWvelLocal()
    {
        ChVector3d wvelL_chrono = GetChVehicle().GetChassisBody().GetAngVelLocal();
        return Utils.FromChrono(wvelL_chrono);
    }

    public double GetSteeringInput() { return inputs.m_steering; }
    public double GetThrottleInput() { return inputs.m_throttle; }
    public double GetBrakingInput() { return inputs.m_braking; }

    public abstract double GetMaxSpeed();
    public abstract ChVehicle GetChVehicle();

    // Powertrain methods are virtual to provide template implementation, subclasses may optionally override
    public virtual ChPowertrainAssembly GetPowertrainAssembly() { return GetChVehicle().GetPowertrainAssembly(); }
    public virtual ChTransmission GetTransmission() { return GetChVehicle().GetTransmission(); }
    public virtual ChEngine GetEngine() { return GetChVehicle().GetEngine(); }

    // Base vehicle initialisation; subclasses have the option to override.
    // If not overridden, the default implementation is used.
    public virtual void Initialize(ChCoordsysd initialPosition, double initialForwardSpeed)
    {
        // Default implementation
        initialPosition = new ChCoordsysd();
        initialForwardSpeed = 0.0;
    }

    protected abstract void OnStart();
    protected abstract void OnAdvance(double step);



}
