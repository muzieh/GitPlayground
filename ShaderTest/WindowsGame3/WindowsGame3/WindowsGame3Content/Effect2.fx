float2 LightPosition;
float Intensity;
float Factor;

sampler TextureSampler : register(s0);
float4 PixelShaderFunction(float4 coor : TEXCOORD0) : COLOR0
{
	float4 Color = tex2D(TextureSampler, coor);
	float4 oryg = Color;
	
	float dist = sqrt( pow (LightPosition.x-coor.x,Factor) + pow((LightPosition.y-coor.y),Factor));
	Color.rgb = Color.rgb * Intensity * 2/dist;

	if( oryg.r < 0.5 && oryg.g < 0.5 && oryg.b < 0.5 )
	{
		Color.r = 0;
		Color.g = 0;
		Color.b = 0;
	}
	return Color;
}

technique Technique1
{
	pass Pass1
	{
		PixelShader = compile ps_2_0 PixelShaderFunction();
	}
}
