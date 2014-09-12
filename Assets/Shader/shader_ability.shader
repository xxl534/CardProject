Shader "Custom/shader_ability" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Brightness("Brightness",range(0.5,8))=6
		_Darkness("Darkness",range(0.5,8))=1
		_Color("Main Color ",color )=(1,1,1,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		float cooldown;
		fixed _Brightness;
		fixed _Darkness;
		fixed4 _Color;
		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
		 fixed bright;
		 const float PI=3.1415926;
		if(cooldown==1)
			{
			bright=_Brightness;
			}
			else
			{
				float x=IN.uv_MainTex.y-0.5;
				float y=IN.uv_MainTex.x-0.5;
				float arc_percentage=atan2(y,x)/(2*PI);
				if(arc_percentage<0)
				{
					arc_percentage+=1;
				}
				if(arc_percentage>cooldown)
				{
					bright=_Darkness;
				}			
				else
				{
					bright=_Brightness;
				}
			}
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb*_Color.rgb*bright;
			o.Alpha = c.a*_Color.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
