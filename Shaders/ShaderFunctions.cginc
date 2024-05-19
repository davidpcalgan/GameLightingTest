fixed3 MapNormals (v2f i, sampler2D map) {
	fixed4 tangentSpaceNormal = tex2D(map, i.uv) * 2 - 1;
	float3 binormal = cross(i.norm, i.tan.xyz);
	return normalize(
		tangentSpaceNormal.x * i.tan +
		-tangentSpaceNormal.y * binormal +
		tangentSpaceNormal.z * i.norm
	);
}

fixed LightDiffusely (v2f i, fixed3 lightDir) {
	return (DotClamped(i.norm, normalize(lightDir))) / (1 + dot(lightDir, lightDir));
}

fixed LightSpecularly (v2f i, fixed3 lightDir, float smoothness) {
	float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);
	return pow(DotClamped(normalize(normalize(lightDir) + viewDir), i.norm), smoothness * 100) / (1 + dot(lightDir, lightDir));
}