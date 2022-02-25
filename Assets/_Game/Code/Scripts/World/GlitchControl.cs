using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GlitchControl : MonoBehaviour
{
    [SerializeField] private bool active;
    [SerializeField] Material hologramMaterial;
    [SerializeField, Range(0,1)] float glitchChance = 0.1f;
    [SerializeField, Range(0,1)] float flickerChance = 0.1f;
    [SerializeField] Range<float> glitchIntensity;
    [SerializeField] Range<float> glowIntensity;
    [SerializeField] private Range<float> blendIntensity;
    [SerializeField] private Range<float> timer;

    IEnumerator Start()
    {
        while (active)
        {
            float glitchTest = Random.Range(0f, 1f);
            float flickerTest = Random.Range(0f, 1f);
            
            if (glitchTest <= glitchChance)
            {
                float originalGlowIntensity = hologramMaterial.GetFloat("_GlowIntensity");
                float originalBlend = hologramMaterial.GetFloat("_GlitchBlend");
                hologramMaterial.SetFloat("_GlitchIntensity", Random.Range(glitchIntensity.Min, glitchIntensity.Max));
                hologramMaterial.SetFloat("_GlowIntensity", Random.Range(glowIntensity.Min, glowIntensity.Max));
                
                // flickering
                if (flickerTest <= flickerChance)
                {
                    hologramMaterial.SetFloat("_GlitchBlend", Random.Range(blendIntensity.Min, blendIntensity.Max));
                    yield return new WaitForSeconds(0.1f);
                    hologramMaterial.SetFloat("_GlitchBlend", originalBlend);
                    yield return new WaitForSeconds(0.1f);
                    hologramMaterial.SetFloat("_GlitchBlend", Random.Range(blendIntensity.Min, blendIntensity.Max));
                    yield return new WaitForSeconds(0.1f);
                    hologramMaterial.SetFloat("_GlitchBlend", originalBlend);
                }

                yield return new WaitForSeconds(Random.Range(timer.Min, timer.Max));
                hologramMaterial.SetFloat("_GlitchBlend", originalBlend);
                hologramMaterial.SetFloat("_GlitchIntensity", 0f);
                hologramMaterial.SetFloat("_GlowIntensity", originalGlowIntensity);
            }
            yield return new WaitForSeconds(Random.Range(timer.Min, timer.Max));
        }
    }
}

[Serializable]
public struct Range<T>
{
    public T Max;
    public T Min;
}
