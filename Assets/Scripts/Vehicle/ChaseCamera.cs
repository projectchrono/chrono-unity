// =============================================================================
// PROJECT CHRONO - http://projectchrono.org
//
// Copyright (c) 2024 projectchrono.org
// All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found
// in the LICENSE file at the top level of the distribution and at
// http://projectchrono.org/license-chrono.txt.
//
// =============================================================================
// Authors: Radu Serban
// =============================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ChaseCamera : MonoBehaviour, IAdvance
{
    private UChVehicle[] vehicles;
    private UChVehicle vehicle;
    private int target_index = 0;

    public bool worldUp;

    [Range (0.0f, 1.0f)]
    public float smoothSpeed = 0.1f;

    [Range (-2.0f, 50.0f)]
    public float cameraDistance = 4;

    [Range(0.0f, 25.0f)]
    public float cameraHeight = 3;

    //public Color textColor;
    private GUIStyle guiStyle = new GUIStyle();
    public Color textColor = Color.black;

    private Vector3 homePos;
    private Quaternion homeRot;
    bool attached = false;

    float deltaTime = 0.0f;

    public void SetHomePosition(Vector3 pos, Quaternion rot)
    {
        homePos = pos;
        homeRot = rot;
        transform.position = homePos;
        transform.rotation = homeRot;
        attached = false;
    }

    public void Awake()
    {
        guiStyle.fontStyle = FontStyle.Bold;
        guiStyle.fontSize = 20;
        guiStyle.normal.textColor = textColor;
        
        homePos = transform.position;
        homeRot = transform.rotation;
    }

    public void Start()
    {
        // Register with the Chrono system (for Advance).
        UChSystem system = (UChSystem)FindObjectOfType(typeof(UChSystem));
        system.Register(gameObject.name, this);

        // Initialize list of vehicles currently in the scene and attach to first one if any
        vehicles = FindObjectsOfType<UChVehicle>();
        if (vehicles.Length > 0)
        {
            vehicle = vehicles[target_index];
            attached = true;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.PageUp) || Input.GetKeyDown(KeyCode.PageDown))
        {
            // Refresh list of vehicles
            vehicles = FindObjectsOfType<UChVehicle>();
            int numVehicles = vehicles.Length;
            if (numVehicles == 0)
            {
                attached = false;
                return;
            }
            if (Input.GetKeyDown(KeyCode.PageUp))
                target_index = (target_index + 1) % numVehicles;
            if (Input.GetKeyDown(KeyCode.PageDown))
                target_index = (target_index + numVehicles - 1) % numVehicles;

            vehicle = vehicles[target_index];
            attached = true;
        }

        if (Input.GetKeyDown(KeyCode.Home))
        {
            if (!attached && vehicle != null)
            {
                attached = true;
            }
            else if (attached)
            {
                transform.position = homePos;
                transform.rotation = homeRot;
                attached = false;
            }
        }

        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    public void Advance(double step)
    {
        // Do nothing if no associated vehicle
        if (!attached)
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
        float wallTime = Time.realtimeSinceStartup;
        float gameTime = Time.unscaledTime;
        float ratio = gameTime / wallTime;
        GUI.Label(new Rect(10, 10, 200, 40), "RTF: " + Mathf.Round(ratio * 100) / 100, guiStyle);

        float FPS = 1 / deltaTime;
        GUI.Label(new Rect(10, 40, 200, 40), "FPS: " + Mathf.Round(FPS * 100) / 100, guiStyle);

        double time = UChSystem.chrono_system.GetChTime();
        GUI.Label(new Rect(Screen.width- 210, 10, 200, 40), "Time: " + Math.Round(time * 100) / 100, guiStyle);

        if (!attached)
            return;

        GUI.Label(new Rect(10, 80, 200, 40), vehicle.name, guiStyle);

        double speedKPH = Math.Round(3.6 * vehicle.GetSpeed());
        GUI.Label(new Rect(10, 120, 200, 40), "Speed (km/h): " + speedKPH.ToString(), guiStyle);
        int currentGear = vehicle.GetTransmission().GetCurrentGear();
        GUI.Label(new Rect(10, 150, 200, 40), "Current Gear: " + currentGear.ToString(), guiStyle);
        double throttle = Math.Round(vehicle.GetThrottleInput() * 100) / 100;
        GUI.Label(new Rect(10, 180, 200, 40), "Throttle: " + throttle.ToString(), guiStyle);
        double braking = Math.Round(vehicle.GetBrakingInput() * 100) / 100;
        GUI.Label(new Rect(10, 210, 200, 40), "Braking: " + braking.ToString(), guiStyle);
        double steering = Math.Round(vehicle.GetSteeringInput() * 100) / 100;
        GUI.Label(new Rect(10, 240, 200, 40), "Steering: " + steering.ToString(), guiStyle);

        var transmission = chrono_vehicle.CastToChAutomaticTransmission(vehicle.GetTransmission());

        if (transmission != null)
        {
            switch (transmission.GetDriveMode())
            {
                case ChAutomaticTransmission.DriveMode.FORWARD:
                    GUI.Label(new Rect(10, 280, 200, 40), "Gear: Forward", guiStyle);
                    break;
                case ChAutomaticTransmission.DriveMode.REVERSE:
                    GUI.Label(new Rect(10, 280, 200, 40), "Gear: Reverse", guiStyle);
                    break;
            }
        }

        switch(transmission.GetShiftMode())
        {
            case ChAutomaticTransmission.ShiftMode.MANUAL:
                GUI.Label(new Rect(10, 320, 200, 40), "Manual", guiStyle);
                break;
            case ChAutomaticTransmission.ShiftMode.AUTOMATIC:
                GUI.Label(new Rect(10, 320, 200, 40), "Automatic", guiStyle);
                break;
        }

        if (vehicle.GetPowertrainAssembly() != null)
        {
            double motorTorque = Math.Round(vehicle.GetPowertrainAssembly().GetEngine().GetOutputMotorshaftTorque());
            GUI.Label(new Rect(10, 360, 200, 40), "Motor Torque (Nm): " + motorTorque.ToString(), guiStyle);
            double motorSpeed = Math.Round(vehicle.GetPowertrainAssembly().GetEngine().GetMotorSpeed() * 60 / (2 * Math.PI));
            GUI.Label(new Rect(10, 400, 200, 40), "Motor Speed (RPM): " + motorSpeed.ToString(), guiStyle);
        }
    }
}
