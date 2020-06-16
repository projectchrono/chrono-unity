using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class IMU : ICommandable
{
    public UChVehicle vehicle;                     // associated vehicle
    public Vector3 sensorLocation = Vector3.zero;  // relative to vehicle chassis reference frame

    public double accelNoiseMean = 0.0;
    public double accelNoiseStdDev = 0.001;

    public double omegaNoiseMean = 0.0;
    public double omegaNoiseStdDev = 0.001;

    public bool filterAcceleration = true;  // filter Chrono acceleration?
    public float filterWindow = 0.25f;      // running filter window (in seconds)

    private ChRunningAverage accFilterX;
    private ChRunningAverage accFilterY;
    private ChRunningAverage accFilterZ;

    public bool output = true;        // output data to file?
    private StreamWriter outputFile;  // output file

    private System.Random rand = new System.Random();

    private const uint type = 2;  // IMU sensor type code

    double GaussianNoise(double mean, double stdDev)
    {
        double u1 = 1.0 - rand.NextDouble();
        double u2 = 1.0 - rand.NextDouble();
        double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
        double randNormal = mean + stdDev * randStdNormal;
        return randNormal;
    }

    Vector3 RandomVec(double mean, double stdDev)
    {
        return new Vector3((float)GaussianNoise(mean, stdDev),
                           (float)GaussianNoise(mean, stdDev),
                           (float)GaussianNoise(mean, stdDev));
    }

    public override void OnStart()
    {
        // Set up running average filters for acceleraiton components
        int filterSteps = Mathf.RoundToInt(filterWindow / Time.fixedDeltaTime);
        accFilterX = new ChRunningAverage(filterSteps);
        accFilterY = new ChRunningAverage(filterSteps);
        accFilterZ = new ChRunningAverage(filterSteps);

        // Output file
        if (output)
        {
            string fileName = Application.persistentDataPath + "/" + "IMU.dat";
            Debug.Log("IMU output file: " + fileName);
            outputFile = File.CreateText(fileName);
        }
    }

    void FixedUpdate()
    {
        long timestamp = time_server.GetPhysicsTicks();

        // Linear acceleration at the sensor location
        Vector3 accel_raw = vehicle.GetAccelerationLocal(sensorLocation);
        Vector3 accel = accel_raw;
        if (filterAcceleration)
        {
            double accX = accFilterX.Add(accel_raw.x);
            double accY = accFilterY.Add(accel_raw.y);
            double accZ = accFilterZ.Add(accel_raw.z);
            accel = new Vector3((float)accX, (float)accY, (float)accZ);
        }
        Vector3 noisy_accel = accel + RandomVec(accelNoiseMean, accelNoiseStdDev);

        // Angular velocity of chassis body
        Vector3 omega = vehicle.GetWvelLocal();
        Vector3 noisy_omega = omega + RandomVec(omegaNoiseMean, omegaNoiseStdDev);

        SendHeader(type, full_name, timestamp);
        SendData(noisy_accel);
        SendData(noisy_omega);
        SendData(transform.rotation);

        if (output)
        {
            outputFile.WriteLine(
                timestamp + " " +                                            // time stamp
                accel_raw.x + " " + accel_raw.y + " " + accel_raw.z + " " +  // acceleration from Chrono simulation
                accel.x + " " + accel.y + " " + accel.z + " " +              // filtered acceleration
                noisy_accel.x + " " + noisy_accel.y + " " + noisy_accel.z    // acceleration with IMU Gaussian noise
                );
        }
    }

    public void OnDestroy()
    {
        if (output)
            outputFile.Close();
    }
}
