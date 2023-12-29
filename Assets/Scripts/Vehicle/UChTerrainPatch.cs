using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UChTerrainPatch : MonoBehaviour
{
    public bool hasContactShape;

    public float coefficientFriction;  // used when no contact shape present
    private ChMaterialSurface mat;     // used when contact shape present
    private ChBody body;

    private Vector3 normal;  // normal to patch top surface
    private Vector3 center;  // center of patch top surface
    private float radius;    // radius of bounding sphere

    public UChTerrainPatch()
    {
        coefficientFriction = 0.9f;
        hasContactShape = false;
    }
    /*
    void Awake()
    {
        ////Debug.Log(name + "  Create contact shape? " + createContactShape);
        
        var pos = transform.position;
        var rot = transform.rotation;
        var size = transform.localScale;
        /*
        float thickness = size.y;
        normal = transform.up;
        center = transform.position + (0.5f * thickness) * normal;
        ////Debug.Log("Center: " + center.ToString("F4"));
        ////Debug.Log("Normal: " + normal.ToString("F4"));
        
        if (hasContactShape)
        {
            var mat_component = this.GetComponent<UChMaterialSurface>();
            mat = mat_component.mat_info.CreateMaterial(mat_component.contact_method);

            body = new ChBody();
            body.SetBodyFixed(true);
            body.SetCollide(true);
            body.SetPos(Utils.ToChronoFlip(pos));
            body.SetRot(Utils.ToChronoFlip(rot));
            // body.GetCollisionModel().Clear();  // obsolete? but need patches to be added so this needs updating.
            body.GetCollisionModel().AddShape(new ChCollisionShapeBox(mat, size.x * 0.5, size.y * 0.5, size.z * 0.5));
            // body.GetCollisionModel().Build();// Changelog seems to imply this is no longer necessary, but set up alternatively
        }

        var rend = GetComponent<Renderer>();
        radius = rend.bounds.extents.magnitude;
        


    }
    */
    private void Start()
    {
        // Moved all of this our of Awake() to the Start, so that these are registered with the system, *after* the system (which is created with its Awake()

        // determine position, rotation and size
        var pos = transform.position;
        var rot = transform.rotation;
        var size = transform.localScale;

        float thickness = size.y;
        normal = transform.up;
        center = transform.position + (0.5f * thickness) * normal;
        ////Debug.Log("Center: " + center.ToString("F4"));
        ////Debug.Log("Normal: " + normal.ToString("F4"));

        if (hasContactShape)
        {
            
            var mat_component = this.GetComponent<UChMaterialSurface>();
            mat = mat_component.mat_info.CreateMaterial(mat_component.contact_method);
            // set position and rotation
            ChCoordsysD objectPos = new ChCoordsysD(Utils.ToChrono(pos), Utils.ToChrono(rot)); // to chrono flip? or just to chrono.

            

             body = new ChBody();
             body.SetBodyFixed(true);
             body.SetCollide(true);
             body.SetPos(Utils.ToChronoFlip(pos));
             body.SetRot(Utils.ToChronoFlip(rot));
             body.GetCollisionModel().AddShape(new ChCollisionShapeBox(mat, size.x * 0.5, size.y * 0.5, size.z * 0.5));

             // body.GetCollisionModel().Build(); Changelog seems to imply this is no longer necessary, but set up alternatively
             
            
            // Rigid Terrain patch usage (rather than body addition to the system)
            //RigidTerrain parentTerrain = GetComponentInParent<UChTerrain>().ReturnRigidTerrain();     // determine the parent terrain (created in uchterrain.cs)

            //var usethis = GetComponent<UChTerrain>().ReturnRigidTerrain();
            //usethis.AddPatch(mat, objectPos, size.x, size.y, size.z); // TODO: need to determine rotation of this patch.
            //Debug.Log("print terrain " + usethis.GetPatches().ToString());
        }
        
        var rend = GetComponent<Renderer>();
        radius = rend.bounds.extents.magnitude;
        // If present, add the rigid body to the Chrono system
        //if (hasContactShape)
        //    UChSystem.chrono_system.AddBody(body);

    }

    public ChVectorD GetNormal() { return Utils.ToChronoFlip(normal); }

    public float GetCoefficientFriction() {
        if (hasContactShape)
            return mat.GetKfriction();
        return coefficientFriction; 
    }

    public bool Project(ChVectorD loc, out double height)
    {
        return Project(Utils.FromChronoFlip(loc), out height, out _);
    }

    public bool Project(Vector3 loc, out double height, out Vector3 C)
    {
        // Ray definition in global 
        Vector3 v = new Vector3(0, -1, 0);     // direction: down global vertical
        Vector3 A = loc - (radius + 100) * v;  // start: above bounding sphere

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
