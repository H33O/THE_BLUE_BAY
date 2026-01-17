Shader "Custom/URP Caustics Lit"
{
    Properties
    {
        [MainTexture] _BaseMap("Base Map", 2D) = "white" {}
        [MainColor] _BaseColor("Base Color", Color) = (1,1,1,1)
        
        _Smoothness("Smoothness", Range(0.0, 1.0)) = 0.5
        _Metallic("Metallic", Range(0.0, 1.0)) = 0.0
        
        [Toggle(_NORMALMAP)] _UseNormalMap("Use Normal Map", Float) = 0
        _BumpMap("Normal Map", 2D) = "bump" {}
        _BumpScale("Normal Scale", Float) = 1.0
        
        [Toggle(_CAUSTICS)] _EnableCaustics("Enable Caustics", Float) = 1
    }
    
    SubShader
    {
        Tags 
        { 
            "RenderType" = "Opaque" 
            "RenderPipeline" = "UniversalPipeline"
            "Queue" = "Geometry"
        }
        
        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode" = "UniversalForward" }
            
            HLSLPROGRAM
            #pragma vertex LitPassVertex
            #pragma fragment LitPassFragment
            
            #pragma shader_feature_local _NORMALMAP
            #pragma shader_feature_local _CAUSTICS
            
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ _ADDITIONAL_LIGHTS
            #pragma multi_compile_fragment _ _SHADOWS_SOFT
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            
            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);
            
            #ifdef _NORMALMAP
            TEXTURE2D(_BumpMap);
            SAMPLER(sampler_BumpMap);
            #endif
            
            CBUFFER_START(UnityPerMaterial)
                float4 _BaseMap_ST;
                float4 _BaseColor;
                float _Smoothness;
                float _Metallic;
                float _BumpScale;
            CBUFFER_END
            
            float _GlobalCausticsIntensity;
            float _GlobalCausticsSpeed;
            float _GlobalCausticsScale;
            float4 _GlobalCausticsColor;
            float _GlobalCausticsTime;
            float _GlobalWaterLevel;
            float _GlobalCausticsFadeDistance;
            
            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float4 tangentOS : TANGENT;
                float2 uv : TEXCOORD0;
            };
            
            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 positionWS : TEXCOORD1;
                float3 normalWS : TEXCOORD2;
                float4 tangentWS : TEXCOORD3;
                float3 viewDirWS : TEXCOORD4;
                float4 shadowCoord : TEXCOORD5;
            };
            
            float3 CalculateCaustics(float3 positionWS, float time)
            {
                float depthBelowWater = _GlobalWaterLevel - positionWS.y;
                
                if (depthBelowWater <= 0)
                {
                    return float3(0, 0, 0);
                }
                
                float depthFade = saturate(depthBelowWater / _GlobalCausticsFadeDistance);
                
                float2 uv = positionWS.xz * _GlobalCausticsScale * 0.1;
                
                float2 uv1 = uv + float2(time * _GlobalCausticsSpeed * 0.05, time * _GlobalCausticsSpeed * 0.03);
                float2 uv2 = uv * 1.3 - float2(time * _GlobalCausticsSpeed * 0.04, -time * _GlobalCausticsSpeed * 0.06);
                float2 uv3 = uv * 0.8 + float2(-time * _GlobalCausticsSpeed * 0.03, time * _GlobalCausticsSpeed * 0.05);
                
                float caustic1 = sin(uv1.x * 10.0 + time) * cos(uv1.y * 10.0 - time * 0.5);
                float caustic2 = sin(uv2.x * 12.0 - time * 0.8) * cos(uv2.y * 12.0 + time * 0.3);
                float caustic3 = sin(uv3.x * 8.0 + time * 0.6) * cos(uv3.y * 8.0 - time * 0.4);
                
                float combined = (caustic1 + caustic2 + caustic3) * 0.333;
                combined = pow(abs(combined), 2.0);
                combined = saturate(combined * 2.0);
                
                return combined * _GlobalCausticsColor.rgb * _GlobalCausticsIntensity * depthFade;
            }
            
            Varyings LitPassVertex(Attributes input)
            {
                Varyings output = (Varyings)0;
                
                VertexPositionInputs vertexInput = GetVertexPositionInputs(input.positionOS.xyz);
                VertexNormalInputs normalInput = GetVertexNormalInputs(input.normalOS, input.tangentOS);
                
                output.positionCS = vertexInput.positionCS;
                output.positionWS = vertexInput.positionWS;
                output.uv = TRANSFORM_TEX(input.uv, _BaseMap);
                output.normalWS = normalInput.normalWS;
                output.tangentWS = float4(normalInput.tangentWS, input.tangentOS.w);
                output.viewDirWS = GetWorldSpaceViewDir(vertexInput.positionWS);
                output.shadowCoord = GetShadowCoord(vertexInput);
                
                return output;
            }
            
            half4 LitPassFragment(Varyings input) : SV_Target
            {
                half4 albedo = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, input.uv) * _BaseColor;
                
                float3 normalWS = normalize(input.normalWS);
                
                #ifdef _NORMALMAP
                    half3 normalTS = UnpackNormalScale(SAMPLE_TEXTURE2D(_BumpMap, sampler_BumpMap, input.uv), _BumpScale);
                    float3 bitangentWS = cross(input.normalWS, input.tangentWS.xyz) * input.tangentWS.w;
                    float3x3 TBN = float3x3(input.tangentWS.xyz, bitangentWS, input.normalWS);
                    normalWS = normalize(mul(normalTS, TBN));
                #endif
                
                InputData inputData = (InputData)0;
                inputData.positionWS = input.positionWS;
                inputData.normalWS = normalWS;
                inputData.viewDirectionWS = SafeNormalize(input.viewDirWS);
                inputData.shadowCoord = input.shadowCoord;
                
                SurfaceData surfaceData = (SurfaceData)0;
                surfaceData.albedo = albedo.rgb;
                surfaceData.alpha = albedo.a;
                surfaceData.metallic = _Metallic;
                surfaceData.smoothness = _Smoothness;
                surfaceData.normalTS = float3(0, 0, 1);
                surfaceData.emission = 0;
                surfaceData.occlusion = 1;
                
                half4 color = UniversalFragmentPBR(inputData, surfaceData);
                
                #ifdef _CAUSTICS
                    if (_GlobalCausticsIntensity > 0.001)
                    {
                        float3 caustics = CalculateCaustics(input.positionWS, _GlobalCausticsTime);
                        color.rgb += caustics;
                    }
                #endif
                
                return color;
            }
            ENDHLSL
        }
        
        Pass
        {
            Name "ShadowCaster"
            Tags { "LightMode" = "ShadowCaster" }
            
            ColorMask 0
            
            HLSLPROGRAM
            #pragma vertex ShadowPassVertex
            #pragma fragment ShadowPassFragment
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            
            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
            };
            
            struct Varyings
            {
                float4 positionCS : SV_POSITION;
            };
            
            float3 _LightDirection;
            
            Varyings ShadowPassVertex(Attributes input)
            {
                Varyings output;
                float3 positionWS = TransformObjectToWorld(input.positionOS.xyz);
                float3 normalWS = TransformObjectToWorldNormal(input.normalOS);
                
                output.positionCS = TransformWorldToHClip(ApplyShadowBias(positionWS, normalWS, _LightDirection));
                
                return output;
            }
            
            half4 ShadowPassFragment(Varyings input) : SV_Target
            {
                return 0;
            }
            ENDHLSL
        }
        
        Pass
        {
            Name "DepthOnly"
            Tags { "LightMode" = "DepthOnly" }
            
            ColorMask R
            
            HLSLPROGRAM
            #pragma vertex DepthOnlyVertex
            #pragma fragment DepthOnlyFragment
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            
            struct Attributes
            {
                float4 positionOS : POSITION;
            };
            
            struct Varyings
            {
                float4 positionCS : SV_POSITION;
            };
            
            Varyings DepthOnlyVertex(Attributes input)
            {
                Varyings output;
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                return output;
            }
            
            half4 DepthOnlyFragment(Varyings input) : SV_Target
            {
                return 0;
            }
            ENDHLSL
        }
    }
    
    FallBack "Universal Render Pipeline/Lit"
}
