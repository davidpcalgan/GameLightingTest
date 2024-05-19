struct v2f
{
	float2 uv : TEXCOORD0;
	fixed3 diff : COLOR0;
	float4 pos : SV_POSITION0;
	fixed3 worldPos : POSITIONT;
	fixed3 norm : NORMAL;
	fixed3 tan : TANGENT;
	SHADOW_COORDS(1) // put shadows data into TEXCOORD1
};

v2f computeVert (appdata_full v, float4 _NormalMap_ST)
{
	v2f o;
	o.pos = UnityObjectToClipPos(v.vertex);
	o.worldPos = mul(unity_ObjectToWorld,v.vertex);
	o.uv = v.texcoord * _NormalMap_ST.xy + _NormalMap_ST.zw;
	o.norm = UnityObjectToWorldNormal(v.normal);
	o.tan = UnityObjectToWorldNormal(v.tangent);
	o.diff = _LightColor0.rgb;
	
	TRANSFER_SHADOW(o)
	return o;
}
