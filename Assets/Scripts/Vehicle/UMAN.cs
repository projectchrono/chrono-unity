using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UMAN : UChVehicle, IAdvance
{
    public MAN_10t man;

    public bool chassisFixed;
    private TireModelType tireModel;
    ////public ChTire.CollisionType tireCollisionType;

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
        tireModel = TireModelType.TMEASY;
        ////tireCollisionType = ChTire.CollisionType.SINGLE_POINT;

        initForwardVel = 0;
    }

    void Start()
    {
        // Register with the Chrono system (for Advance).
        UChSystem system = (UChSystem)FindObjectOfType(typeof(UChSystem));
        system.Register(gameObject.name, this);

        man = new MAN_10t(UChSystem.chrono_system);

        man.SetChassisFixed(chassisFixed);
        man.SetShaftBasedDrivetrain(true);
        man.SetTireType(tireModel);

        //man.SetAerodynamicDrag(0.5, 5.0, 1.2);

        var csys = new ChCoordsysD(Utils.ToChronoFlip(transform.position), Utils.ToChronoFlip(transform.rotation));
        man.SetInitPosition(csys);

        man.SetInitFwdVel(initForwardVel);

        man.Initialize();

        // Hide the editing child object and enable the run-time components
        var listOfChildren = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in listOfChildren)
            r.enabled = false;
        ////GetComponentInChildren<Renderer>().enabled = false;

        Object chassis_prefab = Resources.Load("MAN/Chassis10t", typeof(GameObject));
        chassis = Instantiate(chassis_prefab, transform) as GameObject;
        chassis.transform.parent = gameObject.transform;

        Object wheelL_prefab = Resources.Load("MAN/WheelLeft", typeof(GameObject));
        wheelF1L = Instantiate(wheelL_prefab, transform) as GameObject;
        wheelF1L.transform.parent = gameObject.transform;
        wheelF2L = Instantiate(wheelL_prefab, transform) as GameObject;
        wheelF2L.transform.parent = gameObject.transform;
        wheelR1L = Instantiate(wheelL_prefab, transform) as GameObject;
        wheelR1L.transform.parent = gameObject.transform;
        wheelR2L = Instantiate(wheelL_prefab, transform) as GameObject;
        wheelR2L.transform.parent = gameObject.transform;

        Object wheelR_prefab = Resources.Load("MAN/WheelRight", typeof(GameObject));
        wheelF1R = Instantiate(wheelR_prefab, transform) as GameObject;
        wheelF1R.transform.parent = gameObject.transform;
        wheelF2R = Instantiate(wheelR_prefab, transform) as GameObject;
        wheelF2R.transform.parent = gameObject.transform;
        wheelR1R = Instantiate(wheelR_prefab, transform) as GameObject;
        wheelR1R.transform.parent = gameObject.transform;
        wheelR2R = Instantiate(wheelR_prefab, transform) as GameObject;
        wheelR2R.transform.parent = gameObject.transform;
    }

    private void Update()
    {
        //// HACK to deal with stuttering due to slow physics.
        //// Note that the MAN vehicle model requires smaller timestep (1 ms).
        Time.maximumDeltaTime = 0.02f;
    }

    public void Advance(double step)
    {
        ////Debug.Log("advance MAN. step = " + step);

        var vehicle_pos = man.GetVehicle().GetVehiclePos();
        var vehicle_rot = man.GetVehicle().GetVehicleRot();

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

        speed = man.GetVehicle().GetVehicleSpeed();
        ////Debug.Log(speed);

        man.Synchronize(UChSystem.chrono_system.GetChTime(), inputs, UChTerrain.chrono_terrain);
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

    public override ChPowertrain GetPowertrain()
    {
        return man.GetPowertrain();
    }
}
