#ifndef FLATKIT_LIGHTING_DR_INCLUDED
#define FLATKIT_LIGHTING_DR_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

inline half NdotLTransition(half3 normal, half3 lightDir, half selfShadingSize, half shadowEdgeSize, half flatness) {
    const half NdotL = dot(normal, lightDir);
    const half angleDiff = saturate((NdotL * 0.5 + 0.5) - selfShadingSize);
    const half angleDiffTransition = smoothstep(0, shadowEdgeSize, angleDiff); 
    return lerp(angleDiff, angleDiffTransition, flatness);
}

inline half NdotLTransitionPrimary(half3 normal, half3 lightDir) { 
    return NdotLTransition(normal, lightDir, _SelfShadingSize, _ShadowEdgeSize, _Flatness);
}

#if defined(DR_CEL_EXTRA_ON)
inline half NdotLTransitionExtra(half3 normal, half3 lightDir) { 
    return NdotLTransition(normal, lightDir, _SelfShadingSizeExtra, _ShadowEdgeSizeExtra, _FlatnessExtra);
}
#endif

inline half NdotLTransitionTexture(half3 normal, half3 lightDir, sampler2D stepTex) {
    const half NdotL = dot(normal, lightDir);
    const half angleDiff = saturate((NdotL * 0.5 + 0.5) - _SelfShadingSize * 0.0);
    const half4 rampColor = tex2D(stepTex, half2(angleDiff, 0.5));
    // NOTE: The color channel here corresponds to the texture format in the shader editor script.
    const half angleDiffTransition = rampColor.r;
    return angleDiffTransition;
}

half3 LightingPhysicallyBased_DSTRM(Light light, half3 normalWS, half3 viewDirectionWS, float3 positionWS)
{
    // If all light in the scene is baked, we use custom light direction for the cel shading.
    light.direction = lerp(light.direction, _LightmapDirection, _OverrideLightmapDir);

    half4 c = _BaseColor;

#if defined(_QUIBLI_GRADIENT)
    _BaseColor = SAMPLE_TEXTURE2D(_ColorGradient, sampler_ColorGradient, float2(1.0, 0.5));
    const half NdotLTPrimary = NdotLTransitionPrimary(normalWS, light.direction);
    float2 gradient_uv = float2(NdotLTPrimary, 0.5);
    c = SAMPLE_TEXTURE2D(_ColorGradient, sampler_ColorGradient, gradient_uv);
#else
#if defined(_CELPRIMARYMODE_SINGLE)
    const half NdotLTPrimary = NdotLTransitionPrimary(normalWS, light.direction);
    c = lerp(UNITY_ACCESS_INSTANCED_PROP(Props, _ColorDim), c, NdotLTPrimary);
#endif  // _CELPRIMARYMODE_SINGLE

#if defined(_CELPRIMARYMODE_STEPS)
    const half NdotLTSteps = NdotLTransitionTexture(normalWS, light.direction, _CelStepTexture);
    c = lerp(_ColorDimSteps, c, NdotLTSteps);
#endif  // _CELPRIMARYMODE_STEPS

#if defined(_CELPRIMARYMODE_CURVE)
    const half NdotLTCurve = NdotLTransitionTexture(normalWS, light.direction, _CelCurveTexture);
    c = lerp(_ColorDimCurve, c, NdotLTCurve);
#endif  // _CELPRIMARYMODE_CURVE

#if defined(DR_CEL_EXTRA_ON)
    const half NdotLTExtra = NdotLTransitionExtra(normalWS, light.direction);
    c = lerp(_ColorDimExtra, c, NdotLTExtra);
#endif  // DR_CEL_EXTRA_ON
#endif

#if defined(DR_GRADIENT_ON)
    const float angleRadians = _GradientAngle / 180.0 * PI;
    const float posGradRotated = (positionWS.x - _GradientCenterX) * sin(angleRadians) + 
                           (positionWS.y - _GradientCenterY) * cos(angleRadians);
    const float gradientTop = _GradientCenterY + _GradientSize * 0.5;
    const half gradientFactor = saturate((gradientTop - posGradRotated) / _GradientSize);
    c = lerp(c, _ColorGradient, gradientFactor);
#endif  // DR_GRADIENT_ON

#if defined(DR_RIM_ON)
    const half NdotL = dot(normalWS, light.direction);
    const float4 rim = 1.0 - dot(viewDirectionWS, normalWS);
    const float rimLightAlign = UNITY_ACCESS_INSTANCED_PROP(Props, _FlatRimLightAlign);
    const float rimSize = UNITY_ACCESS_INSTANCED_PROP(Props, _FlatRimSize);
    const float rimSpread = 1.0 - rimSize - NdotL * rimLightAlign;
    const float rimEdgeSmooth = UNITY_ACCESS_INSTANCED_PROP(Props, _FlatRimEdgeSmoothness);
    const float rimTransition = smoothstep(rimSpread - rimEdgeSmooth * 0.5, rimSpread + rimEdgeSmooth * 0.5, rim);
    c = lerp(c, UNITY_ACCESS_INSTANCED_PROP(Props, _FlatRimColor), rimTransition);
#endif  // DR_RIM_ON

#if defined(DR_SPECULAR_ON)
    // Halfway between lighting direction and view vector.
    const float3 halfVector = normalize(light.direction + viewDirectionWS);
    const float NdotH = dot(normalWS, halfVector) * 0.5 + 0.5;
    const float specularSize = UNITY_ACCESS_INSTANCED_PROP(Props, _FlatSpecularSize);
    const float specEdgeSmooth = UNITY_ACCESS_INSTANCED_PROP(Props, _FlatSpecularEdgeSmoothness);
    const float specular = saturate(pow(NdotH, 100.0 * (1.0 - specularSize) * (1.0 - specularSize)));
    const float specularTransition = smoothstep(0.5 - specEdgeSmooth * 0.1, 0.5 + specEdgeSmooth * 0.1, specular);
    c = lerp(c, UNITY_ACCESS_INSTANCED_PROP(Props, _FlatSpecularColor), specularTransition);
#endif  // DR_SPECULAR_ON

#if defined(_UNITYSHADOWMODE_MULTIPLY)
    c *= lerp(1, light.shadowAttenuation, _UnityShadowPower);
#endif
#if defined(_UNITYSHADOWMODE_COLOR)
    const half4 unityShadowColor = UNITY_ACCESS_INSTANCED_PROP(Props, _UnityShadowColor);
    c = lerp(lerp(c, unityShadowColor, unityShadowColor.a), c, light.shadowAttenuation);
#endif

    c.rgb *= light.color * light.distanceAttenuation;

    return c.rgb;
}

