using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UChBodyGround))]
public class UChBodyGroundEditor : Editor
{
    override public void OnInspectorGUI()
    {
        UChBodyGround ground = (UChBodyGround)target;

        ground.showFrameGizmo = EditorGUILayout.Toggle("Show Frame Gizmo", ground.showFrameGizmo);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(ground);
        }
    }
}
