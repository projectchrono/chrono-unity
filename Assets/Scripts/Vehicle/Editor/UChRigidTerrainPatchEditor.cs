using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UChRigidTerrainPatch))]
public class UChRigidTerrainPatchEditor : Editor
{

    SerializedProperty patchTypeProp;
    SerializedProperty tiledProp;
    SerializedProperty numberOfTilesProp;
    SerializedProperty smoothingFactorProp;
    SerializedProperty coarseMeshGridSizenProp;
    SerializedProperty refineGreaterThanAngleProp;
    private int selectedcoarseMeshGridSizeIndex;


    void OnEnable()
    {
        // Setup the SerializedProperties
        patchTypeProp = serializedObject.FindProperty("patchType");
        tiledProp = serializedObject.FindProperty("tiled");
        numberOfTilesProp = serializedObject.FindProperty("numberOfTiles");
        smoothingFactorProp = serializedObject.FindProperty("smoothingFactor");
        coarseMeshGridSizenProp = serializedObject.FindProperty("coarseMeshGridSizen");
        refineGreaterThanAngleProp = serializedObject.FindProperty("refineGreaterThanAngle");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Draw the default inspector without the auto-handled properties
        DrawPropertiesExcluding(serializedObject, "patchType", "tiled", "numberOfTiles", "smoothingFactor", "coarseMeshGridSize", "refineGreaterThanAngle");

        UChRigidTerrainPatch patch = (UChRigidTerrainPatch)target;

        EditorGUILayout.PropertyField(patchTypeProp);

        if (patchTypeProp.enumValueIndex != (int)UChRigidTerrainPatch.PatchType.unityTerrainPatch)
        {
            // If patchType is not unityTerrainPatch, show the tiled option
            EditorGUILayout.PropertyField(tiledProp);

            if (tiledProp.boolValue)
            {
                // Show additional options if tiled is true
                EditorGUILayout.PropertyField(numberOfTilesProp);
            }
        } else { // Terrain Patch

            // Create a slider in the inspector
            GUIContent terrainResolutionLabel = new GUIContent("Chrono Terrain Mesh Grid Size (m)", "Beginning of coarse mesh grid squares prior to refinement. Too high misses details, too low is computationally expensive. A value of 2 is usually sufficient");
            float slider = EditorGUILayout.Slider(terrainResolutionLabel, (float)patch.coarseMeshGridSize, 0.1f, 5.0f);
            // Update the terrainResolution property based on the slider's value
            patch.coarseMeshGridSize = slider;
            // Show the smoothing
            EditorGUILayout.PropertyField(smoothingFactorProp);
            EditorGUILayout.PropertyField(refineGreaterThanAngleProp);
        }

        serializedObject.ApplyModifiedProperties();



        // Create MaterialSurface component if not present
        var mat_surf = patch.gameObject.GetComponent<UChMaterialSurface>();
        if (!mat_surf)
            patch.gameObject.AddComponent<UChMaterialSurface>();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(patch);
        }
    }
}
