Shader "Custom/scan"
{
   Properties
    {
        _DayTex ("Day", 2D) = "white" {}
        _NightTex ("Night", 2D) = "white" {}
        _HandTex ("Hand", 2D) = "white" {}
        _Light ("Night Lights", Range (1, 10)) = 1
        _Color ("Color ", Color) = (1,0,0,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        
        Pass
        {                
              Name "Water"
              Tags { "RenderType"="Opaque" }
              // ShaderLab commands go here.
              Cull BACK
              Blend SrcAlpha OneMinusSrcAlpha
                
              SetTexture [_DayTex] {
                 combine texture
              }
              // HLSL code goes here.
        }
        
            
        CGPROGRAM
        #pragma surface surf Lambert alpha:fade


        sampler2D _DayTex;
        sampler2D _NightTex;
        sampler2D _HandTex;
        float _Light;
        float4 _Color;        

        float wN;
        
        struct Input
        {
            float2 uv_DayTex;
            float2 uv_NightTex;
            float2 uv_HandTex;

            float3 worldPos;
            float3 viewDir;
        };   

        void surf (Input IN, inout SurfaceOutput o)
        {
            float3 dayResult = tex2D(_DayTex, IN.uv_DayTex);
            float3 nightResult = tex2D(_NightTex, IN.uv_NightTex) * _Color;
            float4 handresult = tex2D(_HandTex, IN.uv_HandTex);      

            if(handresult.a < 0.5f) {
                o.Albedo = handresult;
            }
            else {
                o.Albedo = nightResult;
                o.Alpha = tex2D(_NightTex, IN.uv_NightTex).a;
                o.Emission = tex2D(_NightTex, IN.uv_NightTex * _Light);
            }
            
        }
        ENDCG
    }

    FallBack "Diffuse"
}