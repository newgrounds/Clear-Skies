//Donze's Toon Shader///////////////////////////////
//By David Donze////////////////////////////////////
//version 1.0///////////////////////////////////////
////////////////////////////////////////////////////

string ParamID = "0x003";

float4 ClearColor : DIFFUSE = {0,0,0,1.0};
float ClearDepth = 1.0;

// Light Info///////////////////////////////
float3 g_LightDir : Direction <  

> = {-0.577, -0.577, 0.577};

float4 g_LightPos : POSITION <  
	string UIName = "Light Position 1"; 
	string Object = "PointLight";
	string Space = "World";
	int refID = 1;
> = {-0.577, -0.577, 0.577,0.0};

float4 g_LightCol : LIGHTCOLOR
<
	int LightRef = 1;
	string UIWidget = "None";
> = float4(0.0f, 0.0f, 0.0f, 0.0f);


float3 g_LightDir2 : Direction <  

> = {-0.577, -0.577, 0.577};

float4 g_LightPos2 : POSITION <  
	string UIName = "Light Position 2"; 
	string Object = "PointLight";
	string Space = "World";
	int refID = 2;
> = {-0.577, -0.577, 0.577,0.0};

float4 g_LightCol2 : LIGHTCOLOR
<
	int LightRef = 2;
	string UIWidget = "None";
> = float4(0.0f, 0.0f, 0.0f, 0.0f);


float g_lineThick
<
	string UIType = "FloatSpinner";
	float UIMin = 0.0;
	float UIMax = 20.0;
	float UIStep = 0.1;
	string UIName = "Line Thickness";
> = 2.0;


float3 g_LineCol
<
	string UIName = "Line Color";
	string UIType = "ColorSwatch";
> = {0.0f, 0.0f, 0.0f};


bool g_toonShade
<
	string UIName = "Enable Toon Shading";
> = true;


//Diffuse//////////////////////////////////////
///////////////////////////////////////////////
bool g_enableDiff
<
	string UIName = "Enable Diff Map";
> = true;

bool g_flatDiff
<
	string UIName = "Flat Tile Diffuse";
> = false;


int g_tileSize
<
	string UIType = "FloatSpinner";
	float UIMin = 1;
	float UIMax = 2048;
	float UIStep = 1;
	string UIName = "Tile Size";
> = 25;


texture g_diffTexture : DiffuseMap< 
	string UIName = "Diffuse Map ";
	int Texcoord = 0;
	//int MapChannel = 1;
>;

float4 g_diffColor : Diffuse
<
	string UIName = "Diffuse Color";
	string UIType = "ColorSwatch";
> = {1.0f, 1.0f, 1.0f, 1.0f };


int g_diffSegs
<
	string UIType = "FloatSpinner";
	float UIMin = 1;
	float UIMax = 20;
	float UIStep = 1;
	string UIName = "Diffuse Segments";
> = 3;



//Specular/////////////////////////////////////
///////////////////////////////////////////////
bool g_enableSpec
<
	string UIName = "Enable Spec Map";
> = true;

texture g_specTexture < 
	string UIName = "Specular Map ";
	int Texcoord = 0;
	//int MapChannel = 1;
>;

float4 g_specColor
<
	string UIName = "Specular Color";
	string UIType = "ColorSwatch";
> = {1.0f, 1.0f, 1.0f, 1.0f };


bool g_enableGloss
<
	string UIName = "Use Spec Alpha for Gloss";
> = true;

float g_glossiness
<
	string UIType = "FloatSpinner";
	float UIMin = 0.0;
	float UIMax = 255.0;
	float UIStep = 0.1;
	string UIName = "Glossiness";
> = 100.0;

int g_specSegs
<
	string UIType = "FloatSpinner";
	float UIMin = 1;
	float UIMax = 20;
	float UIStep = 1;
	string UIName = "Specular Segments";
> = 2;




//Normal//////////////////////////////////////
//////////////////////////////////////////////
bool g_enableNorm
<
	string UIName = "Enable Norm Map";
> = true;

texture g_normTexture < 
	string UIName = "Normal Map ";
	int Texcoord = 0;
	//int MapChannel = 1;
>;

bool g_invertY
<
	string UIName = "Invert Normal Map Green Channel";
> = false;


//Misc. Stuff/////////////////////////////////
//////////////////////////////////////////////
//Shadow toggle is disabled so all the branching will fit in PS 2.0
//bool g_enableShadows
//<
//	string UIName = "Enable Shadows";
//> = true;




sampler2D diffSampler = sampler_state
{
	Texture = <g_diffTexture>;
	AddressU  = WRAP;        
	AddressV  = WRAP;
	AddressW  = WRAP;
	MinFilter = Linear;
	MagFilter = Linear;
	MipFilter = Linear;
};

