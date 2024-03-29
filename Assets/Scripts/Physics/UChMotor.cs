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

public enum MotorType
{
    STATE = 0,
    SPEED = 1,
    FORCE = 2
}

public class UChMotor : MonoBehaviour
{
    public UChBody body1;
    public UChBody body2;

    public MotorType type;
}
