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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UChJointCylindrical : MonoBehaviour
{
    public UChBody body1;
    public UChBody body2;

    private ChLinkLockCylindrical joint;

    void Start()
    {
        joint = new ChLinkLockCylindrical();
        ChFramed csys = new ChFramed(Utils.ToChrono(transform.position), Utils.ToChrono(transform.rotation));
        joint.Initialize(body1.GetChBody(), body2.GetChBody(), csys);

        UChSystem.chrono_system.AddLink(joint);
    }

    void Update()
    {
        var csys = joint.GetMarker1().GetAbsCoordsys();
        transform.position = Utils.FromChrono(csys.pos);
        transform.rotation = Utils.FromChrono(csys.rot);
    }
}
