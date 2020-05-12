using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UChDriver : MonoBehaviour
{
    private double steering;
    private double throttle;
    private double braking;

    private double steering_desired;
    private double throttle_desired;
    private double braking_desired;

    private double steering_delta;
    private double throttle_delta;
    private double braking_delta;

    private double steering_gain;
    private double throttle_gain;
    private double braking_gain;

    private double step;

    public UChVehicle vehicle;

    void Start()
    {
        step = UChSystem.chrono_system.GetStep();

        steering = 0;
        throttle = 0;
        braking = 0;

        steering_desired = 0;
        throttle_desired = 0;
        braking_desired = 0;

        steering_gain = 4;
        throttle_gain = 4;
        braking_gain = 4;

        steering_delta = step / 1;
        throttle_delta = step / 4;
        braking_delta = step / 4;
    }

    void FixedUpdate()
    {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        // Set desired steering, throttle, and braking targets
        if (horiz > 0)
        {
            steering_desired = Utils.Clamp(steering_desired + steering_delta, -1, 1);
        } else if (horiz < 0)
        {
            steering_desired = Utils.Clamp(steering_desired - steering_delta, -1, 1);
        }
        if (vert > 0)
        {
            throttle_desired = Utils.Clamp(throttle_desired + throttle_delta, 0, 1);
            if (throttle_desired > 0)
            {
                braking_desired = Utils.Clamp(braking_desired - 3 * braking_delta, 0, 1);
            }
        } else if (vert < 0)
        {
            throttle_desired = Utils.Clamp(throttle_desired - 3 * throttle_delta, 0, 1);
            if (throttle_desired == 0)
            {
                braking_desired = Utils.Clamp(braking_desired + braking_delta, 0, 1);
            }
        }

        // Integrate dynamics
        steering += step * steering_gain * (steering_desired - steering);
        throttle += step * throttle_gain * (throttle_desired - throttle);
        braking += step * braking_gain * (braking_desired - braking);

        //Debug.Log(throttle + "        " + braking);

        vehicle.SetDriverInputs(steering, throttle, braking);
    }

    // When attaching to a Game Object, hide the transform
    void OnValidate()
    {
        transform.hideFlags = HideFlags.NotEditable | HideFlags.HideInInspector;
    }
}
