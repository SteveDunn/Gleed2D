// ------------------------------------------------------------------------------------------------ //
// ----- Copyright 2011 Christopher Harris --------------------- http://krypton.codeplex.com/ ----- //
// ----------------------------------------------------------------- mailto:xixonia@gmail.com ----- //
// ------------------------------------------------------------------------------------------------ //

// ------------------------------------------------------------------------------------------------ //
// ----- Parameters ------------------------------------------------------------------------------- //

float4x4 Matrix;

texture Texture0;
texture Texture1;
float2	LightPosition;
float	LightIntensityFactor = 1;

float	ShadowStrech = 1000000;

float2 TexelBias;

float4 AmbientColor;

float Bluriness = 0.5f;
float BlurFactorU = 0;
float BlurFactorV = 0;

// ------------------------------------------------------------------------------------------------ //
// ----- Samplers --------------------------------------------------------------------------------- //

sampler2D tex0 = sampler_state
{
	Texture = <Texture0>;
	AddressU = Clamp;
	AddressV = Clamp;
};

sampler2D tex1 = sampler_state
{
	Texture = <Texture1>;
	AddressU = Clamp;
	AddressV = Clamp;
};

// ------------------------------------------------------------------------------------------------ //
// ----- Structures ------------------------------------------------------------------------------- //

struct ShadowHullVertex
{
	float4 Position : POSITION0;
	float2 Normal	: NORMAL0;
	float4 Color	: COLOR0;
};

struct VertexPositionColor
{
	float4 Position : POSITION0;
	float4 Color	: COLOR0;
};

struct VertexPositionColorTexture
{
	float4 Position	: POSITION0;
	float4 Color	: COLOR0;
	float2 TexCoord	: TEXCOORD0;
};

struct VertexPositionTexture
{
	float4 Position	: POSITION0;
	float2 TexCoord : TEXCOORD0;
};

struct Color2
{
	float4 Color0 : COLOR0;
	float4 Color1 : COLOR1;
};

// ------------------------------------------------------------------------------------------------ //
// ----- Vertex Shaders --------------------------------------------------------------------------- //

VertexPositionTexture VS_TextureNoTransform(VertexPositionTexture input)
{
	return input;
};

VertexPositionColorTexture VS_ColorTexture(VertexPositionColorTexture input)
{
	input.Position = mul(input.Position, Matrix);

	return input;
};

VertexPositionColor VS_Hull(ShadowHullVertex input)
{
	VertexPositionColor output;

	output.Position = mul(input.Position, Matrix);
	output.Color = input.Color;

	return output;
};

VertexPositionColor VS_Hull_RadialStretch(ShadowHullVertex input)
{
    float2 direction = normalize(LightPosition.xy - input.Position.xy);
    
    if(dot(input.Normal.xy, direction) < 0)
    {
		// Stretch backfacing vertices
		input.Position.xy -= direction * ShadowStrech;
    }
	
	VertexPositionColor output;

	output.Position = mul(input.Position, Matrix);
	output.Color = input.Color;

	return output;
};

// ------------------------------------------------------------------------------------------------ //
// ----- Pixel Shaders ---------------------------------------------------------------------------- //

// ------------------------------------------------------------------------------------------------ //
// ----- Techniques ------------------------------------------------------------------------------- //

float4 PS_Texture(in float2 texCoord : TEXCOORD0) : COLOR0
{
	return tex2D(tex0, texCoord + TexelBias);
};

float4 PS_ColorTexture(in float4 color : COLOR0, in float2 texCoord : TEXCOORD0) : COLOR0
{
	return tex2D(tex0, texCoord) * color;
};

float4 PS_LightTexture(VertexPositionColorTexture input) : COLOR0
{
	return pow(abs(tex2D(tex0, input.TexCoord)) * input.Color, LightIntensityFactor);
};

float4 PS_Color(in float4 color : COLOR0) : COLOR0
{
	return color;
};

float4 PS_White() : COLOR0
{
	return float4(1, 1, 1, 1);
};

float4 PS_Black() : COLOR0
{
	return float4(0, 0, 0, 1);
};

float4 PS_Debug() : COLOR0
{
	return float4(1, 0, 0, 1);
};

