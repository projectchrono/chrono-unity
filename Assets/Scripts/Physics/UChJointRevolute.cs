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

using UnityEngine;

//// TODO:  Double-check check proper implementation of joint limits.
////        Must take into account body order, orientation, etc.

public class UChJointRevolute : MonoBehaviour
{
    public UChBody body1;
    public UChBody body2;

    public bool enableLimits = false;
    public double minAngle = 0;
    public double maxAngle = 0;

    public double angle;  // current joint relative angle

    private ChLinkLockRevolute joint;

    public ChLinkLockRevolute GetChJoint()
    {
        return joint;
    }

    void Start()
    {
        joint = new ChLinkLockRevolute();
        ChFramed csys = new ChFramed(Utils.ToChrono(transform.position), Utils.ToChrono(transform.rotation));
        joint.Initialize(body1.GetChBody(), body2.GetChBody(), csys);

        
        if (enableLimits)
        {
            joint.LimitRz().SetActive(true);
            joint.LimitRz().SetMin(minAngle * Mathf.PI / 180);
            joint.LimitRz().SetMax(maxAngle * Mathf.PI / 180);
        }
        
        if (joint != null) {
            ////Debug.Log("Joint is functional " + joint.GetName());
            UChSystem.chrono_system.AddLink(joint);
        }
    }

    void Update()
    {
        
        var csys = joint.GetMarker1().GetAbsCoordsys();
        transform.position = Utils.FromChrono(csys.pos);
        transform.rotation = Utils.FromChrono(csys.rot);

        angle = joint.GetRelAngle() * (180 / Mathf.PI);
        angle = (int)(angle * 100.0f) / 100.0f;
        
    }
}
