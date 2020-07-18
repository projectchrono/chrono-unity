using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UChTerrainPatch))]
public class UChTerrainPatchEditor : Editor
{
    override public void OnInspectorGUI()
    {
        UChTerrainPatch patch = (UChTerrainPatch)target;

        patch.hasContactShape = EditorGUILayout.Toggle("Create Contact Shape", patch.hasContactShape);

        if (!patch.hasContactShape)
        {
            patch.coefficientFriction = EditorGUILayout.FloatField("Friction", patch.coefficientFriction);

            // Remove MaterialSurface component if present
            var mat_surf = patch.gameObject.GetComponent<UChMaterialSurface>();
            if (mat_surf)
                DestroyImmediate(mat_surf);
        }
        else
        {
            // Create MaterialSurface component if not present
            var mat_surf = patch.gameObject.GetComponent<UChMaterialSurface>();
            if (!mat_surf)
                patch.gameObject.AddComponent<UChMaterialSurface>();
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(patch);
        }
    }
}
