Shader "Custom/shader_bright" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Brightness("Brightness",range(0.5,8))=6
		_Color("Main Color ",color )=(1,1,1,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		fixed _Brightness;
		fixed4 _Color;
		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb*_Color.rgb*_Brightness;
			o.Alpha = c.a*_Color.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
