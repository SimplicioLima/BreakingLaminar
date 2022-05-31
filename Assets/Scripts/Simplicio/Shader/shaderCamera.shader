Shader "Custom/shaderCamera"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _raio ("Raio", Range (0, 1)) = 0
        _speed ("Speed", Range (0, 100)) = 1
        _DoTheWigle ("Wiggle", Range (1, 100)) = 2
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float4 filter(sampler2D tex, float2 uv, float2 size)
            {
                float4 res = float4(0,0,0,0);
                float3x3 f = { 1, 1, 1,
                                1, 1, 1,
                               1, 1, 1};
    

                for(float x = -1 ; x <= 1; x++)
                {
                     for(float y = -1 ; y <= 1; y++)
                      {
                        res += tex2D(tex, uv + float2(x * size.x, y * size.y)) * f[x][y];
                      }
                }
        
                return res ;
            }

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            float4 _MainTex_;
            float _raio;
            float _speed;
            float _DoTheWigle;

            fixed4 frag (v2f i) : SV_Target
            {
                float2 newUvs = _raio * float2(sin(_Time.y * _speed + i.uv.y * _DoTheWigle), cos(_Time.y * _speed + i.uv.x * _DoTheWigle));
                fixed4 col = tex2D(_MainTex, i.uv + newUvs);
                
                return col;
            }
            ENDCG
        }
    }
}
