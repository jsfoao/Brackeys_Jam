using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    private bool _hovering;
    public UnityEvent OnHoverIn;
    public UnityEvent OnHoverOut;
    public UnityEvent OnInteract;

    public void HoverIn()
    {
        if (_hovering) { return; }
        _hovering = true;
        
        Debug.Log("Hover in");
        OnHoverIn.Invoke();
    }

    public void HoverOut()
    {
        if (!_hovering) { return; }
        _hovering = false;
        
        Debug.Log("Hover out");
        OnHoverOut.Invoke();
    }
    
    private void InOutline()
    {
        var mpb = new MaterialPropertyBlock();
        mpb.SetFloat("_OutlineWidth", 0.1f);
        GetComponent<Renderer>().SetPropertyBlock(mpb);
    }
    
    private void OutOutline()
    {
        var mpb = new MaterialPropertyBlock();
        mpb.SetFloat("_OutlineWidth", 0f);
        GetComponent<Renderer>().SetPropertyBlock(mpb);
    }

    private void Awake()
    {
        OnHoverIn.AddListener(InOutline);
        OnHoverOut.AddListener(OutOutline);
    }
}

public enum SelectionState
{
    Idle, Selected
}
