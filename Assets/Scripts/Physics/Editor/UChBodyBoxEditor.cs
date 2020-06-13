using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UChBodyBox))]
public class UChBodyBoxEditor : UChBodyEditor
{
    public override void OnInspectorGUI()
    {
        UChBodyBox body = (UChBodyBox)target;

        base.OnInspectorGUI();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(body);
        }
    }
}
