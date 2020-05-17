using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.1f;

    [Range (-2.0f, 10.0f)]
    public float cameraDistance = 4;

    [Range(0.0f, 10.0f)]
    public float cameraHeight = 3;

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + target.right * (-cameraDistance) + target.up * cameraHeight;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        Quaternion desiredRotation = target.rotation * Quaternion.Euler(0, 90, 0);
        Quaternion smoothedRotation = Quaternion.Lerp(transform.rotation, desiredRotation, smoothSpeed);
        transform.rotation = smoothedRotation;
    }
}
