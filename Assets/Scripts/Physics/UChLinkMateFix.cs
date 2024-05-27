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

using UnityEngine;

public class UCHLinkMateFix : MonoBehaviour
{
    public UChBody body1;
    public UChBody body2;

    private bool useRelativePositions = true;

    // Define local positions and orientations for the initial relative configuration
    // of body1 and body2. These are used to define the initial frames of the link.
    public Vector3 positionOnBody1 = Vector3.zero;
    public Vector3 positionOnBody2 = Vector3.zero;

    private ChLinkMateFix link;

    void Start()
    {
        if (body1 == null || body2 == null ||
            !body1.gameObject.activeInHierarchy || !body2.gameObject.activeInHierarchy ||
            !body1.enabled || !body2.enabled)
        {
            Debug.LogError("UChLinkMateFix requires two UChBody objects assigned.");
            return;
        }


        // Run Quat calculations in order to set the primary (controlling) alignment of the Mate Fix using body1's rotation
        // This is done to avoid rotating body2 to force the axial alignment between them. Instead, body2 can be set in its existing
        // Unity rotation and the calcs below should determine the difference and ensure the frame applied is the inverse
        Quaternion currentRotationBody1 = body1.transform.rotation;
        Quaternion currentRotationBody2 = body2.transform.rotation;
        // Calculate the intended rotation of body2 relative to body1 if they were forced
        Quaternion desiredRotationBody2RelativeToBody1 = Quaternion.Inverse(currentRotationBody1) * currentRotationBody2;
        // Adjustment is essentially the inverse of the desired relative rotation, so that body2 is not forced to align axes with body1
        Quaternion adjustment = Quaternion.Inverse(desiredRotationBody2RelativeToBody1);
        ChQuaterniond chronoAdjustment = Utils.ToChrono(adjustment);

        // Now locations
        // Calculate the relative position vectors directly, without adding the body's global position
        ChVector3d relativeVector1 = Utils.ToChrono(positionOnBody1);
        ChVector3d relativeVector2 = Utils.ToChrono(positionOnBody2);

        // Create the frames for the points based solely on their relative positions.
        // set the rotation as QUNIT. Mate will rotate the body to align the axes. Adjust to suit
        ChFramed frame1 = new ChFramed(relativeVector1, chrono.QUNIT);
        ChFramed frame2 = new ChFramed(relativeVector2, chronoAdjustment);

        // Initialise the link
        link = new ChLinkMateFix();
        // custom UpcastToChBodyFrame() call to workaround the swig polymorphism inhertiance issues
        link.Initialize(chrono.CastToChBodyFrame(body1.GetChBody()), chrono.CastToChBodyFrame(body2.GetChBody()), useRelativePositions, frame1, frame2);

        // Add the link to the Chrono system
        UChSystem.chrono_system.AddLink(link);
        link.Setup(); // necessary?
    }

    void FixedUpdate()
    {
        // ensure in step with the rest of the physics system
        link.SyncCollisionModels();
        link.Update(UChSystem.chrono_system.GetChTime(), true);

        //Debug.Log("link 1 position: x:" + link.GetFrame1Abs().GetPos().x + " y: " + link.GetFrame1Abs().GetPos().y + " z: " + link.GetFrame1Abs().GetPos().z);
        //Debug.Log("link 2 position: x:" + link.GetFrame2Abs().GetPos().x + " y: " + link.GetFrame2Abs().GetPos().y + " z: " + link.GetFrame2Abs().GetPos().z);
    }

    void OnDrawGizmos()
    {
        if (body1 == null || body2 == null)
        {
            return;
        }

        // To ensure the 'relative' units entered aren't incorrectly visualised by unity (as comapared to chrono)
        // 1. convert local points to "scale-neutral" local points
        Vector3 adjustedPosition1 = new Vector3(positionOnBody1.x / body1.transform.lossyScale.x, positionOnBody1.y / body1.transform.lossyScale.y, positionOnBody1.z / body1.transform.lossyScale.z);
        Vector3 adjustedPosition2 = new Vector3(positionOnBody2.x / body2.transform.lossyScale.x, positionOnBody2.y / body2.transform.lossyScale.y, positionOnBody2.z / body2.transform.lossyScale.z);

        // Convert local positions to world positions
        Vector3 worldPositionOnBody1 = body1.transform.TransformPoint(adjustedPosition1);
        Vector3 worldPositionOnBody2 = body2.transform.TransformPoint(adjustedPosition2);

        // Set Gizmos color for visibility
        Gizmos.color = Color.blue;

        // Draw line between the specified positions on each body
        Gizmos.DrawLine(worldPositionOnBody1, worldPositionOnBody2);

        // Set Gizmos color for the spheres, if you want them a different color
        Gizmos.color = Color.red;

        // Draw spheres at the specified positions on each body
        // Can watch the Mate joining these in the scene view at runtime.
        Gizmos.DrawSphere(worldPositionOnBody1, 0.05f);
        Gizmos.DrawSphere(worldPositionOnBody2, 0.05f);
    }


}
