﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UChBodySphere : UChBody
{
    public double radius;
    public float density;

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
        double mass = density * ((4.0 / 3.0) * Math.PI * Math.Pow(radius, 3));
        double inertia = (2.0 / 5.0) * mass * Math.Pow(radius, 2);

        body = new ChBodyAuxRef();
        body.SetDensity(density);
        body.SetMass(mass);
        body.SetInertiaXX(new ChVectorD(inertia, inertia, inertia));

        body.GetCollisionModel().ClearModel();
        body.GetCollisionModel().AddSphere(mat, radius);
        body.GetCollisionModel().BuildModel();
    }

    public override void AddToSystem()
    {
        base.AddToSystem();

        if (mat.GetContactMethod() != UChSystem.chrono_system.GetContactMethod())
            throw new Exception("Incompatible contact method (" + gameObject.name + ")");
    }
}
