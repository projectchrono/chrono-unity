using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CameraFollower : MonoBehaviour, IAdvance
{
    private UChVehicle[] vehicles;
    private UChVehicle vehicle;
    private int target_index = 0;

    public bool worldUp;

    [Range (0.0f, 1.0f)]
    public float smoothSpeed = 0.1f;

    [Range (-2.0f, 10.0f)]
    public float cameraDistance = 4;

    [Range(0.0f, 10.0f)]
    public float cameraHeight = 3;

    //public Color textColor;
    private GUIStyle guiStyle = new GUIStyle();

    public void Awake()
    {
        guiStyle.fontStyle = FontStyle.Bold;
        guiStyle.normal.textColor = Color.yellow;
    }

    public void Start()
    {
        // Register with the Chrono system (for Advance).
        UChSystem system = (UChSystem)FindObjectOfType(typeof(UChSystem));
        system.Register(gameObject.name, this);

        // Initialize list of vehicles currently in the scene and attach to first one if any
        vehicles = FindObjectsOfType<UChVehicle>();
        if (vehicles.Length > 0)
            vehicle = vehicles[target_index];
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.PageUp) || Input.GetKeyDown(KeyCode.PageDown))
        {
            // Refresh list of vehicles
            vehicles = FindObjectsOfType<UChVehicle>();
            int numVehicles = vehicles.Length;
            if (numVehicles == 0)
                return;
            if (Input.GetKeyDown(KeyCode.PageUp))
                target_index = (target_index + 1) % numVehicles;
            if (Input.GetKeyDown(KeyCode.PageDown))
                target_index = (target_index + numVehicles - 1) % numVehicles;

            vehicle = vehicles[target_index];
        }
    }

    public void Advance(double step)
    {
        // Do nothing if no associated vehicle
        if (vehicle == null)
            return;

        ////Debug.Log("advance ChaseCam. step = " + step);

        //// TODO: worldUp option requires more work...

        var target_xform = vehicle.transform;

        if (worldUp)  // Camera vertical == World vertical
        {
            Vector3 target_point = target_xform.TransformPoint(new Vector3(0.5f, 0, 0));
            transform.LookAt(target_point);

            Vector3 desiredPosition = target_xform.position + target_xform.right * (-cameraDistance) + target_xform.up * cameraHeight;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
        else          // Camera vertical == Vehicle vertical
        {
            Vector3 desiredPosition = target_xform.position + target_xform.right * (-cameraDistance) + target_xform.up * cameraHeight;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            Quaternion desiredRotation = target_xform.rotation * Quaternion.Euler(0, 90, 0);
            Quaternion smoothedRotation = Quaternion.Lerp(transform.rotation, desiredRotation, smoothSpeed);
            transform.rotation = smoothedRotation;
        }
    }

    private void OnGUI()
    {
        if (vehicle == null)
            return;

        double speedKPH = Math.Round(3.6 * vehicle.GetSpeed());
        GUI.Label(new Rect(10, 10, 200, 40), "Speed (km/h): " + speedKPH.ToString(), guiStyle);
        double throttle = Math.Round(vehicle.GetThrottleInput() * 100) / 100;
        GUI.Label(new Rect(10, 40, 200, 40), "Throttle: " + throttle.ToString(), guiStyle);
        double braking = Math.Round(vehicle.GetBrakingInput() * 100) / 100;
        GUI.Label(new Rect(10, 60, 200, 40), "Braking: " + braking.ToString(), guiStyle);
        double steering = Math.Round(vehicle.GetSteeringInput() * 100) / 100;
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
