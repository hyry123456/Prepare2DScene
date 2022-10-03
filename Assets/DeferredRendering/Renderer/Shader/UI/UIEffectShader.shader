//实现UI特效用的特殊材质
Shader "Defferer/UIEffectShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off
            HLSLINCLUDE
            #pragma vertex vert
            #pragma fragment frag

            #include "../../ShaderLibrary/Common.hlsl"

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            struct Attributes {
                float3 positionOS : POSITION;
                float2 baseUV : TEXCOORD0;
                float4 color : COLOR;
            };

            struct FragInput
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            FragInput vert(Attributes input){
                FragInput output;
                output.positionCS = TransformObjectToHClip(input.positionOS);
                output.uv = input.baseUV;
                output.color = input.color;
            }

            float4 frag(FragInput input) : SV_Target{
                return SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv.xy) * input.color;
            }

            ENDHLSL
        }
    }
}
