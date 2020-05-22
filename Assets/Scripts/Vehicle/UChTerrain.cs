using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class UFlatTerrain : ChTerrain
{
    private double m_height;
    private ChVectorD m_normal;
    private float m_mu;

    public UFlatTerrain(double height, float mu)
    {
        m_height = height;
        m_normal = new ChVectorD(0, 1, 0);
        m_mu = mu;
    }

    public override double GetHeight(ChVectorD loc) {
        return m_height; 
    }
    public override ChVectorD GetNormal(ChVectorD loc) {
        return m_normal; 
    }
    public override float GetCoefficientFriction(ChVectorD loc) { 
        return m_mu; 
    }
}

public class UPatchTerrain : ChTerrain
{
    private int m_np;
    public List<UChTerrainPatch> m_patches;

    public UPatchTerrain(List<UChTerrainPatch> patches)
    {
        m_patches = patches;
    }

    public override double GetHeight(ChVectorD loc)
    {
        double height;
        bool hit = FindPoint(loc, out height, out _, out _);
        return hit ? height : 0.0;
    }

    public override ChVectorD GetNormal(ChVectorD loc)
    {
        ChVectorD normal;
        bool hit = FindPoint(loc, out _, out normal, out _);
        return hit ? normal : new ChVectorD(0, 1, 0);
    }

    public override float GetCoefficientFriction(ChVectorD loc)
    {
        float mu;
        bool hit = FindPoint(loc, out _, out _, out mu);
        return hit ? mu : 0.9f;
    }

    private bool FindPoint(ChVectorD loc, out double height, out ChVectorD normal, out float mu)
    {
        // Loop through all patches.  Keep track of highest ray intersection point.
        bool hit = false;
        height = double.MinValue;
        normal = new ChVectorD(0, 1, 0);
        mu = 0.9f;

        foreach (var patch in m_patches)
        {
            double pheight;
            bool phit = patch.Project(loc, out pheight);
            if (phit && pheight > height)
            {
                hit = true;
                height = pheight;
                normal = patch.GetNormal();
                mu = patch.GetCoefficientFriction();
            }
        }

        return hit;
    }
}

public class UUnityTerrain : ChTerrain
{
    private float m_mu;
    private Terrain m_terrain;

    public UUnityTerrain(Terrain terrain, float mu)
    {
        m_terrain = terrain;
        m_mu = mu;
    }

    public override double GetHeight(ChVectorD loc)
    {
        return m_terrain.SampleHeight(Utils.FromChronoFlip(loc));
    }
    
    public override ChVectorD GetNormal(ChVectorD loc)
    {
        var worldPos = Utils.FromChronoFlip(loc);
        var terrainLocalPos = worldPos - m_terrain.transform.position;
        var normalizedPos = new Vector2(Mathf.InverseLerp(0.0f, m_terrain.terrainData.size.x, terrainLocalPos.x),
                                    Mathf.InverseLerp(0.0f, m_terrain.terrainData.size.z, terrainLocalPos.z));
        var terrainNormal = m_terrain.terrainData.GetInterpolatedNormal(normalizedPos.x, normalizedPos.y);

        return Utils.ToChronoFlip(terrainNormal);
    }

    public override float GetCoefficientFriction(ChVectorD loc)
    {
        return m_mu;
    }
}

public class UChTerrain : MonoBehaviour
{
    public enum Type
    {
        Flat,   // flat horizontal 
        Patch,  // collection of rectangular patches
        Unity   // Unity terrain object
    }
    public Type type;

    public static ChTerrain chrono_terrain;

    public double height;
    public Terrain unityTerrain;
    public float coefficientFriction;

    public List<UChTerrainPatch> patches;

    public UChTerrain()
    {
        height = 0;
        coefficientFriction = 0.9f;
    }

    void Awake()
    {
        switch (type)
        {
            case Type.Flat:
                chrono_terrain = new UFlatTerrain(height, coefficientFriction);
                break;
            case Type.Patch:
                chrono_terrain = new UPatchTerrain(patches);
                break;
            case Type.Unity:
                chrono_terrain = new UUnityTerrain(unityTerrain, coefficientFriction);
                break;
        }
    }

    // When attaching to a Game Object, hide the transform
    void OnValidate()
    {
        transform.hideFlags = HideFlags.NotEditable | HideFlags.HideInInspector;
    }
}

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
                break;
        }

        EditorGUI.indentLevel--;

        if (terrain.type != UChTerrain.Type.Patch)
            terrain.coefficientFriction = EditorGUILayout.FloatField("Coefficient Friction", terrain.coefficientFriction);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(terrain);
        }
    }
}
