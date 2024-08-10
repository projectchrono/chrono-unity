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

using UnityEngine;

public class UGator : UChVehicle
{
    public Gator gator;

    public enum UTireModelType
    {
        TMEASY = TireModelType.TMEASY,
        RIGID = TireModelType.RIGID,
        RIGID_MESH = TireModelType.RIGID_MESH,
    }

    public enum UBrakeType
    {
        SIMPLE = BrakeType.SIMPLE,
        SHAFTS = BrakeType.SHAFTS
    }

    public bool chassisFixed;
    public UTireModelType tireModel;
    public ChTire.CollisionType tireCollisionType;
    public double tireStepSize = 1e-3;

    public UBrakeType brakeType;
    public bool brakeLocking;

    public double initForwardVel;
    public double initWheelAngSpeed;

    private GameObject chassis;
    private GameObject wheelFL;
    private GameObject wheelFR;
    private GameObject wheelRL;
    private GameObject wheelRR;

    private ChEngineSimple engine;
    private Gator_AutomaticTransmissionSimple transmission;
    ChPowertrainAssembly powertrainAssembly;

    public UGator()
    {
        // Set default values
        chassisFixed = false;

        tireModel = UTireModelType.TMEASY;
        tireCollisionType = ChTire.CollisionType.SINGLE_POINT;

        brakeType = UBrakeType.SHAFTS;
        brakeLocking = false;

        initForwardVel = 0;
        initWheelAngSpeed = 0;

        // Initialise engine and transmission
        engine = new Gator_EngineSimple("GatorEngine");
        transmission = new Gator_AutomaticTransmissionSimple("GatorTransmission");
        powertrainAssembly = new ChPowertrainAssembly(engine, transmission);
    }

    protected override void OnStart()
    {
        gator = new Gator(UChSystem.chrono_system);

        gator.SetChassisFixed(chassisFixed);
        gator.SetTireType((TireModelType)tireModel);
        gator.SetTireCollisionType(tireCollisionType);
        gator.SetTireStepSize(tireStepSize);
        gator.SetAerodynamicDrag(0.5, 5.0, 1.2);
        gator.SetBrakeType((BrakeType)brakeType);
        gator.EnableBrakeLocking(brakeLocking);

        // Enable this if user wants to make use of collisions
        gator.SetChassisCollisionType(CollisionType.PRIMITIVES); 

        ////Vector3 pos = transform.position;
        ////Quaternion quat = transform.rotation;
        ////Debug.Log("quat = " + quat.w + " " + quat.x + " " + quat.y + " " + quat.z);
        var csys = new ChCoordsysd(Utils.ToChronoFlip(transform.position), Utils.ToChronoFlip(transform.rotation));
        gator.SetInitPosition(csys);

        gator.SetInitFwdVel(initForwardVel);
        vector_double omega = new vector_double();
        omega.Add(initWheelAngSpeed);
        omega.Add(initWheelAngSpeed);
        omega.Add(initWheelAngSpeed);
        omega.Add(initWheelAngSpeed);
        gator.SetInitWheelAngVel(omega);
        gator.Initialize(); // Initialise the vehicle
        Debug.Log("Gator is initialized: " + (gator != null).ToString());
        Debug.Log("Gator total mass: " + gator.GetVehicle().GetMass());

        // must be done after vehicle is initialised.
        gator.GetVehicle().InitializePowertrain(powertrainAssembly);


        // Get the vehicle components 
        foreach (Transform child in transform)
        {
            if (child.name == "Chassis")
                chassis = child.gameObject;
            else if (child.name == "WheelFrontLeft")
                wheelFL = child.gameObject;
            else if (child.name == "WheelFrontRight")
                wheelFR = child.gameObject;
            else if (child.name == "WheelRearLeft")
                wheelRL = child.gameObject;
            else if (child.name == "WheelRearRight")
                wheelRR = child.gameObject;
        }

    }

