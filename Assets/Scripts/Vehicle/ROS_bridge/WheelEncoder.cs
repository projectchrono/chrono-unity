using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelEncoder : ICommandable
{
    public UChVehicle vehicle;  // associated vehicle

    string frame_id = "base_link";        //// what should this be?
    string child_frame_id = "base_link";  //// what should this be?

    public const uint type = 13;  // WheelEncoder sensor type code

    void FixedUpdate()
    {
        long timestamp = time_server.GetPhysicsTicks();
        float speed = (float)vehicle.GetSpeed();

        ////Debug.Log("speed = " + speed);
        
        SendHeader(type, full_name, timestamp);
        SendData(frame_id);
        SendData(child_frame_id);
        SendData(speed);
        //ASSUMPTION: the covariance is 10% of velocity squared
        SendData(Mathf.Pow(speed*0.10f,2));
        
    }
}
