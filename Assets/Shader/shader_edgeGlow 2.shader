Shader "Custom/shader_edgeGlow2" {
	Properties {
		_EdgeColor("Edge Color ",color )=(1,1,1,1)
		_MainTex("Main Texture",2D)="white"{}
		_EdgeWidth("Edge Width",range(0,1))=0.5
	}
	SubShader {
		Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }
	ZWrite Off
  Cull Off
  Fog {
   Color (0,0,0,0)
  }
  Blend SrcAlpha One
  AlphaTest Greater 0.01
  ColorMask RGB
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};
		float4 _EdgeColor;
		float _EdgeWidth;
		void surf (Input IN, inout SurfaceOutput o) {
		float x0=IN.uv_MainTex.x-0.5;
		float y0=IN.uv_MainTex.y-0.5;
		float2 r0;
		if(abs(y0)>=abs(x0))
		{
			r0.y=0.5*sign(y0);
			r0.x=r0.y*x0/y0;
		}
		else
		{
			r0.x=0.5*sign(x0);
			r0.y=r0.x*y0/x0;
		}
		float dis=sqrt(r0.x*r0.x+r0.y*r0.y);
		float x1=x0==0?0:0.5*x0/dis;
		float y1=y0==0?0:0.5*y0/dis;
		float2 uv_1;
			uv_1.x=x1+0.5;
			uv_1.y=y1+0.5;
			float4 newPix=tex2D(_MainTex,uv_1);
	
			o.Albedo = newPix.rgb*_EdgeColor.rgb*2;
			
			o.Alpha=newPix.a;
	
			
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
