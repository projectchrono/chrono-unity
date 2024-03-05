using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Physics items will cause Unity to crash if trying to add bodies to the system before any of those
// have intialised first. Therefore, force the execution to be after these (default execution order is '0')
[DefaultExecutionOrder(100)]
public class UChJointSpherical : MonoBehaviour
{
    public UChBody body1;
    public UChBody body2;

    private ChLinkLockSpherical joint;

    void Start()
    {
        joint = new ChLinkLockSpherical();
        ChFramed csys = new ChFramed(Utils.ToChrono(transform.position), Utils.ToChrono(transform.rotation));
        joint.Initialize(body1.GetChBody(), body2.GetChBody(), csys);

        UChSystem.chrono_system.AddLink(joint);
    }

    void Update()
    {
        var csys = joint.GetMarker1().GetAbsCsys();
        transform.position = Utils.FromChrono(csys.pos);
        transform.rotation = Utils.FromChrono(csys.rot);
    }
}
