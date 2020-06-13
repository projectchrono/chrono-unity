using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UChBodySphere))]
public class UChBodySphereEditor : UChBodyEditor
{
    public override void OnInspectorGUI()
    {
        UChBodySphere body = (UChBodySphere)target;

        base.OnInspectorGUI();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        body.radius = EditorGUILayout.DoubleField("Radius", body.radius);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(body);
        }
    }
}