    protected override void OnAdvance(double step)
    {
        var vehicle_pos = gator.GetVehicle().GetPos();
        var vehicle_rot = gator.GetVehicle().GetRot();

        var spindleFL_pos = gator.GetVehicle().GetSpindlePos(0, VehicleSide.LEFT);
        var spindleFL_rot = gator.GetVehicle().GetSpindleRot(0, VehicleSide.LEFT);

        var spindleFR_pos = gator.GetVehicle().GetSpindlePos(0, VehicleSide.RIGHT);
        var spindleFR_rot = gator.GetVehicle().GetSpindleRot(0, VehicleSide.RIGHT);

        var spindleRL_pos = gator.GetVehicle().GetSpindlePos(1, VehicleSide.LEFT);
        var spindleRL_rot = gator.GetVehicle().GetSpindleRot(1, VehicleSide.LEFT);

        var spindleRR_pos = gator.GetVehicle().GetSpindlePos(1, VehicleSide.RIGHT);
        var spindleRR_rot = gator.GetVehicle().GetSpindleRot(1, VehicleSide.RIGHT);

        // Set position of the vehicle GameObject and of all moving parts

        var veh_pos = Utils.FromChronoFlip(vehicle_pos);
        var veh_rot = Utils.FromChronoFlip(vehicle_rot);

        /// Debug.Log("vehicle pos:  " + veh_pos.ToString("F5"));

        transform.position = veh_pos;
        transform.rotation = veh_rot;

        chassis.transform.position = veh_pos;
        chassis.transform.rotation = veh_rot;

        wheelFL.transform.position = Utils.FromChronoFlip(spindleFL_pos);
        wheelFL.transform.rotation = Utils.FromChronoFlip(spindleFL_rot);

        wheelFR.transform.position = Utils.FromChronoFlip(spindleFR_pos);
        wheelFR.transform.rotation = Utils.FromChronoFlip(spindleFR_rot);

        wheelRL.transform.position = Utils.FromChronoFlip(spindleRL_pos);
        wheelRL.transform.rotation = Utils.FromChronoFlip(spindleRL_rot);

        wheelRR.transform.position = Utils.FromChronoFlip(spindleRR_pos);
        wheelRR.transform.rotation = Utils.FromChronoFlip(spindleRR_rot);

        ////Debug.Log("wheelFL pos: " + Utils.FromChrono(spindleFL_pos));
        ////Debug.Log("wheelFR pos: " + Utils.FromChrono(spindleFR_pos));
        ////Debug.Log("wheelRL pos: " + Utils.FromChrono(spindleRL_pos));
        ////Debug.Log("wheelRR pos: " + Utils.FromChrono(spindleRR_pos));

        ////Debug.Log("0. inputs: " + inputs.m_steering + " " + inputs.m_throttle + " " + inputs.m_braking);
        ////Debug.Log("0. powertrain speed and torque: " + vehicle.GetPowertrain().GetMotorSpeed() + "    " + vehicle.GetPowertrain().GetMotorTorque());

        if (gator.GetVehicle().GetTransmission().GetCurrentGear() == -1 && gator.GetVehicle().GetSpeed() <= -3)
        {
            // Debug.Log("Too fast in reverse. Unstable vehicle");
            inputs.m_clutch = 0;
            inputs.m_throttle = 0;
            inputs.m_braking = 1;
        }


        gator.Synchronize(UChSystem.chrono_system.GetChTime(), inputs, chTerrain);

        ////Debug.Log("1. powertrain speed and torque: " + vehicle.GetPowertrain().GetMotorSpeed() + "    " + vehicle.GetPowertrain().GetMotorTorque());
        ////Debug.Log("1. spindle torque (left / right): " 
        ////    + vehicle.GetVehicle().GetDriveline().GetSpindleTorque(1, VehicleSide.LEFT) + "   " 
        ////    + vehicle.GetVehicle().GetDriveline().GetSpindleTorque(1, VehicleSide.RIGHT));
        ////Debug.Log("1. driveshaft speed: " + vehicle.GetVehicle().GetDriveline().GetDriveshaftSpeed());

        gator.Advance(step);

        ////var tFL_force = vehicle.GetVehicle().GetTire(0, VehicleSide.LEFT).ReportTireForce(chTerrain);
        ////var tRL_force = vehicle.GetVehicle().GetTire(1, VehicleSide.LEFT).ReportTireForce(chTerrain);
        ////Debug.Log("2. tire FL: " + Utils.FromChrono(tFL_force.force));
        ////Debug.Log("2. tire RL: " + Utils.FromChrono(tRL_force.force));
        ////Debug.Log("2. tire FL (x): " + tFL_force.force.x);
    }

    public override double GetMaxSpeed()
    {
        return 8.3;  // 30 km/h
    }

    public override ChVehicle GetChVehicle()
    {
        return gator.GetVehicle();
    }
     
    public override ChPowertrainAssembly GetPowertrainAssembly()
    {
        // Return the specific powertrain assembly for Gator
        return gator.GetVehicle().GetPowertrainAssembly();
    }

    // Override the GetTransmission method
    public override ChTransmission GetTransmission()
    {
        // Return the transmission specified above in UGator
        return gator.GetVehicle().GetTransmission();
    }

    // Override the GetEngine method
    public override ChEngine GetEngine()
    {
        // Return the engine set up in UGator
        return gator.GetVehicle().GetEngine();

    }

}