void StylizeLight(inout Light light)
{
    const half shadowAttenuation = saturate(light.shadowAttenuation * _UnityShadowSharpness);
    light.shadowAttenuation = shadowAttenuation;

    const half distanceAttenuation = smoothstep(0, _LightFalloffSize, light.distanceAttenuation);
    light.distanceAttenuation = distanceAttenuation;

    const half3 lightColor = lerp(half3(1, 1, 1), light.color, _LightContribution);
    light.color = lightColor;
}

half4 UniversalFragment_DSTRM(InputData inputData, half3 albedo, half3 emission, half alpha)
{
    Light mainLight = GetMainLight(inputData.shadowCoord);
#if LIGHTMAP_ON
    mainLight.distanceAttenuation = 1.0;
#endif
    StylizeLight(mainLight);
    MixRealtimeAndBakedGI(mainLight, inputData.normalWS, inputData.bakedGI, half4(0, 0, 0, 0));

    // Apply Flat Kit stylizing to `inputData.bakedGI`.
#if LIGHTMAP_ON
    const half sharpness01 = (_UnityShadowSharpness - 1.0) / (10.0 - 1.0);  // UI range is set to 1.0 - 10.0.
    const half blur = max(1.0 - sharpness01, 0.001);
    const half transitionPoint = 1.0 - _LightFalloffSize;
    inputData.bakedGI = smoothstep(transitionPoint, transitionPoint + blur, length(inputData.bakedGI));

    /*
    #if defined(_UNITYSHADOWMODE_MULTIPLY)
        inputData.bakedGI *= _UnityShadowPower;
    #endif
    #if defined(_UNITYSHADOWMODE_COLOR)
        half4 unityShadowColor = UNITY_ACCESS_INSTANCED_PROP(Props, _UnityShadowColor);
        inputData.bakedGI = lerp(inputData.bakedGI, unityShadowColor, unityShadowColor.a * inputData.bakedGI);
    #endif
    */

    // return half4(inputData.bakedGI, 1);
#endif

    BRDFData brdfData;
    InitializeBRDFData(albedo, 1.0 - 1.0 / kDieletricSpec.a, 0, 0, alpha, brdfData);
    half3 color = GlobalIllumination(brdfData, inputData.bakedGI, 1.0, inputData.normalWS, inputData.viewDirectionWS);
    color += LightingPhysicallyBased_DSTRM(mainLight, inputData.normalWS, inputData.viewDirectionWS, inputData.positionWS);

#ifdef _ADDITIONAL_LIGHTS
    const uint pixelLightCount = GetAdditionalLightsCount();
    for (uint lightIndex = 0u; lightIndex < pixelLightCount; ++lightIndex)
    {
        Light light = GetAdditionalLight(lightIndex, inputData.positionWS);
        StylizeLight(light);
        color += LightingPhysicallyBased_DSTRM(light, inputData.normalWS, inputData.viewDirectionWS, inputData.positionWS);
    }
#endif

#ifdef _ADDITIONAL_LIGHTS_VERTEX
    color += inputData.vertexLighting * brdfData.diffuse;
#endif

    color += emission;
    return half4(color, alpha);
}

#endif // FLATKIT_LIGHTING_DR_INCLUDED