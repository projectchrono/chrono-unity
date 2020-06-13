using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UChBodyConvexHull))]
public class UChBodyConvexHullEditor : UChBodyEditor
{
    public override void OnInspectorGUI()
    {
        UChBodyConvexHull body = (UChBodyConvexHull)target;

        base.OnInspectorGUI();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        int size = 0;
        size = EditorGUILayout.DelayedIntField("Number of Points", body.points.Count);
        while (size < body.points.Count)
            body.points.RemoveAt(body.points.Count - 1);
        while (size > body.points.Count)
            body.points.Add(new Vector3());

        for (int i = 0; i < size; i++)
        {
            body.points[i] = EditorGUILayout.Vector3Field("Point " + (i + 1), body.points[i]);
        }

        body.pointGizmoRadius = EditorGUILayout.FloatField("Point Radius Gizmo", body.pointGizmoRadius);
        body.showCollisionShape = EditorGUILayout.Toggle("Show Collision Shape", body.showCollisionShape);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(body);
        }
    }
}
