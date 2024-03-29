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
// Authors: Radu Serban, Josh Diyn
// =============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UMAN : UChVehicle
{
    public MAN_10t man;

    public enum UBrakeType
    {
        SIMPLE = BrakeType.SIMPLE,
        SHAFTS = BrakeType.SHAFTS
    }

    public bool chassisFixed;
    // private PowertrainModelType powertrainType; eliminated 5/5/23
    private TireModelType tireModel;
    ////public ChTire.CollisionType tireCollisionType;

    public UBrakeType brakeType;
    public bool brakeLocking;

    public double initForwardVel;

    private GameObject chassis;
    private GameObject wheelF1L;
    private GameObject wheelF1R;
    private GameObject wheelF2L;
    private GameObject wheelF2R;
    private GameObject wheelR1L;
    private GameObject wheelR1R;
    private GameObject wheelR2L;
    private GameObject wheelR2R;

    public UMAN()
    {
        chassisFixed = false;

        //powertrainType = PowertrainModelType.SHAFTS; obsolete
        tireModel = TireModelType.TMEASY;
        ////tireCollisionType = ChTire.CollisionType.SINGLE_POINT;

        brakeType = UBrakeType.SHAFTS;
        brakeLocking = true;

        initForwardVel = 0;
    }

    protected override void OnStart()
    {
        man = new MAN_10t(UChSystem.chrono_system);

        man.SetChassisFixed(chassisFixed);
        //man.SetPowertrainType(powertrainType);
        man.SetTireType(tireModel);
        ////man.SetAerodynamicDrag(0.5, 5.0, 1.2);
        man.SetBrakeType((BrakeType)brakeType);
        man.EnableBrakeLocking(brakeLocking);

        var csys = new ChCoordsysd(Utils.ToChronoFlip(transform.position), Utils.ToChronoFlip(transform.rotation));
        man.SetInitPosition(csys);

        man.SetInitFwdVel(initForwardVel);

        man.Initialize();

        // Get the vehicle components 
        foreach (Transform child in transform)
        {
            if (child.name == "Chassis10t")
                chassis = child.gameObject;
            else if (child.name == "WheelFront1Left")
                wheelF1L = child.gameObject;
            else if (child.name == "WheelFront1Right")
                wheelF1R = child.gameObject;
            else if (child.name == "WheelFront2Left")
                wheelF2L = child.gameObject;
            else if (child.name == "WheelFront2Right")
                wheelF2R = child.gameObject;
            else if (child.name == "WheelRear1Left")
                wheelR1L = child.gameObject;
            else if (child.name == "WheelRear1Right")
                wheelR1R = child.gameObject;
            else if (child.name == "WheelRear2Left")
                wheelR2L = child.gameObject;
            else if (child.name == "WheelRear2Right")
                wheelR2R = child.gameObject;
        }

        //// HACK to deal with stuttering due to slow physics.
        //// Note that the MAN vehicle model requires smaller timestep (1 ms).
        //Debug.Log("MAX step: " + Time.maximumDeltaTime);
        Time.maximumDeltaTime = 0.02f;
    }

    protected override void OnAdvance(double step)
    {
        var vehicle_pos = man.GetVehicle().GetPos();
        var vehicle_rot = man.GetVehicle().GetRot();

        var spindleF1L_pos = man.GetVehicle().GetSpindlePos(0, VehicleSide.LEFT);
        var spindleF1L_rot = man.GetVehicle().GetSpindleRot(0, VehicleSide.LEFT);

        var spindleF1R_pos = man.GetVehicle().GetSpindlePos(0, VehicleSide.RIGHT);
        var spindleF1R_rot = man.GetVehicle().GetSpindleRot(0, VehicleSide.RIGHT);

        var spindleF2L_pos = man.GetVehicle().GetSpindlePos(1, VehicleSide.LEFT);
        var spindleF2L_rot = man.GetVehicle().GetSpindleRot(1, VehicleSide.LEFT);

        var spindleF2R_pos = man.GetVehicle().GetSpindlePos(1, VehicleSide.RIGHT);
        var spindleF2R_rot = man.GetVehicle().GetSpindleRot(1, VehicleSide.RIGHT);

        var spindleR1L_pos = man.GetVehicle().GetSpindlePos(2, VehicleSide.LEFT);
        var spindleR1L_rot = man.GetVehicle().GetSpindleRot(2, VehicleSide.LEFT);
        
        var spindleR1R_pos = man.GetVehicle().GetSpindlePos(2, VehicleSide.RIGHT);
        var spindleR1R_rot = man.GetVehicle().GetSpindleRot(2, VehicleSide.RIGHT);

        var spindleR2L_pos = man.GetVehicle().GetSpindlePos(3, VehicleSide.LEFT);
        var spindleR2L_rot = man.GetVehicle().GetSpindleRot(3, VehicleSide.LEFT);

        var spindleR2R_pos = man.GetVehicle().GetSpindlePos(3, VehicleSide.RIGHT);
        var spindleR2R_rot = man.GetVehicle().GetSpindleRot(3, VehicleSide.RIGHT);

        // Set position of the vehicle GameObject and of all moving parts

        var veh_pos = Utils.FromChronoFlip(vehicle_pos);
        var veh_rot = Utils.FromChronoFlip(vehicle_rot);

        transform.position = veh_pos;
        transform.rotation = veh_rot;

        chassis.transform.position = veh_pos;
        chassis.transform.rotation = veh_rot;

        wheelF1L.transform.position = Utils.FromChronoFlip(spindleF1L_pos);
        wheelF1L.transform.rotation = Utils.FromChronoFlip(spindleF1L_rot);

        wheelF1R.transform.position = Utils.FromChronoFlip(spindleF1R_pos);
        wheelF1R.transform.rotation = Utils.FromChronoFlip(spindleF1R_rot);

        wheelF2L.transform.position = Utils.FromChronoFlip(spindleF2L_pos);
        wheelF2L.transform.rotation = Utils.FromChronoFlip(spindleF2L_rot);

        wheelF2R.transform.position = Utils.FromChronoFlip(spindleF2R_pos);
        wheelF2R.transform.rotation = Utils.FromChronoFlip(spindleF2R_rot);

        wheelR1L.transform.position = Utils.FromChronoFlip(spindleR1L_pos);
        wheelR1L.transform.rotation = Utils.FromChronoFlip(spindleR1L_rot);

        wheelR1R.transform.position = Utils.FromChronoFlip(spindleR1R_pos);
        wheelR1R.transform.rotation = Utils.FromChronoFlip(spindleR1R_rot);

        wheelR2L.transform.position = Utils.FromChronoFlip(spindleR2L_pos);
        wheelR2L.transform.rotation = Utils.FromChronoFlip(spindleR2L_rot);

        wheelR2R.transform.position = Utils.FromChronoFlip(spindleR2R_pos);
        wheelR2R.transform.rotation = Utils.FromChronoFlip(spindleR2R_rot);

        man.Synchronize(UChSystem.chrono_system.GetChTime(), inputs, chTerrain);
        man.Advance(step);
    }

    public override double GetMaxSpeed()
    {
        return 20.0; // 72 km/h
    }

    public override ChVehicle GetChVehicle()
    {
        return man.GetVehicle();
    }
   
    public override ChPowertrainAssembly GetPowertrainAssembly()
    {
        return man.GetVehicle().GetPowertrainAssembly();
    }

    public override ChEngine GetEngine()
    {
        return man.GetVehicle().GetEngine();
    }

    public override ChTransmission GetTransmission()
    {
        return man.GetVehicle().GetTransmission();
    }

}
