// =============================================================================
// PROJECT CHRONO - http://projectchrono.org
//
// Copyright (c) 2024 projectchrono.org
// All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found
// in the LICENSE file at the top level of the distribution.
//
// =============================================================================
// Authors: Json Zhou
// =============================================================================

using UnityEngine;

// Add Terrain prior to the UChVehicle (which is at -900)
[DefaultExecutionOrder(-950)]
public class UChSCMTerrainManager : UChTerrainManager
{
    public double BekkerKphi = 0.82e6; // Bekker Kphi
    public double BekkerKc = 0.14e4;   // Bekker Kc
    public double BekkerN = 1.0;       // Bekker n
    public double MohrCohesion = 0.017e4;  // Mohr cohesion
    public double MohrFriction = 35.0;   // Mohr friction angle in degrees
    public double JanosiShear = 1.78e-2; // Janosi shear
    public double ElasticK = 2e8; // Elastic stiffness
    public double DampingR = 3e4; // Damping
    public bool Bulldozing = true; // Bulldozing effect on/off
    public double ErosionAngle = 55;     // angle of erosion of the displaced material [degrees]
    public double FlowFactor = 1;       // growth of lateral volume relative to pressed volume
    public int ErosionIterations = 5;   // number of erosion refinements per timestep
    public int ErosionPropagations = 6;  // number of concentric vertex selections subject to erosion

    void Start()
    {
        SCMTerrain scm_terrain = GetComponentInChildren<UChSCMTerrain>().chronoTerrain;
        scm_terrain.SetSoilParameters(BekkerKphi,    // Bekker Kphi
                                BekkerKc,        // Bekker Kc
                                BekkerN,      // Bekker n exponent
                                MohrCohesion,     // Mohr cohesive limit (Pa)
                                MohrFriction,       // Mohr friction limit (degrees)
                                JanosiShear,     // Janosi shear coefficient (m)
                                ElasticK,      // Elastic stiffness (Pa/m), before plastic yield
                                DampingR       // Damping (Pa s/m), proportional to negative vertical speed
        );
        scm_terrain.EnableBulldozing(Bulldozing);
        scm_terrain.SetBulldozingParameters(ErosionAngle, FlowFactor, ErosionIterations, ErosionPropagations);
        chronoTerrain = scm_terrain;

    }

}

