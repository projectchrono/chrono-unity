using System;
using UnityEngine;

//// TODO: take into account object scale!!!

public class UChBodyMesh : UChBody
{
    private ChMaterialSurface mat;

    public bool collisionMesh;
    public string collisionMeshOBJFile;

    public double sweptSphereRadius;

    public UChBodyMesh()
    {
        automaticMass = true;
        collisionMesh = false;
        sweptSphereRadius = 0;
    }

    public override void Create()
    {
        // Get a handle to the associated material component and create the Chrono material
        var mat_component = this.GetComponent<UChMaterialSurface>();
        ////mat_component.DebugInfo();
        mat = mat_component.mat_info.CreateMaterial(mat_component.contact_method);

        // Create Chrono collision mesh
        var chrono_mesh = new ChTriangleMeshConnected();

        bool use_file = false;
        if (collisionMesh)
        {
            var ext = System.IO.Path.GetExtension(collisionMeshOBJFile);
            ////Debug.Log(gameObject.name + "  -- Collision mesh file extension: " + ext);
            use_file = System.IO.File.Exists(collisionMeshOBJFile) && String.Equals(ext, ".obj");
            if (!use_file)
            {
                Debug.Log(gameObject.name + "  -- Specified OBJ file " + collisionMeshOBJFile + " does not exist or is incorrect type.\nUsing mesh filter!");
            }
        }

        if (use_file)
        {
            ////Debug.Log(gameObject.name + "  -- Loading collision mesh from " + collisionMeshOBJFile);
            chrono_mesh.LoadWavefrontMesh(collisionMeshOBJFile, false, false);
        }
        else
        {
            ////Debug.Log(gameObject.name + "  -- Using mesh filter for collision");
            var mesh_component = this.GetComponent<MeshFilter>();
            var mesh = mesh_component.mesh;
            chrono_mesh.m_vertices.Capacity = mesh.vertices.Length;
            chrono_mesh.m_face_v_indices.Capacity = mesh.triangles.Length / 3;
            for (int i = 0; i < mesh.vertices.Length; i++)
            {
                chrono_mesh.m_vertices.Add(Utils.ToChrono(mesh.vertices[i]));
            }
            for (int i = 0; i < mesh.triangles.Length; i += 3)
            {
                chrono_mesh.m_face_v_indices.Add(new ChVectorI(mesh.triangles[i], mesh.triangles[i + 1], mesh.triangles[i + 2]));
            }
        }

        // Create the underlying Chrono body and its collision shape
        body = new ChBodyEasyMesh(chrono_mesh, density, automaticMass, false, true, mat, 0.01);

        // Update UChBody properties
        if (automaticMass)
        {
            mass = body.GetMass();
            COM = Utils.FromChrono(body.GetFrame_COG_to_REF().GetPos());
            inertiaMoments = Utils.FromChrono(body.GetInertiaXX());
            inertiaProducts = Utils.FromChrono(body.GetInertiaXY());
        }
    }

    public override void AddToSystem()
    {
        base.AddToSystem();

        if (mat.GetContactMethod() != UChSystem.chrono_system.GetContactMethod())
            throw new Exception("Incompatible contact method (" + gameObject.name + ")");
    }
}
