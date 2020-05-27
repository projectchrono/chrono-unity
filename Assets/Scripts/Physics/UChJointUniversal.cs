using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UChJointUniversal : MonoBehaviour
{
    public UChBody body1;
    public UChBody body2;

    private ChLinkUniversal joint;

    void Start()
    {
        joint = new ChLinkUniversal();
        ChFrameD frame = new ChFrameD(Utils.ToChrono(transform.position), Utils.ToChrono(transform.rotation));
        joint.Initialize(body1.GetChBody(), body2.GetChBody(), frame);

        UChSystem.chrono_system.AddLink(joint);
    }

    void Update()
    {
        var frame1 = joint.GetFrame1Abs();
        var frame2 = joint.GetFrame2Abs();

        ChVectorD x = frame1.GetA().Get_A_Xaxis();
        ChVectorD y = frame2.GetA().Get_A_Yaxis();
        ChVectorD z = x.Cross(y);

        ChMatrix33D R = new ChMatrix33D();
        R.Set_A_axis(x, y, z);

        transform.position = Utils.FromChrono(frame1.GetPos());
        transform.rotation = Utils.FromChrono(R.Get_A_quaternion());
    }
}
