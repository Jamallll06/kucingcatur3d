using System.Collections.Generic;
using UnityEngine;

public class KingPiece : ChessPiece
{
    public override List<Vector2Int> GetLegalMoves()
    {
        List<Vector2Int> moves = new List<Vector2Int>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                Vector2Int target =
                    CurrentPosition + new Vector2Int(x, y);

                if (IsEmptyTile(target))
                    moves.Add(target);
            }
        }

        return moves;
    }
}