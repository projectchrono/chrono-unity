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
// Authors: Josh Diyn
// =============================================================================
// Note: Typically needs a high step to avoid simulation errors. For e.g 0.005
// if experiencing out of bounds or NaN issues where high load on the link
// or high stiffness is causing the bodies to experience excessive forces.
// This bushing type does not typically support high stiffness. See Chrono doc
// =============================================================================

using UnityEngine;

public class UChLinkBushing : MonoBehaviour
{
    public UChBody body1;
    public UChBody body2;

    private ChLinkBushing bushing;

    // Enum for bushing type
    public enum BushingType { Mount, Spherical }
    public BushingType bushingType = BushingType.Mount;

    // Stiffness and Damping matrices for the bushing joint
    public ChMatrix66d kFactor;
    public ChMatrix66d rFactor;

    // Translational and rotational stiffness and damping values so the user can assign seperatly
    public double translationalStiffness = 100000;
    public double translationalDamping = 10000;
    
    public double rotationalStiffness = 50000;
    public double rotationalDamping = 5000;

    void Start()
    {
        if (body1 == null || body2 == null ||
            !body1.gameObject.activeInHierarchy || !body2.gameObject.activeInHierarchy ||
            !body1.enabled || !body2.enabled)
        {
            Debug.LogError("UChLinkBushing requires two UChBody objects assigned.");
            return;
        }

        bushing = new ChLinkBushing();
        ChFramed csys = new ChFramed(Utils.ToChrono(transform.position), Utils.ToChrono(transform.rotation)); // taken from the existing game object to which the bushing is attached

        // Setup the stiffness and damping
        kFactor = new ChMatrix66d();
        rFactor = new ChMatrix66d();

        // Populate matrices based on type
        if (bushingType == BushingType.Mount)
        {
            for (int i = 0; i < 3; i++)
            {
                kFactor.SetItem(i, i, translationalStiffness);
                rFactor.SetItem(i, i, translationalDamping);
            }
            for (int i = 3; i < 6; i++)
            {
                kFactor.SetItem(i, i, rotationalStiffness);
                rFactor.SetItem(i, i, rotationalDamping);
            }
        }
        else if (bushingType == BushingType.Spherical)
        {
            // Spherical bushing only has 3 degrees of freedom
            for (int i = 0; i < 3; i++)
            {
                kFactor.SetItem(i, i, rotationalStiffness);
                rFactor.SetItem(i, i, rotationalDamping);
            }
        }

        bushing.Initialize(body1.GetChBody(), body2.GetChBody(), csys, kFactor, rFactor);

        // add to the chrono system
        UChSystem.chrono_system.AddLink(bushing);
    }

    void Update()
    {
        // translate/rotate the bushing's game object in line with Chrono at computer FPS
        var csys = bushing.GetMarker1().GetAbsCoordsys();
        transform.position = Utils.FromChrono(csys.pos);
        transform.rotation = Utils.FromChrono(csys.rot);
    }

    private void FixedUpdate()
    {
        var torque = Utils.FromChrono(bushing.GetTorque());
        var force = Utils.FromChrono(bushing.GetForce());
        // printouts
        Debug.Log("Bushing Torque: " + torque);
        Debug.Log("Bushing Force: " + force);

        // error checking to see if step size is not enough and bushing stiffness causes break in simulation
        if (float.IsNaN(force.x) || float.IsNaN(force.y) || float.IsNaN(force.z))
        {
            Debug.LogError("Bushing broken. Check simulation settings. Perhaps try increasing step resolution.");
            bushing.SetBroken(true);
        }

        // ensure in step with the rest of the physics system
        bushing.Update(UChSystem.chrono_system.GetChTime(), true);
    }
}
