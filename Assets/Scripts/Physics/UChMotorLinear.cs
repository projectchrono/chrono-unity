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
using UnityEngine.Android;

public class UChMotorLinear : UChMotor
{    
    private ChLinkMotorLinear motor;

    private ChLinkMotorLinear Create(MotorType type)
    {
        switch (type)
        {
            case MotorType.STATE:
            default:
                var motorP = new ChLinkMotorLinearPosition();
                return motorP;
            case MotorType.SPEED:
                var motorS = new ChLinkMotorLinearSpeed();
                return motorS;
            case MotorType.FORCE:
                var motorF = new ChLinkMotorLinearForce();
                return motorF;
        }
    }

    void Start()
    {
        motor = Create(type);
        ChFramed frame = new ChFramed(Utils.ToChrono(transform.position), Utils.ToChrono(transform.rotation));
        motor.Initialize(body1.GetChBody(), body2.GetChBody(), frame);

        // Get a handle to the associated function component and set the motor's function
        var fun_component = this.GetComponent<UChFunction>();
        motor.SetMotorFunction(fun_component.GetChFunction());

        UChSystem.chrono_system.AddLink(motor);
    }

    void Update()
    {
        var csys = motor.GetFrame1Abs();
        transform.position = Utils.FromChrono(csys.GetPos());
        transform.rotation = Utils.FromChrono(csys.GetRot());
    }
}
