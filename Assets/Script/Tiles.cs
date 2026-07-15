using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int GridPosition;

    public bool IsOccupied;

    private MeshRenderer meshRenderer;

    private Material defaultMaterial;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        defaultMaterial = meshRenderer.material;
    }

    public void SetMaterial(Material mat)
    {
        meshRenderer.material = mat;
    }

    public void ResetTile()
    {
        meshRenderer.material = defaultMaterial;
    }

    private void OnMouseDown()
    {
        GridManager.Instance.SelectTile(this);
    }
}