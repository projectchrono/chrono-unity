using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UChBodySphere : UChBody
{
    public double radius;
    public double density;

    private ChMaterialSurface mat;

    public UChBodySphere()
    {
        radius = 0.5;
        density = 1000;
    }

    public override void Create()
    {
        // Get a handle to the associated material
        var mat_component = this.GetComponent<UChMaterialSurface>();
        ////mat_component.DumpInfo();
        mat = mat_component.mat_info.CreateMaterial(mat_component.contact_method);

        // Create the underlying Chrono body and initialize
        body = new ChBodyEasySphere(radius, density, false, true, mat);
    }

    public override void AddToSystem()
    {
        base.AddToSystem();

        if (mat.GetContactMethod() != UChSystem.chrono_system.GetContactMethod())
            throw new Exception("Incompatible contact method (" + gameObject.name + ")");
    }
}
