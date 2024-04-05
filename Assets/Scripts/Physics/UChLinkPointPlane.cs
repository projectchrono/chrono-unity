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

using UnityEngine;

public class UChLinkPointPlane : MonoBehaviour
{
    public UChBody pointBody1; // The body with the point
    public UChBody planeBody2; // The body with the plane

    public Vector3 pointLocalPositionOnBody1;
    public Vector3 planePointLocalOnBody2;
    public Vector3 planeNormalFromBody2;

    ChLinkLockPointPlane link = new ChLinkLockPointPlane();

    void Start()
    {   // ChFrame for the point
        // Convert the local point coordinates to world coordinates, then to Chrono
        ChVector3d pointLocation1 = Utils.ToChrono(pointBody1.transform.TransformPoint(pointLocalPositionOnBody1));
        // No meaningful rotation for the point
        ChQuaterniond pointRotation1 = Utils.ToChrono(Quaternion.identity);

        // Chframe for the plane
        // Modify rotation from local normal to world normal in Unity, ensuring it's oriented along the body's local axes  (using forward vector)
        Quaternion planeWorldRotation = planeBody2.transform.rotation * Quaternion.FromToRotation(Vector3.forward, planeNormalFromBody2);
        // Positioning the plane in world space, ensuring it uses standard Unity units for its position
        Vector3 planeWorldPositionUnity = planeBody2.transform.position + planeBody2.transform.TransformDirection(planePointLocalOnBody2);

        // Convert world coordinates to Chrono
        ChVector3d planeLocation2 = Utils.ToChrono(planeWorldPositionUnity);
        ChQuaterniond planeRotation = Utils.ToChrono(planeWorldRotation);

        // Set the frames
        var frame1 = new ChFramed(pointLocation1, pointRotation1);
        var frame2 = new ChFramed(planeLocation2, planeRotation);

        // Initialise and add the link to main system
        link.Initialize(pointBody1.GetChBody(), planeBody2.GetChBody(), false, frame1, frame2);
        UChSystem.chrono_system.AddLink(link);

    }

    // Draw the objects in the editor so we can see what's being set up for this link
    // This has duplicate calcs as above, but in order to see orientations in the Editor, it seems necessary
    void OnDrawGizmos()
    {
        // ensure no zero calls to lookRotation. i.e don't draw if user has put (0,0,0)
        if (planeBody2 == null || planeNormalFromBody2 == Vector3.zero)
        {
            return;
        }

        // Draw the point
        Vector3 pointWorldPosition = pointBody1.transform.TransformPoint(pointLocalPositionOnBody1);
        Gizmos.color = Color.blue;
        float pointSize = 0.1f;
        Gizmos.DrawSphere(pointWorldPosition, pointSize);

        // Calculate the world space position of the plane point using the similar logic as abov in Start()
        Vector3 planeWorldPosition = planeBody2.transform.position + planeBody2.transform.TransformDirection(planePointLocalOnBody2);
        // Calculate the world space direction of the plane normal
        Vector3 planeNormalWorld = planeBody2.transform.TransformDirection(planeNormalFromBody2);
        // Modify rotation from local normal to world normal in Unity, ensuring it's oriented along the body's local axes (use 'up' vector for correct orientation)
        Quaternion planeRotation = planeBody2.transform.rotation * Quaternion.FromToRotation(Vector3.up, planeNormalFromBody2);

        // Draw plane
        Gizmos.color = Color.cyan;
        Vector3 planeScale = new Vector3(1.5f, 0.01f, 1.5f);
        Gizmos.matrix = Matrix4x4.TRS(planeWorldPosition, planeRotation, planeScale);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        Gizmos.matrix = Matrix4x4.identity; //reset

        // Draw a normal line
        Gizmos.color = Color.yellow;
        Vector3 lineEnd = planeWorldPosition + planeNormalWorld * 0.7f; // Calculate end point (negated to point in the Unity axis direction)
        Gizmos.DrawLine(planeWorldPosition, lineEnd); // Draw line from centre of plane

    }

    //private void FixedUpdate()
    //{
    //    // Debug Feedback
    //    Vector3 locationPoint = Utils.FromChrono(link.GetFrame1Abs().GetPos());
    //    Quaternion rotationPoint = Utils.FromChrono(link.GetFrame1Abs().GetRot());
    //    Debug.Log("ChLink Point location " + locationPoint + " ChLink Point Rotation " + rotationPoint);
    //    Vector3 locationPlane = Utils.FromChrono(link.GetFrame2Rel().GetPos());
    //    Quaternion rotationPlane = Utils.FromChrono(link.GetFrame2Rel().GetRot());
    //    Debug.Log("ChLink Plane relative location " + locationPlane + " ChLink Plane Rotation (relative) " + rotationPlane.eulerAngles);
    //}

}
