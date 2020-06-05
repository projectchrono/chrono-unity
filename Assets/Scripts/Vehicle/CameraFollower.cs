using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour, IAdvance
{
    public UChVehicle target;

    public bool worldUp;

    [Range (0.0f, 1.0f)]
    public float smoothSpeed = 0.1f;

    [Range (-2.0f, 10.0f)]
    public float cameraDistance = 4;

    [Range(0.0f, 10.0f)]
    public float cameraHeight = 3;

    public void Start()
    {
        // Register with the Chrono system (for Advance).
        UChSystem system = (UChSystem)FindObjectOfType(typeof(UChSystem));
        system.Register(gameObject.name, this);
    }

    public void Advance(double step)
    {
        ////Debug.Log("advance ChaseCam. step = " + step);

        //// TODO: worldUp option requires more work...

        var target_xform = target.transform;

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
}
