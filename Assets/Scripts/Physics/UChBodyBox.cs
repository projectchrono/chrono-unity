using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UChBodyBox : UChBody
{
    public float density;

    private ChMaterialSurface mat;

    public UChBodyBox()
    {
        density = 1000;
    }

    public override void Create()
    {
        // Get a handle to the associated material component and create the Chrono material
        var mat_component = this.GetComponent<UChMaterialSurface>();
        ////mat_component.DumpInfo();
        mat = mat_component.mat_info.CreateMaterial(mat_component.contact_method);

        // Create the underlying Chrono body and initialize
        var size = transform.localScale;
        double mass = density * (size.x * size.y * size.z);

        body = new ChBodyAuxRef();
        body.SetDensity(density);
        body.SetMass(mass);
        body.SetInertiaXX(new ChVectorD((1.0 / 12.0) * mass * (Math.Pow(size.y, 2) + Math.Pow(size.z, 2)),
                                        (1.0 / 12.0) * mass * (Math.Pow(size.x, 2) + Math.Pow(size.z, 2)),
                                        (1.0 / 12.0) * mass * (Math.Pow(size.x, 2) + Math.Pow(size.y, 2))));

        body.GetCollisionModel().ClearModel();
        body.GetCollisionModel().AddBox(mat, size.x * 0.5, size.y * 0.5, size.z * 0.5);
        body.GetCollisionModel().BuildModel();
    }

    public override void AddToSystem()
    {
        base.AddToSystem();

        if (mat.GetContactMethod() != UChSystem.chrono_system.GetContactMethod())
            throw new Exception("Incompatible contact method (" + gameObject.name + ")");
    }
}
