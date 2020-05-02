using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UChBodyMesh : UChBody
{
    private ChMaterialSurface mat;

    private ChTriangleMeshConnected chrono_mesh;

    public UChBodyMesh()
    {
        automaticMass = true;
    }

    public override void Create()
    {
        // Get a handle to the associated material component and create the Chrono material
        var mat_component = this.GetComponent<UChMaterialSurface>();
        ////mat_component.DebugInfo();
        mat = mat_component.mat_info.CreateMaterial(mat_component.contact_method);

        // Get a handle to the associated mesh filter
        var mesh_component = this.GetComponent<MeshFilter>();
        var mesh = mesh_component.mesh;
        chrono_mesh = new ChTriangleMeshConnected();
        chrono_mesh.m_vertices.Capacity = mesh.vertices.Length;
        chrono_mesh.m_face_v_indices.Capacity = mesh.triangles.Length / 3;
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            chrono_mesh.m_vertices.Add(Utils.ToChrono(mesh.vertices[i]));
        }
        for (int i = 0; i < mesh.triangles.Length; i+=3) {
            chrono_mesh.m_face_v_indices.Add(new ChVectorI(mesh.triangles[i], mesh.triangles[i+1], mesh.triangles[i+2]));
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

[CustomEditor(typeof(UChBodyMesh))]
public class UChBodyMeshEditor : UChBodyEditor
{
    public override void OnInspectorGUI()
    {
        UChBodyMesh body = (UChBodyMesh)target;

        base.OnInspectorGUI();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(body);
        }
    }
}