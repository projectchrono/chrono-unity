using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UChBodyGround : UChBody
{
    public UChBodyGround()
    {
        isFixed = true;
        collide = false;
        linearVelocity = Vector3.zero;
        angularVelocity = Vector3.zero;
    }

    // When attaching to a Game Object, hide the transform
    void OnValidate()
    {
        transform.hideFlags = HideFlags.NotEditable | HideFlags.HideInInspector;
    }
}

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
