using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandDriver : ICommandable, IAdvance
{
    // Speed PID controller gains
    public double speedKp { set; get; }   // proportional gain
    public double speedKd { set; get; }   // differential gain
    public double speedKi { set; get; }   // integral gain

    // Driver outputs (vehicle inputs)
    private double m_steering;  // [-1, +1]
    private double m_throttle;  // [ 0, +1]
    private double m_braking;   // [ 0, +1]

    // External commands
    private double targetSpeed;
    private double wheelAngle;

    // PID controller errors
    private double m_err;   // current P error
    private double m_errd;  // current D error
    private double m_erri;  // current I error

    public UChVehicle vehicle;  // associated vehicle

    public CommandDriver()
    {
        targetSpeed = 0;
        wheelAngle = 0;

        m_steering = 0;
        m_throttle = 0;
        m_braking = 0;

        speedKp = 0.6;
        speedKd = 0.2;
        speedKi = 0.1;
    }

    public override void OnStart()
    {
        // Speed cruise controller mode
        targetSpeed = 0;
        m_err = 0;
        m_errd = 0;
        m_erri = 0;

        // Register with the Chrono system (for Advance).
        UChSystem system = (UChSystem)FindObjectOfType(typeof(UChSystem));
        system.Register(gameObject.name + "_command_driver", this);

        // Set associated vehicle
        vehicle = GetComponent<UChVehicle>();
    }

    public override bool OnCommand(string[] command)
    {
        string command_name = command[1];
        if (command_name == "set_speed")
        {
            targetSpeed = float.Parse(command[2]);
            wheelAngle = float.Parse(command[3]);
            return true;
        }

        return false;
    }

    public void Advance(double step)
    {
        // Steering vehicle input

        //// HACK!!!!!   Normalize the steering input based on a maximum wheel angle for the Gator
        double maxWheelAngle = 0.765;
        m_steering = -wheelAngle / maxWheelAngle;

        // Throttle and braking input (cruise-control)

        if (targetSpeed < 0 && vehicle.GetChVehicle().GetPowertrain().GetDriveMode() == ChPowertrain.DriveMode.FORWARD)
            vehicle.GetChVehicle().GetPowertrain().SetDriveMode(ChPowertrain.DriveMode.REVERSE);
        if (targetSpeed > 0 && vehicle.GetChVehicle().GetPowertrain().GetDriveMode() == ChPowertrain.DriveMode.REVERSE)
            vehicle.GetChVehicle().GetPowertrain().SetDriveMode(ChPowertrain.DriveMode.FORWARD);

        if (Math.Abs(targetSpeed) < 0.1)
        {
            m_throttle = 0;
            m_braking = 1;
        }
        else
        {
            double throttle_threshold = 0.2;
            double crt_speed = vehicle.GetSpeed();
            double err = Math.Abs(targetSpeed) - crt_speed;  // Calculate current error
            m_errd = (err - m_err) / step;                   // Estimate error derivative (backward FD approximation)
            m_erri += (err + m_err) * step / 2;              // Calculate current error integral (trapezoidal rule).
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


            ////Debug.Log("target: " + Math.Round(3.6 * targetSpeed * 100) / 100 +
            ////          "   crt: " + Math.Round(3.6 * crt_speed * 100) / 100  +
            ////          "   Speed PID out: " + output + "   Kp = " + speed_Kp);
        }


        ////var s = Math.Round(m_steering * 100) / 100;
        ////var t = Math.Round(m_throttle * 100) / 100;
        ////var b = Math.Round(m_braking * 100) / 100;
        ////Debug.Log(t + "        " + b + "   " + s);

        vehicle.SetDriverInputs(m_steering, m_throttle, m_braking);
    }

    // When attaching to a Game Object, hide the transform
    void OnValidate()
    {
        transform.hideFlags = HideFlags.NotEditable | HideFlags.HideInInspector;
    }
}