sampler2D normSampler = sampler_state
{
	Texture = <g_normTexture>;
	AddressU  = WRAP;        
	AddressV  = WRAP;
	AddressW  = WRAP;
	MinFilter = Linear;
	MagFilter = Linear;
	MipFilter = Linear;
};


sampler2D specSampler = sampler_state
{
	Texture = <g_specTexture>;
	AddressU  = WRAP;        
	AddressV  = WRAP;
	AddressW  = WRAP;
	MinFilter = Linear;
	MagFilter = Linear;
	MipFilter = Linear;
};



//Normal Map Calculator/////////////////////////////
float3 NormalCalc(float3 mapNorm)
{
	float3 N = mapNorm.xyz*2-1;
	//N.y = -N.y;
	N.xyz = normalize(N.xyz);
	return N;
}




//Render to Texture Stuff///////////////////////
////////////////////////////////////////////////
#define RTT_SIZE 512

float blurSize = 1.0f / RTT_SIZE;

texture RttMap1 : RENDERCOLORTARGET < 
	float2 Dimensions = { RTT_SIZE, RTT_SIZE };
	int MIPLEVELS = 1;
	string format = "X8R8G8B8";
	string UIWidget = "None";
>;

sampler RttSamp1 = sampler_state 
{
	texture = <RttMap1>;
	AddressU  = CLAMP;        
	AddressV  = CLAMP;
	AddressW  = CLAMP;
	MIPFILTER = NONE;
	MINFILTER = LINEAR;
	MAGFILTER = LINEAR;
};


texture DepthBuffer : RENDERDEPTHSTENCILTARGET
<
	float2 Dimensions = { RTT_SIZE, RTT_SIZE };
	string format = "D24S8";
	string UIWidget = "None";
>;





float4x4 World              : WORLD;
float4x4 WorldI             : WORLDI;
float4x4 View               : VIEW;
float4x4 ViewI              : VIEWI;
float4x4 Projection         : PROJECTION;
float4x4 WorldViewProj      : WORLDVIEWPROJ;
float4x4 WorldView          : WORLDVIEW;


struct VSInput
{
	float4 Position     : POSITION;
	float3 Normal       : NORMAL;
	float3 Tangent      : TANGENT;
	float3 BiNormal     : BINORMAL;
	float2 UV0          : TEXCOORD0;	
	float3 Colour       : TEXCOORD1;
	float3 Alpha        : TEXCOORD2;
	float3 Illum        : TEXCOORD3;
	//float3 UV1          : TEXCOORD4;
	//float3 UV2          : TEXCOORD5;
	//float3 UV3          : TEXCOORD6;
	//float3 UV4          : TEXCOORD7;
};

struct VSOutput
{
	float4 Position     : POSITION;
   	float4 UV0          : TEXCOORD0;
   	float3 LightDir     : TEXCOORD1;
   	float3 Normal       : TEXCOORD2;
   	float3 ViewDir      : TEXCOORD3;
	float4 UV1          : TEXCOORD4;
	float2 UV2          : TEXCOORD5;
	float3 viewPos      : TEXCOORD6;
	//float2 UV4          : TEXCOORD7;
};

struct PSInput
{
   	float4 UV0          : TEXCOORD0;
   	float3 LightDir     : TEXCOORD1;
   	float3 Normal       : TEXCOORD2;
   	float3 ViewDir      : TEXCOORD3;
	float4 UV1          : TEXCOORD4;
	float2 UV2          : TEXCOORD5;
	float3 viewPos      : TEXCOORD6;
	//float2 UV4          : TEXCOORD7;
};

struct PSOutput
{
	float4 Colour		: COLOR;
};





VSOutput VS (VSInput VSIn, uniform float4 lightPosIn)
{
	VSOutput VSOut = (VSOutput)0;

	VSOut.Normal = VSIn.Normal;

	VSOut.Position = mul(VSIn.Position, WorldViewProj);
	VSOut.viewPos = VSOut.Position;

	float4 g_CamPos = mul(ViewI[3],WorldI);
	float4 lightPos = mul(lightPosIn, WorldI);
	float3 lightVec = (lightPos.xyz - VSIn.Position.xyz);
	float3 ViewDir = g_CamPos.xyz - VSIn.Position.xyz;

	float3x3 objToTangent;
	objToTangent[0] = VSIn.BiNormal;
	objToTangent[1] = VSIn.Tangent;
	objToTangent[2] = VSIn.Normal;
	
	VSOut.LightDir = normalize(mul(objToTangent, lightVec));
	VSOut.ViewDir = normalize(mul(objToTangent, ViewDir));
	

   	VSOut.UV0.xy = VSIn.UV0.xy;
	VSOut.UV0.y += 1.0f;
   	VSOut.UV1 = mul(VSIn.Position,World);

	return VSOut;
}


