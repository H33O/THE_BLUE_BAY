Shader "Custom/CausticProjector"
{
    Properties
    {
        _ShadowTex ("Caustic Texture", 2D) = "white" {}
        _Intensity ("Intensity", Range(0, 2)) = 1
        _Color ("Tint Color", Color) = (0.5, 0.8, 1, 1)
    }
    
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        
        Pass
        {
            ZWrite Off
            Blend DstColor One
            Offset -1, -1
            
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
                float4 uvShadow : TEXCOORD0;
            };
            
            TEXTURE2D(_ShadowTex);
            SAMPLER(sampler_ShadowTex);
            
            float4x4 unity_Projector;
            
            CBUFFER_START(UnityPerMaterial)
                float _Intensity;
                float4 _Color;
            CBUFFER_END
            
            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uvShadow = mul(unity_Projector, IN.positionOS);
                return OUT;
            }
            
            half4 frag(Varyings IN) : SV_Target
            {
                float2 uv = IN.uvShadow.xy / IN.uvShadow.w;
                half4 caustic = SAMPLE_TEXTURE2D(_ShadowTex, sampler_ShadowTex, uv);
                
                caustic.rgb *= _Color.rgb * _Intensity;
                
                clip(IN.uvShadow.w);
                
                return caustic;
            }
            ENDHLSL
        }
    }
}
