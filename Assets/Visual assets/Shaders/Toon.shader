Shader "Custom/Toon"
{
	Properties
	{
		_Color("Color", Color) = (1, 1, 1, 1)
		MainTex("Main Texture", 2D) = "white" {}
		AmbientColor("Ambient Color", Color) = (1,1,1,1)

		[HDR]
		SpecularColor("Specular Color", Color) = (1,1,1,1)
		Gloss("Gloss", Float) = 32

		[HDR]
		RimColor("Rim Color", Color) = (1,1,1,1)
		RimAmount("Rim Amount", Range(0, 1)) = 0
	}
		SubShader
	{
		Pass
		{
			Tags
			{
				"LightMode" = "ForwardBase"
				"PassFlags" = "OnlyDirectional"
			}

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			#include "Lighting.cginc"

			struct appdata
			{
				float3 normal : NORMAL;
				float4 vertex : POSITION;
				float4 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 worldNormal : NORMAL;
				float3 viewDirection : TEXCOORD1;
			};

			sampler2D MainTex;
			float4 MainTex_ST;

			v2f vert(appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, MainTex);
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
				o.viewDirection = WorldSpaceViewDir(v.vertex);
				return o;
			}

			float4 Color;
			float4 AmbientColor;

			float Gloss;
			float4 SpecularColor;

			float4 RimColor;
			float RimIntensity;

			float4 frag(v2f i) : SV_Target
			{
				float3 normal = normalize(i.worldNormal);
				float lightIntensity = smoothstep(0, 0.01, dot(_WorldSpaceLightPos0, normal));
				float4 light = lightIntensity * _LightColor0;

				float3 viewDirection = normalize(i.viewDirection);

				float3 LplusVnormal = normalize(_WorldSpaceLightPos0 + viewDirection);

				float specularIntensity = pow(lightIntensity * (dot(normal, LplusVnormal)), Gloss * 10);
				float specularIntensitySmooth = smoothstep(0.005, 0.01, specularIntensity);
				float4 specular = specularIntensitySmooth * SpecularColor;

				float rimIntensity = (1 - dot(viewDirection, normal)) * dot(_WorldSpaceLightPos0, normal);
				rimIntensity = smoothstep(RimIntensity - 0.01, RimIntensity + 0.01, rimIntensity);
				float4 rim = rimIntensity * RimColor;

				float4 sample = tex2D(MainTex, i.uv);

				return Color * sample * (AmbientColor + light + specular + rim);
			}
			ENDCG
		}
	}
}