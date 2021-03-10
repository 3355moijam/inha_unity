// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "CustomShillouette"
{
	Properties
	{
		_ASEOutlineColor("Outline Color", Color) = (0.8396226,0.03696444,0.03696444,0)
		_ASEOutlineWidth("Outline Width", Float) = 0.04
	}

		SubShader
		{
			Tags{ }
			Cull Front
			
			//ZTest Greater
			CGPROGRAM
			#pragma target 3.0
			#pragma surface outlineSurf Outline nofog  keepalpha noshadow noambient novertexlights nolightmap nodynlightmap nodirlightmap nometa noforwardadd vertex:outlineVertexDataFunc 



			struct Input {
				half filler;
			};
			uniform half4 _ASEOutlineColor;
			uniform half _ASEOutlineWidth;
			void outlineVertexDataFunc(inout appdata_full v, out Input o)
			{
				UNITY_INITIALIZE_OUTPUT(Input, o);
				v.vertex.xyz += (v.normal * _ASEOutlineWidth);
			}
			inline half4 LightingOutline(SurfaceOutput s, half3 lightDir, half atten) { return half4 (0,0,0, s.Alpha); }
			void outlineSurf(Input i, inout SurfaceOutput o)
			{
				o.Emission = _ASEOutlineColor.rgb;
				o.Alpha = 1;
			}
			ENDCG


		}
			Fallback "Diffuse"
				CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16100
424.3333;1451.333;1044;1367.667;1711.187;546.1519;1;True;True
Node;AmplifyShaderEditor.SamplerNode;1;-677.5,-271.5;Float;True;Property;_Albedo;Albedo;0;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-727.5,-83.5;Float;True;Property;_Normal;Normal;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;3;-684.5,148.5;Float;False;Property;_Metallic;Metallic;2;0;Create;True;0;0;False;0;0.12;0.12;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-669.5,258.5;Float;False;Property;_Smoothness;Smoothness;3;0;Create;True;0;0;False;0;0.35;0.35;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;CustomOutline;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;6;False;-1;False;0;False;-1;0;False;-1;False;7;Opaque;0.5;True;True;0;False;Opaque;;Geometry;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;True;0;0.8396226,0.03696444,0.03696444,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;0;0;1;0
WireConnection;0;1;2;0
WireConnection;0;3;3;0
WireConnection;0;4;4;0
ASEEND*/
//CHKSM=7EE6D7B8F1036FD0EE1B009A4EEB03D50E7ED809