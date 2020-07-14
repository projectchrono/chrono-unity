using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UGator : UChVehicle
{
    public Gator gator;

    public enum UTireModelType
    {
        TMEASY = TireModelType.TMEASY,
        RIGID = TireModelType.RIGID,
        RIGID_MESH = TireModelType.RIGID_MESH
    }

    public bool chassisFixed;
    //public PowertrainModelType powertrainModel;
    //public DrivelineType drivelineModel;
    public UTireModelType tireModel;
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
        tireModel = UTireModelType.TMEASY;
        ////tireCollisionType = ChTire.CollisionType.SINGLE_POINT;

        initForwardVel = 0;
        initWheelAngSpeed = 0;
    }

    protected override void OnStart()
    {
        gator = new Gator(UChSystem.chrono_system);

        gator.SetChassisFixed(chassisFixed);
        gator.SetTireType((TireModelType)tireModel);
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

        ////Debug.Log("0. inputs: " + inputs.m_steering + " " + inputs.m_throttle + " " + inputs.m_braking);
        ////Debug.Log("0. powertrain speed and torque: " + gator.GetPowertrain().GetMotorSpeed() + "    " + gator.GetPowertrain().GetMotorTorque());

        gator.Synchronize(UChSystem.chrono_system.GetChTime(), inputs, UChTerrain.chrono_terrain);

        ////Debug.Log("1. powertrain speed and torque: " + gator.GetPowertrain().GetMotorSpeed() + "    " + gator.GetPowertrain().GetMotorTorque());
        ////Debug.Log("1. spindle torque (left / right): " 
        ////    + gator.GetVehicle().GetDriveline().GetSpindleTorque(1, VehicleSide.LEFT) + "   " 
        ////    + gator.GetVehicle().GetDriveline().GetSpindleTorque(1, VehicleSide.RIGHT));
        ////Debug.Log("1. driveshaft speed: " + gator.GetVehicle().GetDriveline().GetDriveshaftSpeed());

        gator.Advance(step);

        ////var tFL_force = gator.GetVehicle().GetTire(0, VehicleSide.LEFT).ReportTireForce(UChTerrain.chrono_terrain);
        ////var tRL_force = gator.GetVehicle().GetTire(1, VehicleSide.LEFT).ReportTireForce(UChTerrain.chrono_terrain);
        ////Debug.Log("2. tire FL: " + Utils.FromChrono(tFL_force.force));
        ////Debug.Log("2. tire RL: " + Utils.FromChrono(tRL_force.force));
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

    protected override Vector3 GetIMULocation()
    {
        return new Vector3(-0.267f, -0.016f, 1.539f);
    }

    protected override Vector3 GetLidarLocation()
    {
        return new Vector3(-0.216f, 0.0f, 1.780f);
    }
}
