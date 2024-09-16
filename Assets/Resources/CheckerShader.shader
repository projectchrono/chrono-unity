// =============================================================================
// PROJECT CHRONO - http://projectchrono.org
//
// Copyright (c) 2024 projectchrono.org
// All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found
// in the LICENSE file at the top level of the distribution.
//
// =============================================================================
// Authors: Josh Diyn
// =============================================================================
//
// A basic grid shader using local coordinates to wrap about a tyre for
// visualisation purposes
//
// =============================================================================
Shader "Custom/CheckerShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _CheckerSize("Checker Size", Float) = 10.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        struct Input
        {
            float3 worldPos;
        };

        half _CheckerSize;
        fixed4 _Color;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Calculate UVs based on local coordinates
            float3 localPos = mul(unity_WorldToObject, float4(IN.worldPos, 1)).xyz;
            float2 uv = localPos.xz * _CheckerSize;
            float checker = step(0.5, frac(uv.x)) * step(0.5, frac(uv.y)) + step(0.5, frac(uv.x + 0.5)) * step(0.5, frac(uv.y + 0.5));
            checker = checker * 2 - 1; // Convert to -1 to +1 range

            fixed4 c = _Color * checker;

            o.Albedo = c.rgb;
            o.Metallic = 0.0;
            o.Smoothness = 0.5;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}

