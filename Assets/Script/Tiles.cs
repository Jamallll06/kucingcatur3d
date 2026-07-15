using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int GridPosition;

    public bool IsOccupied;

    private MeshRenderer meshRenderer;

    [Header("Materials")]

    public Material whiteMaterial;

    public Material blackMaterial;

    public Material moveMaterial;

    public Material attackMaterial;

    public Material selectedMaterial;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetMaterial(Material mat)
    {
        meshRenderer.material = mat;
    }
}