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
// Authors: Josh Diyn
// =============================================================================
//
// A shader explicitly for the rendering of the collision shapes from Chrono.
// Includes a distance limit, to cull any vertices over a certain distance
// from the camera.
//
// =============================================================================
Shader "Custom/DrawCollisionShape"
{
    Properties
    {
        _Color("Color", Color) = (0,1,1,1)
        _MaxDistance("Max Distance", Float) = 25.0

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite On
            Cull Off
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            fixed4 _Color;
            float _MaxDistance;

            struct appdata
            {
                uint vertexID : SV_VertexID;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float4 color : COLOR;
                float distance : TEXCOORD2;
            };

            StructuredBuffer<float3> vertexPositions;

            v2f vert(appdata v)
            {
                v2f o;
                uint index = v.vertexID;
                float3 position = vertexPositions[index];
                o.pos = UnityObjectToClipPos(float4(position, 1.0));
                o.distance = length(_WorldSpaceCameraPos - position); // Calculate distance from camera to vertex
                o.color = _Color;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float alpha = i.distance < _MaxDistance ? 1.0 : 0.0; // Apply distance limit
                return float4(i.color.rgb, alpha); // Draw the vertex color
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}