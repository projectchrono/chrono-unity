using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Terrain_Mesh_Viewer))]
public class TerrainMeshViewerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Terrain_Mesh_Viewer script = (Terrain_Mesh_Viewer)target;
        // Add a description area
        EditorGUILayout.HelpBox("This is used as a reverse pull of the height data from Chrono, to check the accuracy of the " +
            "mesh generated against the Unity Terrain object. Won't run unless there's an active system and can find " +
            "a RigidTerrain object. Can be moved anywhere in the world and the heights queried. It will build " +
            "a corresponding mesh with a grid shader so any patch terrain parameters can be adjusted for accuracy.", MessageType.Info);
        DrawDefaultInspector(); // Draws the default inspector
        if (GUILayout.Button("RigidTerrain Accuracy Check in Editor (Game Mode)"))
        {

            script.GenerateMesh(); // Calls the method to create the mesh
            script.UpdateMeshHeights();
        }
    }
}
