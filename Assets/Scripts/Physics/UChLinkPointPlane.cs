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

    void Start()
    {   // ChFrame for the point
        ChVector3d pointLocation1 = Utils.ToChrono(pointBody1.transform.TransformPoint(pointLocalPositionOnBody1)); // convert the local point coords to world, to chrono
        ChQuaterniond pointRotation1 = Utils.ToChrono(Quaternion.identity); // no meaningful rotation for the point

        // Set Rotation of the plane
        // Ensure relationship with local body
        Quaternion alignToNormal = Quaternion.LookRotation(planeNormalFromBody2, planeBody2.transform.up);
        planeBody2.transform.localRotation = alignToNormal;

        // Convert the plane rotation to Chrono
        ChQuaterniond planeRotation = Utils.ToChrono(alignToNormal);
        ChVector3d planeLocation2 = Utils.ToChrono(planePointLocalOnBody2);

        // Set the frames
        var frame1 = new ChFramed(pointLocation1, pointRotation1);
        var frame2 = new ChFramed(planeLocation2, planeRotation);

        // Initialise and add the link to main system
        var link = new ChLinkLockPointPlane();
        link.Initialize(pointBody1.GetChBody(), planeBody2.GetChBody(), false, frame1, frame2);
        UChSystem.chrono_system.AddLink(link);
    }

    // Draw the objects in the editor so we can see what's being set up for this link
    void OnDrawGizmos()
    {
        // ensure no zero calls to lookRotation. i.e don't draw if user has put (0,0,0)
        if (planeBody2 == null || planeNormalFromBody2 == Vector3.zero)
        {
            return;
        }
        // Set the plane normal direction (accoutning for RHF)
        Quaternion planeRotation = Quaternion.LookRotation(planeNormalFromBody2, planeBody2.transform.up) * Quaternion.Euler(-90,0,0);

        // Draw the point
        Vector3 pointWorldPosition = pointBody1.transform.TransformPoint(pointLocalPositionOnBody1);
        Gizmos.color = Color.blue;
        float pointSize = 0.1f;
        Gizmos.DrawSphere(pointWorldPosition, pointSize);

        // Draw plane
        Gizmos.color = Color.cyan;
        Vector3 planeScale = new Vector3(10f, 0.01f, 10f);
        Gizmos.matrix = Matrix4x4.TRS(planePointLocalOnBody2, planeRotation, planeScale);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        Gizmos.matrix = Matrix4x4.identity; //reset

        // Draw a normal line
        Gizmos.color = Color.yellow;
        Vector3 lineEnd = planePointLocalOnBody2 - planeRotation * Vector3.up * 2f; // Calculate end point (negated to point in the Unity axis direction)
        Gizmos.DrawLine(planePointLocalOnBody2, lineEnd); // Draw line from centre of plane

    }

}