PSOutput PS (PSInput PSIn, uniform half4 lightColIn, uniform int numPass) : COLOR
{
	PSOutput PSOut = (PSOutput)0;

	half4 diffCol;
	half4 specCol;
	float4 tempNorm;
	half3 lightVec = normalize(PSIn.LightDir);

	float2 diffuseUV = PSIn.UV0;
	float2 screenPos = PSIn.viewPos.xy/g_tileSize;

	
	//Set Base Diff Color///////////////////////////////
	////////////////////////////////////////////////////
	diffCol = g_diffColor;
	if(g_enableDiff)
	{
	  diffCol = tex2D(diffSampler, diffuseUV.xy);
	}
	if(g_flatDiff && g_enableDiff)
	{
	  diffCol = tex2D(diffSampler, screenPos.xy);
	}
	half diffAlpha = diffCol.a;



	//Normals Calculation///////////////////////////////
	////////////////////////////////////////////////////
	tempNorm.rgb = float3(0.5f,0.5f,1.0f);
	if(g_enableNorm)
	{
	  tempNorm = tex2D(normSampler, diffuseUV.xy);
	}
	if(g_invertY && g_enableNorm)
	{
	  tempNorm.y = 1-tempNorm.y;
	}
	tempNorm.xyz = tempNorm.xyz*2-1;
	tempNorm.xyz = normalize(tempNorm.xyz);
	half lightNorm = (dot(tempNorm, lightVec));
	
	if(g_toonShade)
	{
	  lightNorm = smoothstep(-1.0, 1.0, lightNorm);
	  lightNorm *= g_diffSegs;
	  lightNorm = ceil(lightNorm);
	  lightNorm /= g_diffSegs;
	}



	//Specular Highlight/////////////////////////////////
	/////////////////////////////////////////////////////
	specCol = g_specColor;
	if(g_enableSpec)
	{
	  specCol = tex2D(specSampler, diffuseUV.xy);
	}
	half specGloss = g_glossiness;
	if(g_enableSpec && g_enableGloss)
	{
	  specGloss = specCol.a * 255.0;
	}
	
	half3 H = normalize(PSIn.LightDir + PSIn.ViewDir);
	half specMag = (dot(tempNorm, H));
	half tempSpecMag = pow(specMag, specGloss);
	
	if(g_toonShade)
	{
	  specGloss /= 255.0;
	  specGloss -= 0.001;
	  tempSpecMag = smoothstep(specGloss, 1.0, specMag);
	  tempSpecMag *= g_specSegs;
	  tempSpecMag = ceil(tempSpecMag);
	  tempSpecMag /= g_specSegs;
	}
	
	half3 tempSpecCol = lightColIn.rgb * specCol.rgb;
	diffCol.rgb += (tempSpecCol.rgb * tempSpecMag);



	//Shading and Shadows/////////////////////////////////
	//////////////////////////////////////////////////////
	diffCol.rgb = (lightColIn.rgb * lightNorm)*diffCol.rgb;



	PSOut.Colour.rgb = (diffCol.rgb/numPass);
	PSOut.Colour.a = diffAlpha;
	
	return PSOut;
}




VSOutput VS_Glow (VSInput VSIn)
{
	VSOutput VSOut = (VSOutput)0;
	
	VSOut.Normal = VSIn.Normal;
	float4 g_CamPos = mul(ViewI[3],WorldI);
	VSOut.ViewDir = g_CamPos.xyz - VSIn.Position.xyz;

	half4 tempPos = float4(VSIn.Position.xyz,1);
	//tempPos += (glowOffset*normalize(float4(VSIn.Normal.xyz,0)));
	VSOut.Position = mul(tempPos, WorldViewProj);

   	VSOut.UV0.xy = VSIn.UV0.xy;
	VSOut.UV0.y += 1.0f;
	return VSOut;
}




VSOutput VS_Q (VSInput VSIn)
{
	VSOutput VSOut = (VSOutput)0;

	VSOut.Position = float4(VSIn.Position.xyz, 1);
	VSOut.Position.x -= (1.0f / RTT_SIZE);
	VSOut.Position.y += (1.0f / RTT_SIZE);
	VSOut.UV0 = float4(VSIn.UV0.xy, 1, 1);

	return VSOut;
}




float4 PS_Glow (PSInput PSIn) : COLOR
{
	float4 PSOut;

	float viewNorm = dot(PSIn.Normal, PSIn.ViewDir);

	//float2 diffuseUV = PSIn.UV0;
	//PSOut.rgb = viewNorm;
	PSOut = float4(0,0,0,0);
	PSOut.rgb = 1.0;
	//half4 diffAlpha = tex2D(diffSampler, diffuseUV.xy);
	//half4 diffCol = tex2D(glowSampler, diffuseUV.xy);

	//PSOut.r = 1.0;
	//diffCol *= diffAlpha.a;

	//PSOut.rgb = diffCol;
	PSOut.a = 1.0;

	return PSOut;
}



