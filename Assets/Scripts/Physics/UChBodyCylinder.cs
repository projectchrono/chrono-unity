using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UChBodyCylinder : UChBody
{
    public double radius;
    public float density;

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
        double mass = density * (Math.PI * Math.Pow(radius, 2) * height);
        
        body = new ChBodyAuxRef();
        body.SetDensity(density);
        body.SetMass(mass);
        body.SetInertiaXX(new ChVectorD((1.0 / 12.0) * mass * (3 * Math.Pow(radius, 2) + Math.Pow(height, 2)),
                                        0.5 * mass * Math.Pow(radius, 2),
                                        (1.0 / 12.0) * mass * (3 * Math.Pow(radius, 2) + Math.Pow(height, 2))));

        body.GetCollisionModel().ClearModel();
        body.GetCollisionModel().AddCylinder(mat, radius, radius, height * 0.5);
        body.GetCollisionModel().BuildModel();
    }

    public override void AddToSystem()
    {
        base.AddToSystem();

        if (mat.GetContactMethod() != UChSystem.chrono_system.GetContactMethod())
            throw new Exception("Incompatible contact method (" + gameObject.name + ")");
    }
}
