using System;
using UnityEngine;

public class Driver : MonoBehaviour, IAdvance
{
    public enum SteeringMode
    {
        Default,      // user left/right control
        PathFollower  // PID lateral controller
    }
    public SteeringMode steeringMode;

    public enum SpeedMode
    {
        Default,       // user throttle/brake control
        CruiseControl  // PID speed controller
    }
    public SpeedMode speedMode;

    // Driver outputs (vehicle inputs)
    private double m_steering;  // [-1, +1]
    private double m_throttle;  // [ 0, +1]
    private double m_braking;   // [ 0, +1]

    // Direct user control of steering, throttle, and braking
    public double steeringGain;
    public double throttleGain;
    public double brakingGain;

    private double steeringDesired;
    private double throttleDesired;
    private double brakingDesired;

    private double steeringDelta;
    private double throttleDelta;
    private double brakingDelta;

    // Path-follower lateral controller
    public UChVehiclePath path;                   // associated path
    ChPathSteeringController steeringController;  // lateral steering controller
    private bool steeringControllerInitialized;

    public double lookAhead;   // look ahead distance
    public double steeringKp;  // proportional gain
    public double steeringKd;  // differential gain
    public double steeringKi;  // integral gain

    public float gizmoRadius;  // radius of tracking spheres

    // Speed cruise controller
    public double targetSpeed; // set target speed

    public double speedKp;  // proportional gain
    public double speedKd;  // differential gain
    public double speedKi;  // integral gain

    private double m_err;   // current P error
    private double m_errd;  // current D error
    private double m_erri;  // current I error

    public UChVehicle vehicle;  // associated vehicle


    public Driver()
    {
        m_steering = 0;
        m_throttle = 0;
        m_braking = 0;

        steeringGain = 4;
        throttleGain = 4;
        brakingGain = 4;

        lookAhead = 5.0;
        steeringKp = 0.8;
        steeringKd = 0.0;
        steeringKi = 0.0;

        speedKp = 0.6;
        speedKd = 0.2;
        speedKi = 0.1;

        steeringMode = SteeringMode.Default;
        speedMode = SpeedMode.CruiseControl;

        gizmoRadius = 0.1f;
    }

    void Start()
    {
        // Register with the Chrono system (for Advance).
        UChSystem system = (UChSystem)FindObjectOfType(typeof(UChSystem));
        system.Register(gameObject.name + "_driver", this);

        double step = UChSystem.chrono_system.GetStep();

        // Set associated vehicle
        vehicle = GetComponent<UChVehicle>();

        // Direct user control mode
        steeringDesired = 0;
        throttleDesired = 0;
        brakingDesired = 0;
        steeringDelta = step / 1;
        throttleDelta = step / 8;
        brakingDelta = step / 4;

        // Path-follower
        steeringControllerInitialized = false;

        // Speed cruise controller mode
        targetSpeed = 0;
        m_err = 0;
        m_errd = 0;
        m_erri = 0;
    }

