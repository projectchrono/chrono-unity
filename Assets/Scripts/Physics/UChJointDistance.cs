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

    public float lineScale = 0.1f;

    private ChLinkDistance joint;
    private LineRenderer lineObject;

    void Start()
    {
        joint = new ChLinkDistance();
        joint.Initialize(body1.GetChBody(), body2.GetChBody(), useRelativePos, Utils.ToChrono(position1), Utils.ToChrono(position2));

        UChSystem.chrono_system.AddLink(joint);

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

    // When attaching to a Game Object, hide the transform
    void OnValidate()
    {
        transform.hideFlags = HideFlags.NotEditable | HideFlags.HideInInspector;
    }

    void OnDrawGizmos()
    {
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
