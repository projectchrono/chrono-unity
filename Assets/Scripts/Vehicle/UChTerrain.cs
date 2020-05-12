using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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

    public override double GetHeight(ChVectorD loc) { return m_height; }
    public override ChVectorD GetNormal(ChVectorD loc) { return m_normal; }
    public override float GetCoefficientFriction(ChVectorD loc) { return m_mu; }
}

public class UUnityTerrain : ChTerrain
{
    private ChVectorD m_normal;
    private float m_mu;

    public UUnityTerrain(float mu)
    {
        m_normal = new ChVectorD(0, 1, 0);
        m_mu = mu;
    }

    public override double GetHeight(ChVectorD loc)
    {
        return Terrain.activeTerrain.SampleHeight(Utils.FromChrono(loc));
    }
    
    public override ChVectorD GetNormal(ChVectorD loc)
    {
        var worldPos = Utils.FromChrono(loc);
        var terrainLocalPos = worldPos - Terrain.activeTerrain.transform.position;
        var normalizedPos = new Vector2(Mathf.InverseLerp(0.0f, Terrain.activeTerrain.terrainData.size.x, terrainLocalPos.x),
                                    Mathf.InverseLerp(0.0f, Terrain.activeTerrain.terrainData.size.z, terrainLocalPos.z));
        var terrainNormal = Terrain.activeTerrain.terrainData.GetInterpolatedNormal(normalizedPos.x, normalizedPos.y);

        return Utils.ToChrono(terrainNormal);
    }

    public override float GetCoefficientFriction(ChVectorD loc)
    {
        return m_mu;
    }
}

public class UChTerrain : MonoBehaviour
{
    public static ChTerrain chrono_terrain;

    public double height;
    public float coefficientFriction;

    public UChTerrain()
    {
        height = 0;
        coefficientFriction = 0.9f;
    }

    void Awake()
    {
        if (gameObject.GetComponent<Terrain>())
        {
            Debug.Log("Attached to a terrain!");
            chrono_terrain = new UUnityTerrain(coefficientFriction);
        }
        else
        {
            chrono_terrain = new UFlatTerrain(height, coefficientFriction);
        }
    }
}

[CustomEditor(typeof(UChTerrain))]
public class UChTerrainEditor : Editor
{
    override public void OnInspectorGUI()
    {
        UChTerrain terrain = (UChTerrain)target;

        if (!terrain.gameObject.GetComponent<Terrain>())
        {
            // Only if not attached to a Unity Terrain object
            terrain.height = EditorGUILayout.DoubleField("Height", terrain.height);
        }

        terrain.coefficientFriction = EditorGUILayout.FloatField("Coefficient Friction", terrain.coefficientFriction);
    }
}
