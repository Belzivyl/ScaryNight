// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "ERB/Particles/FireDistort"
{
	Properties
	{
		_Tex1("Tex1", 2D) = "white" {}
		_Tex2("Tex2", 2D) = "white" {}
		_Mask("Mask", 2D) = "white" {}
		_NormalMap("NormalMap", 2D) = "bump" {}
		_SpeedTex1("Speed Tex1", Vector) = (0,0,0,0)
		_SpeedTex2XYEmission("Speed Tex2 XY / Emission", Vector) = (0,0,0,0)
		_Color2("Color 2", Color) = (1,0,0,1)
		_Distortionpower("Distortion power", Float) = 0
		_Color1("Color 1", Color) = (1,0.5423229,0,1)
		_Opacity("Opacity", Range( 0 , 3)) = 1
		[Toggle]_Usedepth("Use depth?", Float) = 0
		_Depthpower("Depth power", Float) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] _tex4coord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		GrabPass{ }
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#pragma target 3.5
		#pragma surface surf Unlit alpha:fade keepalpha noshadow 
		#undef TRANSFORM_TEX
		#define TRANSFORM_TEX(tex,name) float4(tex.xy * name##_ST.xy + name##_ST.zw, tex.z, tex.w)
		struct Input
		{
			float2 uv_texcoord;
			float4 screenPos;
			float4 uv_tex4coord;
			float4 vertexColor : COLOR;
		};

		UNITY_DECLARE_SCREENSPACE_TEXTURE( _GrabTexture )
		uniform float _Distortionpower;
		uniform sampler2D _NormalMap;
		uniform float4 _SpeedTex2XYEmission;
		uniform float4 _NormalMap_ST;
		uniform float4 _Color1;
		uniform float4 _Color2;
		uniform sampler2D _Tex1;
		uniform float4 _SpeedTex1;
		uniform float4 _Tex1_ST;
		uniform sampler2D _Tex2;
		uniform float4 _Tex2_ST;
		uniform sampler2D _Mask;
		uniform float4 _Mask_ST;
		uniform float _Usedepth;
		uniform float _Opacity;
		UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
		uniform float4 _CameraDepthTexture_TexelSize;
		uniform float _Depthpower;


		inline float4 ASE_ComputeGrabScreenPos( float4 pos )
		{
			#if UNITY_UV_STARTS_AT_TOP
			float scale = -1.0;
			#else
			float scale = 1.0;
			#endif
			float4 o = pos;
			o.y = pos.w * 0.5f;
			o.y = ( pos.y - o.y ) * _ProjectionParams.x * scale + o.y;
			return o;
		}


		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 appendResult21 = (float2(_SpeedTex2XYEmission.x , _SpeedTex2XYEmission.y));
			float2 uv0_NormalMap = i.uv_texcoord * _NormalMap_ST.xy + _NormalMap_ST.zw;
			float2 panner76 = ( 1.0 * _Time.y * appendResult21 + uv0_NormalMap);
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_grabScreenPos = ASE_ComputeGrabScreenPos( ase_screenPos );
			float4 ase_grabScreenPosNorm = ase_grabScreenPos / ase_grabScreenPos.w;
			float4 screenColor80 = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_GrabTexture,( float4( UnpackScaleNormal( tex2D( _NormalMap, panner76 ), _Distortionpower ) , 0.0 ) + ase_grabScreenPosNorm ).xy);
			float Emission39 = _SpeedTex2XYEmission.z;
			float2 appendResult16 = (float2(_SpeedTex1.x , _SpeedTex1.y));
			float2 uv0_Tex1 = i.uv_texcoord * _Tex1_ST.xy + _Tex1_ST.zw;
			float2 panner7 = ( 1.0 * _Time.y * appendResult16 + uv0_Tex1);
			float4 uv0_Tex2 = i.uv_tex4coord;
			uv0_Tex2.xy = i.uv_tex4coord.xy * _Tex2_ST.xy + _Tex2_ST.zw;
			float2 appendResult14 = (float2(_SpeedTex1.z , _SpeedTex1.w));
			float2 panner8 = ( 1.0 * _Time.y * appendResult14 + uv0_Tex1);
			float2 appendResult52 = (float2(uv0_Tex2.z , uv0_Tex2.w));
			float4 tex2DNode4 = tex2D( _Tex1, ( panner8 + appendResult52 ) );
			float2 panner20 = ( 1.0 * _Time.y * appendResult21 + uv0_Tex2.xy);
			float4 tex2DNode5 = tex2D( _Tex2, ( panner20 + appendResult52 ) );
			float2 uv_Mask = i.uv_texcoord * _Mask_ST.xy + _Mask_ST.zw;
			float4 tex2DNode6 = tex2D( _Mask, uv_Mask );
			float temp_output_27_0 = ( ( ( ( ( tex2D( _Tex1, ( panner7 + uv0_Tex2.z ) ).r + tex2DNode4.r ) * tex2DNode4.r * tex2DNode5.g ) + ( tex2DNode6.b * 0.5 ) ) * tex2DNode5.g ) * tex2DNode6.b );
			float4 lerpResult33 = lerp( _Color1 , _Color2 , temp_output_27_0);
			float4 temp_output_38_0 = ( Emission39 * lerpResult33 * i.vertexColor );
			o.Emission = ( screenColor80 * temp_output_38_0 ).rgb;
			float temp_output_72_0 = saturate( ( _Color1.a * _Color2.a * temp_output_27_0 * i.vertexColor.a * _Opacity ) );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float screenDepth68 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture,UNITY_PROJ_COORD( ase_screenPos )));
			float distanceDepth68 = abs( ( screenDepth68 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _Depthpower ) );
			o.Alpha = lerp(temp_output_72_0,( temp_output_72_0 * saturate( distanceDepth68 ) ),_Usedepth);
		}

		ENDCG
	}
}
/*ASEBEGIN
Version=16800
763;92;835;655;1645.288;689.9354;2.307262;True;False
Node;AmplifyShaderEditor.Vector4Node;13;-2850.329,30.79891;Float;False;Property;_SpeedTex1;Speed Tex1;4;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;14;-2455.048,144.7154;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector4Node;19;-2811.947,399.0959;Float;False;Property;_SpeedTex2XYEmission;Speed Tex2 XY / Emission;5;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;22;-2555.222,252.9486;Float;False;0;5;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;16;-2456.836,22.10848;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;10;-2550.9,-105.4652;Float;False;0;17;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WireNode;51;-2103.454,-23.63686;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;21;-2457.837,428.2516;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;7;-2286.721,-58.46526;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;8;-2279.111,77.22112;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;52;-2250.709,311.8192;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;20;-2293.603,416.2857;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;50;-2070.306,-139.2706;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;48;-2070.306,61.61702;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;17;-2316.18,-272.3633;Float;True;Property;_Tex1;Tex1;0;0;Create;True;0;0;False;0;None;None;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SimpleAddOpNode;49;-2090.228,327.2533;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;4;-1942.498,-60.04169;Float;True;Property;_Tex0;Tex0;0;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;18;-1942.855,-241.3017;Float;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;6;-1940.419,303.0058;Float;True;Property;_Mask;Mask;2;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;24;-1567.076,-149.5738;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;5;-1940.967,122.1309;Float;True;Property;_Tex2;Tex2;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;-1417.404,-90.89656;Float;False;3;3;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;32;-1445.526,66.21613;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;26;-1257.95,-8.849566;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;74;-983.9416,-803.0931;Float;False;0;78;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;43;-1106.04,71.12432;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;46;-827.3337,395.2631;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-856.3154,126.7215;Float;True;2;2;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;75;-739.9286,-552.9492;Float;False;Property;_Distortionpower;Distortion power;7;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;76;-721.4047,-681.6199;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;67;-812.7114,701.162;Float;False;Property;_Depthpower;Depth power;11;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;37;-837.8048,-79.7439;Float;False;Property;_Color2;Color 2;6;0;Create;True;0;0;False;0;1,0,0,1;1,0,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;66;-849.0128,584.3337;Float;False;Property;_Opacity;Opacity;9;0;Create;True;0;0;False;0;1;1;0;3;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;34;-847.2845,-269.326;Float;False;Property;_Color1;Color 1;8;0;Create;True;0;0;False;0;1,0.5423229,0,1;1,0.5423229,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GrabScreenPosition;77;-419.6038,-460.3022;Float;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;39;-2472.299,543.7406;Float;False;Emission;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;78;-483.6039,-668.3019;Float;True;Property;_NormalMap;NormalMap;3;0;Create;True;0;0;False;0;None;302951faffe230848aa0d3df7bb70faa;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DepthFade;68;-594.168,683.6335;Float;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;45;-561.9205,160.7722;Float;False;5;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;47;-420.7433,289.7996;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;69;-330.0047,685.9266;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;33;-546.3663,-82.49611;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;72;-306.3064,352.9389;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;79;-163.5999,-572.3022;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.GetLocalVarNode;40;-554.0267,-173.6586;Float;False;39;Emission;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;70;-120.1179,461.1889;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenColorNode;80;-17.6841,-646.906;Float;False;Global;_GrabScreen0;Grab Screen 0;2;0;Create;True;0;0;False;0;Object;-1;False;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;38;-336.4936,-99.03915;Float;False;3;3;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;82;380.9049,-97.27734;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ToggleSwitchNode;71;168.1009,328.8182;Float;False;Property;_Usedepth;Use depth?;10;0;Create;True;0;0;False;0;0;2;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;90;191.4783,182.4515;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;73;853.904,69.3654;Float;False;True;3;Float;;0;0;Unlit;ERB/Particles/FireDistort;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;14;0;13;3
WireConnection;14;1;13;4
WireConnection;16;0;13;1
WireConnection;16;1;13;2
WireConnection;51;0;22;3
WireConnection;21;0;19;1
WireConnection;21;1;19;2
WireConnection;7;0;10;0
WireConnection;7;2;16;0
WireConnection;8;0;10;0
WireConnection;8;2;14;0
WireConnection;52;0;22;3
WireConnection;52;1;22;4
WireConnection;20;0;22;0
WireConnection;20;2;21;0
WireConnection;50;0;7;0
WireConnection;50;1;51;0
WireConnection;48;0;8;0
WireConnection;48;1;52;0
WireConnection;49;0;20;0
WireConnection;49;1;52;0
WireConnection;4;0;17;0
WireConnection;4;1;48;0
WireConnection;18;0;17;0
WireConnection;18;1;50;0
WireConnection;24;0;18;1
WireConnection;24;1;4;1
WireConnection;5;1;49;0
WireConnection;23;0;24;0
WireConnection;23;1;4;1
WireConnection;23;2;5;2
WireConnection;32;0;6;3
WireConnection;26;0;23;0
WireConnection;26;1;32;0
WireConnection;43;0;26;0
WireConnection;43;1;5;2
WireConnection;27;0;43;0
WireConnection;27;1;6;3
WireConnection;76;0;74;0
WireConnection;76;2;21;0
WireConnection;39;0;19;3
WireConnection;78;1;76;0
WireConnection;78;5;75;0
WireConnection;68;0;67;0
WireConnection;45;0;34;4
WireConnection;45;1;37;4
WireConnection;45;2;27;0
WireConnection;45;3;46;4
WireConnection;45;4;66;0
WireConnection;47;0;46;0
WireConnection;69;0;68;0
WireConnection;33;0;34;0
WireConnection;33;1;37;0
WireConnection;33;2;27;0
WireConnection;72;0;45;0
WireConnection;79;0;78;0
WireConnection;79;1;77;0
WireConnection;70;0;72;0
WireConnection;70;1;69;0
WireConnection;80;0;79;0
WireConnection;38;0;40;0
WireConnection;38;1;33;0
WireConnection;38;2;47;0
WireConnection;82;0;80;0
WireConnection;82;1;38;0
WireConnection;71;0;72;0
WireConnection;71;1;70;0
WireConnection;90;0;80;0
WireConnection;90;1;38;0
WireConnection;73;2;90;0
WireConnection;73;9;71;0
ASEEND*/
//CHKSM=9DF49358672F6A34C2A90EB4536201D65B8C7A48