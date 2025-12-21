// =============================================================================
// PROJECT CHRONO - http://projectchrono.org
//
// Copyright (c) 2024 projectchrono.org
// All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found
// in the LICENSE file at the top level of the distribution.
//
// =============================================================================
// Authors: Josh Diyn
// =============================================================================
// Generic Json-built UWheeledVehicle. This has been tested with the in-built
// HMMWV, Gator, MAN and UAZ jsons and hence should work for most of the chrono
// wheeled vehicles.
// =============================================================================


using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic; // for List<>
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


// This is essentially a storage class for holding the UWheeledVehicle generated data (mostly linking gameobjects to chrono tires)
// doing this as a class (vs. a list in a list) allows unity to present parent child arrays/lists in the inspector
[System.Serializable]
public class WheelGameobjects
{
    // All wheel objects attached to this axle
    public List<GameObject> visualGameobjects;
}

public class UWheeledVehicle : UChVehicle
{
    public enum TireAssignmentMode
    {
        SingleFile,
        PerAxleList,
        PerWheelList
    }

    [Header("JSON for the vehicle")]
    public string topLevelVehicleJSON;  // e.g. "hmmwv/vehicle/HMMWV_Vehicle_4WD.json"

    [Header("Powertrain JSON")]
    public string engineJSON;
    public string transJSON;

    [Header("Tire JSON")]
    [Tooltip("Select how tire JSON files are assigned: single file for all wheels, per axle, or per wheel.")]
    public TireAssignmentMode tireAssignmentMode = TireAssignmentMode.SingleFile;
    public string tireJSON = "hmmwv/tire/HMMWV_TMeasyTire.json";

    /// <summary>
    /// Backwards compatibility shim for editor tooling that still toggles a single-file flag.
    /// Maps to <see cref="tireAssignmentMode"/> internally.
    /// </summary>
    public bool useSingleTireFile
    {
        get => tireAssignmentMode == TireAssignmentMode.SingleFile;
        set
        {
            if (value)
            {
                tireAssignmentMode = TireAssignmentMode.SingleFile;
            }
            else if (tireAssignmentMode == TireAssignmentMode.SingleFile)
            {
                tireAssignmentMode = TireAssignmentMode.PerAxleList;
            }
        }
    }

    public bool usePerWheelTireOverrides
    {
        get => tireAssignmentMode == TireAssignmentMode.PerWheelList;
        set
        {
            if (value)
            {
                tireAssignmentMode = TireAssignmentMode.PerWheelList;
            }
            else if (tireAssignmentMode == TireAssignmentMode.PerWheelList)
            {
                tireAssignmentMode = TireAssignmentMode.PerAxleList;
            }
        }
    }

    /// <summary>
    /// When using per-axle or per-wheel assignment modes, populate entries accordingly.
    /// For per-wheel mode, order items Axle0(L,R), Axle1(L,R), etc. Leave entries empty
    /// to fall back to the default tireJSON.
    /// </summary>
    [Header("If not single, multiple tire JSONs (one per wheel)")]
    public List<string> perAxleTireSpec = new List<string>();

    [SerializeField, HideInInspector]
    private string tireOverrideStateHash = string.Empty;

    [Header("Initial Conditions")]
    public bool chassisFixed = false;
    public bool brakeLocking = false;
    public double initForwardVel = 0;
    [Tooltip("Init Wheel Angle Velocity not currently implemented")]
    public double initWheelAngVel = 0;
    public float tireStepSize = 0.001f;

    [Header("Collision / Visualisation")]
    public ChTire.CollisionType tireCollisionType = ChTire.CollisionType.SINGLE_POINT;

    [Header("Axle Data (Gameobjects to Axles)")]
    [SerializeField]
    public List<WheelGameobjects> axleData = new List<WheelGameobjects>();

