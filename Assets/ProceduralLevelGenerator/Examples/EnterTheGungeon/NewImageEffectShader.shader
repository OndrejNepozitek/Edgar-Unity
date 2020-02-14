// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Test/BWDiffuse" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_bwBlend ("Black & White blend", Range (0, 1)) = 0
		_alpha ("Alpha", Range (0, 1)) = 0.5
		_OtherTex ("Other (RGB)", 2D) = "black" {}
		_Color ("Main Color (A=Opacity)", Color) = (1,1,1,1)
		_Position("Position", Vector) = (.0, .0, .0)
		_Offset("Offset", Vector) = (.0, .0, .0)
		_TexSize("TexSize", Vector) = (.0, .0, .0)
	}
	SubShader {
	    Tags
        {
            "RenderType"="Transparent"
            "Queue" = "Transparent"
        }

		// Blend SrcAlpha OneMinusSrcAlpha

		Pass {
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform float _bwBlend;
			uniform float _alpha;
			uniform sampler2D _OtherTex;
			uniform sampler2D _CameraDepthTexture ;
			uniform float3 _Position;
			uniform float3 _TexSize;
			uniform float2 _Offset;

			uniform float4x4 _ViewProjInv;
			float4 GetWorldPositionFromDepth( float2 uv_depth )
			{    
				float depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, uv_depth);
				float4 H = float4(uv_depth.x*2.0-1.0, (uv_depth.y)*2.0-1.0, depth, 1.0);
				float4 D = mul(_ViewProjInv,H);
				return D/D.w;
			}

			struct v2f 
			{
				float4 pos  : POSITION;
				float2 uv   : TEXCOORD0;
				float2 wpos : TEXCOORD1;
			};

			/*v2f vert( appdata_img v )
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.wpos = mul(unity_ObjectToWorld, v.vertex).xyz;
				o.uv =  v.texcoord.xy;
				return o;
			}*/

			float4 frag(v2f_img i) : COLOR {
				float4 c = tex2D(_MainTex, i.uv);
				float4 d = tex2D(_OtherTex, i.uv);
				
				float lum = c.r*.3 + c.g*.59 + c.b*.11;
				float3 bw = float3( lum, lum, lum ); 
				
				float4 result = c;
				float4 worldpos = GetWorldPositionFromDepth(i.uv);
				float4 e = tex2D(_OtherTex, worldpos.xy / 20);
				

				//result.a = d.r;
				//result.a = floor(worldpos.x + 0.5) / 10;

				/*if (result.a < 0) {
					result.a = -1 * result.a;
				}*/

				/*result.rgb = e;
				result.a = 1;*/

				float floorX = floor(- _Offset.x + worldpos.x + 0.5) - 1;
				float floorY = floor(- _Offset.y + worldpos.y) - 0.5;
				float2 floorWorldpos = float2(floorX, floorY);

				if (floorX > 0 && floorY > 0) {
					if (floorWorldpos.x < _TexSize.x && floorWorldpos.y < _TexSize.y) {
						float4 f = tex2D(_OtherTex, float2(floorWorldpos.x / float(_TexSize.x), floorWorldpos.y / float(_TexSize.y)));

						if (f.r >= 1) {
							// result.rgba = float4(0, 0, 0, 0);
							result.rgba = float4(0.145098, 0.07450981, 0.1019608, 0);
							result.rgba = lerp(c.rgba, float4(0.145098, 0.07450981, 0.1019608, 0), 0.5);
						}
					}
				} else {
					// result.rgb = worldpos.xyz;
				}

				return result;
			}
			ENDCG
		}
	}
}