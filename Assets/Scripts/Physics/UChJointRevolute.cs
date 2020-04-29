using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//// TODO:  Double-check check proper implementation of joint limits.
////        Must take into account body order, orientation, etc.

public class UChJointRevolute : MonoBehaviour
{
    public UChBody body1;
    public UChBody body2;

    public bool enableLimits = false;
    public double minAngle = 0;
    public double maxAngle = 0;

    public double angle;  // current joint relative angle

    private ChLinkLockRevolute joint;

    public ChLinkLockRevolute GetChJoint()
    {
        return joint;
    }

    void Start()
    {
        joint = new ChLinkLockRevolute();
        ChCoordsysD csys = new ChCoordsysD(Utils.ToChrono(transform.position), Utils.ToChrono(transform.rotation));
        joint.Initialize(body1.GetChBody(), body2.GetChBody(), csys);

        if (enableLimits)
        {
            joint.GetLimit_Rz().SetActive(true);
            joint.GetLimit_Rz().SetMin(minAngle * Mathf.PI / 180);
            joint.GetLimit_Rz().SetMax(maxAngle * Mathf.PI / 180);
        }

        UChSystem.chrono_system.AddLink(joint);
    }

    void FixedUpdate()
    {
        var csys = joint.GetMarker1().GetAbsCoord();
        transform.position = Utils.FromChrono(csys.pos);
        transform.rotation = Utils.FromChrono(csys.rot);

        angle = joint.GetRelAngle() * (180 / Mathf.PI);
        angle = (int)(angle * 100.0f) / 100.0f;
    }
}

[CustomEditor(typeof(UChJointRevolute))]
public class UChJointRevoluteEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var joint = target as UChJointRevolute;

        joint.body1 = (UChBody)EditorGUILayout.ObjectField("Body 1", joint.body1, typeof(UChBody), true);
        joint.body2 = (UChBody)EditorGUILayout.ObjectField("Body 2", joint.body2, typeof(UChBody), true);

        joint.enableLimits = EditorGUILayout.Toggle("Joint Limits", joint.enableLimits);

        if (joint.enableLimits)
        {
            EditorGUI.indentLevel++;
            joint.minAngle = EditorGUILayout.DoubleField("Min Angle", joint.minAngle);
            joint.maxAngle = EditorGUILayout.DoubleField("Max Angle", joint.maxAngle);
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.LabelField("Angle", joint.angle.ToString());

        if (GUI.changed)
        {
            EditorUtility.SetDirty(joint);
        }
    }
}
