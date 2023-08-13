void CelShadeLighting_float (
    in UnitySamplerState Sampler,  in float2 uv, in float3 Normal, in float3 ClipSpacePos, in float3 WorldPos, 
    in UnityTexture2D MainTexture, in float4 BaseColorTint, in float4 ShadowColorTint, in float RampSmoothness, 
    in float HaloOffset, in float HaloSmoothness, in float4 HaloColorTint,
out float3 Output, out float3 Direction)
{
    #ifdef SHADERGRAPH_PREVIEW
    Output = float3(0.5, 0.5, 0);
    Direction = float3(0.5, 0.5, 0);
    #else
        #ifdef SHADOWS_SCREEN
            half4 shadowCoord = ComputeScreenPos(ClipSpacePos);
        #else
            half4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
        #endif
    
        #if _MAIN_LIGHT_SHADOWS_CASCADE || _MAIN_LIGHT_SHADOWS
            Light light = GetMainLight(shadowCoord);
        #else
            Light light = GetMainLight();
        #endif

        half d = dot(Normal, light.direction);
        half ramp = smoothstep(0, RampSmoothness, d) * light.shadowAttenuation;
        half haloRamp = smoothstep(HaloOffset, HaloOffset + HaloSmoothness, d) * light.shadowAttenuation;
        float3 Lerp1 = lerp(ShadowColorTint, BaseColorTint, ramp);
        float3 Lerp2 = lerp(HaloColorTint, Lerp1, haloRamp);

        float3 Color = Lerp2 * MainTexture.Sample(Sampler, uv);
        //ramp *= light.shadowAttenuation;

        // Output = light.color * (ramp + RampTint);
        

        Output = Color;
        Direction = light.direction;
    #endif
} 