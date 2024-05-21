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
// Authors: Radu Serban, Josh Diyn
// =============================================================================

using System;
using UnityEngine;

public class UChBodyCylinder : UChBody
{
    [SerializeField] private double radius; // remove this from being a public editable, instead, get the value from the mesh filter (assumed cylinder) or the transform.

    private ChContactMaterial mat;

    public UChBodyCylinder()
    {
        automaticMass = true;
    }

    public override void CalculateMassProperties()
    {
        var height = 2 * transform.localScale.y;
        mass = density * (Math.PI * Math.Pow(radius, 2) * height);
        inertiaMoments.x = (float)((1.0 / 12.0) * mass * (3 * Math.Pow(radius, 2) + Math.Pow(height, 2)));
        inertiaMoments.y = (float)(0.5 * mass * Math.Pow(radius, 2));
        inertiaMoments.z = (float)((1.0 / 12.0) * mass * (3 * Math.Pow(radius, 2) + Math.Pow(height, 2)));
    }

    private void CalculateRadiusFromMesh()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            // Assuming the mesh is perfectly centered and uniform
            float meshDiameter = meshFilter.mesh.bounds.size.x * transform.localScale.x;
            radius = meshDiameter / 2.0;
        }
        else
        {
            Debug.LogError("MeshFilter not found on the ChBodyCylinder, using X transform");
            radius = transform.localScale.x / 2.0;

        }
    }

    public override void Create()
    {
        // Get a handle to the associated material component and create the Chrono material
        var mat_component = this.GetComponent<UChMaterialSurface>();
        ////mat_component.DebugInfo();
        mat = mat_component.mat_info.CreateMaterial(mat_component.contact_method);

        // Create the underlying Chrono body and its collision shape
        var height = 2 * transform.localScale.y;
        CalculateRadiusFromMesh();
        body = new ChBodyAuxRef();
        // Create and add a cylinder collision shape using ChCollisionShapeCylinder
        // ChQuarternion y-to-z: the standard Chrono cylinder shape is defined with its axis along the Z-axis of the shape frame.
        // Whereas in a Y-up world, the axis is along the Y of the shape frame. 
        body.AddCollisionShape(new ChCollisionShapeCylinder(mat, radius, height), new ChFramed(new ChVector3d(), new ChQuaterniond(chrono.Q_ROTATE_Y_TO_Z)));
    }

    public override void AddToSystem()
    {
        base.AddToSystem();

        if (mat.GetContactMethod() != UChSystem.chrono_system.GetContactMethod())
            throw new Exception("Incompatible contact method (" + gameObject.name + ")");
    }
}
