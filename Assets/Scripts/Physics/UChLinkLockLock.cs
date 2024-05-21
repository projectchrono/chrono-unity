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
// Authors: Josh Diyn
// =============================================================================

// TODO:  This is a WIP
using UnityEngine;

public class UChLinkLockLock : MonoBehaviour
{
    public UChBody body1;
    public UChBody body2;

    // Define local positions and orientations for the initial relative configuration
    // of body1 and body2. These are used to define the initial frames of the link.
    public Vector3 localPositionOnBody1 = Vector3.zero;
 //   public Quaternion localRotationOnBody1 = Quaternion.identity;
    public Vector3 localPositionOnBody2 = Vector3.zero;
 //   public Quaternion localRotationOnBody2 = Quaternion.identity;

    public bool useRelativePositions;

    // Motion restriction tick boxes
    public bool lockMotionX = false;
    public bool lockMotionY = false;
    public bool lockMotionZ = false;
    public bool lockRotationX = false;
    public bool lockRotationY = false;
    public bool lockRotationZ = false;

    private ChLinkLockLock link;

    void Start()
    {
        if (body1 == null || body2 == null)
        {
            Debug.LogError("UChLinkLockLock requires two UChBody objects assigned.");
            return;
        }

        // Convert the local position and rotation to Chrono types for body1
        ChVector3d positionBody1 = Utils.ToChrono(body1.transform.TransformPoint(localPositionOnBody1));
        ChQuaterniond rotationBody1 = Utils.ToChrono(body1.transform.rotation);

        // Convert the local position and rotation to Chrono types for body2
        ChVector3d positionBody2 = Utils.ToChrono(body2.transform.TransformPoint(localPositionOnBody2));
        ChQuaterniond rotationBody2 = Utils.ToChrono(body2.transform.rotation);


        // Define the initial frames for the link based on the specified local positions and rotations
        ChFrameMovingd frame1 = new ChFrameMovingd(positionBody1, rotationBody1);
        ChFrameMovingd frame2 = new ChFrameMovingd(positionBody2, rotationBody2);


        // Initialise the link
        link = new ChLinkLockLock();

        // Apply motion restrictions based on tick boxes
        ApplyMotionRestrictions();

        link.Initialize(body1.GetChBody(), body2.GetChBody(), useRelativePositions, frame1, frame2);
        // Add the link to the Chrono system
        UChSystem.chrono_system.AddLink(link);
    }

    void OnDrawGizmos()
    {
        if (body1 == null || body2 == null)
        {
            return;
        }
        Gizmos.color = Color.blue;
        var position1 = localPositionOnBody1;
        var position2 = localPositionOnBody2;

        if (useRelativePositions)
        {
            // To ensure the 'relative' units entered aren't incorrectly visualised by unity (as comapared to chrono)
            // 1. convert local points to "scale-neutral" local points
            Vector3 adjustedPosition1 = new Vector3(position1.x / body1.transform.lossyScale.x, position1.y / body1.transform.lossyScale.y, position1.z / body1.transform.lossyScale.z);
            Vector3 adjustedPosition2 = new Vector3(position2.x / body2.transform.lossyScale.x, position2.y / body2.transform.lossyScale.y, position2.z / body2.transform.lossyScale.z);

            // Convert local positions to world positions
            Vector3 worldPositionOnBody1 = body1.transform.TransformPoint(adjustedPosition1);
            Vector3 worldPositionOnBody2 = body2.transform.TransformPoint(adjustedPosition2);
            Gizmos.DrawLine(worldPositionOnBody1, worldPositionOnBody2);
        }
        else
        {
            Gizmos.DrawLine(position1, position2);
        }

    }

    void ApplyMotionRestrictions()
    {
        // Lock motion in X direction by setting the limit around the current position to 0/0
        if (lockMotionX)
        {
            var limitX = link.LimitX();
            limitX.SetActive(true); // Activate the limit
            limitX.SetMin(0); // Set minimum limit 0
            limitX.SetMax(0); // Set maximum limit 0
        }

        if (lockMotionY)
        {
            var limitY = link.LimitY();
            limitY.SetActive(true);
            limitY.SetMin(0);
            limitY.SetMax(0);
        }

        if (lockMotionZ)
        {
            var limitZ = link.LimitZ();
            limitZ.SetActive(true);
            limitZ.SetMin(0);
            limitZ.SetMax(0);
        }

        // Lock rotation around the X axis
        if (lockRotationX)
        {
            var limitRx = link.LimitRx();
            limitRx.SetActive(true);
            limitRx.SetMin(0);
            limitRx.SetMax(0);
        }

        // Lock rotation around the Y axis
        if (lockRotationY)
        {
            var limitRy = link.LimitRy();
            limitRy.SetActive(true);
            limitRy.SetMin(0);
            limitRy.SetMax(0);
        }

        // Lock rotation around the Z axis
        if (lockRotationZ)
        {
            var limitRz = link.LimitRz();
            limitRz.SetActive(true);
            limitRz.SetMin(0);
            limitRz.SetMax(0);
        }

    }

}