float4 PS_BlurH(in float2 texCoord : TEXCOORD0) : COLOR0
{
	float blurFactor = Bluriness * BlurFactorU / 4.0f;

	return
		tex2D(tex0, float2(texCoord.x - blurFactor * 4,	texCoord.y)	+ TexelBias) * 0.05f +
		tex2D(tex0, float2(texCoord.x - blurFactor * 3,	texCoord.y)	+ TexelBias) * 0.09f +
		tex2D(tex0, float2(texCoord.x - blurFactor * 2,	texCoord.y)	+ TexelBias) * 0.12f +
		tex2D(tex0, float2(texCoord.x - blurFactor,		texCoord.y)	+ TexelBias) * 0.15f +
		tex2D(tex0, float2(texCoord.x,					texCoord.y)	+ TexelBias) * 0.18f +
		tex2D(tex0, float2(texCoord.x + blurFactor,		texCoord.y)	+ TexelBias) * 0.15f +
		tex2D(tex0, float2(texCoord.x + blurFactor * 2,	texCoord.y)	+ TexelBias) * 0.12f +
		tex2D(tex0, float2(texCoord.x + blurFactor * 3,	texCoord.y)	+ TexelBias) * 0.09f +
		tex2D(tex0, float2(texCoord.x + blurFactor * 4,	texCoord.y)	+ TexelBias) * 0.05f;
}

float4 PS_BlurV(in float2 texCoord : TEXCOORD0) : COLOR0
{
	float blurFactor = Bluriness * BlurFactorV / 4.0f;

	return
		tex2D(tex0, float2(texCoord.x, texCoord.y - blurFactor * 4)	+ TexelBias) * 0.05f +
		tex2D(tex0, float2(texCoord.x, texCoord.y - blurFactor * 3)	+ TexelBias) * 0.09f +
		tex2D(tex0, float2(texCoord.x, texCoord.y - blurFactor * 2)	+ TexelBias) * 0.12f +
		tex2D(tex0, float2(texCoord.x, texCoord.y - blurFactor)		+ TexelBias) * 0.15f +
		tex2D(tex0, float2(texCoord.x, texCoord.y)					+ TexelBias) * 0.18f +
		tex2D(tex0, float2(texCoord.x, texCoord.y + blurFactor)		+ TexelBias) * 0.15f +
		tex2D(tex0, float2(texCoord.x, texCoord.y + blurFactor * 2)	+ TexelBias) * 0.12f +
		tex2D(tex0, float2(texCoord.x, texCoord.y + blurFactor * 3)	+ TexelBias) * 0.09f +
		tex2D(tex0, float2(texCoord.x, texCoord.y + blurFactor * 4)	+ TexelBias) * 0.05f;
}

// ------------------------------------------------------------------------------------------------
// ----- Technique: TextureToTarget ---------------------------------------------------------------

technique TextureToTarget_Add
{
	pass Pass1
	{
		StencilEnable = False;

		BlendOp = Add;
		SrcBlend = One;
		DestBlend = One;

		AlphaBlendEnable = True;
		
		CullMode = CCW;

		VertexShader = compile vs_2_0 VS_TextureNoTransform();
		PixelShader = compile ps_2_0 PS_Texture();
	}
};

technique TextureToTarget_Multiply
{
	pass Pass1
	{
		StencilEnable = False;

		BlendOp = Add;
		SrcBlend = Zero;
		DestBlend = SrcColor;

		AlphaBlendEnable = True;
		
		CullMode = CCW;

		VertexShader = compile vs_2_0 VS_TextureNoTransform();
		PixelShader = compile ps_2_0 PS_Texture();
	}
};

technique LightTexture
{
	pass Pass1
	{
		StencilEnable = False;

		BlendOp = Add;
		DestBlend = Zero;
		SrcBlend = SrcAlpha;
		
		AlphaBlendEnable = True;

		VertexShader = compile vs_2_0 VS_ColorTexture();
		PixelShader = compile ps_2_0 PS_LightTexture();
	}
};

// ------------------------------------------------------------------------------------------------
// ----- Technique: PointLight_Shadow -------------------------------------------------------------

