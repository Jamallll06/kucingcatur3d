using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChessPiece : MonoBehaviour
{
    [SerializeField] protected Vector2Int startingPosition;
    [SerializeField] private float heightAboveTile = 0.65f;
    [SerializeField] private float moveDuration = 0.2f;
    [SerializeField] private int health = 3;

    public Vector2Int CurrentPosition { get; private set; }

    private bool isMoving;

    protected virtual void Start()
    {
        CurrentPosition = startingPosition;
        transform.position = GetWorldPosition(CurrentPosition);

        GridManager.Instance.GetTile(CurrentPosition).IsOccupied = true;
    }

    protected virtual void OnMouseDown()
    {
        if (!isMoving)
            GridManager.Instance.SelectPiece(this);
    }

    public abstract List<Vector2Int> GetLegalMoves();

    public virtual bool TryMoveTo(Vector2Int targetPosition)
    {
        if (isMoving || !GetLegalMoves().Contains(targetPosition))
            return false;

        StartCoroutine(MoveRoutine(targetPosition, true));
        return true;
    }

    public virtual bool TryParryMoveTo(Vector2Int targetPosition)
    {
        if (isMoving || !IsEmptyTile(targetPosition))
            return false;

        StartCoroutine(MoveRoutine(targetPosition, false));
        return true;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        Debug.Log(
            $"{gameObject.name} menerima {damage} damage. HP: {health}"
        );

        if (health > 0)
            return;

        GridManager.Instance.GetTile(CurrentPosition).IsOccupied = false;

        Debug.Log($"{gameObject.name} kalah!");
        gameObject.SetActive(false);
    }

    private IEnumerator MoveRoutine(
        Vector2Int targetPosition,
        bool endPlayerTurn)
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

        if (endPlayerTurn && TurnManager.Instance != null)
            TurnManager.Instance.EndPlayerTurn();
    }

    protected bool IsEmptyTile(Vector2Int position)
    {
        Tile tile = GridManager.Instance.GetTile(position);

        return tile != null && !tile.IsOccupied;
    }

    protected Vector3 GetWorldPosition(Vector2Int position)
    {
        Tile tile = GridManager.Instance.GetTile(position);

        return tile.transform.position + Vector3.up * heightAboveTile;
    }
}