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
// Authors: Josh Diyn
// =============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static ChronoGlobals;

public class UGenericVehicle : UChVehicle
{
    public WheeledVehicle vehicle;
    // This field holds the full path to the selected configuration, including the file name and extension.
    [SerializeField]
    [HideInInspector]
    private string selectedConfigurationFullPath;
    public bool chassisFixed;
    public ChTire.CollisionType tireCollisionType;
    public bool brakeLocking;
    public double initForwardVel;
    public double initWheelAngSpeed;

    private GameObject chassis;
    public List<List<GameObject>> wheelArray = new List<List<GameObject>>();

    public UGenericVehicle()
    {
        // Set default values
        chassisFixed = false;
        tireCollisionType = ChTire.CollisionType.SINGLE_POINT;
        brakeLocking = false;
        initForwardVel = 0;
        initWheelAngSpeed = 0;
        //Hack
        // Initialize the list with the expected number of axles,
        // and for each axle, initialize the list for wheel positions.
        for (int i = 0; i < 2; i++) // Assuming 2 axles for this example
        {
            wheelArray.Add(new List<GameObject>(new GameObject[2])); // Assuming 2 positions (left, right) per axle
        }
    }

    protected override void OnStart()
    {
        // Set the path to the Chrono data files so that all generic .json references are contextualised
        chrono.SetChronoDataPath(Application.dataPath + "/Data/");
        // Make use of the json drop down selection
        vehicle = new WheeledVehicle(UChSystem.chrono_system, (selectedConfigurationFullPath));

        // adjust for the vehicle's wheel height
        //var movement = transform.position;
        //Debug.Log("starting point" + movement.ToString());

        //movement.y = (float)(vehicle.GetAxle(0).GetWheel(0).GetRadius()) + 0.05f;
        //Debug.Log("wheel radius = " + vehicle.GetAxle(0).GetWheel(0).GetRadius());
        //transform.position = movement;
        //Debug.Log("moved point point" + transform.position.ToString());
        var csys = new ChCoordsysd(Utils.ToChronoFlip(transform.position), Utils.ToChronoFlip(transform.rotation));
        vehicle.Initialize(csys, initForwardVel); // Initialise the vehicle
 
        vehicle.GetChassis().SetFixed(chassisFixed);
        vehicle.EnableBrakeLocking(brakeLocking);

        //// Create and initialize the powertrain system
        ChEngine engine = chrono_vehicle.ReadEngineJSON(chrono_vehicle.GetDataFile("generic/powertrain/EngineSimple.json"));
        ChTransmission transmission = chrono_vehicle.ReadTransmissionJSON(chrono_vehicle.GetDataFile("generic/powertrain/AutomaticTransmissionShafts.json"));
        ChPowertrainAssembly powertrain = new ChPowertrainAssembly(engine, transmission);
        vehicle.InitializePowertrain(powertrain);

        ////// Create and initialize the tires
        foreach (ChAxle axle in vehicle.GetAxles())
        {
            foreach (ChWheel wheel in axle.GetWheels())
            {
                ChTire tire = chrono_vehicle.ReadTireJSON(chrono_vehicle.GetDataFile("generic/tire/TMeasyTire.json")); // Fiala Tire model causes NaN issues with hard braking. Check implementaiton.
                vehicle.InitializeTire(tire, wheel, VisualizationType.NONE, tireCollisionType);
            }
        }

        Debug.Log("Vehicle is initialized: " + (vehicle != null).ToString());
        Debug.Log("Vehicle total mass: " + vehicle.GetMass());
        //Debug.Log("Vehicle component list: " + vehicle.ExportComponentList()); // This causes issues with the custom json file - possible encoding issues? needs looking into.
       
        // Get the vehicle components 
        foreach (Transform child in transform)
        {
            if (child.name == "Chassis")
                chassis = child.gameObject;
            else if (child.name == "WheelFrontLeft")
                wheelArray[0][0] = child.gameObject;
            else if (child.name == "WheelFrontRight")
                wheelArray[0][1] = child.gameObject;
            else if (child.name == "WheelRearLeft")
                wheelArray[1][0] = child.gameObject;
            else if (child.name == "WheelRearRight")
                wheelArray[1][1] = child.gameObject;
        }
    }

