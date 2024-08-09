﻿// =============================================================================
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

public class UHMMWV : UChVehicle
{
    public HMMWV_Full hmmwv;

    public enum UTireModelType
    {
        TMEASY = TireModelType.TMEASY,
        FIALA = TireModelType.FIALA,
        PAC89 = TireModelType.PAC89,
        PAC02 = TireModelType.PAC02,
        RIGID = TireModelType.RIGID,
        RIGID_MESH = TireModelType.RIGID_MESH
    }

    public enum UBrakeType
    {
        SIMPLE = BrakeType.SIMPLE,
        SHAFTS = BrakeType.SHAFTS
    }

    public bool chassisFixed;
    //public PowertrainModelType powertrainModel;
    public DrivelineTypeWV drivelineModel;
    public UTireModelType tireModel;
    public ChTire.CollisionType tireCollisionType;
    public double tireStepSize = 1e-3;

    public UBrakeType brakeType;
    public bool brakeLocking;

    public double initForwardVel;
    public double initWheelAngSpeed;

    public Material chassisMaterial;

    private GameObject chassis;
    private GameObject wheelFL;
    private GameObject wheelFR;
    private GameObject wheelRL;
    private GameObject wheelRR;



    public UHMMWV()
    {
        chassisFixed = false;

        drivelineModel = DrivelineTypeWV.AWD;

        tireModel = UTireModelType.TMEASY;
        tireCollisionType = ChTire.CollisionType.SINGLE_POINT;

        brakeType = UBrakeType.SHAFTS;
        brakeLocking = false;

        initForwardVel = 0;
        initWheelAngSpeed = 0;
    }

    protected override void OnStart()
    {
        hmmwv = new HMMWV_Full(UChSystem.chrono_system);

        hmmwv.SetChassisFixed(chassisFixed);
        hmmwv.SetChassisCollisionType(CollisionType.HULLS);

        hmmwv.SetDriveType(drivelineModel);
        hmmwv.SetTireType((TireModelType)tireModel);
        hmmwv.SetTireCollisionType(tireCollisionType);
        hmmwv.SetTireStepSize(tireStepSize);
        hmmwv.SetAerodynamicDrag(0.5, 5.0, 1.2);
        hmmwv.SetBrakeType((BrakeType)brakeType);
        hmmwv.EnableBrakeLocking(brakeLocking);

        hmmwv.SetEngineType(EngineModelType.SHAFTS);
        hmmwv.SetTransmissionType(TransmissionModelType.AUTOMATIC_SHAFTS);        

        ////Vector3 pos = transform.position;
        ////Quaternion quat = transform.rotation;
        ////Debug.Log("quat = " + quat.w + " " + quat.x + " " + quat.y + " " + quat.z);
        var csys = new ChCoordsysd(Utils.ToChronoFlip(transform.position), Utils.ToChronoFlip(transform.rotation));
        hmmwv.SetInitPosition(csys);

        hmmwv.SetInitFwdVel(initForwardVel);
        vector_double omega = new vector_double();
        omega.Add(initWheelAngSpeed);
        omega.Add(initWheelAngSpeed);
        omega.Add(initWheelAngSpeed);
        omega.Add(initWheelAngSpeed);
        hmmwv.SetInitWheelAngVel(omega);

        hmmwv.Initialize();

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

        if (chassisMaterial != null)
        {
            var r = chassis.GetComponentInChildren<Renderer>();
            Material[] sharedMaterialsCopy = r.sharedMaterials;
            sharedMaterialsCopy[0] = chassisMaterial;
            r.sharedMaterials = sharedMaterialsCopy;
        }
    }

    protected override void OnAdvance(double step)
    {
        var vehicle_pos = hmmwv.GetVehicle().GetPos();
        var vehicle_rot = hmmwv.GetVehicle().GetRot();

        var spindleFL_pos = hmmwv.GetVehicle().GetSpindlePos(0, VehicleSide.LEFT);
        var spindleFL_rot = hmmwv.GetVehicle().GetSpindleRot(0, VehicleSide.LEFT);

        var spindleFR_pos = hmmwv.GetVehicle().GetSpindlePos(0, VehicleSide.RIGHT);
        var spindleFR_rot = hmmwv.GetVehicle().GetSpindleRot(0, VehicleSide.RIGHT);

        var spindleRL_pos = hmmwv.GetVehicle().GetSpindlePos(1, VehicleSide.LEFT);
        var spindleRL_rot = hmmwv.GetVehicle().GetSpindleRot(1, VehicleSide.LEFT);

        var spindleRR_pos = hmmwv.GetVehicle().GetSpindlePos(1, VehicleSide.RIGHT);
        var spindleRR_rot = hmmwv.GetVehicle().GetSpindleRot(1, VehicleSide.RIGHT);


        //var spindleFL_chassis_rel_rot = ChronoEngine_csharp.Qcross(ChronoEngine_csharp.Qconjugate(vehicle_rot), spindleFL_rot);

        //Debug.Log(System.Math.Round(vehicle_rot.e0 * 1000) / 1000 + "  " +
        //          System.Math.Round(vehicle_rot.e1 * 1000) / 1000 + "  " +
        //          System.Math.Round(vehicle_rot.e2 * 1000) / 1000 + "  " +
        //          System.Math.Round(vehicle_rot.e3 * 1000) / 1000
        //);

        //Debug.Log(System.Math.Round(spindleFL_chassis_rel_rot.e0 * 1000) / 1000 + "  " +
        //          System.Math.Round(spindleFL_chassis_rel_rot.e1 * 1000) / 1000 + "  " +
        //          System.Math.Round(spindleFL_chassis_rel_rot.e2 * 1000) / 1000 + "  " +
        //          System.Math.Round(spindleFL_chassis_rel_rot.e3 * 1000) / 1000
        //    );

        // Set position of the vehicle GameObject and of all moving parts

        var veh_pos = Utils.FromChronoFlip(vehicle_pos);
        var veh_rot = Utils.FromChronoFlip(vehicle_rot);

        ////Debug.Log("vehicle pos:  " + veh_pos.ToString("F5"));

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

        hmmwv.Synchronize(UChSystem.chrono_system.GetChTime(), inputs, chTerrain);
        hmmwv.Advance(step);
    }

    public override double GetMaxSpeed()
    {
        return 25.0; // 90 km/h
    }

    public override ChVehicle GetChVehicle()
    {
        return hmmwv.GetVehicle();
    }

    public override ChPowertrainAssembly GetPowertrainAssembly()
    {
        return hmmwv.GetVehicle().GetPowertrainAssembly();
    }

    public override ChEngine GetEngine()
    {
        return hmmwv.GetVehicle().GetEngine();
    }

    public override ChTransmission GetTransmission()
    {
        return hmmwv.GetVehicle().GetTransmission();
    }

}

