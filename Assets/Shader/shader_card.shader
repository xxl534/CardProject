Shader "Custom/Card" {
	Properties {
		_MainColor("Main Color ",color )=(1,1,1,1)
		_MainTex("Main Texture",2D)="white"{}
//		_Brightness("Brightness",range(0,10))=6
	}
	SubShader {
//		Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }
Tags{"RenderType"="Opaque"}
//	ZWrite Off
//  Cull Off
//  Fog {
//   Color (0,0,0,0)
//  }
  Blend SrcAlpha OneMinusSrcAlpha
//  Blend SrcAlpha OneMinusSrcAlpha
  AlphaTest Greater 0.01
  ColorMask RGB
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};
		float4 _MainColor;
		void surf (Input IN, inout SurfaceOutput o) {
		
			float4 pix=tex2D(_MainTex,IN.uv_MainTex);
//			o.Albedo = pix.rgb;
			o.Albedo = pix.rgb*_MainColor.rgb;
//			if(pix.a>0.01)
//			{
//				o.Alpha=1;
//			}
//			else
//			{
//			o.Alpha=0;
//			}
			o.Alpha=pix.a*_MainColor.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
