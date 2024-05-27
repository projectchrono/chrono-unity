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
// Authors: Radu Serban, Josh Diyn
// =============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UChJointDistance : MonoBehaviour
{
    public UChBody body1;
    public UChBody body2;

    public bool useRelativePos = true;
    public Vector3 position1;
    public Vector3 position2;

    [SerializeField] private float currentCalculatedDistance;

    public float lineScale = 0.1f;

    private ChLinkDistance joint;
    private LineRenderer lineObject;

    void Start()
    {
        // catch error to halt crashes when trying to run with no bodies attached.
        if (body1 == null || body2 == null)
        {
            Debug.LogError("UChJointDistance requires two UChBody objects assigned.");
            return;
        }
        joint = new ChLinkDistance();
        joint.Initialize(body1.GetChBody(), body2.GetChBody(), useRelativePos, Utils.ToChrono(position1), Utils.ToChrono(position2)); // default setting is an auto distance and bilateral constraint

        UChSystem.chrono_system.AddLink(joint);
        joint.Setup(); // recompute

        lineObject = gameObject.AddComponent<LineRenderer>();
        lineObject.material = new Material(Shader.Find("Sprites/Default"));
        lineObject.sortingOrder = -1;
        lineObject.startColor = new Color(0.4f, 0, 0);
        lineObject.endColor = new Color(0.4f, 0, 0);
        lineObject.widthMultiplier = lineScale;
        lineObject.positionCount = 2;
        lineObject.alignment = LineAlignment.View;
    }

    void Update()
    {
        var p1 = Utils.FromChrono(joint.GetEndPoint1Abs());
        var p2 = Utils.FromChrono(joint.GetEndPoint2Abs());

        // Update line in game view
        lineObject.SetPosition(0, p1);
        lineObject.SetPosition(1, p2);
    }

    private void FixedUpdate()
    {
        // ensure in step with the rest of the physics system
        joint.SyncCollisionModels();
        joint.Update(UChSystem.chrono_system.GetChTime(), true);

    }

    // When attaching to a Game Object, hide the transform
    void OnValidate()
    {
        transform.hideFlags = HideFlags.NotEditable | HideFlags.HideInInspector;
    }

    void OnDrawGizmos()
    {
        if (useRelativePos)
        {
            // To ensure the 'relative' units entered aren't incorrectly visualised by unity (as comapared to chrono)
            // 1. convert local points to "scale-neutral" local points
            Vector3 adjustedPosition1 = new Vector3(position1.x / body1.transform.lossyScale.x, position1.y / body1.transform.lossyScale.y, position1.z / body1.transform.lossyScale.z);
            Vector3 adjustedPosition2 = new Vector3(position2.x / body2.transform.lossyScale.x, position2.y / body2.transform.lossyScale.y, position2.z / body2.transform.lossyScale.z);

            // Convert local positions to world positions
            Vector3 worldPositionOnBody1 = body1.transform.TransformPoint(adjustedPosition1);
            Vector3 worldPositionOnBody2 = body2.transform.TransformPoint(adjustedPosition2);
            Gizmos.DrawLine(worldPositionOnBody1, worldPositionOnBody2);
            // Calculate and assign the line length
            currentCalculatedDistance = Vector3.Distance(worldPositionOnBody1, worldPositionOnBody2);
        }
        else
        {
            Gizmos.DrawLine(position1, position2);
            currentCalculatedDistance = Vector3.Distance(position1, position2);
        }
    }
}
