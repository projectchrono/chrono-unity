using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public class UTerrain : MonoBehaviour
{
    public static ChTerrain chrono_terrain;

    public double height;
    public float coefficientFriction;

    public UTerrain()
    {
        height = 0;
        coefficientFriction = 0.8f;
    }

    void Awake()
    {
        chrono_terrain = new UFlatTerrain(height, coefficientFriction);
    }


}
