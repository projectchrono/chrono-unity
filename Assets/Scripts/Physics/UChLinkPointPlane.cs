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

    public Vector3 pointOnBody1;
    [Tooltip("The plane localtion and normal are relative to body 2, and importantly, will use that body's axes also.")]
    public Vector3 planeLocalBody2;
    public Vector3 planeNormalBody2;
    public bool showNormalGizmo = true;

    ChLinkLockPointPlane link = new ChLinkLockPointPlane();

    void Start()
    {
        // catch error to halt crashes when trying to run with no bodies attached.
        if (pointBody1 == null || planeBody2 == null || !pointBody1.isActiveAndEnabled || !planeBody2.isActiveAndEnabled)
        {
            Debug.LogError("UChLinkPointPlane requires two UChBody objects assigned.");
            return;
        }

        // ChFrame setupfor the point
        ChVector3d pointLocation1 = Utils.ToChrono(pointOnBody1);
        // No meaningful rotation for the point
        ChQuaterniond pointRotation1 = Utils.ToChrono(Quaternion.identity);

        // Chframe for the plane
        // Use local normal to define the plane's rotation
        Quaternion planeLocalRotation = Quaternion.LookRotation(planeNormalBody2, Vector3.up);

        // Convert local plane position and rotation to Chrono
        ChVector3d planeLocation2 = Utils.ToChrono(planeLocalBody2);
        ChQuaterniond planeRotation = Utils.ToChrono(planeLocalRotation);

        // Set the frames
        var frame1 = new ChFramed(pointLocation1, pointRotation1);
        var frame2 = new ChFramed(planeLocation2, planeRotation);

        // Initialise and add the link to main system
        link.Initialize(pointBody1.GetChBody(), planeBody2.GetChBody(), true, frame1, frame2);
        UChSystem.chrono_system.AddLink(link);
    }

    // Draw the objects in the editor so we can see what's being set up for this link
    void OnDrawGizmos()
    {
        // ensure no zero calls to lookRotation. i.e don't draw if user has put (0,0,0)
        if (planeBody2 == null || planeNormalBody2 == Vector3.zero)
        {
            return;
        }
        if (!showNormalGizmo)
            return;

        // To ensure the 'relative' units entered aren't incorrectly visualised by unity (as comapared to chrono), we need to scale them a bit (can be checked with Abs spring visualisation at runtime)
        // 1. convert local points to "scale-neutral" local points
        Vector3 adjustedPosition1 = new Vector3(pointOnBody1.x / pointBody1.transform.lossyScale.x,
                                                pointOnBody1.y / pointBody1.transform.lossyScale.y,
                                                pointOnBody1.z / pointBody1.transform.lossyScale.z);
        // 2. transform these adjusted points into world space for correct visualisation
        Vector3 pointWorldPosition = pointBody1.transform.TransformPoint(adjustedPosition1);

        // Draw the point
        Gizmos.color = Color.blue;
        float pointSize = 0.1f;
        Gizmos.DrawSphere(pointWorldPosition, pointSize);

        // Same again for the plane location (to offset any local body scaling)
        Vector3 adjustedPlanePosition = new Vector3(planeLocalBody2.x / planeBody2.transform.lossyScale.x,
                                            planeLocalBody2.y / planeBody2.transform.lossyScale.y,
                                            planeLocalBody2.z / planeBody2.transform.lossyScale.z);
        Vector3 worldPlaneLocation = planeBody2.transform.TransformPoint(adjustedPlanePosition);

        // Use the local normal to define the plane's rotation in local space
        Quaternion planeLocalRotation = Quaternion.LookRotation(planeNormalBody2, Vector3.up);
        Quaternion worldPlaneRotation = planeBody2.transform.rotation * planeLocalRotation;

        // Draw the plane   
        Gizmos.color = Color.cyan;
        Vector3 planeScale = new Vector3(1.5f, 1.5f, 0.05f);  // Make the plane appear as a thin rectangle
        Gizmos.matrix = Matrix4x4.TRS(worldPlaneLocation, worldPlaneRotation, planeScale);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        Gizmos.matrix = Matrix4x4.identity;  // Reset the Gizmos matrix after drawing

        // Draw a line to visually indicate the normal
        Gizmos.color = Color.yellow;
        Vector3 worldPlaneNormal = planeBody2.transform.TransformDirection(planeNormalBody2);
        Vector3 normalLineEnd = worldPlaneLocation + worldPlaneNormal * 1.5f;  // Extend the normal out from the plane 1.5
        Gizmos.DrawLine(worldPlaneLocation, normalLineEnd);
    }

}
