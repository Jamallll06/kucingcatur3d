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
    private bool isInitialized;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
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

    public bool IsTileAvailable(Vector2Int position)
    {
        Tile tile = GetTile(position);

        if (tile == null)
            return false;


        if (tile.IsBlocked)
            return false;


        if (tile.IsOccupied)
            return false;


        return true;
    }

    public Tile GetRandomFreeTile()
    {
        List<Tile> availableTiles = new List<Tile>();


        foreach (Tile tile in Tiles)
        {
            if (tile == null)
                continue;


            if (tile.IsOccupied)
                continue;


            if (tile.IsBlocked)
                continue;


            availableTiles.Add(tile);
        }


        if (availableTiles.Count == 0)
            return null;


        return availableTiles[
            Random.Range(0, availableTiles.Count)
        ];
    }

    public void ApplyGridSize(int newWidth, int newHeight)
    {
        width = newWidth;
        height = newHeight;
    }

    public void InitializeGrid(
    int newWidth,
    int newHeight
    )
    {

        if (isInitialized)
            return;



        width = newWidth;

        height = newHeight;



        GenerateGrid();



        isInitialized = true;


        Debug.Log(
            $"Grid Created {width} x {height}"
        );

    }

}