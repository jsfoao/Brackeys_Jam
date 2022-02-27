using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(PostProcessVolume))]
public class PostFXController : MonoBehaviour
{
    [SerializeField] private PostFXProfile[] postFXProfiles;
    private PostProcessVolume _postProcessVolume;


    
    public void LoadResumeProfile()
    {
        PostFXProfile profile = GetProfile("Ingame");
        _postProcessVolume.profile = profile.PPProfile;
    }
    
    public void LoadPauseProfile()
    {
        PostFXProfile profile = GetProfile("Pause");
        _postProcessVolume.profile = profile.PPProfile;
    }
    
    public void LoadProfile(string name)
    {
        PostFXProfile profile = GetProfile(name);

        if (profile == null) { return; }
        _postProcessVolume.profile = profile.PPProfile;
    }

    private PostFXProfile GetProfile(string name)
    {
        foreach (var profile in postFXProfiles)
        {
            if (profile.Name == name)
            {
                return profile;
            }
        }
        return null;
    }

    private void Start()
    {
        _postProcessVolume = GetComponent<PostProcessVolume>();
    }
}

[Serializable]
public class PostFXProfile
{
    public PostProcessProfile PPProfile;
    public string Name;
}