    // pointer to the built WheeledVehicle
    private WheeledVehicle UChJSONVehicle = null;


    
    //////// Functions
    // At start, call the builder to create the vehicle from the json specs
    protected override void OnStart()
    {
        bool success = BuildFromTopLevelJSON(topLevelVehicleJSON);
        if (!success)
        {
            Debug.LogWarning($"{name}: JSON load failed, so no vehicle was built.");
        }
        else
        {
            // Vehicle successfully built; no verbose diagnostics required here anymore.
        }
    }

    // Validate JSON file syntax
    private bool ValidateJSON(string filePath, string description)
    {
        try
        {
            string jsonText = File.ReadAllText(filePath);
            JToken.Parse(jsonText);
            return true;
        }
        catch (JsonReaderException jex)
        {
            Debug.LogError($"{name}: {description} has invalid JSON syntax at {filePath}\n" +
                          $"Error: {jex.Message}\n" +
                          $"Line {jex.LineNumber}, Position {jex.LinePosition}");
            return false;
        }
        catch (Exception ex)
        {
            Debug.LogError($"{name}: Failed to read {description} at {filePath}\n" +
                          $"Error: {ex.Message}");
            return false;
        }
    }

    // Build from JSON
    private bool BuildFromTopLevelJSON(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            Debug.LogWarning($"{name}: No top-level JSON file specified.");
            return false;
        }

        // resolve actual file path
        string vehicleJson = chrono_vehicle.GetVehicleDataFile(fileName);
        if (!File.Exists(vehicleJson))
        {
            Debug.LogWarning($"{name}: top-level JSON not found: {vehicleJson}");
            return false;
        }

        // Validate vehicle JSON before attempting to load
        if (!ValidateJSON(vehicleJson, "Vehicle JSON"))
        {
            return false;
        }

        // sanity check that it's Type=Vehicle
        string text = File.ReadAllText(vehicleJson);
        if (!text.Contains("\"Type\": \"Vehicle\""))
        {
            Debug.LogWarning($"{name}: top-level JSON not recognized as Type=Vehicle: {fileName}");
            return false;
        }

