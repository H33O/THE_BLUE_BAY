Shader "Custom/UnderwaterDistortion"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Intensity ("Distortion Intensity", Range(0, 0.1)) = 0.02
        _CustomTime ("Custom Time", Float) = 0
        _DistortionScale ("Distortion Scale", Float) = 20
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalPipeline" }
        
        Pass
        {
            Name "UnderwaterDistortion"
            
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
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };
            
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            
            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                float _Intensity;
                float _CustomTime;
                float _DistortionScale;
            CBUFFER_END
            
            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);
                return OUT;
            }
            
            half4 frag(Varyings IN) : SV_Target
            {
                float2 distortionUV = IN.uv * _DistortionScale;
                
                float wave1 = sin(distortionUV.x * 3.0 + _CustomTime * 2.0) * cos(distortionUV.y * 2.5 + _CustomTime * 1.5);
                float wave2 = sin(distortionUV.y * 2.0 - _CustomTime * 1.8) * cos(distortionUV.x * 3.5 - _CustomTime * 2.2);
                
                float2 distortion = float2(wave1, wave2) * _Intensity;
                
                float vignette = length(IN.uv - 0.5) * 2.0;
                vignette = saturate(1.0 - vignette * 0.5);
                distortion *= vignette;
                
                float2 distortedUV = IN.uv + distortion;
                
                half4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, distortedUV);
                
                return color;
            }
            ENDHLSL
        }
    }
}
