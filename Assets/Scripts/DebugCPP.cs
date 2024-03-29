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

using AOT;
using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class DebugCPP : MonoBehaviour
{

    // Use this for initialization
    void OnEnable()
    {
        RegisterDebugCallback(OnDebugCallback);
    }

    //------------------------------------------------------------------------------------------------

    [DllImport("ChronoEngine", CallingConvention = CallingConvention.Cdecl)]
    static extern void RegisterDebugCallback(debugCallback cb);
    
    //Create string param callback delegate
    delegate void debugCallback(IntPtr request, int color, int size);
    
    enum Color { red, green, blue, black, white, yellow, orange };
    
    [MonoPInvokeCallback(typeof(debugCallback))]
    static void OnDebugCallback(IntPtr request, int color, int size)
    {
        //Ptr to string
        string debug_string = Marshal.PtrToStringAnsi(request, size);

        //Add Specified Color
        debug_string =
            String.Format("{0}{1}{2}{3}{4}",
            "<color=",
            ((Color)color).ToString(),
            ">",
            debug_string,
            "</color>"
            );

        UnityEngine.Debug.Log(debug_string);
    }
}
