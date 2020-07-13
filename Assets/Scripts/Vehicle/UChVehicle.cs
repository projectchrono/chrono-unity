using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UChVehicle : MonoBehaviour, IAdvance
{
    protected DriverInputs inputs;

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
        vehicle.SetDataPath(Application.dataPath + "/Data/");
        ////Debug.Log("vehicle path" + vehicle.GetDataPath());
    }

    void Start()
    {
        // Register with the Chrono system (for Advance).
        UChSystem system = (UChSystem)FindObjectOfType(typeof(UChSystem));
        system.Register(gameObject.name, this);

        // HACK!!!!
        // If this vehicle has the tag "Player", it is assumed that it was created from a spwan message
        // and that it will be controlled through ROS commands.
        // - Disable the existing Driver component and attach a CommandDriver
        // - Instantiate an IMU sensor and attach to this vehicle
        if (gameObject.tag == "Player")
        {
            // Convention is that 0 yaw corresponds to the vehicle facing +Z. 
            // Since the vehicle is constructed in an ISO frame, this requires a 90 rotation about the vertical.
            transform.rotation = transform.rotation * Quaternion.AngleAxis(-90, Vector3.up);

            // Disable the current vehicle driver component and create a commandable driver
            Debug.Log("Set up vehicle " + gameObject.name + " for ROS commands");
            gameObject.GetComponent<Driver>().enabled = false;
            CommandDriver drv = gameObject.AddComponent<CommandDriver>();
            drv.speedKp = 0.8;
            drv.speedKi = 0.2;
            drv.speedKd = 0.1;

            // Create an IMU sensor and associate it with the vehicle
            UnityEngine.Object IMU_prefab = Resources.Load("IMU", typeof(GameObject));
            GameObject imu = Instantiate(IMU_prefab, transform) as GameObject;
            imu.GetComponent<IMU>().vehicle = this;
            imu.name = "imu";

            // Create a WheelEncoder sensor and associate it with the vehicle
            UnityEngine.Object WE_prefab = Resources.Load("WheelEncoder", typeof(GameObject));
            GameObject we = Instantiate(WE_prefab, transform) as GameObject;
            we.GetComponent<WheelEncoder>().vehicle = this;
            we.name = "wheel_encoders";

            //// TODO: Interogate the concrete vehicle to set parameters such as:
            //// - location of sensors
            //// - sensor-specific paramters
            //// - drive speed controller gains
            //// - etc.
            imu.GetComponent<IMU>().sensorLocation = GetIMULocation();
        }

        OnStart();
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
        ChVectorD velG_chrono = GetChVehicle().GetVehiclePointVelocity(new ChVectorD(0, 0, 0));
        ChVectorD velL_chrono = GetChVehicle().GetChassisBody().Dir_World2Body(velG_chrono);
        ////Debug.Log(velL_chrono.x + " " + velL_chrono.y + " " + velL_chrono.z);

        return velL_chrono.x;
    }

    /*
    // Return the vehicle linear acceleration, expressed in the global frame.
    // The location is assumed to be provided in the local vehicle ISO frame. 
    // This needs some massaging related to what frame things are expressed in.
    public Vector3 GetAccelerationGlobal(Vector3 loc)
    {
        ChVectorD accL_chrono = GetChVehicle().GetVehiclePointAcceleration(Utils.ToChrono(loc));
        ChVectorD accG_chrono = GetChVehicle().GetChassisBody().Dir_Body2World(accL_chrono);
        return new Vector3((float)accG_chrono.x, (float)accG_chrono.z, (float)accG_chrono.y);
    }
    */

    // Return the vehicle linear acceleration, expressed in a local ISO frame (x fwd, y left, z up).
    // The location is assumed to be provided in the local vehicle ISO frame. 
    // Note that this is what Chrono::Vehicle currently returns from GetVehiclePointAcceleration,
    // regardless of the world frame orientation.
    public Vector3 GetAccelerationLocal(Vector3 loc)
    {
        ChVectorD accL_chrono = GetChVehicle().GetVehiclePointAcceleration(Utils.ToChrono(loc));
        return Utils.FromChrono(accL_chrono);
    }

    /*
    // Return the vehicle angular velocity, expressed in the global frame.
    // This needs some massaging related to what frame things are expressed in.
    public Vector3 GetWvelGlobal()
    {
        ChVectorD wvelG_chrono = GetChVehicle().GetChassisBody().GetWvel_par();
        return new Vector3((float)wvelG_chrono.x, (float)wvelG_chrono.z, (float)wvelG_chrono.y);
    }
    */

    // Return the vehicle angular velocity, expressed in a local ISO frame (x fwd, y left, z up).
    public Vector3 GetWvelLocal()
    {
        ChVectorD wvelL_chrono = GetChVehicle().GetChassisBody().GetWvel_loc();
        return Utils.FromChrono(wvelL_chrono);
    }

    public double GetSteeringInput() { return inputs.m_steering; }
    public double GetThrottleInput() { return inputs.m_throttle; }
    public double GetBrakingInput() { return inputs.m_braking; }

    public abstract double GetMaxSpeed();
    public abstract ChVehicle GetChVehicle();
    public abstract ChPowertrain GetChPowertrain();

    protected abstract void OnStart();
    protected abstract void OnAdvance(double step);

    // Return the IMU sensor location, relative to the vehicle reference frame (ISO frame)
    protected virtual Vector3 GetIMULocation() { return Vector3.zero; }

    // Return Lidar sensor location, relative to the vehicle reference frame (ISO frame)
    protected virtual Vector3 GetLidarLocation() { return Vector3.zero; }
}
