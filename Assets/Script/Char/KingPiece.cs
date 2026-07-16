using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingPiece : MonoBehaviour
{
    [SerializeField] private Vector2Int startingPosition = new Vector2Int(4, 4);
    [SerializeField] private float heightAboveTile = 0.65f;
    [SerializeField] private float moveDuration = 0.2f;

    public Vector2Int CurrentPosition { get; private set; }

    private bool isMoving;

    private void Start()
    {
        CurrentPosition = startingPosition;
        SetPositionInstant(CurrentPosition);

        GridManager.Instance.GetTile(CurrentPosition).IsOccupied = true;
    }

    private void OnMouseDown()
    {
        if (!isMoving)
            GridManager.Instance.SelectKing(this);
    }

    public List<Vector2Int> GetLegalMoves()
    {
        List<Vector2Int> moves = new List<Vector2Int>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                Vector2Int targetPosition =
                    CurrentPosition + new Vector2Int(x, y);

                Tile targetTile =
                    GridManager.Instance.GetTile(targetPosition);

                if (targetTile != null && !targetTile.IsOccupied)
                    moves.Add(targetPosition);
            }
        }

        return moves;
    }

    public bool TryMoveTo(Vector2Int targetPosition)
    {
        if (isMoving || !GetLegalMoves().Contains(targetPosition))
            return false;

        StartCoroutine(MoveRoutine(targetPosition));
        return true;
    }

    private IEnumerator MoveRoutine(Vector2Int targetPosition)
    {
        isMoving = true;

        GridManager.Instance.GetTile(CurrentPosition).IsOccupied = false;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = GetWorldPosition(targetPosition);

        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;

            float progress = Mathf.Clamp01(elapsed / moveDuration);
            progress = Mathf.SmoothStep(0f, 1f, progress);

            transform.position = Vector3.Lerp(
                startPosition,
                endPosition,
                progress
            );

            yield return null;
        }

        transform.position = endPosition;
        CurrentPosition = targetPosition;

        GridManager.Instance.GetTile(CurrentPosition).IsOccupied = true;

        isMoving = false;
        Debug.Log($"King berpindah ke: {CurrentPosition}");
    }

    private void SetPositionInstant(Vector2Int boardPosition)
    {
        transform.position = GetWorldPosition(boardPosition);
    }

    private Vector3 GetWorldPosition(Vector2Int boardPosition)
    {
        Tile tile = GridManager.Instance.GetTile(boardPosition);

        return tile.transform.position + Vector3.up * heightAboveTile;
    }
}