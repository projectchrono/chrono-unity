using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UChRigidTerrainManager))]
public class UChRigidTerrainManagerEditor : Editor
{

    public override void OnInspectorGUI()
    {
        // Draw the default inspector
        DrawDefaultInspector();

        UChRigidTerrainManager manager = (UChRigidTerrainManager)target;

        // Dynamic terrain grid scanner that centers about the vehicle and reports into Gizmos drawing the terrain heights
        if (!manager.gameObject.GetComponent<TerrainWireframe>())
            manager.gameObject.AddComponent<TerrainWireframe>();


    }

    // Navigate all descendants of a GameObject to apply the material
    void ApplyMaterialToChildren(GameObject obj, Material mat)
    {
        foreach (Transform child in obj.transform)
        {
            MeshRenderer renderer = child.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.material = mat; // Apply the material
            }

            // Apply the material recursively to the grandchildren
            ApplyMaterialToChildren(child.gameObject, mat);
        }
    }
}
