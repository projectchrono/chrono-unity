using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UChBodyBox))]
public class UChBodyBoxEditor : UChBodyEditor
{
    public override void OnInspectorGUI()
    {
        UChBodyBox body = (UChBodyBox)target;

        //// TODO: Add this dependency to the other UChBodies
        // Check if UChMaterialSurface component is attached to the object, since creation of a chbodybox requires it
        UChMaterialSurface matSurface = body.GetComponent<UChMaterialSurface>();
        if (matSurface == null)
        {
            // If not, add the UChMaterialSurface component to the object
            matSurface = body.gameObject.AddComponent<UChMaterialSurface>();
        }

        base.OnInspectorGUI();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(body);
        }
    }
}
