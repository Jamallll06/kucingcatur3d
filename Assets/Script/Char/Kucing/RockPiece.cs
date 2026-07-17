using System.Collections.Generic;
using UnityEngine;

public class RookPiece : ChessPiece
{
    private static readonly Vector2Int[] Directions =
    {
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0),
        new Vector2Int(0, 1),
        new Vector2Int(0, -1)
    };

    public override List<Vector2Int> GetLegalMoves()
    {
        List<Vector2Int> moves = new List<Vector2Int>();

        foreach (Vector2Int direction in Directions)
        {
            Vector2Int target = CurrentPosition + direction;

            while (true)
            {
                Tile targetTile = GridManager.Instance.GetTile(target);

                if (targetTile == null || targetTile.IsOccupied)
                    break;

                moves.Add(target);
                target += direction;
            }
        }

        return moves;
    }
}