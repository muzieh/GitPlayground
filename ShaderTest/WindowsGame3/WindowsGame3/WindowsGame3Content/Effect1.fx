float PercentageR;
float PercentageG;
float PercentageB;

sampler TextureSampler : register(s0);
float4 PixelShaderFunction(float4 Tex : TEXCOORD0) : COLOR0
{
	float4 Color = tex2D(TextureSampler, Tex);
	float r = Color.r;
	float g = Color.g;
	float b = Color.b;
	
	Color.r = r * PercentageR;
	Color.g = g * PercentageG;
	Color.b = b * PercentageB;
	return Color;
	
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
