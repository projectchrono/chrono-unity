using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
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



    public static Vector3 FromChronoFlip(ChVectorD v)
    {
        return new Vector3((float)v.x, (float)v.y, -(float)v.z);
    }

    public static Quaternion FromChronoFlip(ChQuaternionD q)
    {
        return new Quaternion(-(float)q.e1, -(float)q.e2, (float)q.e3, (float)q.e0);
    }

    public static ChVectorD ToChronoFlip(Vector3 v)
    {
        return new ChVectorD(v.x, v.y, -v.z);
    }

    public static ChQuaternionD ToChronoFlip(Quaternion q)
    {
        return new ChQuaternionD(q.w, -q.x, -q.y, q.z);
    }

    public static double Clamp(double x, double a, double b) { return Math.Max(a, Math.Min(b, x)); }
}
