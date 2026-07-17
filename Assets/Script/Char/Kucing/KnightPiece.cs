using System.Collections.Generic;
using UnityEngine;

public class KnightPiece : ChessPiece
{
    private static readonly Vector2Int[] KnightMoves =
    {
        new Vector2Int(2, 1),
        new Vector2Int(2, -1),
        new Vector2Int(-2, 1),
        new Vector2Int(-2, -1),

        new Vector2Int(1, 2),
        new Vector2Int(1, -2),
        new Vector2Int(-1, 2),
        new Vector2Int(-1, -2)
    };

    public override List<Vector2Int> GetLegalMoves()
    {
        List<Vector2Int> moves = new List<Vector2Int>();

        foreach (Vector2Int move in KnightMoves)
        {
            Vector2Int target = CurrentPosition + move;

            if (IsEmptyTile(target))
                moves.Add(target);
        }

        return moves;
    }
}