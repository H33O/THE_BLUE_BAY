Shader "Custom/WaterSurface"
{
    Properties
    {
        _Color ("Water Color", Color) = (0.1, 0.4, 0.7, 0.8)
        _DeepColor ("Deep Water Color", Color) = (0.0, 0.1, 0.3, 0.9)
        _FresnelColor ("Fresnel Color", Color) = (0.7, 0.9, 1.0, 1.0)
        _Smoothness ("Smoothness", Range(0, 1)) = 0.9
        _NormalStrength ("Normal Strength", Range(0, 2)) = 0.5
        
        [Header(Wave Settings)]
        _WaveSpeed ("Wave Speed", Float) = 1.0
        _WaveScale ("Wave Scale", Float) = 0.1
        _WaveHeight ("Wave Height", Float) = 0.5
        
        [Header(Transparency)]
        _Transparency ("Transparency", Range(0, 1)) = 0.7
        _DepthFade ("Depth Fade Distance", Float) = 5.0
    }
    
    SubShader
    {
        Tags 
        { 
            "RenderType" = "Transparent" 
            "Queue" = "Transparent"
            "RenderPipeline" = "UniversalPipeline"
        }
        
        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode" = "UniversalForward" }
            
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            
            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float2 uv : TEXCOORD0;
            };
            
            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float3 positionWS : TEXCOORD0;
                float3 normalWS : TEXCOORD1;
                float2 uv : TEXCOORD2;
                float3 viewDirWS : TEXCOORD3;
                float fogCoord : TEXCOORD4;
            };
            
            CBUFFER_START(UnityPerMaterial)
                float4 _Color;
                float4 _DeepColor;
                float4 _FresnelColor;
                float _Smoothness;
                float _NormalStrength;
                float _WaveSpeed;
                float _WaveScale;
                float _WaveHeight;
                float _Transparency;
                float _DepthFade;
            CBUFFER_END
            
            float3 CalculateWaveNormal(float2 uv, float time)
            {
                float2 uv1 = uv * _WaveScale + float2(time * _WaveSpeed * 0.05, time * _WaveSpeed * 0.03);
                float2 uv2 = uv * _WaveScale * 0.7 - float2(time * _WaveSpeed * 0.04, time * _WaveSpeed * 0.06);
                
                float wave1 = sin(uv1.x * 6.28 + uv1.y * 3.14) * cos(uv1.y * 4.0);
                float wave2 = cos(uv2.x * 4.0 - uv2.y * 5.0) * sin(uv2.x * 3.0);
                
                float3 normal;
                normal.x = (wave1 + wave2) * _NormalStrength;
                normal.y = 1.0;
                normal.z = (wave1 - wave2) * _NormalStrength * 0.5;
                
                return normalize(normal);
            }
            
            Varyings vert(Attributes input)
            {
                Varyings output;
                
                float3 positionWS = TransformObjectToWorld(input.positionOS.xyz);
                float time = _Time.y;
                
                float wave = sin(positionWS.x * _WaveScale + time * _WaveSpeed) * 
                           cos(positionWS.z * _WaveScale * 0.7 - time * _WaveSpeed * 0.8);
                wave += sin(positionWS.z * _WaveScale * 1.3 + time * _WaveSpeed * 1.2) * 0.5;
                
                positionWS.y += wave * _WaveHeight;
                
                output.positionWS = positionWS;
                output.positionCS = TransformWorldToHClip(positionWS);
                output.normalWS = TransformObjectToWorldNormal(input.normalOS);
                output.uv = input.uv;
                output.viewDirWS = GetWorldSpaceViewDir(positionWS);
                output.fogCoord = ComputeFogFactor(output.positionCS.z);
                
                return output;
            }
            
            half4 frag(Varyings input) : SV_Target
            {
                float3 viewDir = normalize(input.viewDirWS);
                float3 normal = CalculateWaveNormal(input.positionWS.xz, _Time.y);
                normal = normalize(lerp(input.normalWS, normal, 0.8));
                
                float fresnel = pow(1.0 - saturate(dot(viewDir, normal)), 3.0);
                
                Light mainLight = GetMainLight();
                float3 lightDir = normalize(mainLight.direction);
                float NdotL = saturate(dot(normal, lightDir));
                
                float3 waterColor = lerp(_DeepColor.rgb, _Color.rgb, NdotL * 0.5 + 0.5);
                waterColor = lerp(waterColor, _FresnelColor.rgb, fresnel * 0.6);
                
                float3 reflection = reflect(-viewDir, normal);
                float3 specular = pow(saturate(dot(reflection, lightDir)), 64.0 * _Smoothness) * mainLight.color;
                
                float3 finalColor = waterColor + specular * 0.5;
                finalColor = MixFog(finalColor, input.fogCoord);
                
                float alpha = lerp(_DeepColor.a, _Color.a, fresnel) * _Transparency;
                
                return half4(finalColor, alpha);
            }
            ENDHLSL
        }
    }
    
    FallBack "Universal Render Pipeline/Lit"
}
