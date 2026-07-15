using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    public int width = 8;

    public int height = 8;

    public float tileSize = 1f;

    public Tile tilePrefab;

    public Material whiteTile;

    public Material blackTile;

    public Tile[,] Tiles;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        Tiles = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 pos = new Vector3(x * tileSize, 0, y * tileSize);

                Tile tile = Instantiate(
                    tilePrefab,
                    pos,
                    Quaternion.Euler(90, 0, 0),
                    transform
                );

                tile.GridPosition = new Vector2Int(x, y);

                bool white = (x + y) % 2 == 0;

                tile.SetMaterial(
                    white ? whiteTile : blackTile
                );

                Tiles[x, y] = tile;
            }
        }
    }
}