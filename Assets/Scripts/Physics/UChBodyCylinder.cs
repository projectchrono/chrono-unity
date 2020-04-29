using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UChBodyCylinder : UChBody
{
    public double radius;
    public double density;

    private ChMaterialSurface mat;

    public UChBodyCylinder()
    {
        radius = 0.5;
        density = 1000;
    }

    public override void Create()
    {
        // Get a handle to the associated material component and create the Chrono material
        var mat_component = this.GetComponent<UChMaterialSurface>();
        ////mat_component.DumpInfo();
        mat = mat_component.mat_info.CreateMaterial(mat_component.contact_method);

        // Create the underlying Chrono body
        var height = 2 * transform.localScale.y;
        body = new ChBodyEasyCylinder(radius, height, density, false, true, mat);
    }

    public override void AddToSystem()
    {
        base.AddToSystem();

        if (mat.GetContactMethod() != UChSystem.chrono_system.GetContactMethod())
            throw new Exception("Incompatible contact method (" + gameObject.name + ")");
    }
}
