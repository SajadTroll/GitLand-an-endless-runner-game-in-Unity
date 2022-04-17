#ifndef FLATKIT_SYLIZED_INPUT_INCLUDED
#define FLATKIT_SYLIZED_INPUT_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Packing.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/CommonMaterial.hlsl"

UNITY_INSTANCING_BUFFER_START(Props)

#ifdef _CELPRIMARYMODE_SINGLE
UNITY_DEFINE_INSTANCED_PROP(half4, _ColorDim)
#endif  // _CELPRIMARYMODE_SINGLE

#ifdef DR_SPECULAR_ON
UNITY_DEFINE_INSTANCED_PROP(half4, _FlatSpecularColor)
UNITY_DEFINE_INSTANCED_PROP(float, _FlatSpecularSize)
UNITY_DEFINE_INSTANCED_PROP(float, _FlatSpecularEdgeSmoothness)
#endif  // DR_SPECULAR_ON

#ifdef DR_RIM_ON
UNITY_DEFINE_INSTANCED_PROP(half4, _FlatRimColor)
UNITY_DEFINE_INSTANCED_PROP(float, _FlatRimSize)
UNITY_DEFINE_INSTANCED_PROP(float, _FlatRimEdgeSmoothness)
UNITY_DEFINE_INSTANCED_PROP(float, _FlatRimLightAlign)
#endif  // DR_RIM_ON

UNITY_DEFINE_INSTANCED_PROP(half4, _UnityShadowColor)

UNITY_INSTANCING_BUFFER_END(Props)

#ifdef _CELPRIMARYMODE_STEPS
half4 _ColorDimSteps;
sampler2D _CelStepTexture;
#endif  // _CELPRIMARYMODE_STEPS

#ifdef _CELPRIMARYMODE_CURVE
half4 _ColorDimCurve;
sampler2D _CelCurveTexture;
#endif  // _CELPRIMARYMODE_CURVE

#ifdef DR_CEL_EXTRA_ON
half4 _ColorDimExtra;
half _SelfShadingSizeExtra;
half _ShadowEdgeSizeExtra;
half _FlatnessExtra;
#endif  // DR_CEL_EXTRA_ON

#ifdef DR_GRADIENT_ON
half4 _ColorGradient;
half _GradientCenterX;
half _GradientCenterY;
half _GradientSize;
half _GradientAngle;
#endif  // DR_GRADIENT_ON

half _TextureImpact;

half _SelfShadingSize;
half _ShadowEdgeSize;
half _LightContribution;
half _LightFalloffSize;
half _Flatness;

half _UnityShadowPower;
half _UnityShadowSharpness;

half _OverrideLightmapDir;
half3 _LightmapDirection;

#endif  // FLATKIT_SYLIZED_INPUT_INCLUDED