sampler TextureSampler : register(s0);

Texture2D<float4> xSpecularMap;
Texture2D<float4> xNormalMap;
float3 xLightPosition;
float4 xLightColor;
uint xWidth;
uint xHeight;

sampler Sampler =
sampler_state
{
	Texture = xNormalMap;
	MipFilter = LINEAR;
	MinFilter = LINEAR;
	MagFilter = LINEAR;
	AddressU = CLAMP;
	AddressV = CLAMP;
	AddressW = CLAMP;
};

float4 main(float4 color : COLOR0, float2 texCoord : TEXCOORD0) : COLOR0
{
	float4 tex = tex2D(TextureSampler, texCoord);

	float3 texC3;
	texC3.x = texCoord.x * xWidth;
	texC3.y = texCoord.y * xHeight;
	texC3.z = 0;

	float4 shadingResult = float4(0, 0, 0, 0);

		float4 normal = xNormalMap.Sample(Sampler, texCoord) - float4(.5, .5, .5, .5);
		float specular = xSpecularMap.Sample(Sampler, texCoord).r;
	float3 displacement = normalize(xLightPosition.xyz - texC3);

		float intensity = dot(normal.xyz, displacement);
	shadingResult += intensity * xLightColor;

	//float3 halfvector = normalize(displacement + float3(0,0,-1));
	//float specintensity = pow(saturate(dot(halfvector, normal.xyz)), .3);
	//shadingResult += specintensity * xLightColor;

	shadingResult += float4(.3, .3, .3, 0);

	return saturate(tex * shadingResult * color);
}

technique Light
{
	pass Pass1
	{
		PixelShader = compile ps_2_0 main();
	}
}