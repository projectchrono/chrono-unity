using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UChDriver : MonoBehaviour
{
    public enum SpeedMode { 
        Default,       // user throttle/brake control
        CruiseControl  // PID speed controller
    }
    public SpeedMode speedMode;

    // Driver outputs (vehicle inputs)
    private double m_steering;  // [-1, +1]
    private double m_throttle;  // [ 0, +1]
    private double m_braking;   // [ 0, +1]

    // Direct user control of throttle and braking
    public double steeringGain;
    public double throttleGain;
    public double brakingGain;

    private double steering_desired;
    private double throttle_desired;
    private double braking_desired;

    private double steering_delta;
    private double throttle_delta;
    private double braking_delta;

    // Speed cruise controller
    public double targetSpeed;  // set target speed

    public double Kp;  // proportional gain
    public double Kd;  // differential gain
    public double Ki;  // integral gain

    private double m_err;   // current P error
    private double m_errd;  // current D error
    private double m_erri;  // current I error


    public UChVehicle vehicle; // associated vehicle
    private double step;       // integration step size (from underlying ChSystem)

    public UChDriver()
    {
        m_steering = 0;
        m_throttle = 0;
        m_braking = 0;

        speedMode = SpeedMode.CruiseControl;
    }

    void Start()
    {
        step = UChSystem.chrono_system.GetStep();

        // Direct user control mode
        steering_desired = 0;
        throttle_desired = 0;
        braking_desired = 0;
        steeringGain = 4;
        throttleGain = 4;
        brakingGain = 4;
        steering_delta = step / 1;
        throttle_delta = step / 8;
        braking_delta = step / 4;

        // Speed cruise controller mode
        targetSpeed = 0;

        Kp = 0.6;
        Kd = 0.2;
        Ki = 0.1;

        m_err = 0;
        m_errd = 0;
        m_erri = 0;
    }

    void FixedUpdate()
    {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        // Set desired steering, throttle, and braking targets
        if (horiz > 0)
        {
            steering_desired = Utils.Clamp(steering_desired - steering_delta, -1, 1);
        } else if (horiz < 0)
        {
            steering_desired = Utils.Clamp(steering_desired + steering_delta, -1, 1);
        }

        m_steering += step * steeringGain * (steering_desired - m_steering);


        switch (speedMode)
        {
            case SpeedMode.Default:

                if (vert > 0)
                {
                    throttle_desired = Utils.Clamp(throttle_desired + throttle_delta, 0, 1);
                    if (throttle_desired > 0)
                    {
                        braking_desired = Utils.Clamp(braking_desired - 3 * braking_delta, 0, 1);
                    }
                }
                else if (vert < 0)
                {
                    throttle_desired = Utils.Clamp(throttle_desired - 3 * throttle_delta, 0, 1);
                    if (throttle_desired == 0)
                    {
                        braking_desired = Utils.Clamp(braking_desired + braking_delta, 0, 1);
                    }
                }

                // Integrate dynamics
                m_throttle += step * throttleGain * (throttle_desired - m_throttle);
                m_braking += step * brakingGain * (braking_desired - m_braking);

                break;

            case SpeedMode.CruiseControl:

                if (vert > 0) { targetSpeed += 0.001; }
                else if (vert < 0) { targetSpeed -= 0.01; }
                if (targetSpeed < 0) { targetSpeed = 0; }

                double throttle_threshold = 0.2;
                double crt_speed = vehicle.GetSpeed();
                double err = targetSpeed - crt_speed;   // Calculate current error
                m_errd = (err - m_err) / step;          // Estimate error derivative (backward FD approximation)
                m_erri += (err + m_err) * step / 2;     // Calculate current error integral (trapezoidal rule).
                m_err = err;                            // Cache new error
                double output = Kp * m_err + Ki * m_erri + Kd * m_errd; // PID output
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
                ////          "   PID out: " + output + "   Kp = " + Kp);

                break;
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

// ==========================================================================================================

[CustomEditor(typeof(UChDriver))]
public class UChDriverEditor : Editor
{
    override public void OnInspectorGUI()
    {
        //// TODO: expose more parameters?
        
        UChDriver driver = (UChDriver)target;

        driver.vehicle = (UChVehicle)EditorGUILayout.ObjectField("Vehicle", driver.vehicle, typeof(UChVehicle), true);

        // Speed control mode
        string[] options = new string[] { "Pedal Control", "Cruise Control" };
        driver.speedMode = (UChDriver.SpeedMode)EditorGUILayout.Popup("Speed Control Mode", (int)driver.speedMode, options, EditorStyles.popup);

        if (driver.speedMode == UChDriver.SpeedMode.CruiseControl)
        {
            EditorGUI.indentLevel++;

            double KPH = Math.Round(3.6 * driver.targetSpeed * 100) / 100;
            KPH = EditorGUILayout.DoubleField("Target Speed", KPH);
            driver.targetSpeed = KPH / 3.6;

            driver.Kp = EditorGUILayout.DoubleField("Kp", driver.Kp);
            driver.Kd = EditorGUILayout.DoubleField("Kd", driver.Kd);
            driver.Ki = EditorGUILayout.DoubleField("Ki", driver.Ki);

            EditorGUI.indentLevel--;
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(driver);
        }
    }
}
