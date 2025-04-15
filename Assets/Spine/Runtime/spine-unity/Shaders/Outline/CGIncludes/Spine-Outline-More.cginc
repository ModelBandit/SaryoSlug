#ifndef SPINE_OUTLINE_PASS_INCLUDED
#define SPINE_OUTLINE_PASS_INCLUDED

#include "UnityCG.cginc"

#ifdef SKELETON_GRAPHIC
#include "UnityUI.cginc"
#endif

#include "../../CGIncludes/Spine-Outline-Common.cginc"

sampler2D _MainTex;

float _OutlineWidth;
float4 _OutlineColor;
float _OutlineSeparation;
float4 _InlineColor;
float4 _MainTex_TexelSize;
float _ThresholdEnd;
float _OutlineSmoothness;
float _OutlineOpaqueAlpha;
float _OutlineMipLevel;
int _OutlineReferenceTexWidth;

#ifdef SKELETON_GRAPHIC
float4 _ClipRect;
#endif

struct VertexInput {
	float4 vertex : POSITION;
	float2 uv : TEXCOORD0;
	float4 vertexColor : COLOR;
	UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct VertexOutput {
	float4 pos : SV_POSITION;
	float2 uv : TEXCOORD0;
	float vertexColorAlpha : COLOR;
#ifdef SKELETON_GRAPHIC
	float4 worldPosition : TEXCOORD1;
#endif
	UNITY_VERTEX_OUTPUT_STEREO
};


#ifdef SKELETON_GRAPHIC

VertexOutput vertOutlineGraphic(VertexInput v) {
	VertexOutput o;

	UNITY_SETUP_INSTANCE_ID(v);
	UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

	o.worldPosition = v.vertex;
	o.pos = UnityObjectToClipPos(o.worldPosition);
	o.uv = v.uv;

#ifdef UNITY_HALF_TEXEL_OFFSET
	o.pos.xy += (_ScreenParams.zw - 1.0) * float2(-1, 1);
#endif

	o.vertexColorAlpha = v.vertexColor.a;
	return o;
}


#else // !SKELETON_GRAPHIC

VertexOutput vertOutline1(VertexInput v) {
	VertexOutput o;

	UNITY_SETUP_INSTANCE_ID(v);
	UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
	
    o.pos = UnityObjectToClipPos(v.vertex);  // 3D 정점을 화면 좌표로 변환

	
	float radius = 10.0; // 거리
	float angle = radians(180.0); // 각도를 라디안으로 변환

	o.pos.x += radius * cos(angle);
	o.pos.y += radius * sin(angle);

    o.uv = v.uv;                            // 텍스처 좌표 전달
    o.vertexColorAlpha = v.vertexColor.a;   // 알파값 전달
	return o;
}
VertexOutput vertOutline2(VertexInput v) {
	VertexOutput o;

	UNITY_SETUP_INSTANCE_ID(v);
	UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
	
    o.pos = UnityObjectToClipPos(v.vertex);  // 3D 정점을 화면 좌표로 변환
	o.pos.x -= _OutlineSeparation;
	o.pos.y += _OutlineSeparation;

    o.uv = v.uv;                            // 텍스처 좌표 전달
    o.vertexColorAlpha = v.vertexColor.a;   // 알파값 전달
	return o;
}
VertexOutput vertOutline3(VertexInput v) {
	VertexOutput o;

	UNITY_SETUP_INSTANCE_ID(v);
	UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
	
    o.pos = UnityObjectToClipPos(v.vertex);  // 3D 정점을 화면 좌표로 변환
	o.pos.y += _OutlineSeparation;

    o.uv = v.uv;                            // 텍스처 좌표 전달
    o.vertexColorAlpha = v.vertexColor.a;   // 알파값 전달
	return o;
}
VertexOutput vertOutline4(VertexInput v) {
	VertexOutput o;

	UNITY_SETUP_INSTANCE_ID(v);
	UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
	
    o.pos = UnityObjectToClipPos(v.vertex);  // 3D 정점을 화면 좌표로 변환
	o.pos.x += _OutlineSeparation;
	o.pos.y += _OutlineSeparation;

    o.uv = v.uv;                            // 텍스처 좌표 전달
    o.vertexColorAlpha = v.vertexColor.a;   // 알파값 전달
	return o;
}
VertexOutput vertOutline5(VertexInput v) {
	VertexOutput o;

	UNITY_SETUP_INSTANCE_ID(v);
	UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
	
    o.pos = UnityObjectToClipPos(v.vertex);  // 3D 정점을 화면 좌표로 변환
	o.pos.x += _OutlineSeparation;

    o.uv = v.uv;                            // 텍스처 좌표 전달
    o.vertexColorAlpha = v.vertexColor.a;   // 알파값 전달
	return o;
}
VertexOutput vertOutline6(VertexInput v) {
	VertexOutput o;

	UNITY_SETUP_INSTANCE_ID(v);
	UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
	
    o.pos = UnityObjectToClipPos(v.vertex);  // 3D 정점을 화면 좌표로 변환
	o.pos.x += _OutlineSeparation;
	o.pos.y -= _OutlineSeparation;

    o.uv = v.uv;                            // 텍스처 좌표 전달
    o.vertexColorAlpha = v.vertexColor.a;   // 알파값 전달
	return o;
}
VertexOutput vertOutline7(VertexInput v) {
	VertexOutput o;

	UNITY_SETUP_INSTANCE_ID(v);
	UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
	
    o.pos = UnityObjectToClipPos(v.vertex);  // 3D 정점을 화면 좌표로 변환
	o.pos.y -= _OutlineSeparation;

    o.uv = v.uv;                            // 텍스처 좌표 전달
    o.vertexColorAlpha = v.vertexColor.a;   // 알파값 전달
	return o;
}
VertexOutput vertOutline8(VertexInput v) {
	VertexOutput o;

	UNITY_SETUP_INSTANCE_ID(v);
	UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
	
    o.pos = UnityObjectToClipPos(v.vertex);  // 3D 정점을 화면 좌표로 변환
	o.pos.x -= _OutlineSeparation;
	o.pos.y -= _OutlineSeparation;

    o.uv = v.uv;                            // 텍스처 좌표 전달
    o.vertexColorAlpha = v.vertexColor.a;   // 알파값 전달
	return o;
}
VertexOutput vertInlineFilter(VertexInput v) {
	VertexOutput o;

	UNITY_SETUP_INSTANCE_ID(v);
	UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
	
    o.pos = UnityObjectToClipPos(v.vertex);  // 3D 정점을 화면 좌표로 변환
	o.pos.x -= _OutlineSeparation*0.5;

    o.uv = v.uv;// 텍스처 좌표 전달

    o.vertexColorAlpha = v.vertexColor.a;   // 알파값 전달
	return o;
}
VertexOutput vertInline1(VertexInput v) {
	VertexOutput o;

	UNITY_SETUP_INSTANCE_ID(v);
	UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
	
    o.pos = UnityObjectToClipPos(v.vertex);  // 3D 정점을 화면 좌표로 변환
	o.pos.x -= _OutlineSeparation*0.5;

    o.uv = v.uv;// 텍스처 좌표 전달

    o.vertexColorAlpha = v.vertexColor.a;   // 알파값 전달
	return o;
}
VertexOutput vertInline2(VertexInput v) {
	VertexOutput o;

	UNITY_SETUP_INSTANCE_ID(v);
	UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
	
    o.pos = UnityObjectToClipPos(v.vertex);  // 3D 정점을 화면 좌표로 변환
	o.pos.x -= _OutlineSeparation*0.5;
	o.pos.y += _OutlineSeparation*0.5;

    o.uv = v.uv;// 텍스처 좌표 전달

    o.vertexColorAlpha = v.vertexColor.a;   // 알파값 전달
	return o;
}
VertexOutput vertInline3(VertexInput v) {
	VertexOutput o;

	UNITY_SETUP_INSTANCE_ID(v);
	UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
	
    o.pos = UnityObjectToClipPos(v.vertex);  // 3D 정점을 화면 좌표로 변환
	o.pos.y += _OutlineSeparation*0.5;

    o.uv = v.uv;// 텍스처 좌표 전달

    o.vertexColorAlpha = v.vertexColor.a;   // 알파값 전달
	return o;
}
VertexOutput vertInline4(VertexInput v) {
	VertexOutput o;

	UNITY_SETUP_INSTANCE_ID(v);
	UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
	
    o.pos = UnityObjectToClipPos(v.vertex);  // 3D 정점을 화면 좌표로 변환
	o.pos.x += _OutlineSeparation*0.5;
	o.pos.y += _OutlineSeparation*0.5;

    o.uv = v.uv;// 텍스처 좌표 전달

    o.vertexColorAlpha = v.vertexColor.a;   // 알파값 전달
	return o;
}
VertexOutput vertInline5(VertexInput v) {
	VertexOutput o;

	UNITY_SETUP_INSTANCE_ID(v);
	UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
	
    o.pos = UnityObjectToClipPos(v.vertex);  // 3D 정점을 화면 좌표로 변환
	o.pos.x += _OutlineSeparation*0.5;

    o.uv = v.uv;// 텍스처 좌표 전달

    o.vertexColorAlpha = v.vertexColor.a;   // 알파값 전달
	return o;
}
VertexOutput vertInline6(VertexInput v) {
	VertexOutput o;

	UNITY_SETUP_INSTANCE_ID(v);
	UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
	
    o.pos = UnityObjectToClipPos(v.vertex);  // 3D 정점을 화면 좌표로 변환
	o.pos.x += _OutlineSeparation*0.5;
	o.pos.y -= _OutlineSeparation*0.5;

    o.uv = v.uv;// 텍스처 좌표 전달

    o.vertexColorAlpha = v.vertexColor.a;   // 알파값 전달
	return o;
}
VertexOutput vertInline7(VertexInput v) {
	VertexOutput o;

	UNITY_SETUP_INSTANCE_ID(v);
	UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
	
    o.pos = UnityObjectToClipPos(v.vertex);  // 3D 정점을 화면 좌표로 변환
	o.pos.y -= _OutlineSeparation*0.5;

    o.uv = v.uv;// 텍스처 좌표 전달

    o.vertexColorAlpha = v.vertexColor.a;   // 알파값 전달
	return o;
}
VertexOutput vertInline8(VertexInput v) {
	VertexOutput o;

	UNITY_SETUP_INSTANCE_ID(v);
	UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
	
    o.pos = UnityObjectToClipPos(v.vertex);  // 3D 정점을 화면 좌표로 변환
	o.pos.x -= _OutlineSeparation*0.5;
	o.pos.y -= _OutlineSeparation*0.5;

    o.uv = v.uv;// 텍스처 좌표 전달

    o.vertexColorAlpha = v.vertexColor.a;   // 알파값 전달
	return o;
}
#endif

float4 fragOutline(VertexOutput i) : SV_Target {

	float4 texColor = computeOutlinePixel(_MainTex, _MainTex_TexelSize.xy, i.uv, i.vertexColorAlpha,
		_OutlineWidth, _OutlineReferenceTexWidth, _OutlineMipLevel,
		_OutlineSmoothness, _ThresholdEnd, _OutlineOpaqueAlpha, _OutlineColor);

#ifdef SKELETON_GRAPHIC
	texColor *= UnityGet2DClipping(i.localPosition.xy, _ClipRect);
#endif

	return texColor;
}

float4 fragInline(VertexOutput i) : SV_Target {

	float4 texColor = computeOutlinePixel(_MainTex, _MainTex_TexelSize.xy, i.uv, i.vertexColorAlpha,
		_OutlineWidth*0.5, _OutlineReferenceTexWidth, _OutlineMipLevel,
		_OutlineSmoothness, _ThresholdEnd, _OutlineOpaqueAlpha, _InlineColor);

#ifdef SKELETON_GRAPHIC
	texColor *= UnityGet2DClipping(i.localPosition.xy, _ClipRect);
#endif

	return texColor;
}

#endif // SPINE_OUTLINE_PASS_INCLUDED