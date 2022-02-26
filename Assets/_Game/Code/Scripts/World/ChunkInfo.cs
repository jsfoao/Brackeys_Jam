using UnityEngine;

public class ChunkInfo : MonoBehaviour
{
    public int row;
    public bool isReal;
    private Renderer _renderer;
    private Collider _collider;
    private GlitchControl _glitchControl;
    public Material glitchedMaterial;
    public Material regularMaterial;
    private SpawnGenerator spawnGenerator;
    void Start()
    {
        spawnGenerator = FindObjectOfType<SpawnGenerator>();
        _renderer = GetComponentInChildren<Renderer>();
        _collider = GetComponentInChildren<Collider>();
        _glitchControl = GetComponentInChildren<GlitchControl>();
        _collider.enabled = isReal;
    }
    
    void Update()
    {
        if (spawnGenerator.PlayerTile - spawnGenerator.mapLength > row)
        {
            Destroy(gameObject);
            return;
        }
        if (spawnGenerator.PlayerTile + GameManager.Instance.controlledEntity.range >= row && !isReal)
        {
            _renderer.material = glitchedMaterial;
            _glitchControl.active = true;
        }
        else
        {
            _renderer.material = regularMaterial;
            _glitchControl.active = false;
        }
    }
}
