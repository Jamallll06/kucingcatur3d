using System.Collections.Generic;
using UnityEngine;

public class QueenPiece : ChessPiece
{
    private static readonly Vector2Int[] Directions =
    {
        new Vector2Int(1, 0),    // kanan
        new Vector2Int(-1, 0),   // kiri
        new Vector2Int(0, 1),    // atas
        new Vector2Int(0, -1),   // bawah

        new Vector2Int(1, 1),    // diagonal kanan atas
        new Vector2Int(1, -1),   // diagonal kanan bawah
        new Vector2Int(-1, 1),   // diagonal kiri atas
        new Vector2Int(-1, -1)   // diagonal kiri bawah
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

                // Berhenti jika keluar papan.
                if (targetTile == null)
                    break;

                // Berhenti sebelum tile yang ditempati bidak lain.
                if (targetTile.IsOccupied)
                    break;

                moves.Add(target);
                target += direction;
            }
        }

        return moves;
    }
}