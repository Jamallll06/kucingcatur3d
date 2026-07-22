using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Tile : MonoBehaviour
{
    public Vector2Int GridPosition { get; private set; }
    public bool IsOccupied { get; set; }
    public bool IsBlocked { get; private set; }


    private MeshRenderer meshRenderer;
    private Material defaultMaterial;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Initialize(Vector2Int position, Material baseMaterial)
    {
        GridPosition = position;
        defaultMaterial = baseMaterial;
        meshRenderer.sharedMaterial = baseMaterial;
        gameObject.name = $"Tile ({position.x}, {position.y})";
    }

    public void SetMaterial(Material material)
    {
        meshRenderer.sharedMaterial = material;
    }

    public void ResetTile()
    {
        meshRenderer.sharedMaterial = defaultMaterial;
    }

    private void OnMouseDown()
    {
        GridManager.Instance.SelectTile(this);
    }

    public void SetBlocked(bool value)
    {
        IsBlocked = value;
    }
}