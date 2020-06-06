using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitialization : MonoBehaviour
{
    private Vector3 cameraHomePos;
    private Quaternion cameraHomeRot;

    void Start()
    {
        // Get the position of the containing camera object
        cameraHomePos = transform.position;
        cameraHomeRot = transform.rotation;

        // Pass this information to the chase camera (as its home position)
        CameraFollower chaseCam = FindObjectOfType<CameraFollower>();
        chaseCam.SetHomePosition(cameraHomePos, cameraHomeRot);

        // Deactivate the containing camera object
        gameObject.SetActive(false);
    }
}