float4 PS_Q (VSOutput VSIn) : COLOR
{
	float4 PSOut;

	half4 diffCol = float4(0,0,0,0);
	half4 diffSamp1;
	half4 diffSamp2;
	half4 diffSamp3;
	half4 diffSamp4;
	half4 diffSamp5;
	half4 diffSamp6;
	half4 diffSamp7;
	half4 diffSamp8;
	half4 diffSamp9;

	//Sample rendered glow and blur it some
	float2 diffuseUV1 = VSIn.UV0; //Primary pixel
	
	//Cardinal directions
	float2 diffuseUV2 = float2((VSIn.UV0.x+blurSize), (VSIn.UV0.y));
	float2 diffuseUV3 = float2((VSIn.UV0.x-blurSize), (VSIn.UV0.y));
	float2 diffuseUV4 = float2((VSIn.UV0.x), (VSIn.UV0.y+blurSize));
	float2 diffuseUV5 = float2((VSIn.UV0.x), (VSIn.UV0.y-blurSize));
	
	//Diagonals
	float2 diffuseUV6 = float2((VSIn.UV0.x+blurSize), (VSIn.UV0.y+blurSize));
	float2 diffuseUV7 = float2((VSIn.UV0.x-blurSize), (VSIn.UV0.y+blurSize));
	float2 diffuseUV8 = float2((VSIn.UV0.x+blurSize), (VSIn.UV0.y+blurSize));
	float2 diffuseUV9 = float2((VSIn.UV0.x+blurSize), (VSIn.UV0.y-blurSize));

	diffSamp1 = tex2D(RttSamp1, diffuseUV1.xy);
	diffSamp2 = tex2D(RttSamp1, diffuseUV2.xy);
	diffSamp3 = tex2D(RttSamp1, diffuseUV3.xy);
	diffSamp4 = tex2D(RttSamp1, diffuseUV4.xy);
	diffSamp5 = tex2D(RttSamp1, diffuseUV5.xy);
	diffSamp6 = tex2D(RttSamp1, diffuseUV6.xy);
	diffSamp7 = tex2D(RttSamp1, diffuseUV7.xy);
	diffSamp8 = tex2D(RttSamp1, diffuseUV8.xy);
	diffSamp9 = tex2D(RttSamp1, diffuseUV9.xy);


	  //diffCol = (diffSamp1+diffSamp2+diffSamp3+diffSamp4+diffSamp5+diffSamp6+diffSamp7+diffSamp8+diffSamp9)/4;
	  diffCol = abs(diffSamp1-((diffSamp2+diffSamp3+diffSamp4+diffSamp5+diffSamp6+diffSamp7+diffSamp8+diffSamp9)/8));
	  diffCol = ceil(diffCol);
	  //diffCol = round(diffCol+0.39);

	PSOut.rgb = diffCol.rgb*g_LineCol;

	PSOut.a = diffCol.rgb;

	return PSOut;
}






technique OneLight
<
	string Script =
			"ClearSetColor=ClearColor;"
			"ClearSetDepth=ClearDepth;"
			"Pass=pass_RTT1;"
			"Pass=pass_Comp1;"
			"Pass=light01;";
>
{
    pass pass_RTT1
    <
        string Script = "RenderColorTarget0=RttMap1;"
        "Clear=Color;"
        "Draw=Geometry;";
    >
    {
	VertexShader = compile vs_2_0 VS_Glow();
        cullmode = cw;
        ZEnable = true;
        PixelShader  = compile ps_2_0 PS_Glow();
    }

    
    pass pass_Comp1
    <
        string Script= "RenderColorTarget0=;"
        "Clear=Depth;"
        "Draw=Buffer;";
    >
    {
	cullmode = none;
	ZEnable = false;
	ZWriteEnable = false;
	AlphaBlendEnable = true;
	//BlendEnable = false;
	SrcBlend = one;
	DestBlend = invSrcAlpha;
	VertexShader = compile vs_2_0 VS_Q();
	PixelShader  = compile ps_2_0 PS_Q();
    }
    pass light01
    <
        string Script= "RenderColorTarget0=;"
        //"Clear=Depth;"
        "Draw=Geometry;";
    >
    {
	VertexShader = compile vs_2_0 VS(g_LightPos);
	CullMode = cw;
	ZEnable = true;
	ZWriteEnable = true;
	AlphaBlendEnable = false;
	SrcBlend = zero;
	DestBlend = one;
	PixelShader  = compile ps_3_0 PS(g_LightCol, 1);
    }
}