Shader "Pristine Major Minor Grid"
{
    // Modified shader originally by bgolus - https://gist.github.com/bgolus
    Properties
    {
        [KeywordEnum(X, Y, Z)] _Axis ("Plane Axis", Float) = 1.0
        [IntRange] _MajorGridDiv ("Major Grid Divisions", Range(2,25)) = 10.0
        _AxisLineWidth ("Axis Line Width", Range(0,1.0)) = 0.04
        _MajorLineWidth ("Major Line Width", Range(0,1.0)) = 0.02
        _MinorLineWidth ("Minor Line Width", Range(0,1.0)) = 0.01

        _MajorLineColor ("Major Line Color", Color) = (1,1,1,1)
        _MinorLineColor ("Minor Line Color", Color) = (1,1,1,1)
        _BaseColor ("Base Color", Color) = (0,0,0,0.1)

        _XAxisColor ("X Axis Line Color", Color) = (1,0,0,1)
        _XAxisDashColor ("X Axis Dash Color", Color) = (0.5,0,0,1)
        _YAxisColor ("Y Axis Line Color", Color) = (0,1,0,1)
        _YAxisDashColor ("Y Axis Dash Color", Color) = (0,0.5,0,1)
        _ZAxisColor ("Z Axis Line Color", Color) = (0,0,1,1)
        _ZAxisDashColor ("Z Axis Dash Color", Color) = (0,0,0.5,1)
        _AxisDashScale ("Axis Dash Scale", Float) = 1.33
        _CenterColor ("Axis Center Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Pass
        {
            // Decrease depth value to render this object on top
            Offset -1, -1
            CGPROGRAM
            #pragma shader_feature _ _AXIS_X _AXIS_Z // _AXIS_Y is default

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float4 uv : TEXCOORD0;
            };

            float _GridScale, _MajorGridDiv;

        #if defined(_AXIS_X)
            #define AXIS_COMPONENTS yz
        #elif defined(_AXIS_Z)
            #define AXIS_COMPONENTS xy
        #else
            #define AXIS_COMPONENTS xz
        #endif

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);

                float div = max(2.0, round(_MajorGridDiv));

                // trick to reduce visual artifacts when far from the world origin
                float3 worldPos = mul(unity_ObjectToWorld, float4(v.vertex.xyz, 1.0)).xyz;
                float3 cameraCenteringOffset = floor(_WorldSpaceCameraPos / div) * div;
                o.uv.yx = (worldPos - cameraCenteringOffset).AXIS_COMPONENTS;
                o.uv.wz = worldPos.AXIS_COMPONENTS;

                return o;
            }

            float _MajorLineWidth, _MinorLineWidth, _AxisLineWidth, _AxisDashScale;
            half4 _MajorLineColor, _MinorLineColor, _BaseColor, _XAxisColor, _XAxisDashColor, _YAxisColor, _YAxisDashColor, _ZAxisColor, _ZAxisDashColor, _CenterColor;

            fixed4 frag (v2f i) : SV_Target
            {
                float4 uvDDXY = float4(ddx(i.uv.xy), ddy(i.uv.xy));
                float2 uvDeriv = float2(length(uvDDXY.xz), length(uvDDXY.yw));

                float axisLineWidth = max(_MajorLineWidth, _AxisLineWidth);
                float2 axisDrawWidth = max(axisLineWidth, uvDeriv);
                float2 axisLineAA = uvDeriv * 1.5;
                float2 axisLines2 = smoothstep(axisDrawWidth + axisLineAA, axisDrawWidth - axisLineAA, abs(i.uv.zw * 2.0));
                axisLines2 *= saturate(axisLineWidth / axisDrawWidth);

                float div = max(2.0, round(_MajorGridDiv));
                float2 majorUVDeriv = uvDeriv / div;
                float majorLineWidth = _MajorLineWidth / div;
                float2 majorDrawWidth = clamp(majorLineWidth, majorUVDeriv, 0.5);
                float2 majorLineAA = majorUVDeriv * 1.5;
                float2 majorGridUV = 1.0 - abs(frac(i.uv.xy / div) * 2.0 - 1.0);
                float2 majorAxisOffset = (1.0 - saturate(abs(i.uv.zw / div * 2.0))) * 2.0;
                majorGridUV += majorAxisOffset; // adjust UVs so center axis line is skipped
                float2 majorGrid2 = smoothstep(majorDrawWidth + majorLineAA, majorDrawWidth - majorLineAA, majorGridUV);
                majorGrid2 *= saturate(majorLineWidth / majorDrawWidth);
                majorGrid2 = saturate(majorGrid2 - axisLines2); // hack
                majorGrid2 = lerp(majorGrid2, majorLineWidth, saturate(majorUVDeriv * 2.0 - 1.0));

                float minorLineWidth = min(_MinorLineWidth, _MajorLineWidth);
                bool minorInvertLine = minorLineWidth > 0.5;
                float minorTargetWidth = minorInvertLine ? 1.0 - minorLineWidth : minorLineWidth;
                float2 minorDrawWidth = clamp(minorTargetWidth, uvDeriv, 0.5);
                float2 minorLineAA = uvDeriv * 1.5;
                float2 minorGridUV = abs(frac(i.uv.xy) * 2.0 - 1.0);
                minorGridUV = minorInvertLine ? minorGridUV : 1.0 - minorGridUV;
                float2 minorMajorOffset = (1.0 - saturate((1.0 - abs(frac(i.uv.zw / div) * 2.0 - 1.0)) * div)) * 2.0;
                minorGridUV += minorMajorOffset; // adjust UVs so major division lines are skipped
                float2 minorGrid2 = smoothstep(minorDrawWidth + minorLineAA, minorDrawWidth - minorLineAA, minorGridUV);
                minorGrid2 *= saturate(minorTargetWidth / minorDrawWidth);
                minorGrid2 = saturate(minorGrid2 - axisLines2); // hack
                minorGrid2 = lerp(minorGrid2, minorTargetWidth, saturate(uvDeriv * 2.0 - 1.0));
                minorGrid2 = minorInvertLine ? 1.0 - minorGrid2 : minorGrid2;
                minorGrid2 = abs(i.uv.zw) > 0.5 ? minorGrid2 : 0.0;

                half minorGrid = lerp(minorGrid2.x, 1.0, minorGrid2.y);
                half majorGrid = lerp(majorGrid2.x, 1.0, majorGrid2.y);

                float2 axisDashUV = abs(frac((i.uv.zw + axisLineWidth * 0.5) * _AxisDashScale) * 2.0 - 1.0) - 0.5;
                float2 axisDashDeriv = uvDeriv * _AxisDashScale * 1.5;
                float2 axisDash = smoothstep(-axisDashDeriv, axisDashDeriv, axisDashUV);
                axisDash = i.uv.zw < 0.0 ? axisDash : 1.0;

            #if defined(UNITY_COLORSPACE_GAMMA)
                half4 xAxisColor = half4(GammaToLinearSpace(_XAxisColor.rgb), _XAxisColor.a);
                half4 yAxisColor = half4(GammaToLinearSpace(_YAxisColor.rgb), _YAxisColor.a);
                half4 zAxisColor = half4(GammaToLinearSpace(_ZAxisColor.rgb), _ZAxisColor.a);
                half4 xAxisDashColor = half4(GammaToLinearSpace(_XAxisDashColor.rgb), _XAxisDashColor.a);
                half4 yAxisDashColor = half4(GammaToLinearSpace(_YAxisDashColor.rgb), _YAxisDashColor.a);
                half4 zAxisDashColor = half4(GammaToLinearSpace(_ZAxisDashColor.rgb), _ZAxisDashColor.a);
                half4 centerColor = half4(GammaToLinearSpace(_CenterColor.rgb), _CenterColor.a);
                half4 majorLineColor = half4(GammaToLinearSpace(_MajorLineColor.rgb), _MajorLineColor.a);
                half4 minorLineColor = half4(GammaToLinearSpace(_MinorLineColor.rgb), _MinorLineColor.a);
                half4 baseColor = half4(GammaToLinearSpace(_BaseColor.rgb), _BaseColor.a);
            #else
                half4 xAxisColor = _XAxisColor;
                half4 yAxisColor = _YAxisColor;
                half4 zAxisColor = _ZAxisColor;
                half4 xAxisDashColor = _XAxisDashColor;
                half4 yAxisDashColor = _YAxisDashColor;
                half4 zAxisDashColor = _ZAxisDashColor;
                half4 centerColor = _CenterColor;
                half4 majorLineColor = _MajorLineColor;
                half4 minorLineColor = _MinorLineColor;
                half4 baseColor = _BaseColor;
            #endif

            #if defined(_AXIS_X)
                half4 aAxisColor = yAxisColor;
                half4 bAxisColor = zAxisColor;
                half4 aAxisDashColor = yAxisDashColor;
                half4 bAxisDashColor = zAxisDashColor;
            #elif defined(_AXIS_Z)
                half4 aAxisColor = xAxisColor;
                half4 bAxisColor = yAxisColor;
                half4 aAxisDashColor = xAxisDashColor;
                half4 bAxisDashColor = yAxisDashColor;
            #else
                half4 aAxisColor = xAxisColor;
                half4 bAxisColor = zAxisColor;
                half4 aAxisDashColor = xAxisDashColor;
                half4 bAxisDashColor = zAxisDashColor;
            #endif

                aAxisColor = lerp(aAxisDashColor, aAxisColor, axisDash.y);
                bAxisColor = lerp(bAxisDashColor, bAxisColor, axisDash.x);
                aAxisColor = lerp(aAxisColor, centerColor, axisLines2.y);

                half4 axisLines = lerp(bAxisColor * axisLines2.y, aAxisColor, axisLines2.x);

                half4 col = lerp(baseColor, minorLineColor, minorGrid *  minorLineColor.a);
                col = lerp(col, majorLineColor, majorGrid * majorLineColor.a);
                col = col * (1.0 - axisLines.a) + axisLines;

            #if defined(UNITY_COLORSPACE_GAMMA)
                col = half4(LinearToGammaSpace(col.rgb), col.a);
            #endif

                return col;
            }
            ENDCG
        }
    }
}