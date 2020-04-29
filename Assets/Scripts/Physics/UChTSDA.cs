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

    public float lineScale = 0.1f;

    private ChLinkTSDA spring;
    private LineRenderer lineObject;

    void Start()
    {
        spring = new ChLinkTSDA();
        spring.Initialize(body1.GetChBody(), body2.GetChBody(), 
                          useRelativePos, Utils.ToChrono(position1), Utils.ToChrono(position2),
                          autoRestLength, restLength);
        spring.SetSpringCoefficient(springCoefficient);
        spring.SetDampingCoefficient(dampingCoefficient);
        spring.SetActuatorForce(actuatorForce);

        UChSystem.chrono_system.AddLink(spring);

        lineObject = gameObject.AddComponent<LineRenderer>();
        lineObject.material = new Material(Shader.Find("Sprites/Default"));
        lineObject.startColor = new Color(0, 0.4f, 0);
        lineObject.endColor = new Color(0, 0.4f, 0);
        lineObject.widthMultiplier = lineScale;
        lineObject.positionCount = 2;
        lineObject.alignment = LineAlignment.View;
    }

    void FixedUpdate()
    {
        var p1 = Utils.FromChrono(spring.GetPoint1Abs());
        var p2 = Utils.FromChrono(spring.GetPoint2Abs());

        // Update line in game view
        lineObject.SetPosition(0, p1);
        lineObject.SetPosition(1, p2);
    }

    // When attaching to a Game Object, hide the transform
    void OnValidate()
    {
        transform.hideFlags = HideFlags.NotEditable | HideFlags.HideInInspector;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        if (useRelativePos)
        {
            Gizmos.DrawLine(body1.transform.TransformPoint(position1), body2.transform.TransformPoint(position2));
        }
        else
        {
            Gizmos.DrawLine(position1, position2);
        }
    }

}
