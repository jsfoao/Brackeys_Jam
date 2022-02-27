using UnityEngine;

public class ChunkInfo : MonoBehaviour
{
    public int row;
    public bool isReal;
    public bool hasPill;
    [SerializeField] private Transform pill;
    
    private Renderer _renderer;
    private Collider _collider;
    private GlitchControl _glitchControl;
    public Material glitchedMaterial;
    public Material regularMaterial;

    void Start()
    {
        _renderer = GetComponentInChildren<Renderer>();
        _collider = GetComponentInChildren<Collider>();
        _glitchControl = GetComponentInChildren<GlitchControl>();
        _collider.enabled = isReal;
        if (hasPill && isReal)
        {
            Vector3 pillSpawn = transform.GetChild(0).position + new Vector3(0, 1, 0);
            Instantiate(pill, pillSpawn, Quaternion.identity);
        }
    }
    
    void Update()
    {
        int playerTile = GameManager.Instance.spawnGenerator.PlayerTile;
        int mapLength = GameManager.Instance.spawnGenerator.mapLength;
        if (playerTile - mapLength > row)
        {
            Destroy(gameObject);
            return;
        }

        float tileSizeZ = GameManager.Instance.spawnGenerator.tileSizeZ;
        float aheadPlaneZ = GameManager.Instance.spawnGenerator.aheadPlane.position.z;
        float behindPlaneZ = GameManager.Instance.spawnGenerator.behindPlane.position.z;
        if (transform.position.z + tileSizeZ/2 < aheadPlaneZ && transform.position.z - tileSizeZ/2 > behindPlaneZ && !isReal)
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
