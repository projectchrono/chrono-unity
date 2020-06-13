using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UChBody))]
public class UChBodyEditor : Editor
{
    override public void OnInspectorGUI()
    {
        UChBody body = (UChBody)target;

        body.isFixed = EditorGUILayout.Toggle("Fixed", body.isFixed);
        body.collide = EditorGUILayout.Toggle("Collide", body.collide);
        body.showFrameGizmo = EditorGUILayout.Toggle("Show Frame Gizmo", body.showFrameGizmo);
        body.comRadiusGizmo = EditorGUILayout.FloatField("COM Radius Gizmo", body.comRadiusGizmo);

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        body.automaticMass = EditorGUILayout.Toggle("Automatic Mass/Inertia", body.automaticMass);

        EditorGUI.indentLevel++;
        if (body.automaticMass)
        {
            body.density = EditorGUILayout.FloatField("Density", body.density);
        }
        else
        {
            body.CalculateMassProperties();
            body.mass = EditorGUILayout.DoubleField("Mass", body.mass);
            body.COM = EditorGUILayout.Vector3Field("Center of Mass", body.COM);
            body.inertiaMoments = EditorGUILayout.Vector3Field("Moments of Inertia", body.inertiaMoments);
            body.inertiaProducts = EditorGUILayout.Vector3Field("Products of Inertia", body.inertiaProducts);
        }
        EditorGUI.indentLevel--;

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        body.linearVelocity = EditorGUILayout.Vector3Field("Linear Velocity", body.linearVelocity);
        body.angularVelocity = EditorGUILayout.Vector3Field("Angular Velocity", body.angularVelocity);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(body);
        }
    }
}

