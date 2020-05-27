using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UChJointPrismatic : MonoBehaviour
{
    public UChBody body1;
    public UChBody body2;

    public bool enableLimits = false;
    public double minDisplacement = 0;
    public double maxDisplacement = 0;

    public double displacement = 0;  // current joint displacement

    private ChLinkLockPrismatic joint;

    public ChLinkLockPrismatic GetChJoint()
    {
        return joint;
    }

    void Start()
    {
        joint = new ChLinkLockPrismatic();
        ChCoordsysD csys = new ChCoordsysD(Utils.ToChrono(transform.position), Utils.ToChrono(transform.rotation));
        joint.Initialize(body1.GetChBody(), body2.GetChBody(), csys);

        if (enableLimits)
        {
            joint.GetLimit_Z().SetActive(true);
            joint.GetLimit_Z().SetMin(minDisplacement);
            joint.GetLimit_Z().SetMax(maxDisplacement);
        }

        UChSystem.chrono_system.AddLink(joint);
    }

    void Update()
    {
        var csys = joint.GetMarker1().GetAbsCoord();
        transform.position = Utils.FromChrono(csys.pos);
        transform.rotation = Utils.FromChrono(csys.rot);

        displacement = joint.GetMarker1().GetAbsCoord().pos.z - joint.GetMarker2().GetAbsCoord().pos.z;
        displacement = (int)(displacement * 1000.0f) / 1000.0f;
    }
}

[CustomEditor(typeof(UChJointPrismatic))]
public class UChJointPrismaticEditor : Editor
{
    override public void OnInspectorGUI()
    {
        var joint = target as UChJointPrismatic;

        joint.body1 = (UChBody)EditorGUILayout.ObjectField("Body 1", joint.body1, typeof(UChBody), true);
        joint.body2 = (UChBody)EditorGUILayout.ObjectField("Body 2", joint.body2, typeof(UChBody), true);

        joint.enableLimits = EditorGUILayout.Toggle("Joint Limits", joint.enableLimits);

        if (joint.enableLimits)
        {
            EditorGUI.indentLevel++;
            joint.minDisplacement = EditorGUILayout.DoubleField("Min Displacement", joint.minDisplacement);
            joint.maxDisplacement = EditorGUILayout.DoubleField("Max Displacement", joint.maxDisplacement);
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.LabelField("Displ. (pos1 - pos2)", joint.displacement.ToString());

        if (GUI.changed)
        {
            EditorUtility.SetDirty(joint);
        }
    }
}