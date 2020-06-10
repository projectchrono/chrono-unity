using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UGator : UChVehicle
{
    public Gator gator;

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

    public UGator()
    {
        chassisFixed = false;
        tireModel = TireModelType.TMEASY;
        ////tireCollisionType = ChTire.CollisionType.SINGLE_POINT;

        initForwardVel = 0;
        initWheelAngSpeed = 0;
    }

    protected override void OnStart()
    {
        gator = new Gator(UChSystem.chrono_system);

        gator.SetChassisFixed(chassisFixed);
        gator.SetTireType(tireModel);
        ////gator.SetTireCollisionType(tireCollisionType);

        gator.SetAerodynamicDrag(0.5, 5.0, 1.2);

        ////Vector3 pos = transform.position;
        ////Quaternion quat = transform.rotation;
        ////Debug.Log("quat = " + quat.w + " " + quat.x + " " + quat.y + " " + quat.z);
        var csys = new ChCoordsysD(Utils.ToChronoFlip(transform.position), Utils.ToChronoFlip(transform.rotation));
        gator.SetInitPosition(csys);

        gator.SetInitFwdVel(initForwardVel);
        vector_double omega = new vector_double();
        omega.Add(initWheelAngSpeed);
        omega.Add(initWheelAngSpeed);
        omega.Add(initWheelAngSpeed);
        omega.Add(initWheelAngSpeed);
        gator.SetInitWheelAngVel(omega);

        gator.EnableBrakeLocking(true);

        gator.Initialize();

        Debug.Log("Gator total mass: " + gator.GetTotalMass());

        // Hide the editing child object and enable the run-time components
        ////var listOfChildren = GetComponentsInChildren<Renderer>();
        ////listOfChildren[0].enabled = false;
        GetComponentInChildren<Renderer>().enabled = false;

        Object chassis_prefab = Resources.Load("Gator/Chassis", typeof(GameObject));
        chassis = Instantiate(chassis_prefab, transform) as GameObject;
        chassis.transform.parent = gameObject.transform;
        if (chassisMaterial != null)
        {
            Material[] mats = chassis.GetComponentInChildren<Renderer>().materials;
            mats[4] = chassisMaterial;
            mats[6] = chassisMaterial;
            chassis.GetComponentInChildren<Renderer>().materials = mats;
        }

        Object wheelFL_prefab = Resources.Load("Gator/WheelFrontLeft", typeof(GameObject));
        wheelFL = Instantiate(wheelFL_prefab, transform) as GameObject;
        wheelFL.transform.parent = gameObject.transform;

        Object wheelRL_prefab = Resources.Load("Gator/WheelRearLeft", typeof(GameObject));
        wheelRL = Instantiate(wheelRL_prefab, transform) as GameObject;
        wheelRL.transform.parent = gameObject.transform;

        Object wheelFR_prefab = Resources.Load("Gator/WheelFrontRight", typeof(GameObject));
        wheelFR = Instantiate(wheelFR_prefab, transform) as GameObject;
        wheelFR.transform.parent = gameObject.transform;

        Object wheelRR_prefab = Resources.Load("Gator/WheelRearRight", typeof(GameObject));
        wheelRR = Instantiate(wheelRR_prefab, transform) as GameObject;
        wheelRR.transform.parent = gameObject.transform;
    }

    protected override void OnAdvance(double step)
    {
        var vehicle_pos = gator.GetVehicle().GetVehiclePos();
        var vehicle_rot = gator.GetVehicle().GetVehicleRot();

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

        gator.Synchronize(UChSystem.chrono_system.GetChTime(), inputs, UChTerrain.chrono_terrain);
        gator.Advance(step);
    }

    public override double GetMaxSpeed()
    {
        return 8.3;  // 30 km/h
    }

    public override ChVehicle GetChVehicle()
    {
        return gator.GetVehicle();
    }

    public override ChPowertrain GetChPowertrain()
    {
        return gator.GetPowertrain();
    }
}