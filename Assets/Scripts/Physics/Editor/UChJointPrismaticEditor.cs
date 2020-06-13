using UnityEngine;
using UnityEditor;

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
