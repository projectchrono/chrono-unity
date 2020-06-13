using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UChBodyCylinder))]
public class UChBodyCylinderEditor : UChBodyEditor
{
    public override void OnInspectorGUI()
    {
        UChBodyCylinder body = (UChBodyCylinder)target;

        base.OnInspectorGUI();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        body.radius = EditorGUILayout.DoubleField("Radius", body.radius);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(body);
        }
    }
}
