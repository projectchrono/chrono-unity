// =============================================================================
// PROJECT CHRONO - http://projectchrono.org
//
// Copyright (c) 2024 projectchrono.org
// All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found
// in the LICENSE file at the top level of the distribution and at
// http://projectchrono.org/license-chrono.txt.
//
// =============================================================================
// Authors: Radu Serban
// =============================================================================

// TODO - ensure there's a uchmaterial surface also attached when this exists on an object!

using System;
using System.Security.AccessControl;

public class UChBodySphere : UChBody
{
    public double radius;

    private ChContactMaterial mat;

    public UChBodySphere()
    {
        radius = 0.5;
        automaticMass = true;
    }

    public override void CalculateMassProperties()
    {
        mass = density * ((4.0 / 3.0) * Math.PI * Math.Pow(radius, 3));
        double inertia = (2.0 / 5.0) * mass * Math.Pow(radius, 2);
        inertiaMoments.x = (float)inertia;
        inertiaMoments.y = (float)inertia;
        inertiaMoments.z = (float)inertia;
    }

    public override void Create()
    {
        // Get a handle to the associated material
        var mat_component = this.GetComponent<UChMaterialSurface>();
        ////mat_component.DebugInfo();
        mat = mat_component.mat_info.CreateMaterial(mat_component.contact_method);

        // Create the underlying Chrono body and its collision shape
        body = new ChBodyAuxRef();
        // Create a sphere collision shape with the new approach ChCollisionShape, assuming ChFrame does not need specifying
        body.AddCollisionShape(new ChCollisionShapeSphere(mat, radius), new ChFramed());
    }

    public override void AddToSystem()
    {
        base.AddToSystem();

        if (mat.GetContactMethod() != UChSystem.chrono_system.GetContactMethod())
            throw new Exception("Incompatible contact method (" + gameObject.name + ")");
    }
}
