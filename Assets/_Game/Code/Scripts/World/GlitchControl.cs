using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GlitchControl : MonoBehaviour
{
    [SerializeField, Range(0,1)] float glitchChance = 0.1f;
    [SerializeField, Range(0,1)] float flickerChance = 0.1f;
    [SerializeField] Range<float> glitchIntensity;
    [SerializeField] Range<float> glowIntensity;
    [SerializeField] private Range<float> blendIntensity;
    
    [SerializeField] private Range<float> timer;
    

    private Material hologramMaterial;
    WaitForSeconds glitchLoopWait = new WaitForSeconds(0.1f);

    void Awake()
    {
        hologramMaterial = GetComponent<Renderer>().material;
    }
    IEnumerator Start()
    {
        while (true)
        {
            float glitchTest = Random.Range(0f, 1f);
            float flickerTest = Random.Range(0f, 1f);
            
            if (glitchTest <= glitchChance)
            {
                float originalGlowIntensity = hologramMaterial.GetFloat("_GlowIntensity");
                hologramMaterial.SetFloat("_GlitchIntensity", Random.Range(glitchIntensity.Min, glitchIntensity.Max));
                hologramMaterial.SetFloat("_GlowIntensity", Random.Range(glowIntensity.Min, glowIntensity.Max));
                
                // flickering
                if (flickerTest <= flickerChance)
                {
                    hologramMaterial.SetFloat("_GlitchBlend", blendIntensity.Max);
                    yield return new WaitForSeconds(0.1f);
                    hologramMaterial.SetFloat("_GlitchBlend", blendIntensity.Min);
                    yield return new WaitForSeconds(0.1f);
                    hologramMaterial.SetFloat("_GlitchBlend", blendIntensity.Max);
                    yield return new WaitForSeconds(0.1f);
                    hologramMaterial.SetFloat("_GlitchBlend", blendIntensity.Min);
                }

                yield return new WaitForSeconds(Random.Range(timer.Min, timer.Max));
                hologramMaterial.SetFloat("_GlitchBlend", blendIntensity.Min);
                hologramMaterial.SetFloat("_GlitchIntensity", 0f);
                hologramMaterial.SetFloat("_GlowIntensity", originalGlowIntensity);
            }
            
            yield return glitchLoopWait;
        }
    }
}

[Serializable]
public struct Range<T>
{
    public T Max;
    public T Min;
}
