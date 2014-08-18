Shader "Custom/UltraedgeGlow" {
	Properties {
		_EdgeColor("Edge Color ",color )=(1,1,1,1)
		_MainTex("Main Texture",2D)="white"{}
		_EdgeWidth("Edge Width",range(0,1))=0.5
		_Speed("Edge Move Speed",float)=0
	}
	SubShader {
		Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }
	ZWrite Off
  Cull Off
  Fog {
   Color (0,0,0,0)
  }
  Blend SrcAlpha One
//  Blend SrcColor one
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
		float _Speed;
		void surf (Input IN, inout SurfaceOutput o) {
		float x0=IN.uv_MainTex.x-0.5;
		float y0=IN.uv_MainTex.y-0.5;
		float2 r0;
		if(abs(y0)>=abs(x0))
		{
			r0.y=sign(y0)*0.5;
			r0.x=r0.y*x0/y0;
			if(abs(y0)<_EdgeWidth*0.5)
			{
				y0=0;
				x0=0;
			}
			else
			{
				x0=(x0-r0.x*_EdgeWidth)/(1-_EdgeWidth);
				y0=(y0-r0.y*_EdgeWidth)/(1-_EdgeWidth);
			}
		}
		else
		{
			r0.x=0.5*sign(x0);
			r0.y=r0.x*y0/x0;
			if(abs(x0)<_EdgeWidth*0.5)
			{
				y0=0;
				x0=0;
			}
			else
			{
				x0=(x0-r0.x*_EdgeWidth)/(1-_EdgeWidth);
				y0=(y0-r0.y*_EdgeWidth)/(1-_EdgeWidth);
			}
		}
		float dis=sqrt(r0.x*r0.x+r0.y*r0.y);
		float x1=x0==0?0:0.5*x0/dis;
		float y1=y0==0?0:0.5*y0/dis;
		float angle=_Time.y*_Speed;
		
		float sinTheta=sin(angle),cosTheta=cos(angle);
		float2x2 rM=float2x2(cosTheta,-sinTheta,sinTheta,cosTheta);
		float2 coor=float2(x1,y1);
		coor=mul(rM,coor);
		float2 uv_1;
			uv_1.x=coor.x+0.5;
			uv_1.y=coor.y+0.5;
			float4 newPix=tex2D(_MainTex,uv_1);
			o.Albedo = newPix.rgb*_EdgeColor.rgb;
			o.Alpha=newPix.a*_EdgeColor.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