technique PointLight_Shadow_Solid
{
	pass Shadow
	{
		StencilEnable = False;

		ScissorTestEnable = True;

		AlphaBlendEnable = True;
		BlendOp = Add;
		SrcBlend = DestColor;
		DestBlend = Zero;

		ColorWriteEnable = Alpha;

		VertexShader = compile vs_2_0 VS_Hull_RadialStretch();
		PixelShader = compile ps_2_0 PS_Color();
	}
};

technique PointLight_Shadow_Illuminated
{
	pass Shadow
	{
		StencilEnable = False;

		ScissorTestEnable = True;

		AlphaBlendEnable = True;
		BlendOp = Add;
		SrcBlend = DestAlpha;
		DestBlend = Zero;

		ColorWriteEnable = Alpha;

		VertexShader = compile vs_2_0 VS_Hull_RadialStretch();
		PixelShader = compile ps_2_0 PS_Color();
	}

	pass Hull
	{
		StencilEnable = False;

		ScissorTestEnable = True;

		AlphaBlendEnable = True;
		BlendOp = Add;
		SrcBlend = One;
		DestBlend = Zero;

		ColorWriteEnable = Alpha;

		VertexShader = compile vs_2_0 VS_Hull();
		PixelShader = compile ps_2_0 PS_White();
	}
};

technique PointLight_Shadow_Occluded
{
	pass Shadow
	{
		StencilEnable = True;
		StencilFunc = Always;
		StencilPass = IncrSat;
		StencilFail = Keep;

		ScissorTestEnable = True;

		AlphaBlendEnable = True;
		BlendOp = Add;
		SrcBlend = DestAlpha;
		DestBlend = Zero;

		ColorWriteEnable = Alpha;

		VertexShader = compile vs_2_0 VS_Hull_RadialStretch();
		PixelShader = compile ps_2_0 PS_Color();
	}

	pass Hull
	{
		StencilEnable = True;
		StencilFunc = Equal;
		StencilRef = 1;
		StencilPass = Keep;

		ScissorTestEnable = True;

		AlphaBlendEnable = True;
		BlendOp = Add;
		SrcBlend = One;
		DestBlend = Zero;

		ColorWriteEnable = Alpha;

		VertexShader = compile vs_2_0 VS_Hull();
		PixelShader = compile ps_2_0 PS_White();
	}
};

technique PointLight_Light
{
	pass Light
	{
		StencilEnable = False;

		ScissorTestEnable = False;

		AlphaBlendEnable = True;
		BlendOp = Add;
		SrcBlend = DestAlpha;
		DestBlend = One;
		
		ColorWriteEnable = Red | Green | Blue;

		VertexShader = compile vs_2_0 VS_ColorTexture();
		PixelShader = compile ps_2_0 PS_LightTexture();
	}
};

technique ClearTarget_Alpha
{
	pass Pass1
	{
		StencilEnable = False;

		AlphaBlendEnable = True;
		BlendOp = Add;
		SrcBlend = One;
		DestBlend = One;
		
		ScissorTestEnable = True;
		
		ColorWriteEnable = Red | Green | Blue | Alpha;
		
		VertexShader = compile vs_2_0 VS_TextureNoTransform();
		PixelShader = compile ps_2_0 PS_Black();
	}
};

// ------------------------------------------------------------------------------------------------
// ----- Technique: Blur --------------------------------------------------------------------------

technique Blur
{
    pass HorizontalBlur
    {
		ScissorTestEnable = False;
		StencilEnable = False;
		AlphaBlendEnable = False;

		CullMode = CCW;

		VertexShader = compile vs_2_0 VS_TextureNoTransform();
        PixelShader = compile ps_2_0 PS_BlurH();
    }
    
    pass VerticalBlur
    {
		ScissorTestEnable = False;
		StencilEnable = False;
		AlphaBlendEnable = False;

		CullMode = CCW;

		VertexShader = compile vs_2_0 VS_TextureNoTransform();
        PixelShader = compile ps_2_0 PS_BlurV();
    }
}

// ------------------------------------------------------------------------------------------------
// ----- Technique: DebugDraw ---------------------------------------------------------------------

technique DebugDraw
{
	pass Solid
	{
		StencilEnable = False;
		AlphaBlendEnable = False;

		VertexShader = compile vs_2_0 VS_Hull();
		PixelShader = compile ps_2_0 PS_Debug();
	}
};