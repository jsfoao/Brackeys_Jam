Shader "FX/Hologram Shader"
{
	Properties
	{
		_Color("Color", Color) = (0, 1, 1, 1)
		_MainTex("Base (RGB)", 2D) = "white" {}
		_GlitchTex("Glitch Texture", 2D) = "white" {}
		_AlphaTexture ("Alpha Mask (R)", 2D) = "white" {}
		_Scale ("Alpha Tiling", Float) = 3
		_ScrollSpeedV("Alpha scroll Speed", Range(0, 5.0)) = 1.0
		_GlowIntensity ("Glow Intensity", Range(0.01, 1.0)) = 0.5
		_GlitchSpeed ("Glitch Speed", Range(0, 50)) = 50.0
		_GlitchIntensity ("Glitch Intensity", Range(0.0, 0.1)) = 0
		_GlitchBlend("Glitch Blend", Range(0,1)) = 0
	}

	SubShader
	{
		Tags{ "Queue" = "Overlay" "IgnoreProjector" = "True" "RenderType" = "Transparent" }

		Pass
		{
			Lighting Off 
			ZWrite On
			Blend SrcAlpha One
			Cull Back

			CGPROGRAM
				
				#pragma vertex vertexFunc
				#pragma fragment fragmentFunc

				#include "UnityCG.cginc"

				struct appdata{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
					float3 normal : NORMAL;
				};

				struct v2f{
					float4 position : SV_POSITION;
					float2 uv : TEXCOORD0;
					float3 grabPos : TEXCOORD1;
					float3 viewDir : TEXCOORD2;
					float3 worldNormal : NORMAL;
				};

				fixed4 _Color, _MainTex_ST;
				sampler2D _MainTex, _AlphaTexture, _GlitchTex;
				half _Scale, _ScrollSpeedV, _GlowIntensity, _GlitchSpeed, _GlitchIntensity, _GlitchBlend;

				v2f vertexFunc(appdata IN)
			{
					v2f OUT;

					//Glitch
					// IN.vertex.z += sin(_Time.y * _GlitchSpeed * 5 * IN.vertex.y) * _GlitchIntensity;
					float x = _Time.y * _GlitchSpeed * 5 * IN.vertex.y;
					IN.vertex.z += (x - floor(x)) * _GlitchIntensity;
					
					OUT.position = UnityObjectToClipPos(IN.vertex);
					OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);

					//Alpha mask coordinates
					OUT.grabPos = UnityObjectToViewPos(IN.vertex);

					//Scroll Alpha mask uv
					OUT.grabPos.y += _Time * _ScrollSpeedV;

					OUT.worldNormal = UnityObjectToWorldNormal(IN.normal);
					OUT.viewDir = normalize(UnityWorldSpaceViewDir(OUT.grabPos.xyz));

					return OUT;
				}

				fixed4 fragmentFunc(v2f IN) : SV_Target
				{
					half dirVertex = (dot(IN.grabPos, 1.0) + 1) / 2;
					
					fixed4 alphaColor = tex2D(_AlphaTexture,  IN.grabPos.xy * _Scale);
					fixed4 pixelColor = tex2D (_MainTex, IN.uv);
					fixed4 glitchColor = tex2D (_GlitchTex, IN.uv * 2);

					pixelColor.w = alphaColor.w;
					fixed4 glitchedColor = (glitchColor * (1 + _GlowIntensity)) * _GlitchBlend;
					fixed4 nonGlitchedColor = (pixelColor * _Color * (1 + _GlowIntensity));
					return glitchedColor + nonGlitchedColor;
				}
			ENDCG
		}
	}
}