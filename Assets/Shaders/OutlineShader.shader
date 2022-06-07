// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Outlined/Silhouetted Diffuse" {
	Properties {
		_Color ("Main Color", Color) = (.5,.5,.5,1)
		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_Outline ("Outline width", Range (0.0, 2)) = 0
		_MainTex ("Base (RGB)", 2D) = "white" { }
	}
 
CGINCLUDE
#include "UnityCG.cginc"
 
struct appdata {
	float4 vertex : POSITION;
	float3 normal : NORMAL;
};
 
struct v2f {
	float4 pos : POSITION;
	float4 color : COLOR;
};
 
uniform float _Outline;
uniform float4 _OutlineColor;
 
v2f vert(appdata v) {
	
	v2f o;
	o.pos = UnityObjectToClipPos(v.vertex);
	
	// just make a copy of incoming vertex data but scaled according to normal direction
							// Inverse transpose of model * view matrix.
	float3 norm   = mul ((float3x3)UNITY_MATRIX_IT_MV, v.normal);
	float2 offset = TransformViewToProjection(norm.xy);
 
	o.pos.xy += offset * o.pos.z * _Outline;
	o.color = _OutlineColor;
	return o;
}
ENDCG
 
	SubShader {
		Tags { "Queue" = "Transparent" }
 
		Pass {
			Name "OUTLINE"
			Tags { "LightMode" = "Always" }

			// Disables culling - all faces are drawn
			Cull Off
			//Controls whether pixels from this object are written to the depth buffer - off for non solid objects
			ZWrite Off
			// How should depth testing be performed
			ZTest Always
			// Write to the given channels of the default render target.
			ColorMask RGB // alpha not used
 
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			half4 frag(v2f i) :COLOR {
				return i.color;
			}
			ENDCG
		}
 
		Pass {
			Name "BASE"
			ZWrite On
			ZTest LEqual
			//Blend SrcAlpha OneMinusSrcAlpha
			Material {
				Diffuse [_Color]
				Ambient [_Color]
			}
			Lighting On

			// The texture block controls how the texture is applied. Inside the texture block can be up to two commands: combine and constantColor.
			SetTexture [_MainTex] {
				// Defines a constant color that can be used in the combine command
				ConstantColor [_Color]
				Combine texture * constant
			}

			// The texture block controls how the texture is applied. Inside the texture block can be up to two commands: combine and constantColor.
			SetTexture [_MainTex] {
				Combine previous * primary DOUBLE
			}
		}
	}
 
	SubShader {
		Tags { "Queue" = "Transparent" }
 
		Pass {
			Name "OUTLINE"
			Tags { "LightMode" = "Always" }
			Cull Front
			ZWrite Off
			ZTest Always
			ColorMask RGB
 
			// you can choose what kind of blending mode you want for the outline
			//Blend SrcAlpha OneMinusSrcAlpha // Normal
			//Blend One One // Additive
			//Blend One OneMinusDstColor // Soft Additive
			//Blend DstColor Zero // Multiplicative
			//Blend DstColor SrcColor // 2x Multiplicative
 
			CGPROGRAM
			#pragma vertex vert
			#pragma exclude_renderers gles xbox360 ps3
			ENDCG
			//combine formula is used for calculating both the RGB and alpha component of the color
								// Primary is the color from the lighting calculation
			SetTexture [_MainTex] { combine primary }
		}
 
		Pass {
			Name "BASE"
			ZWrite On
			ZTest LEqual
			//Blend SrcAlpha OneMinusSrcAlpha
			Material {
				Diffuse [_Color]
				Ambient [_Color]
			}
			Lighting On

			// textures are applied
			SetTexture [_MainTex] {
				// Constant is the color specified in ConstantColor
				ConstantColor [_Color]
				Combine texture * constant
			}
			// textures are applied
			SetTexture [_MainTex] {
				Combine previous * primary DOUBLE
			}
		}
	}
 
	Fallback "Diffuse"
}