using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }

    [Header("Board Settings")]
    [SerializeField] private int width = 8;
    [SerializeField] private int height = 8;
    [SerializeField] private float tileSize = 1f;
    [SerializeField] private Transform boardParent;

    [Header("Prefabs & Materials")]
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Material whiteTileMaterial;
    [SerializeField] private Material blackTileMaterial;
    [SerializeField] private Material selectedTileMaterial;
    [SerializeField] private Material moveTileMaterial;

    public Tile[,] Tiles { get; private set; }

    private Tile selectedTile;

    private KingPiece selectedKing;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        GenerateGrid();
    }

    

    private void GenerateGrid()
    {
        Tiles = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 position = new Vector3(
                    x * tileSize - (width - 1) * tileSize * 0.5f,
                    0f,
                    y * tileSize - (height - 1) * tileSize * 0.5f
                );

                Tile tile = Instantiate(
                    tilePrefab,
                    position,
                    Quaternion.identity,
                    boardParent
                );

                bool isWhite = (x + y) % 2 == 0;
                Material baseMaterial = isWhite
                    ? whiteTileMaterial
                    : blackTileMaterial;

                tile.Initialize(new Vector2Int(x, y), baseMaterial);
                Tiles[x, y] = tile;
            }
        }
    }

    public void SelectTile(Tile tile)
    {
        if (selectedKing != null)
        {
            bool moved = selectedKing.TryMoveTo(tile.GridPosition);

            if (moved)
            {
                selectedKing = null;
                ClearHighlights();
                return;
            }
        }

        ClearHighlights();

        selectedTile = tile;
        selectedTile.SetMaterial(selectedTileMaterial);

        Debug.Log($"Tile dipilih: {tile.GridPosition}");
    }

    public Tile GetTile(Vector2Int position)
    {
        bool isOutsideBoard =
            position.x < 0 || position.x >= width ||
            position.y < 0 || position.y >= height;

        return isOutsideBoard ? null : Tiles[position.x, position.y];
    }

    public void ClearHighlights()
    {
        foreach (Tile tile in Tiles)
        {
            tile.ResetTile();
        }

        selectedTile = null;
    }

    public void HighlightMoves(System.Collections.Generic.List<Vector2Int> moves)
    {
        ClearHighlights();

        foreach (Vector2Int move in moves)
        {
            Tile tile = GetTile(move);

            if (tile != null)
                tile.SetMaterial(moveTileMaterial);
        }
    }

    public void SelectKing(KingPiece king)
    {
        selectedKing = king;

        HighlightMoves(king.GetLegalMoves());

        Debug.Log($"King dipilih: {king.CurrentPosition}");
    }
}