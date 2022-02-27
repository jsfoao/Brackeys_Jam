using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GlitchControl : MonoBehaviour
{
    [SerializeField] public bool active;
    [SerializeField] Material hologramMaterial;
    [SerializeField, Range(0,1)] float glitchChance = 0.1f;
    [SerializeField, Range(0,1)] float flickerChance = 0.1f;
    [SerializeField] Range<float> glitchIntensity;
    [SerializeField] Range<float> glowIntensity;
    [SerializeField] private Range<float> blendIntensity;
    [SerializeField] private Range<float> timer;
    [SerializeField] private float intensityMultiplier;
    

    private MaterialPropertyBlock _mpb;
    private Renderer _renderer;

    private void Awake()
    {
        _mpb = new MaterialPropertyBlock();
        _renderer = GetComponent<Renderer>();
    }

    IEnumerator Start()
    {
        while (active)
        {
            float glitchTest = Random.Range(0f, 1f);
            float flickerTest = Random.Range(0f, 1f);
            
            if (glitchTest <= glitchChance)
            {
                float originalGlowIntensity = _mpb.GetFloat("_GlowIntensity");
                float originalBlend = _mpb.GetFloat("_GlitchBlend");
                _mpb.SetFloat("_GlitchIntensity", Random.Range(glitchIntensity.Min, glitchIntensity.Max) * intensityMultiplier);
                _mpb.SetFloat("_GlowIntensity", Random.Range(glowIntensity.Min, glowIntensity.Max));
                _renderer.SetPropertyBlock(_mpb);

                // flickering
                if (flickerTest <= flickerChance)
                {
                    _mpb.SetFloat("_GlitchBlend", Random.Range(blendIntensity.Min, blendIntensity.Max));
                    _renderer.SetPropertyBlock(_mpb);
                    yield return new WaitForSeconds(0.1f);
                    _mpb.SetFloat("_GlitchBlend", originalBlend);
                    _renderer.SetPropertyBlock(_mpb);
                    yield return new WaitForSeconds(0.1f);
                    _mpb.SetFloat("_GlitchBlend", Random.Range(blendIntensity.Min, blendIntensity.Max));
                    _renderer.SetPropertyBlock(_mpb);
                    yield return new WaitForSeconds(0.1f);
                    _mpb.SetFloat("_GlitchBlend", originalBlend);
                    _renderer.SetPropertyBlock(_mpb);
                }

                yield return new WaitForSeconds(Random.Range(timer.Min, timer.Max));
                _mpb.SetFloat("_GlitchBlend", originalBlend);
                _mpb.SetFloat("_GlitchIntensity", 0f);
                _mpb.SetFloat("_GlowIntensity", originalGlowIntensity);
                _renderer.SetPropertyBlock(_mpb);
            }
            yield return new WaitForSeconds(Random.Range(timer.Min, timer.Max));
            _renderer.SetPropertyBlock(_mpb);
        }
    }
}

[Serializable]
public struct Range<T>
{
    public T Max;
    public T Min;
}
