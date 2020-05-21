using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UChTerrainPatch : MonoBehaviour
{
    public float mu; 
    private Vector3 normal;  // normal to patch top surface
    private Vector3 center;  // center of patch top surface
    private float radius;    // radius of bounding sphere

    void Awake()
    {
        float thickness = transform.localScale.y;
        normal = transform.up;
        center = transform.position + (0.5f * thickness) * normal;

        ////Debug.Log("Center: " + center.ToString("F4"));
        ////Debug.Log("Normal: " + normal.ToString("F4"));

        var rend = GetComponent<Renderer>();
        radius = rend.bounds.extents.magnitude;
    }

    public Vector3 GetNormal() { return normal; }

    public float GetCoefficientFriction() { return mu; }

    public bool Project(Vector3 loc, out float height) { return Project(loc, out height, out _); }

    public bool Project(Vector3 loc, out float height, out Vector3 C)
    {
        // Ray definition in global 
        Vector3 v = new Vector3(0, -1, 0);      // direction: down global vertical
        Vector3 A = loc - (radius + 100) * v;   // start: above bounding sphere

        // Intersect ray with top plane
        float t = Vector3.Dot(center - A, normal) / Vector3.Dot(v, normal);
        C = A + t * v;
        height = C.y;

        // Transform intersection point in local frame and check bounds
        // Attention: scale already taken into account by InverseTransformPoint!
        var Cl = transform.InverseTransformPoint(C);
        return Mathf.Abs(Cl.x) <= 0.5 && Mathf.Abs(Cl.z) <= 0.5;
    }
}
