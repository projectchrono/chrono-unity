using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Determine what to trigger
public enum PathFollowingOption
{
    EnablePathFollowing,
    Restore
}


public class PathFollowTrigger : MonoBehaviour
{
    public bool enablePathFollowing = true; // Set true to enable path following, false to revert to normal driving
    public PathFollowingOption pathFollowingOption;

    public UChVehiclePath pathFollowerObject; // Drag the path follower object here in the Inspector
    public bool restore = false; // restore the previous state when switching
    public double pathTargetSpeed = 10.0;     // Set the desired speed
    public double lookAhead;   // look ahead distance

    // Control gains
    public double steeringKp = 0.8;
    public double steeringKd = 0.0;
    public double steeringKi = 0.0;
    public double speedKp = 0.6;
    public double speedKd = 0.2;
    public double speedKi = 0.1;

    // Variables to save previous state
    private Driver.SteeringMode prevSteeringMode;
    private Driver.SpeedMode prevSpeedMode;

    private void OnTriggerEnter(Collider other)
    {
        UChVehicle vehicle = other.GetComponent<UChVehicle>();
        if (vehicle != null)
        {
            Driver driver = vehicle.GetComponent<Driver>();
            if (driver != null)
            {
                if (enablePathFollowing)
                {
                    // Save previous state
                    prevSteeringMode = driver.steeringMode;
                    prevSpeedMode = driver.speedMode;
                    // Shouldn't need to save the gains, unless the user explicitly desires past gains to remain

                    // Set path-following mode and control gains
                    SetPathFollowingMode(driver);
                }
                else if (pathFollowingOption == PathFollowingOption.Restore)
                {
                    // Revert to saved state
                    driver.steeringMode = prevSteeringMode;
                    driver.speedMode = prevSpeedMode;
                    driver.targetSpeed = 0 / 3.6; // exit the trigger point and hand back control, with 0km/h
                }
            }
        }
    }

    // Each may have specific requirements for gains and speed
    private void SetPathFollowingMode(Driver driver)
    {
        driver.steeringKp = steeringKp;
        driver.steeringKd = steeringKd;
        driver.steeringKi = steeringKi;
        driver.lookAhead = lookAhead;
        driver.speedKp = speedKp;
        driver.speedKd = speedKd;
        driver.speedKi = speedKi;
        driver.targetSpeed = pathTargetSpeed / 3.6;
        driver.steeringMode = Driver.SteeringMode.PathFollower;
        driver.speedMode = Driver.SpeedMode.CruiseControl;
        driver.path = pathFollowerObject.GetComponent<UChVehiclePath>();
        Debug.Log("Path following mode enabled. Target speed set to: " + pathTargetSpeed);
    }


}
