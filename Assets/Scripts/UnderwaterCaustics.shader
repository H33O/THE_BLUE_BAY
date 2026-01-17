Shader "Hidden/UnderwaterCaustics"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _CausticTex ("Caustic Texture", 2D) = "white" {}
        _Intensity ("Intensity", Range(0, 2)) = 0.5
        _Scale ("Scale", Range(0.1, 5)) = 1.0
        _Speed ("Speed", Range(0, 2)) = 0.3
        _Color ("Caustic Color", Color) = (0.5, 0.8, 1, 1)
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalPipeline" }
        
        Pass
        {
            Name "UnderwaterCaustics"
            ZTest Always
            ZWrite Off
            Cull Off
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            
            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };
            
            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };
            
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            
            TEXTURE2D(_CausticTex);
            SAMPLER(sampler_CausticTex);
            
            TEXTURE2D(_CameraDepthTexture);
            SAMPLER(sampler_CameraDepthTexture);
            
            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                float4 _CausticTex_ST;
                float _Intensity;
                float _Scale;
                float _Speed;
                float4 _Color;
                float _CustomTime;
            CBUFFER_END
            
            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);
                return OUT;
            }
            
            float3 GenerateProceduralCaustics(float2 uv, float time)
            {
                float2 uv1 = uv * _Scale + float2(time * _Speed * 0.05, time * _Speed * 0.03);
                float2 uv2 = uv * _Scale * 1.3 - float2(time * _Speed * 0.04, -time * _Speed * 0.06);
                float2 uv3 = uv * _Scale * 0.8 + float2(-time * _Speed * 0.03, time * _Speed * 0.05);
                
                float caustic1 = sin(uv1.x * 10.0 + time) * cos(uv1.y * 10.0 - time * 0.5);
                float caustic2 = sin(uv2.x * 12.0 - time * 0.8) * cos(uv2.y * 12.0 + time * 0.3);
                float caustic3 = sin(uv3.x * 8.0 + time * 0.6) * cos(uv3.y * 8.0 - time * 0.4);
                
                float combined = (caustic1 + caustic2 + caustic3) * 0.333;
                
                combined = pow(abs(combined), 2.0);
                combined = saturate(combined * 2.0);
                
                return combined * _Color.rgb;
            }
            
            half4 frag(Varyings IN) : SV_Target
            {
                half4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv);
                
                float3 caustics = GenerateProceduralCaustics(IN.uv, _CustomTime);
                
                color.rgb += caustics * _Intensity;
                
                return color;
            }
            ENDHLSL
        }
    }
    
    Fallback Off
}
