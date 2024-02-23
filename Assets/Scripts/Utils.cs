using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    // Express a Chrono vector as a Unity vector (no frame rotation or chirality conversion).
    public static Vector3 FromChrono(ChVector3d v)
    {
        return new Vector3((float)v.x, (float)v.y, (float)v.z);
    }

    // Express a Chrono quaternion as a Unity quaternion (no frame rotation or chirality conversion).
    public static Quaternion FromChrono(ChQuaterniond q)
    {
        return new Quaternion((float)q.e1, (float)q.e2, (float)q.e3, (float)q.e0);
    }

    // Express a Unity vector as a Chrono vector (no frame rotation or chirality conversion).
    public static ChVector3d ToChrono(Vector3 v)
    {
        return new ChVector3d(v.x, v.y, v.z);
    }

    // Express a Unity quaternion as a Chrono quaternion (no frame rotation or chirality conversion).
    public static ChQuaterniond ToChrono(Quaternion q)
    {
        return new ChQuaterniond(q.w, q.x, q.y, q.z);
    }

    // Vector conversion from a Chrono RHF to a Unity LHF (no rotation conversion).
    public static Vector3 FromChronoFlip(ChVector3d v)
    {
        return new Vector3((float)v.x, (float)v.y, -(float)v.z);
    }

    // Quaternion conversion from a Chrono RHF to a Unity LHF (no rotation conversion).
    public static Quaternion FromChronoFlip(ChQuaterniond q)
    {
        return new Quaternion(-(float)q.e1, -(float)q.e2, (float)q.e3, (float)q.e0);
    }

    // Vector conversion from a Unity LHF to a Chrono RHF (no rotation conversion).
    public static ChVector3d ToChronoFlip(Vector3 v)
    {
        return new ChVector3d(v.x, v.y, -v.z);
    }

    // Quaternion conversion from a Unity LHF to a Chrono RHF (no rotation conversion).
    public static ChQuaterniond ToChronoFlip(Quaternion q)
    {
        return new ChQuaterniond(q.w, -q.x, -q.y, q.z);
    }

    // Clamp the given value x in the range [a,b].
    public static double Clamp(double x, double a, double b) { return Math.Max(a, Math.Min(b, x)); }
}