    protected override void OnAdvance(double step)
    {
        var vehicle_pos = vehicle.GetPos();
        var vehicle_rot = vehicle.GetRot();
        var veh_pos = Utils.FromChronoFlip(vehicle_pos);
        var veh_rot = Utils.FromChronoFlip(vehicle_rot);
        ////Debug.Log("vehicle pos:  " + veh_pos.ToString("F5"));

        transform.position = veh_pos;
        transform.rotation = veh_rot;
        chassis.transform.position = veh_pos;
        chassis.transform.rotation = veh_rot;

        wheelArray[0][0].transform.position = Utils.FromChronoFlip(vehicle.GetAxle(0).m_wheels[0].GetPos()); // There's alternative ways to access this
        wheelArray[0][0].transform.rotation = Utils.FromChronoFlip(vehicle.GetAxle(0).m_wheels[0].GetSpindle().GetRot());
        wheelArray[0][1].transform.position = Utils.FromChronoFlip(vehicle.GetAxle(0).m_wheels[1].GetPos());
        wheelArray[0][1].transform.rotation = Utils.FromChronoFlip(vehicle.GetAxle(0).m_wheels[1].GetSpindle().GetRot());
        wheelArray[1][0].transform.position = Utils.FromChronoFlip(vehicle.GetAxle(1).m_wheels[0].GetPos());
        wheelArray[1][0].transform.rotation = Utils.FromChronoFlip(vehicle.GetAxle(1).m_wheels[0].GetSpindle().GetRot());
        wheelArray[1][1].transform.position = Utils.FromChronoFlip(vehicle.GetAxle(1).m_wheels[1].GetPos());
        wheelArray[1][1].transform.rotation = Utils.FromChronoFlip(vehicle.GetAxle(1).m_wheels[1].GetSpindle().GetRot());

        //Debug.Log("0. inputs: " + inputs.m_steering + " " + inputs.m_throttle + " " + inputs.m_braking);
        //Debug.Log("0. powertrain speed and torque: " + vehicle.GetPowertrainAssembly().GetEngine().GetMotorSpeed() + "    " + vehicle.GetPowertrainAssembly().GetOutputTorque());

        vehicle.Synchronize(UChSystem.chrono_system.GetChTime(), inputs, chTerrain);

        //Debug.Log("1. powertrain speed and torque: " + vehicle.GetPowertrain().GetMotorSpeed() + "    " + vehicle.GetPowertrain().GetMotorTorque());
        //Debug.Log("1. spindle torque (left / right): " 
        //    + vehicle.GetVehicle().GetDriveline().GetSpindleTorque(1, VehicleSide.LEFT) + "   " 
        //    + vehicle.GetVehicle().GetDriveline().GetSpindleTorque(1, VehicleSide.RIGHT));
        //Debug.Log("1. driveshaft speed: " + vehicle.GetVehicle().GetDriveline().GetDriveshaftSpeed());

        vehicle.Advance(step);
        //var tFL_force = vehicle.GetTire(0, VehicleSide.LEFT).ReportTireForce(chTerrain);
        //var tRL_force = vehicle.GetTire(1, VehicleSide.LEFT).ReportTireForce(chTerrain);
        //Debug.Log("2. tire FL: " + Utils.FromChrono(tFL_force.force));
        //Debug.Log("2. tire RL: " + Utils.FromChrono(tRL_force.force));
        //Debug.Log("2. tire FL (x): " + tFL_force.force.x);
    }

    public override double GetMaxSpeed()
    {
        return 27.7;  // 100 km/h
    }

    public override ChVehicle GetChVehicle()
    {
        return vehicle;
    }

    public override ChPowertrainAssembly GetPowertrainAssembly()
    {
        return vehicle.GetPowertrainAssembly();
    }

    public override ChTransmission GetTransmission()
    {
        return vehicle.GetPowertrainAssembly().GetTransmission();
    }

    public override ChEngine GetEngine()
    {
        return vehicle.GetEngine();
    }

    // Method for querying tyre forces
    public List<(Vector3 force, Vector3 position)> ReportTireForcesAndPositions()
    {
        List<(Vector3 force, Vector3 position)> tireData = new List<(Vector3 force, Vector3 position)>();

        for (int axleIndex = 0; axleIndex < vehicle.GetNumberAxles(); axleIndex++)
        {
            for (int wheelIndex = 0; wheelIndex < vehicle.GetAxle(axleIndex).m_wheels.Count; wheelIndex++)
            {
                ChVector3d tireForce;
                ChVector3d wheelPos;
                VehicleSide side = wheelIndex % 2 == 0 ? VehicleSide.LEFT : VehicleSide.RIGHT; // Assuming only two tyres per axle. Change if otherwise.

                ChCoordsysd wheelFrame = vehicle.GetTire(axleIndex, side).GetCOMFrame().GetCoordsys();
                var tireForceLocal = vehicle.GetTire(axleIndex, side).ReportTireForceLocal(chTerrain, wheelFrame);

                tireForce = tireForceLocal.force;
                wheelPos = wheelFrame.pos;

                // Convert to Unity's coordinate system, aligned to the tyre's local frame
                Vector3 localForce = Utils.FromChronoFlip(tireForce);
                Vector3 globalPosition = Utils.FromChronoFlip(wheelPos);

                tireData.Add((localForce, globalPosition));
            }
        }
        return tireData;
    }



}