        try
        {
            // Instantiate the Chrono WheeledVehicle using the JSON
            UChJSONVehicle = new WheeledVehicle(UChSystem.chrono_system, vehicleJson);

            Vector3 initPos = transform.position;

            // initialise
            var csys = new ChCoordsysd(Utils.ToChronoFlip(initPos), Utils.ToChronoFlip(transform.rotation));
            Debug.Log("Initial position set to: " +
                System.Math.Round(csys.pos.x * 1000) / 1000 + "  " +
                System.Math.Round(csys.pos.y * 1000) / 1000 + "  " +
                System.Math.Round(csys.pos.z * 1000) / 1000
            );

            UChJSONVehicle.Initialize(csys, initForwardVel);

            // set chassis fixed / brake locking
            UChJSONVehicle.GetChassis().SetFixed(chassisFixed);
            UChJSONVehicle.EnableBrakeLocking(brakeLocking);
            
            // give it a name
            UChJSONVehicle.SetName(this.gameObject.name);

            // read & initialise engine+trans
            if (!string.IsNullOrEmpty(engineJSON) && !string.IsNullOrEmpty(transJSON))
            {
                string engPath = chrono_vehicle.GetVehicleDataFile(engineJSON);
                string transPath = chrono_vehicle.GetVehicleDataFile(transJSON);

                if (File.Exists(engPath) && File.Exists(transPath))
                {
                    // Validate engine and transmission JSONs
                    if (!ValidateJSON(engPath, "Engine JSON") || !ValidateJSON(transPath, "Transmission JSON"))
                    {
                        Debug.LogWarning($"{name}: Invalid engine or transmission JSON. Skipping powertrain.");
                    }
                    else
                    {
                        ChEngine engine = chrono_vehicle.ReadEngineJSON(engPath);
                        ChTransmission trans = chrono_vehicle.ReadTransmissionJSON(transPath);
                        ChPowertrainAssembly powertrain = new ChPowertrainAssembly(engine, trans);
                        UChJSONVehicle.InitializePowertrain(powertrain);
                    }
                }
                else
                {
                    Debug.LogWarning($"{name}: engine/trans JSON not found at {engPath} or {transPath}");
                }
            }

            // create & initialise tires
            var axleList = UChJSONVehicle.GetAxles();
            uint numAxles = UChJSONVehicle.GetNumberAxles();
            int totalWheelCount = 0;
            for (int axleIdx = 0; axleIdx < numAxles; axleIdx++)
            {
                totalWheelCount += axleList[axleIdx].GetWheels().Count;
            }

            bool overridesCurrent = IsTireOverrideStateCurrent((int)numAxles, totalWheelCount);
            bool runtimeForceReset = !overridesCurrent;

            EnsureTireOverrideEntries((int)numAxles, totalWheelCount, forceReset: runtimeForceReset, defaultEntry: tireJSON);

            if (runtimeForceReset && tireAssignmentMode != TireAssignmentMode.SingleFile)
            {
                Debug.LogWarning(
                    $"{name}: Tire overrides were out of sync with the current vehicle layout ({topLevelVehicleJSON}). " +
                    "Resetting to the default tire JSON before building.");
            }

            UpdateTireOverrideStateHash((int)numAxles, totalWheelCount);

            int globalWheelIndex = 0;

            for (int axleIndex = 0; axleIndex < numAxles; axleIndex++)
            {
                ChAxle axle = axleList[axleIndex];
                var wheels = axle.GetWheels();

                for (int w = 0; w < wheels.Count; w++)
                {
                    string tirePath = ResolveTirePath(axleIndex, w, globalWheelIndex);
                    globalWheelIndex++;

                    if (string.IsNullOrEmpty(tirePath) || !File.Exists(tirePath))
                    {
                        Debug.LogWarning($"{name}: Tire JSON for axle #{axleIndex}, wheel #{w} not found. Skipping.");
                        continue;
                    }

                    // Validate tire JSON per wheel for clarity
                    if (!ValidateJSON(tirePath, $"Tire JSON (Axle {axleIndex}, Wheel {w})"))
                    {
                        Debug.LogWarning($"{name}: Invalid tire JSON for axle #{axleIndex}, wheel #{w}. Skipping.");
                        continue;
                    }

                    Debug.Log($"{name}: Axle {axleIndex}, Wheel {w}: Loading tire from {tirePath}");

                    ChWheel wheel = wheels[w];
                    ChTire tire = chrono_vehicle.ReadTireJSON(tirePath);
                    tire.SetStepsize(tireStepSize);
                    UChJSONVehicle.InitializeTire(tire, wheel, VisualizationType.NONE, tireCollisionType);

                    Debug.Log($"{name}: Axle {axleIndex}, Wheel {w}: Tire initialized with collision type {tireCollisionType}");
                }
            }

            ////// set initial wheel angular velocity - not tested
            //if (initWheelAngVel != 0)
            //{
            //    for (int axleIndex = 0; axleIndex < numAxles; axleIndex++)
            //    {
            //        ChAxle axle = axleList[axleIndex];
            //        foreach (var wheel in axle.GetWheels())
            //        {
            //            // this doesn't work yet
            //            ChVector3d omega = new ChVector3d(0, 0, initWheelAngVel);
            //            wheel.GetSpindle().SetAngVelLocal(omega);
            //        }
            //    }
            //}

            // Debug outputs
            //Debug.Log($"{name}: built JSON-based wheeled vehicle from {topLevelVehicleJSON} \n" +
            //          $"Engine loaded from: {engineJSON}\n" +
            //          $"Transmission loaded from: {transJSON}\n" +
            //          $"Number of axles {UChJSONVehicle.GetNumberAxles()} \n");

            return true;
        }
        catch (Exception e)
        {
            Debug.LogWarning($"{name}: Exception building from top-level => {e.Message}");
            UChJSONVehicle = null;
            return false;
        }
    }

    protected override void OnAdvance(double step)
    {
        if (UChJSONVehicle != null)
        {
            UpdateFromJsonVehicle(step);
        }
    }

    // synchronises the Chrono vehicle with inputs & terrain, and importantly,
    // iterates thourhg the gameobjects to update the Unity transforms for visualisation
    // (and unity interaction if desired)
    private void UpdateFromJsonVehicle(double step)
    {
        // Get vehicle position and rotation (same as gator.GetVehicle().GetPos())
        var pos = UChJSONVehicle.GetPos();
        var rot = UChJSONVehicle.GetRot();
        
        transform.position = Utils.FromChronoFlip(pos);
        transform.rotation = Utils.FromChronoFlip(rot);
        
        // Also update the chassis visual mesh if it exists as a child GameObject
        Transform chassisTransform = transform.Find("Chassis");
        if (chassisTransform != null)
        {
            chassisTransform.position = Utils.FromChronoFlip(pos);
            chassisTransform.rotation = Utils.FromChronoFlip(rot);
        }

        // for each axle update the sub-list in axleData[axleIndex].visualGameobjects
        var axleList = UChJSONVehicle.GetAxles();
        int axleCount = (int)UChJSONVehicle.GetNumberAxles();

        for (int axleIndex = 0; axleIndex < axleCount; axleIndex++)
        {
            ChAxle chronoAxle = axleList[axleIndex];
            var chronoWheels = chronoAxle.GetWheels();

            // Make sure we have an entry in axleData
            if (axleIndex >= axleData.Count)
                break;

            var unityWheelsForThisAxle = axleData[axleIndex].visualGameobjects;
            if (unityWheelsForThisAxle == null) continue;

            // only update as many wheels as Chrono says exist - not dictacted by Unity
            int count = Mathf.Min(chronoWheels.Count, unityWheelsForThisAxle.Count);
            for (int w = 0; w < count; w++)
            {
                GameObject unityWheelGO = unityWheelsForThisAxle[w];
                if (!unityWheelGO) continue;

                // use Chrono's correct spindle transform
                ChWheel cwheel = chronoWheels[w];
                VehicleSide side = cwheel.GetSide();

                var wheelPos = UChJSONVehicle.GetSpindlePos(axleIndex, side);
                var wheelRot = UChJSONVehicle.GetSpindleRot(axleIndex, side);

                unityWheelGO.transform.position = Utils.FromChronoFlip(wheelPos);
                unityWheelGO.transform.rotation = Utils.FromChronoFlip(wheelRot);
            }
        }

        // get chrono to update
        UChJSONVehicle.Synchronize(UChSystem.chrono_system.GetChTime(), inputs, chTerrain);
        UChJSONVehicle.Advance(step);
    }

    private string ResolveTirePath(int axleIndex, int wheelIndex, int globalWheelIndex)
    {
        string defaultTirePath = chrono_vehicle.GetVehicleDataFile(tireJSON);
        switch (tireAssignmentMode)
        {
            case TireAssignmentMode.SingleFile:
                if (string.IsNullOrEmpty(defaultTirePath))
                {
                    Debug.LogWarning($"{name}: Tire assignment mode is SingleFile but no default tire JSON is configured.");
                }
                return defaultTirePath;
            case TireAssignmentMode.PerAxleList:
                return ResolveTireOverrideFromList(axleIndex, $"axle #{axleIndex}", defaultTirePath);
            case TireAssignmentMode.PerWheelList:
                return ResolveTireOverrideFromList(globalWheelIndex, $"wheel #{globalWheelIndex}", defaultTirePath);
            default:
                return defaultTirePath;
        }
    }

    private string ResolveTireOverrideFromList(int listIndex, string label, string defaultTirePath)
    {
        string tirePath = string.Empty;

        if (listIndex < perAxleTireSpec.Count)
        {
            string userTireFile = perAxleTireSpec[listIndex];
            if (!string.IsNullOrEmpty(userTireFile))
            {
                tirePath = chrono_vehicle.GetVehicleDataFile(userTireFile);
            }
        }

        if (string.IsNullOrEmpty(tirePath))
        {
            if (!string.IsNullOrEmpty(defaultTirePath))
            {
                Debug.LogWarning($"{name}: Tire JSON entry missing for {label}. Falling back to {tireJSON}.");
                tirePath = defaultTirePath;
            }
            else
            {
                Debug.LogWarning($"{name}: Tire JSON entry missing for {label} and no default tire JSON is configured.");
            }
        }

        return tirePath;
    }

    public void RefreshTireOverridesFromInspector(bool forceReset)
    {
        int axleCount = axleData?.Count ?? 0;
        int wheelCount = 0;
        if (axleData != null)
        {
            foreach (var axle in axleData)
            {
                if (axle?.visualGameobjects != null)
                {
                    wheelCount += axle.visualGameobjects.Count;
                }
            }
        }

        EnsureTireOverrideEntries(axleCount, wheelCount, forceReset, tireJSON);
        UpdateTireOverrideStateHash(axleCount, wheelCount);
    }

    private void UpdateTireOverrideStateHash(int axleCount, int wheelCount)
    {
        tireOverrideStateHash = BuildTireOverrideStateHash(axleCount, wheelCount);
    }

    private bool IsTireOverrideStateCurrent(int axleCount, int wheelCount)
    {
        if (axleCount < 0 || wheelCount < 0)
        {
            return false;
        }

        string expectedHash = BuildTireOverrideStateHash(axleCount, wheelCount);
        return string.Equals(tireOverrideStateHash, expectedHash, StringComparison.Ordinal);
    }

    private string BuildTireOverrideStateHash(int axleCount, int wheelCount)
    {
        return string.Join("|",
            topLevelVehicleJSON ?? string.Empty,
            tireAssignmentMode.ToString(),
            axleCount,
            wheelCount,
            perAxleTireSpec?.Count ?? 0
        );
    }

    private void EnsureTireOverrideEntries(int expectedAxleCount, int expectedWheelCount, bool forceReset, string defaultEntry)
    {
        if (tireAssignmentMode == TireAssignmentMode.SingleFile)
        {
            if (forceReset)
            {
                perAxleTireSpec.Clear();
            }
            return;
        }

        int expectedEntries = tireAssignmentMode == TireAssignmentMode.PerAxleList ? expectedAxleCount : expectedWheelCount;
        expectedEntries = Mathf.Max(0, expectedEntries);

        if (expectedEntries == 0)
        {
            perAxleTireSpec.Clear();
            return;
        }

        if (string.IsNullOrEmpty(defaultEntry))
        {
            defaultEntry = string.Empty;
        }

        if (forceReset)
        {
            perAxleTireSpec.Clear();
            for (int i = 0; i < expectedEntries; i++)
            {
                perAxleTireSpec.Add(defaultEntry);
            }
            return;
        }

        if (perAxleTireSpec.Count > expectedEntries)
        {
            perAxleTireSpec.RemoveRange(expectedEntries, perAxleTireSpec.Count - expectedEntries);
        }

        while (perAxleTireSpec.Count < expectedEntries)
        {
            perAxleTireSpec.Add(defaultEntry);
        }
    }


    public override ChVehicle GetChVehicle()
    {
        return UChJSONVehicle;
    }

    public override ChPowertrainAssembly GetPowertrainAssembly()
    {
        return UChJSONVehicle?.GetPowertrainAssembly();
    }

    public override ChEngine GetEngine()
    {
        return UChJSONVehicle?.GetEngine();
    }

    public override ChTransmission GetTransmission()
    {
        return UChJSONVehicle?.GetTransmission();
    }

    public override double GetMaxSpeed()
    {
        // this might need adjusting by JSON rather than arbitrary hard coded value
        return 33.3;
    }
}
