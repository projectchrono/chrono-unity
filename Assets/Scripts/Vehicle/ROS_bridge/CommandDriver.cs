using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CommandDriver : ICommandable, IAdvance
{
    // Driver outputs (vehicle inputs)
    private double m_steering;  // [-1, +1]
    private double m_throttle;  // [ 0, +1]
    private double m_braking;   // [ 0, +1]

    // Commands
    public double targetSpeed;
    public double wheelAngle;

    // Speed cruise controller

    public double speed_Kp;   // proportional gain
    public double speed_Kd;   // differential gain
    public double speed_Ki;   // integral gain

    private double m_err;   // current P error
    private double m_errd;  // current D error
    private double m_erri;  // current I error

    public UChVehicle vehicle;  // associated vehicle

    public GUIStyle guiStyle;
    public Color guiTextColor;

    public CommandDriver()
    {
        targetSpeed = 0;
        wheelAngle = 0;

        m_steering = 0;
        m_throttle = 0;
        m_braking = 0;

        speed_Kp = 0.6;
        speed_Kd = 0.2;
        speed_Ki = 0.1;
    }

    public override void OnStart()
    {
        // Speed cruise controller mode
        targetSpeed = 0;
        m_err = 0;
        m_errd = 0;
        m_erri = 0;

        guiStyle.fontStyle = FontStyle.Bold;
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
        ////Debug.Log("advance Driver. step = " + step);

        // Forward/reverse
        if (Input.GetButton("Fire1"))
        {
            vehicle.GetChVehicle().GetPowertrain().SetDriveMode(ChPowertrain.DriveMode.FORWARD);
        }
        if (Input.GetButton("Fire2"))
        {
            vehicle.GetChVehicle().GetPowertrain().SetDriveMode(ChPowertrain.DriveMode.REVERSE);
        }

        // Steering vehicle input

        //// HACK!!!!!   Normalize the steering input based on a maximum wheel angle for the Gator
        double maxWheelAngle = 0.765;
        m_steering = wheelAngle / maxWheelAngle;

        // Throttle and braking input (cruise-control)

        if (targetSpeed < 0) { targetSpeed = 0; }

        if (targetSpeed == 0)
        {
            m_throttle = 0;
            m_braking = 1;
        }
        else
        {
            double throttle_threshold = 0.2;
            double crt_speed = vehicle.GetSpeed();
            double err = targetSpeed - crt_speed;   // Calculate current error
            m_errd = (err - m_err) / step;          // Estimate error derivative (backward FD approximation)
            m_erri += (err + m_err) * step / 2;     // Calculate current error integral (trapezoidal rule).
            m_err = err;                            // Cache new error
            double output = speed_Kp * m_err + speed_Ki * m_erri + speed_Kd * m_errd; // PID output
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

    private void OnGUI()
    {
        if (Application.isEditor)
        {
            double speedKPH = Math.Round(3.6 * vehicle.GetSpeed());
            GUI.Label(new Rect(10, 10, 200, 40), "Speed (km/h): " + speedKPH.ToString(), guiStyle);
            double throttle = Math.Round(m_throttle * 100) / 100;
            GUI.Label(new Rect(10, 40, 200, 40), "Throttle: " + throttle.ToString(), guiStyle);
            double braking = Math.Round(m_braking * 100) / 100;
            GUI.Label(new Rect(10, 60, 200, 40), "Braking: " + braking.ToString(), guiStyle);
            double steering = Math.Round(m_steering * 100) / 100;
            GUI.Label(new Rect(10, 80, 200, 40), "Steering: " + steering.ToString(), guiStyle);

            switch (vehicle.GetChVehicle().GetPowertrain().GetDriveMode())
            {
                case ChPowertrain.DriveMode.FORWARD:
                    GUI.Label(new Rect(10, 100, 200, 40), "Gear: Forward", guiStyle);
                    break;
                case ChPowertrain.DriveMode.REVERSE:
                    GUI.Label(new Rect(10, 100, 200, 40), "Gear: Reverse", guiStyle);
                    break;
            }

            double motorTorque = Math.Round(vehicle.GetPowertrain().GetMotorTorque());
            GUI.Label(new Rect(10, 130, 200, 40), "Motor Torque (Nm): " + motorTorque.ToString(), guiStyle);
            double motorSpeed = Math.Round(vehicle.GetPowertrain().GetMotorSpeed() * 60 / (2 * Math.PI));
            GUI.Label(new Rect(10, 150, 200, 40), "Motor Speed (RPM): " + motorSpeed.ToString(), guiStyle);

            float wallTime = Time.realtimeSinceStartup;
            float gameTime = Time.unscaledTime;
            float ratio = gameTime / wallTime;
            GUI.Label(new Rect(10, 170, 200, 40), "Time factor: " + Mathf.Round(ratio * 100) / 100, guiStyle);
        }
    }
}

// ==========================================================================================================

[CustomEditor(typeof(CommandDriver))]
public class CommandDriverEditor : Editor
{
    override public void OnInspectorGUI()
    {
        //// TODO: expose more parameters?

        ////EditorGUILayout.HelpBox("Test help", MessageType.Info);

        ////GUIStyle myStyle = GUI.skin.GetStyle("HelpBox");
        ////myStyle.richText = true;
        ////EditorGUILayout.TextArea("This is my text <b>AND IT IS BOLD</b>", myStyle);

        CommandDriver driver = (CommandDriver)target;

        driver.vehicle = (UChVehicle)EditorGUILayout.ObjectField("Vehicle", driver.vehicle, typeof(UChVehicle), true);

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        // Cruise-control parameters

        driver.speed_Kp = EditorGUILayout.DoubleField(new GUIContent("Kp", "Gain for PID proportional term"), driver.speed_Kp);
        driver.speed_Kd = EditorGUILayout.DoubleField(new GUIContent("Kd", "Gain for PID derivative term"), driver.speed_Kd);
        driver.speed_Ki = EditorGUILayout.DoubleField(new GUIContent("Ki", "Gain for PID integral term"), driver.speed_Ki);

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        Color textColor = driver.guiStyle.normal.textColor;
        textColor = EditorGUILayout.ColorField("GUI Text Color", textColor);
        driver.guiStyle.normal.textColor = textColor;

        if (GUI.changed)
        {
            EditorUtility.SetDirty(driver);
        }
    }
}
