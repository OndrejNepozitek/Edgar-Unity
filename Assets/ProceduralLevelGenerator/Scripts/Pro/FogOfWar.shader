Shader "Hidden/FogOfWar" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_VisionTex ("Vision texture (RGB)", 2D) = "black" {}	
		_VisionTexOffset("Vision texture offset", Vector) = (.0, .0, .0)
		_VisionTexSize("Vision texture size", Vector) = (.0, .0, .0)
		_FogColor ("Fog Color", Color) = (1,1,1,1)
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

			uniform sampler2D _CameraDepthTexture;
			uniform float4x4 _ViewProjInv;

			uniform sampler2D _MainTex;
			uniform sampler2D _VisionTex;
			uniform float2 _VisionTexOffset;
			uniform float2 _VisionTexSize;
			uniform float4 _FogColor;

			float4 GetWorldPositionFromDepth( float2 uv_depth )
			{    
				float depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, uv_depth);
				float4 H = float4(uv_depth.x*2.0-1.0, (uv_depth.y)*2.0-1.0, depth, 1.0);
				float4 D = mul(_ViewProjInv,H);
				return D/D.w;
			}

			float4 frag(v2f_img i) : COLOR {
				float4 c = tex2D(_MainTex, i.uv);			
				float4 result = c;
				float4 worldpos = GetWorldPositionFromDepth(i.uv);
				
				float floorX = floor(- _VisionTexOffset.x + worldpos.x + 0.5) - 1;
				float floorY = floor(- _VisionTexOffset.y + worldpos.y) - 0.5;
				float2 floorWorldpos = float2(floorX, floorY);

				if (floorX > 0 && floorY > 0 && floorWorldpos.x < _VisionTexSize.x && floorWorldpos.y < _VisionTexSize.y) {
					float4 f = tex2D(_VisionTex, float2(floorWorldpos.x / float(_VisionTexSize.x), floorWorldpos.y / float(_VisionTexSize.y)));
					result.rgba = lerp(_FogColor, c.rgba, f.r);
				} else {
					result.rgba = lerp(_FogColor, c.rgba, 0);
				}

				return result;
			}
			ENDCG
		}
	}
}