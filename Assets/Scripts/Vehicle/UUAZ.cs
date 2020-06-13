using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UUAZ : UChVehicle
{
    public UAZBUS uaz;

    public bool chassisFixed;
    //public PowertrainModelType powertrainModel;
    //public DrivelineType drivelineModel;
    private TireModelType tireModel;
    ////public ChTire.CollisionType tireCollisionType;

    public double initForwardVel;
    public double initWheelAngSpeed;

    public Material chassisMaterial;

    private GameObject chassis;
    private GameObject wheelFL;
    private GameObject wheelFR;
    private GameObject wheelRL;
    private GameObject wheelRR;

    public UUAZ()
    {
        chassisFixed = false;
        tireModel = TireModelType.TMEASY;
        ////tireCollisionType = ChTire.CollisionType.SINGLE_POINT;

        initForwardVel = 0;
        initWheelAngSpeed = 0;
    }

    protected override void OnStart()
    {
        uaz = new UAZBUS(UChSystem.chrono_system);

        uaz.SetChassisFixed(chassisFixed);
        uaz.SetTireType(tireModel);
        ////uaz.SetTireCollisionType(tireCollisionType);

        uaz.SetAerodynamicDrag(0.5, 5.0, 1.2);

        ////Vector3 pos = transform.position;
        ////Quaternion quat = transform.rotation;
        ////Debug.Log("quat = " + quat.w + " " + quat.x + " " + quat.y + " " + quat.z);
        var csys = new ChCoordsysD(Utils.ToChronoFlip(transform.position), Utils.ToChronoFlip(transform.rotation));
        uaz.SetInitPosition(csys);

        uaz.SetInitFwdVel(initForwardVel);
        vector_double omega = new vector_double();
        omega.Add(initWheelAngSpeed);
        omega.Add(initWheelAngSpeed);
        omega.Add(initWheelAngSpeed);
        omega.Add(initWheelAngSpeed);
        uaz.SetInitWheelAngVel(omega);

        uaz.Initialize();

        Debug.Log("UAZBUS total mass: " + uaz.GetTotalMass());

        // Hide the editing child object and enable the run-time components
        ////var listOfChildren = GetComponentsInChildren<Renderer>();
        ////listOfChildren[0].enabled = false;
        GetComponentInChildren<Renderer>().enabled = false;

        Object chassis_prefab = Resources.Load("UAZ/Chassis", typeof(GameObject));
        chassis = Instantiate(chassis_prefab, transform) as GameObject;
        chassis.transform.parent = gameObject.transform;
        if (chassisMaterial != null)
            chassis.GetComponentInChildren<Renderer>().sharedMaterial = chassisMaterial;

        Object wheelL_prefab = Resources.Load("UAZ/WheelLeft", typeof(GameObject));
        wheelFL = Instantiate(wheelL_prefab, transform) as GameObject;
        wheelFL.transform.parent = gameObject.transform;
        wheelRL = Instantiate(wheelL_prefab, transform) as GameObject;
        wheelRL.transform.parent = gameObject.transform;

        Object wheelR_prefab = Resources.Load("UAZ/WheelRight", typeof(GameObject));
        wheelFR = Instantiate(wheelR_prefab, transform) as GameObject;
        wheelFR.transform.parent = gameObject.transform;
        wheelRR = Instantiate(wheelR_prefab, transform) as GameObject;
        wheelRR.transform.parent = gameObject.transform;
    }

    protected override void OnAdvance(double step)
    {
        var vehicle_pos = uaz.GetVehicle().GetVehiclePos();
        var vehicle_rot = uaz.GetVehicle().GetVehicleRot();

        var spindleFL_pos = uaz.GetVehicle().GetSpindlePos(0, VehicleSide.LEFT);
        var spindleFL_rot = uaz.GetVehicle().GetSpindleRot(0, VehicleSide.LEFT);

        var spindleFR_pos = uaz.GetVehicle().GetSpindlePos(0, VehicleSide.RIGHT);
        var spindleFR_rot = uaz.GetVehicle().GetSpindleRot(0, VehicleSide.RIGHT);

        var spindleRL_pos = uaz.GetVehicle().GetSpindlePos(1, VehicleSide.LEFT);
        var spindleRL_rot = uaz.GetVehicle().GetSpindleRot(1, VehicleSide.LEFT);

        var spindleRR_pos = uaz.GetVehicle().GetSpindlePos(1, VehicleSide.RIGHT);
        var spindleRR_rot = uaz.GetVehicle().GetSpindleRot(1, VehicleSide.RIGHT);

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

        uaz.Synchronize(UChSystem.chrono_system.GetChTime(), inputs, UChTerrain.chrono_terrain);
        uaz.Advance(step);
    }

    public override double GetMaxSpeed()
    {
        return 20.0;  // 72 km/h
    }

    public override ChVehicle GetChVehicle()
    {
        return uaz.GetVehicle();
    }

    public override ChPowertrain GetChPowertrain()
    {
        return uaz.GetPowertrain();
    }
}