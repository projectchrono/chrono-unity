using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UChTerrain))]
public class UChTerrainEditor : Editor
{
    override public void OnInspectorGUI()
    {
        UChTerrain terrain = (UChTerrain)target;

        string[] options = new string[] { "Flat", "Patch", "Unity" };
        terrain.type = (UChTerrain.Type)EditorGUILayout.Popup("Type", (int)terrain.type, options, EditorStyles.popup);

        EditorGUI.indentLevel++;

        switch (terrain.type)
        {
            case UChTerrain.Type.Flat:
                terrain.height = EditorGUILayout.DoubleField("Height", terrain.height);
                terrain.coefficientFriction = EditorGUILayout.FloatField("Coefficient Friction", terrain.coefficientFriction);
                break;
            case UChTerrain.Type.Patch:
                int size = EditorGUILayout.DelayedIntField("Number of Patches", terrain.patches.Count);
                while (size < terrain.patches.Count)
                    terrain.patches.RemoveAt(terrain.patches.Count - 1);
                while (size > terrain.patches.Count)
                    terrain.patches.Add(null);
                for (int i = 0; i < size; i++)
                    terrain.patches[i] = (UChTerrainPatch)EditorGUILayout.ObjectField("Patch " + (i + 1), terrain.patches[i], typeof(UChTerrainPatch), true);
                break;
            case UChTerrain.Type.Unity:
                terrain.unityTerrain = (Terrain)EditorGUILayout.ObjectField("Unity Terrain", terrain.unityTerrain, typeof(Terrain), true);
                terrain.coefficientFriction = EditorGUILayout.FloatField("Coefficient Friction", terrain.coefficientFriction);
                break;
        }

        EditorGUI.indentLevel--;

        if (GUI.changed)
        {
            EditorUtility.SetDirty(terrain);
        }
    }
}
