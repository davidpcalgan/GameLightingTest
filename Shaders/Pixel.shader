Shader "Custom/Pixel" {
    Properties
    {
        _NormalMap ("Normal Map", 2D) = "bump" {}
		_Smoothness ("Smoothness", Range(0.01,1)) = 0.5
    }
    SubShader
    {
        Pass
        {
            Tags {"LightMode"="ForwardBase"}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityStandardBRDF.cginc"
            #include "Lighting.cginc"

            // compile shader into multiple variants, with and without shadows
            // (we don't care about any lightmaps yet, so skip these variants)
            #pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight
            // shadow helper functions and macros
            #include "AutoLight.cginc"
			
			#include "VertexShader.cginc"
			#include "ShaderFunctions.cginc"
			
            sampler2D _NormalMap;
			float4 _NormalMap_ST;
			float _Smoothness;
			
			v2f vert (appdata_full v) {
				return computeVert (v, _NormalMap_ST);
			}

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = fixed4(1,1,1,1);
				
				i.norm = MapNormals(i, _NormalMap);
				
				half diffuse = LightDiffusely(i, _WorldSpaceLightPos0.xyz);
				half specular = LightSpecularly(i, _WorldSpaceLightPos0.xyz, _Smoothness);
				
                fixed shadow = SHADOW_ATTENUATION(i);
				
                col.rgb = shadow * i.diff * ( diffuse * (1-specular) + specular );
                return col;
            }
            ENDCG
        }
		Pass
        {
            Tags {"LightMode"="ForwardAdd"}
			Blend One One
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            // compile shader into multiple variants, with and without shadows
            // (we don't care about any lightmaps yet, so skip these variants)
            #pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight
            // shadow helper functions and macros
            #include "AutoLight.cginc"
			
			#include "VertexShader.cginc"
			#include "ShaderFunctions.cginc"

            sampler2D _NormalMap;
			float4 _NormalMap_ST;
			float _Smoothness;
			
			v2f vert (appdata_full v) {
				return computeVert (v, _NormalMap_ST);
			}

            fixed4 frag (v2f i) : SV_Target
            {
				fixed4 col = fixed4(1,1,1,1);
				fixed3 lightDir = _WorldSpaceLightPos0.xyz - i.worldPos;
				
				i.norm = MapNormals(i, _NormalMap);
				
				fixed diffuse = LightDiffusely(i, lightDir);
				fixed specular = LightSpecularly(i, lightDir, _Smoothness);
				
                fixed shadow = SHADOW_ATTENUATION(i);
				
                col.rgb = shadow * i.diff * ( diffuse * (1-specular) + specular );
                return col;
            }
            ENDCG
        }

        // shadow casting support
        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
    }
}