using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UChJointGear : MonoBehaviour
{
    [SerializeField]
    public struct ShaftFrame
    {
        Vector3 origin;
        Vector3 direction;
    }

    public UChBody body1;
    public UChBody body2;

    public ShaftFrame shaft1Frame;

    public Vector3 shaft1Origin;
    public Vector3 shaft1Direction;

    public Vector3 shaft2Origin;
    public Vector3 shaft2Direction;

    public double transmissionRatio = 1;
    public bool enforcePhase = true;
    public bool epicyclic = false;

    private ChLinkLockGear gear;

    public UChJointGear()
    {
        shaft1Direction = new Vector3(0, 0, 1);
        shaft2Direction = new Vector3(0, 0, 1);
    }

    void Start()
    {
        gear = new ChLinkLockGear();
        ChFramed csys = new ChFramed(Utils.ToChrono(transform.position), Utils.ToChrono(transform.rotation));
        gear.Initialize(body1.GetChBody(), body2.GetChBody(), csys);

        //// TODO: Check that this is correct.
        var rot1 = Quaternion.LookRotation(shaft1Direction.normalized);
        gear.Set_local_shaft1(new ChFramed(Utils.ToChrono(shaft1Origin), Utils.ToChrono(rot1)));
        var rot2 = Quaternion.LookRotation(shaft2Direction.normalized);
        gear.Set_local_shaft2(new ChFramed(Utils.ToChrono(shaft2Origin), Utils.ToChrono(rot2)));

        gear.Set_tau(transmissionRatio);
        gear.Set_epicyclic(epicyclic);
        gear.Set_checkphase(enforcePhase);

        UChSystem.chrono_system.AddLink(gear);
    }

    void Update()
    {
        ChVector3d pos = gear.GetMarker2().GetAbsCsys().pos;
        transform.position = Utils.FromChrono(pos);
    }
}
