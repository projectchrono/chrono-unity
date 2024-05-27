﻿// =============================================================================
// PROJECT CHRONO - http://projectchrono.org
//
// Copyright (c) 2024 projectchrono.org
// All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found
// in the LICENSE file at the top level of the distribution.
//
// =============================================================================
// Authors: Radu Serban
// =============================================================================

using UnityEngine;

public class UChJointPrismatic : MonoBehaviour
{
    public UChBody body1;
    public UChBody body2;

    public bool enableLimits = false;
    public double minDisplacement = 0;
    public double maxDisplacement = 0;

    public double displacement = 0;  // current bushing displacement

    private ChLinkLockPrismatic joint;

    public ChLinkLockPrismatic GetChJoint()
    {
        return joint;
    }

    void Start()
    {
        joint = new ChLinkLockPrismatic();
        ChFramed csys = new ChFramed(Utils.ToChrono(transform.position), Utils.ToChrono(transform.rotation));
        joint.Initialize(body1.GetChBody(), body2.GetChBody(), csys);

        if (enableLimits)
        {
            joint.LimitZ().SetActive(true);
            joint.LimitZ().SetMin(minDisplacement);
            joint.LimitZ().SetMax(maxDisplacement);
        }

        UChSystem.chrono_system.AddLink(joint);
    }

    void Update()
    {
        var csys = joint.GetMarker1().GetAbsCoordsys();
        transform.position = Utils.FromChrono(csys.pos);
        transform.rotation = Utils.FromChrono(csys.rot);

        displacement = joint.GetMarker1().GetAbsCoordsys().pos.z - joint.GetMarker2().GetAbsCoordsys().pos.z;
        displacement = (int)(displacement * 1000.0f) / 1000.0f;
    }
}
