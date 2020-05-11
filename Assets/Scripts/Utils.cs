using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    static readonly ChQuaternionD xflip;
    static Utils() {
        xflip = new ChQuaternionD();
        xflip.Q_from_AngZ(ChronoEngine_csharp.CH_C_PI);
    }

    public static Vector3 FromChrono(ChVectorD v)
    {
        return new Vector3((float)v.x, (float)v.y, (float)v.z);
    }

    public static Quaternion FromChrono(ChQuaternionD q)
    {
        return new Quaternion((float)q.e1, (float)q.e2, (float)q.e3, (float)q.e0);
    }

    public static ChVectorD ToChrono(Vector3 v)
    {
        return new ChVectorD(v.x, v.y, v.z);
    }

    public static ChQuaternionD ToChrono(Quaternion q)
    {
        return new ChQuaternionD(q.w, q.x, q.y, q.z);
    }

    public static ChVectorD ISOtoLHF(ChVectorD v)
    {
        return new ChVectorD(v.x, -v.y, v.z);
    }

    public static ChQuaternionD ISOtoLHF(ChQuaternionD q) {
        var q_new = new ChQuaternionD();
        q_new.Cross(q, xflip);
        return q_new;
    }
}
