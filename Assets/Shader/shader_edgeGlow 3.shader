Shader "Custom/shader_edgeGlow3" {
	Properties {
		_EdgeColor("Edge Color ",color )=(1,1,1,1)
		_MainTex("Main Texture",2D)="white"{}
		_EdgeWidth("Edge Width",range(0,1))=0.5
		_Speed("Edge Move Speed",float)=0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert
		#include "UnityCG.cginc"

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};
		float4 _EdgeColor;
		float _EdgeWidth;
		float _Speed;
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
		
		float angle=_Time.y*_Speed;
		float sinTheta=sin(angle),cosTheta=cos(angle);
		float2x2 rM=float2x2(cosTheta,-sinTheta,sinTheta,cosTheta);
//			float2x2 rM=float2x2(1,0,0,1);
		float2 coor=float2(x1,y1);
//		coor.x=x1;
//		coor.y=y1;
		coor=mul(rM,coor);
		float2 uv_1;
			uv_1.x=coor.x+0.5;
			uv_1.y=coor.y+0.5;
//			uv_1.x=x1+0.5;
//			uv_1.y=y1+0.5;
			float4 newPix=tex2D(_MainTex,uv_1);
			o.Albedo = newPix.rgb;
			o.Alpha=newPix.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
