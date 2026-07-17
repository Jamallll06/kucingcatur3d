using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossAttackPattern
{
    SingleTarget,
    Cross,
    Square
}

public class BossAI : MonoBehaviour
{
    [Header("Boss Movement")]
    [SerializeField] private float moveDuration = 0.4f;
    [SerializeField] private float heightAboveTile = 0.8f;

    [Header("Attack")]
    [SerializeField] private float telegraphDuration = 1f;
    [SerializeField] private int damage = 1;

    [Header("Targeting")]
    [Range(0f, 1f)]
    [SerializeField] private float targetAccuracy = 0.65f;

    [SerializeField] private int missRadius = 1;

    [Header("Parry")]
    [SerializeField] private KeyCode parryKey = KeyCode.Space;
    [SerializeField] private float parryWindowDuration = 0.25f;

    private bool isParryWindowOpen;
    private bool parrySucceeded;

    private void Update()
    {
        if (isParryWindowOpen && Input.GetKeyDown(parryKey))
        {
            parrySucceeded = true;
            isParryWindowOpen = false;

            Debug.Log("PARRY BERHASIL!");
        }
    }

    public IEnumerator ExecuteTurn()
    {
        ChessPiece targetPiece = FindNearestPiece();

        if (targetPiece == null)
            yield break;

        Vector2Int targetCenter =
            ChooseTargetPosition(targetPiece.CurrentPosition);

        BossAttackPattern pattern =
            (BossAttackPattern)Random.Range(0, 3);

        List<Vector2Int> attackTiles =
            GetAttackTiles(targetCenter, pattern);

        yield return MoveToAttackPosition(targetCenter);

        GridManager.Instance.ShowAttackTelegraph(attackTiles);

        parrySucceeded = false;

        float parryTime = Mathf.Clamp(
            parryWindowDuration,
            0.01f,
            telegraphDuration
        );

        yield return new WaitForSeconds(telegraphDuration - parryTime);

        isParryWindowOpen = true;
        Debug.Log("PARRY SEKARANG! Tekan SPACE.");

        yield return new WaitForSeconds(parryTime);

        isParryWindowOpen = false;

        if (parrySucceeded)
        {
            MovePieceToSafeTile(targetPiece, attackTiles);
        }
        else
        {
            DamageCharactersOnTiles(attackTiles);
        }

        GridManager.Instance.ClearHighlights();
    }

    private IEnumerator MoveToAttackPosition(Vector2Int targetPosition)
    {
        Tile targetTile = GridManager.Instance.GetTile(targetPosition);

        if (targetTile == null)
            yield break;

        Vector3 startPosition = transform.position;
        Vector3 endPosition =
            targetTile.transform.position + Vector3.up * heightAboveTile;

        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;

            float progress = Mathf.SmoothStep(
                0f,
                1f,
                elapsed / moveDuration
            );

            transform.position = Vector3.Lerp(
                startPosition,
                endPosition,
                progress
            );

            yield return null;
        }

        transform.position = endPosition;
    }

    private void MovePieceToSafeTile(
        ChessPiece piece,
        List<Vector2Int> attackTiles)
    {
        // Jika bidak tidak berada di area serangan,
        // parry tetap sukses tetapi bidak tidak perlu dipindahkan.
        if (!attackTiles.Contains(piece.CurrentPosition))
            return;

        Vector2Int safeTile = FindNearestSafeTile(
            piece.CurrentPosition,
            attackTiles
        );

        if (safeTile == piece.CurrentPosition)
            return;

        piece.TryParryMoveTo(safeTile);

        Debug.Log(
            $"{piece.gameObject.name} dash ke tile aman: {safeTile}"
        );
    }

    private Vector2Int FindNearestSafeTile(
        Vector2Int origin,
        List<Vector2Int> attackTiles)
    {
        Vector2Int bestTile = origin;
        float closestDistance = float.MaxValue;

        for (int x = 0; x < GridManager.Instance.Width; x++)
        {
            for (int y = 0; y < GridManager.Instance.Height; y++)
            {
                Vector2Int position = new Vector2Int(x, y);
                Tile tile = GridManager.Instance.GetTile(position);

                bool isSafe =
                    tile != null &&
                    !tile.IsOccupied &&
                    !attackTiles.Contains(position);

                if (!isSafe)
                    continue;

                float distance = Vector2Int.Distance(origin, position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    bestTile = position;
                }
            }
        }

        return bestTile;
    }

    private ChessPiece FindNearestPiece()
    {
        ChessPiece[] pieces = FindObjectsByType<ChessPiece>(
            FindObjectsSortMode.None
        );

        ChessPiece nearestPiece = null;
        float nearestDistance = float.MaxValue;

        foreach (ChessPiece piece in pieces)
        {
            float distance = Vector3.Distance(
                transform.position,
                piece.transform.position
            );

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestPiece = piece;
            }
        }

        return nearestPiece;
    }

    private Vector2Int ChooseTargetPosition(Vector2Int piecePosition)
    {
        if (Random.value <= targetAccuracy)
            return piecePosition;

        for (int attempt = 0; attempt < 10; attempt++)
        {
            Vector2Int offset = new Vector2Int(
                Random.Range(-missRadius, missRadius + 1),
                Random.Range(-missRadius, missRadius + 1)
            );

            if (offset == Vector2Int.zero)
                continue;

            Vector2Int missPosition = piecePosition + offset;

            if (GridManager.Instance.GetTile(missPosition) != null)
                return missPosition;
        }

        return piecePosition;
    }

    private List<Vector2Int> GetAttackTiles(
        Vector2Int center,
        BossAttackPattern pattern)
    {
        List<Vector2Int> tiles = new List<Vector2Int>();

        switch (pattern)
        {
            case BossAttackPattern.SingleTarget:
                AddIfValid(tiles, center);
                break;

            case BossAttackPattern.Cross:
                AddIfValid(tiles, center);
                AddIfValid(tiles, center + Vector2Int.up);
                AddIfValid(tiles, center + Vector2Int.down);
                AddIfValid(tiles, center + Vector2Int.left);
                AddIfValid(tiles, center + Vector2Int.right);
                break;

            case BossAttackPattern.Square:
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        AddIfValid(
                            tiles,
                            center + new Vector2Int(x, y)
                        );
                    }
                }
                break;
        }

        return tiles;
    }

    private void AddIfValid(
        List<Vector2Int> tiles,
        Vector2Int position)
    {
        if (GridManager.Instance.GetTile(position) != null)
            tiles.Add(position);
    }

    private void DamageCharactersOnTiles(
        List<Vector2Int> attackTiles)
    {
        ChessPiece[] pieces = FindObjectsByType<ChessPiece>(
            FindObjectsSortMode.None
        );

        foreach (ChessPiece piece in pieces)
        {
            if (attackTiles.Contains(piece.CurrentPosition))
                piece.TakeDamage(damage);
        }
    }
}