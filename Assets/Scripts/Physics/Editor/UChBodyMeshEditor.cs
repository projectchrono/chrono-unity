using UnityEngine;
using UnityEditor;

//// TODO: take into account object scale!!!

[CustomEditor(typeof(UChBodyMesh))]
public class UChBodyMeshEditor : UChBodyEditor
{
    public override void OnInspectorGUI()
    {
        UChBodyMesh body = (UChBodyMesh)target;

        base.OnInspectorGUI();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        body.collisionMesh = EditorGUILayout.Toggle("OBJ File", body.collisionMesh);

        if (body.collisionMesh)
            body.collisionMeshOBJFile = EditorGUILayout.TextField("File Name", body.collisionMeshOBJFile);

        body.sweptSphereRadius = EditorGUILayout.DoubleField("Swept Sphere Radius", body.sweptSphereRadius);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(body);
        }
    }
}
