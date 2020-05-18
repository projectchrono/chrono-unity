using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UHMMWV : UChVehicle
{
    public HMMWV_Full hmmwv;

    public bool chassisFixed;
    public PowertrainModelType powertrainModel;
    public DrivelineType drivelineModel;
    public TireModelType tireModel;
    public ChTire.CollisionType tireCollisionType;

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
        ChWorldFrame.SetYUP();
        //ChWorldFrame.Set(new ChMatrix33D(1));

        chassisFixed = false;
        powertrainModel = PowertrainModelType.SHAFTS;
        drivelineModel = DrivelineType.AWD;
        tireModel = TireModelType.TMEASY;
        tireCollisionType = ChTire.CollisionType.SINGLE_POINT;

        initForwardVel = 0;
        initWheelAngSpeed = 0;
    }

    void Start()
    {
        hmmwv = new HMMWV_Full(UChSystem.chrono_system);

        hmmwv.SetChassisFixed(chassisFixed);
        hmmwv.SetPowertrainType(powertrainModel);
        hmmwv.SetDriveType(drivelineModel);
        hmmwv.SetTireType(tireModel);
        hmmwv.SetTireCollisionType(tireCollisionType);

        hmmwv.SetAerodynamicDrag(0.5, 5.0, 1.2);

        ////Vector3 pos = transform.position;
        ////Quaternion quat = transform.rotation;
        ////Debug.Log("quat = " + quat.w + " " + quat.x + " " + quat.y + " " + quat.z);
        var csys = new ChCoordsysD(Utils.ToChronoFlip(transform.position), Utils.ToChronoFlip(transform.rotation));
        hmmwv.SetInitPosition(csys);

        hmmwv.SetInitFwdVel(initForwardVel);
        vector_double omega = new vector_double();
        omega.Add(initWheelAngSpeed);
        omega.Add(initWheelAngSpeed);
        omega.Add(initWheelAngSpeed);
        omega.Add(initWheelAngSpeed);
        hmmwv.SetInitWheelAngVel(omega);

        hmmwv.Initialize();

        // Hide the editing child object and enable the run-time components
        ////var listOfChildren = GetComponentsInChildren<Renderer>();
        ////listOfChildren[0].enabled = false;
        GetComponentInChildren<Renderer>().enabled = false;

        Object chassis_prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Vehicle/HMMWV/Chassis.prefab", typeof(GameObject));
        chassis = Instantiate(chassis_prefab, transform) as GameObject;
        chassis.transform.parent = gameObject.transform;
        if (chassisMaterial != null)
            chassis.GetComponentInChildren<Renderer>().sharedMaterial = chassisMaterial;

        Object wheelL_prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Vehicle/HMMWV/WheelLeft.prefab", typeof(GameObject));
        wheelFL = Instantiate(wheelL_prefab, transform) as GameObject;
        wheelFL.transform.parent = gameObject.transform;
        wheelRL = Instantiate(wheelL_prefab, transform) as GameObject;
        wheelRL.transform.parent = gameObject.transform;

        Object wheelR_prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Vehicle/HMMWV/WheelRight.prefab", typeof(GameObject));
        wheelFR = Instantiate(wheelR_prefab, transform) as GameObject;
        wheelFR.transform.parent = gameObject.transform;
        wheelRR = Instantiate(wheelR_prefab, transform) as GameObject;
        wheelRR.transform.parent = gameObject.transform;
    }

    void FixedUpdate()
    {
        var vehicle_pos = hmmwv.GetVehicle().GetVehiclePos();
        var vehicle_rot = hmmwv.GetVehicle().GetVehicleRot();

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

        //Debug.Log("vehicle pos:  " + veh_pos.ToString("F5"));

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

        ////Debug.Log(hmmwv.GetVehicle().GetVehicleSpeed());

        speed = hmmwv.GetVehicle().GetVehicleSpeed();
        hmmwv.Synchronize(UChSystem.chrono_system.GetChTime(), inputs, UChTerrain.chrono_terrain);
        hmmwv.Advance(Time.fixedDeltaTime);
    }

    public override double GetMaxSpeed()
    {
        return 25.0; // 90 km/h
    }

    public override ChPowertrain GetPowertrain()
    {
        return hmmwv.GetPowertrain();
    }
}
