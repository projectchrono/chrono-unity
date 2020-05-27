﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UChJointSpherical : MonoBehaviour
{
    public UChBody body1;
    public UChBody body2;

    private ChLinkLockSpherical joint;

    void Start()
    {
        joint = new ChLinkLockSpherical();
        ChCoordsysD csys = new ChCoordsysD(Utils.ToChrono(transform.position), Utils.ToChrono(transform.rotation));
        joint.Initialize(body1.GetChBody(), body2.GetChBody(), csys);

        UChSystem.chrono_system.AddLink(joint);
    }

    void Update()
    {
        var csys = joint.GetMarker1().GetAbsCoord();
        transform.position = Utils.FromChrono(csys.pos);
        transform.rotation = Utils.FromChrono(csys.rot);
    }
}
