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

public class UChRSDA : MonoBehaviour
{
    // Set the bodies to which this link will be attached
    // TODO:- should we set certain points on the bodies? currently this will only be the origin point of body1 and body2
    public UChBody body1;
    public UChBody body2;

    // spring and actuator values
    public double springCoefficient = 0;
    public double dampingCoefficient = 0;
    public double actuatorTorque = 0;
    public bool autoRestAngle = false;
    public double restAngle = 0;
    [Tooltip("Turn on/off visualisation of the spring at runtime")]
    public bool runtimeVisualisation = true;

    private ChLinkRSDA rotationalSpringDamper;
    private ChLinkLockParallel zAxisParallelLock; // if necessary for the user to maintain the z-axes as parallel between the objects (commented out below)
    private LineRenderer arrowLineRenderer; // For the arrow visualisation

    void Start()
    {
        // Convert Unity position and rotation to Chrono ChFrame for body1
        ChVector3d position1 = Utils.ToChrono(body1.transform.position);
        ChQuaterniond rotation1 = Utils.ToChrono(body1.transform.rotation);
        ChFramed frame1 = new ChFramed(position1, rotation1);

        // Convert Unity position and rotation to Chrono ChFrame for body2
        ChVector3d position2 = Utils.ToChrono(body2.transform.position);
        ChQuaterniond rotation2 = Utils.ToChrono(body2.transform.rotation);
        ChFramed frame2 = new ChFramed(position2, rotation2);

        // create and initialise the RSDA
        rotationalSpringDamper = new ChLinkRSDA();
        rotationalSpringDamper.Initialize(body1.GetChBody(), body2.GetChBody(), true, frame1, frame2);

        // Set variables
        rotationalSpringDamper.SetSpringCoefficient(springCoefficient);
        rotationalSpringDamper.SetDampingCoefficient(dampingCoefficient);
        rotationalSpringDamper.SetActuatorTorque(actuatorTorque);
        if (!autoRestAngle)
            rotationalSpringDamper.SetRestAngle(restAngle);

        // Initialize the LockParallel using the same frames
        //zAxisParallelLock = new ChLinkLockParallel();
        //zAxisParallelLock.Initialize(body1.GetChBody(), body2.GetChBody(), true, frame1, frame2);
        //UChSystem.chrono_system.AddLink(zAxisParallelLock);

        // Add the RSDA to the Chrono system
        UChSystem.chrono_system.AddLink(rotationalSpringDamper);

        if (runtimeVisualisation)
        {
            // Initialise the line renderer for the arrow
            GameObject arrowLineRendererObject = new GameObject("ArrowLineRenderer");
            arrowLineRendererObject.transform.parent = this.transform; // Set as child of current GameObject
            arrowLineRenderer = arrowLineRendererObject.AddComponent<LineRenderer>();
            arrowLineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            arrowLineRenderer.startColor = Color.yellow;
            arrowLineRenderer.endColor = Color.red;
            arrowLineRenderer.startWidth = 0.05f;
            arrowLineRenderer.endWidth = 0.05f;
        }
    }

    void FixedUpdate()
    {
        // Draw the visualisation for the RSDA
        if (runtimeVisualisation)
            DrawRotationalArrow(body1.transform.position, body2.transform.position);

        // Get the angle from the RSDA
        float angleDegrees = (float)(rotationalSpringDamper.GetAngle() * Mathf.Rad2Deg);
        Debug.Log("RSDA Angle: " + angleDegrees);
    }

    void DrawRotationalArrow(Vector3 start, Vector3 end, float arrowHeadLength = 0.2f, float springRadius = 0.5f, float arrowHeadAngle = 135.0f)
    {
        Vector3 lineDirection = (start - end).normalized;
        // Determine the perpendicular orientation to the line between the bodies
        Vector3 anyPerpendicular;
        if (Vector3.Cross(lineDirection, Vector3.up).magnitude > 0.01f)
        {
            anyPerpendicular = Vector3.Cross(lineDirection, Vector3.up).normalized;
        }
        else
        {
            anyPerpendicular = Vector3.Cross(lineDirection, Vector3.forward).normalized;
        }
        Vector3 centerPoint = (start + end) / 2;
        int arcPoints = 20; // Number of points for the arc
        arrowLineRenderer.positionCount = arcPoints + 1; // Account for the arrowhead as a single line

        // Create the arc
        for (int i = 0; i < arcPoints; i++)
        {
            float angle = i * (270.0f / (arcPoints - 1)) - 115.0f; // Span the arc over 270 degrees
            Quaternion rot = Quaternion.AngleAxis(angle, lineDirection);
            Vector3 arcPoint = centerPoint + rot * anyPerpendicular * springRadius;
            arrowLineRenderer.SetPosition(i, arcPoint);
        }

        // Arrowhead
        Vector3 arcEndPoint = arrowLineRenderer.GetPosition(arcPoints - 1);
        Vector3 lastArcDirection = (arcEndPoint - arrowLineRenderer.GetPosition(arcPoints - 2)).normalized;
        // Rotate the lastArcDirection around the lineDirection by arrowHeadAngle to adjust the arrowhead direction
        // arrowHeadAngle is set in the class parameters
        Quaternion arrowHeadRotation = Quaternion.AngleAxis(arrowHeadAngle, lineDirection);
        Vector3 adjustedArrowHeadDirection = arrowHeadRotation * lastArcDirection;
        Vector3 arrowHeadEnd = arcEndPoint + adjustedArrowHeadDirection * arrowHeadLength;
        arrowLineRenderer.SetPosition(arcPoints, arrowHeadEnd);
    }


    void OnDrawGizmos()
    {
        if (body1 == null || body2 == null)
            return;

        // Draw a straight line between body1 and body2
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(body1.transform.position, body2.transform.position);

        // Set up for the circle
        Vector3 start = body1.transform.position;
        Vector3 end = body2.transform.position;
        Vector3 center = (start + end) / 2;
        float radius = 0.5f; // Radius of the visualisation circle

        // Draw the circle made up of segments
        int segments = 20; // Increase for a smoother circle
        Vector3 previousPoint = center + new Vector3(radius, 0, 0); // Starting point
        for (int i = 1; i <= segments; i++)
        {
            float angle = i * 2 * Mathf.PI / segments;
            Vector3 newPoint = center + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0); // Adjusted to be about the  XY plane
            Gizmos.DrawLine(previousPoint, newPoint);
            previousPoint = newPoint;
        }

        // Arrowhead lines
        // Calculate the direction of the arrowhead
        Vector3 direction = (previousPoint - center).normalized;
        // Create two lines for the arrowhead
        float arrowHeadLength = 0.1f;
        Vector3 arrowSide1 = Quaternion.Euler(0, 0, 30) * -direction * arrowHeadLength;
        Vector3 arrowSide2 = Quaternion.Euler(0, 0, 135) * -direction * arrowHeadLength;
        Gizmos.DrawLine(previousPoint, previousPoint + arrowSide1);
        Gizmos.DrawLine(previousPoint, previousPoint + arrowSide2);
    }

    // When attaching to a GameObject, hide the transform
    void OnValidate()
    {
        transform.hideFlags = HideFlags.NotEditable | HideFlags.HideInInspector;
    }
}
