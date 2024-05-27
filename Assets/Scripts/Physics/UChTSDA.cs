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

public class UChTSDA : MonoBehaviour
{
    public UChBody body1;
    public UChBody body2;

    public bool useRelativePos = true;
    public Vector3 position1;
    public Vector3 position2;

    public double springCoefficient = 0;
    public double dampingCoefficient = 0;
    public double actuatorForce = 0;

    public bool autoRestLength = false;
    public double restLength = 0;

    private ChLinkTSDA spring;
    private LineRenderer lineObject;

    [Header("Helical Spring Visual Parameters")]
    public float lineScale = 0.05f;    
    public float springRadius = 0.1f;
    public int turns = 15;
    public int pointsPerTurn = 10;

    void Start()
    {
        if (body1 == null || body2 == null ||
            !body1.gameObject.activeInHierarchy || !body2.gameObject.activeInHierarchy ||
            !body1.enabled || !body2.enabled)
        {
            Debug.LogError("UChTSDA requires two UChBody objects assigned.");
            return;
        }

        spring = new ChLinkTSDA();
        spring.Initialize(body1.GetChBody(), body2.GetChBody(), 
                          useRelativePos, Utils.ToChrono(position1), Utils.ToChrono(position2));
        spring.SetSpringCoefficient(springCoefficient);
        spring.SetDampingCoefficient(dampingCoefficient);
        spring.SetActuatorForce(actuatorForce);
        if (autoRestLength)
            spring.SetRestLength(restLength);

        UChSystem.chrono_system.AddLink(spring);

        lineObject = gameObject.AddComponent<LineRenderer>();
        lineObject.material = new Material(Shader.Find("Sprites/Default"));
        lineObject.sortingOrder = -1;
        lineObject.startColor = new Color(0, 0.4f, 0);
        lineObject.endColor = new Color(0, 0.6f, 0);
        lineObject.widthMultiplier = lineScale;
        lineObject.positionCount = 2;
        lineObject.alignment = LineAlignment.View;
    }

    void Update()
    {
        var p1 = Utils.FromChrono(spring.GetPoint1Abs());
        var p2 = Utils.FromChrono(spring.GetPoint2Abs());

        DrawHelicalSpring(p1, p2);
    }

    // When attaching to a Game Object, hide the transform
    void OnValidate()
    {
        transform.hideFlags = HideFlags.NotEditable | HideFlags.HideInInspector;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;

        if (useRelativePos)
        {
            // To ensure the 'relative' units entered aren't incorrectly visualised by unity (as comapared to chrono),
            // we need to scale them a bit (can be checked with Abs spring visualisation at runtime)
            // 1. convert local points to "scale-neutral" local points
            Vector3 adjustedPosition1 = new Vector3(position1.x / body1.transform.lossyScale.x,
                                                    position1.y / body1.transform.lossyScale.y,
                                                    position1.z / body1.transform.lossyScale.z);
            Vector3 adjustedPosition2 = new Vector3(position2.x / body2.transform.lossyScale.x,
                                                    position2.y / body2.transform.lossyScale.y,
                                                    position2.z / body2.transform.lossyScale.z);

            // 2. transform these adjusted points into world space for correct visualisation
            Vector3 worldPosition1 = body1.transform.TransformPoint(adjustedPosition1);
            Vector3 worldPosition2 = body2.transform.TransformPoint(adjustedPosition2);

            // Draw the Gizmo line between these points
            Gizmos.DrawLine(worldPosition1, worldPosition2);
        }
        else
        {
            Gizmos.DrawLine(position1, position2);
        }
    }

    void DrawHelicalSpring(Vector3 p1, Vector3 p2)
    {
        // Calculate the direction from p1 to p2
        Vector3 direction = (p2 - p1).normalized;
        // Calculate the spring's length as the distance between p1 and p2
        float length = Vector3.Distance(p1, p2);
        // Calculate the number of points based on turns and points per turn
        int totalPoints = turns * pointsPerTurn;
        lineObject.positionCount = totalPoints;
        Vector3[] positions = new Vector3[totalPoints];

        // Adjust the angle step based on the total number of points and turns
        float angleStep = 2 * Mathf.PI * turns / totalPoints;

        // Calculate the central position of the spring
        Vector3 centralPosition = (p1 + p2) / 2.0f;

        // Calculate a vector perpendicular to the direction for radial displacements
        Vector3 upVector = Vector3.up;
        if (Vector3.Angle(direction, upVector) < 1.0f || Vector3.Angle(direction, upVector) > 179.0f)
        {
            // Handle the case where direction and upVector are nearly aligned
            upVector = Vector3.right;
        }
        Vector3 radialDirection = Vector3.Cross(direction, upVector).normalized;

        for (int i = 0; i < totalPoints; i++)
        {
            float t = i / (float)(totalPoints - 1); // Normalized [0, 1]
            float angle = angleStep * i;
            float radialDisplacement = springRadius * Mathf.Cos(angle);
            float verticalDisplacement = length * t - length / 2;

            // Calculate point position along the central axis
            Vector3 pointOnAxis = direction * verticalDisplacement;
            // Calculate radial displacement
            Vector3 radialDisplacementVector = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, direction) * radialDirection * springRadius;

            // Position the point by combining central, axial, and radial components
            positions[i] = centralPosition + pointOnAxis + radialDisplacementVector;
        }

        lineObject.SetPositions(positions);
    }

}