    public void Advance(double step)
    {
        ////Debug.Log("advance Driver. step = " + step);

        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        // Forward/reverse
        if (Input.GetButton("Fire1"))
        {
            vehicle.GetChPowertrain().SetDriveMode(ChPowertrain.DriveMode.FORWARD);
        }
        if (Input.GetButton("Fire2"))
        {
            vehicle.GetChPowertrain().SetDriveMode(ChPowertrain.DriveMode.REVERSE);
        }

        // Set current steering, depending on selected lateral control mode
        switch (steeringMode)
        {
            case SteeringMode.Default:

                if (horiz > 0) { steeringDesired = Utils.Clamp(steeringDesired - steeringDelta, -1, 1); }
                else if (horiz < 0) { steeringDesired = Utils.Clamp(steeringDesired + steeringDelta, -1, 1); }

                m_steering += step * steeringGain * (steeringDesired - m_steering);

                break;

            case SteeringMode.PathFollower:

                if (path)
                {
                    // HACK to ensure that the ChVehicle was created (in some vehicle assembly's Start())!!
                    if (!steeringControllerInitialized)
                    {
                        steeringController = new ChPathSteeringController(path.GetChVehiclePath());
                        steeringController.SetLookAheadDistance(lookAhead);
                        steeringController.SetGains(steeringKp, steeringKi, steeringKd);
                        steeringController.Reset(vehicle.GetChVehicle());
                        steeringControllerInitialized = true;
                    }

                    var output = steeringController.Advance(vehicle.GetChVehicle(), step);
                    m_steering = Utils.Clamp(output, -1.0, +1.0);
                }

                break;
        }


        // Set current throttle and braking, depending on selected longitudinal control mode
        switch (speedMode)
        {
            case SpeedMode.Default:

                if (vert > 0)
                {
                    throttleDesired = Utils.Clamp(throttleDesired + throttleDelta, 0, 1);
                    if (throttleDesired > 0)
                    {
                        brakingDesired = Utils.Clamp(brakingDesired - 3 * brakingDelta, 0, 1);
                    }
                }
                else if (vert < 0)
                {
                    throttleDesired = Utils.Clamp(throttleDesired - 3 * throttleDelta, 0, 1);
                    if (throttleDesired == 0)
                    {
                        brakingDesired = Utils.Clamp(brakingDesired + brakingDelta, 0, 1);
                    }
                }

                // Integrate dynamics
                m_throttle += step * throttleGain * (throttleDesired - m_throttle);
                m_braking += step * brakingGain * (brakingDesired - m_braking);

                break;

            case SpeedMode.CruiseControl:

                if (vert > 0) { targetSpeed += 0.002; }
                else if (vert < 0) { targetSpeed -= 0.01; }
                if (targetSpeed < 0) { targetSpeed = 0; }
                if (targetSpeed > vehicle.GetMaxSpeed()) { targetSpeed = vehicle.GetMaxSpeed(); }

                if (targetSpeed == 0)
                {
                    m_throttle = 0;
                    m_braking = 1;
                    break;
                }

                double throttle_threshold = 0.2;
                double crt_speed = vehicle.GetSpeed();
                double err = Math.Abs(targetSpeed) - crt_speed;  // Calculate current error
                m_errd = (err - m_err) / step;                   // Estimate error derivative (backward FD approximation)
                m_erri += (err + m_err) * step / 2;              // Calculate current error integral (trapezoidal rule)
                m_err = err;                                     // Cache new error
                double output = speedKp * m_err + speedKi * m_erri + speedKd * m_errd; // PID output
                output = Utils.Clamp(output, -1.0, +1.0);
                if (output > 0)
                {
                    // Vehicle moving too slow
                    m_braking = 0;
                    m_throttle = output;
                }
                else if (m_throttle > throttle_threshold)
                {
                    // Vehicle moving too fast: reduce throttle
                    m_braking = 0;
                    m_throttle = 1 + output;
                }
                else
                {
                    // Vehicle moving too fast: apply brakes
                    m_braking = -output;
                    m_throttle = 0;
                }

                // Prevent brake locking if target speed is positive
                if (targetSpeed > 0 && m_braking > 0.98)
                    m_braking = 0.98;

                ////Debug.Log("target: " + Math.Round(3.6 * targetSpeed * 100) / 100 +
                ////          "   crt: " + Math.Round(3.6 * crt_speed * 100) / 100  +
                ////          "   Speed PID out: " + output + "   Kp = " + speed_Kp);

                break;
        }

        ////var s = Math.Round(m_steering * 100) / 100;
        ////var t = Math.Round(m_throttle * 100) / 100;
        ////var b = Math.Round(m_braking * 100) / 100;
        ////Debug.Log(t + "        " + b + "   " + s);

        vehicle.SetDriverInputs(m_steering, m_throttle, m_braking);
    }

    private void OnDrawGizmos()
    {
        if (steeringControllerInitialized)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(Utils.FromChronoFlip(steeringController.GetSentinelLocation()), gizmoRadius);
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(Utils.FromChronoFlip(steeringController.GetTargetLocation()), gizmoRadius);
        }
    }
}
