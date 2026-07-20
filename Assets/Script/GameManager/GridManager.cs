using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }

    [Header("Board")]
    [SerializeField] private int width = 8;
    [SerializeField] private int height = 8;
    [SerializeField] private float tileSize = 1f;
    [SerializeField] private Transform boardParent;

    [Header("Prefab")]
    [SerializeField] private Tile tilePrefab;

    [Header("Materials")]
    [SerializeField] private Material whiteTileMaterial;
    [SerializeField] private Material blackTileMaterial;
    [SerializeField] private Material selectedTileMaterial;
    [SerializeField] private Material moveTileMaterial;
    [SerializeField] private Material attackTileMaterial;

    public Tile[,] Tiles { get; private set; }
    public int Width => width;
    public int Height => height;

    private Tile selectedTile;
    private ChessPiece selectedPiece;

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

    public Tile GetTile(Vector2Int position)
    {
        bool outsideBoard =
            position.x < 0 || position.x >= width ||
            position.y < 0 || position.y >= height;

        return outsideBoard ? null : Tiles[position.x, position.y];
    }

    public Vector2Int WorldToGrid(Vector3 worldPosition)
    {
        int x = Mathf.RoundToInt(
            worldPosition.x / tileSize +
            (width - 1) * 0.5f
        );

        int y = Mathf.RoundToInt(
            worldPosition.z / tileSize +
            (height - 1) * 0.5f
        );


        return new Vector2Int(x, y);
    }

    public Vector3 GridToWorld(Vector2Int gridPosition)
    {
        return new Vector3(
            gridPosition.x * tileSize -
            (width - 1) * tileSize * 0.5f,

            0f,

            gridPosition.y * tileSize -
            (height - 1) * tileSize * 0.5f
        );
    }

    public void SelectPiece(ChessPiece piece)
    {
        if (TurnManager.Instance == null ||
            !TurnManager.Instance.IsPlayerTurn)
            return;

        selectedPiece = piece;
        HighlightMoves(piece.GetLegalMoves());
    }

    public void SelectTile(Tile tile)
    {
        if (TurnManager.Instance == null ||
            !TurnManager.Instance.IsPlayerTurn)
            return;

        if (selectedPiece != null)
        {
            if (selectedPiece.TryMoveTo(tile.GridPosition))
            {
                selectedPiece = null;
                ClearHighlights();
            }

            return;
        }

        ClearHighlights();

        selectedTile = tile;
        selectedTile.SetMaterial(selectedTileMaterial);
    }

    public void HighlightMoves(List<Vector2Int> moves)
    {
        ClearHighlights();

        foreach (Vector2Int move in moves)
        {
            Tile tile = GetTile(move);

            if (tile != null)
                tile.SetMaterial(moveTileMaterial);
        }
    }

    public void ShowAttackTelegraph(List<Vector2Int> targetPositions)
    {
        ClearHighlights();

        foreach (Vector2Int position in targetPositions)
        {
            Tile tile = GetTile(position);

            if (tile != null)
                tile.SetMaterial(attackTileMaterial);
        }
    }

    public void ClearHighlights()
    {
        foreach (Tile tile in Tiles)
            tile.ResetTile();

        selectedTile = null;
    }
}