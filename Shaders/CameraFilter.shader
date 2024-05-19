Shader "Hidden/ToCel" {
    Properties {
		_Darkvision ("Darkvision", Range(0,1)) = 0
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }
    SubShader {
        Pass {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"
            uniform sampler2D _MainTex;
            uniform float _Darkvision;
            float4 frag(v2f_img i) : COLOR {
                float4 c = tex2D(_MainTex, i.uv);
                
                float lum = c.r*.3 + c.g*.59 + c.b*.11;
                float3 bw = _Darkvision * 0.6 * float3(lum, lum, lum) + (1 - _Darkvision) * c.rgb;
				bw = bw * ((lum > 0.9 ? 2 : (lum > 0.4 ? 0.5 : (lum > 0.05 ? 0.1 : 0))) / lum);
                
                float4 result = c;
                result.rgb = bw; 
                return result;
            }
            ENDCG
        }
    }
}