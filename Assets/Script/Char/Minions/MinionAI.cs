using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MinionAI : MonoBehaviour
{
    [Header("Stat")]
    [SerializeField] private int attackDamage = 1;

    [SerializeField] private float attackRange = 1.2f;

    [SerializeField] private float moveSpeed = 4f;

    private ChessPiece target;

    private bool isMoving;

    private Vector2Int gridPosition;

    public IEnumerator ExecuteTurn()
    {
        if (isMoving)
            yield break;

        target = FindNearestHero();

        if (target == null)
            yield break;

        float distance = Vector3.Distance(
            transform.position,
            target.transform.position
        );

        if (distance <= attackRange)
        {
            target.TakeDamage(attackDamage);

            Debug.Log("Minion menyerang Hero");

            yield break;
        }

        yield return MoveTowardsTarget();
    }

    private IEnumerator MoveTowardsTarget()
    {
        isMoving = true;

        Vector3 start = transform.position;

        Vector3 end = Vector3.MoveTowards(
            transform.position,
            target.transform.position,
            1f
        );

        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * moveSpeed;

            transform.position =
                Vector3.Lerp(start, end, t);

            yield return null;
        }

        isMoving = false;
    }

    private ChessPiece FindNearestHero()
    {
        ChessPiece[] heroes =
            FindObjectsByType<ChessPiece>(
                FindObjectsSortMode.None
            );

        ChessPiece nearest = null;

        float minDistance = Mathf.Infinity;

        foreach (ChessPiece hero in heroes)
        {
            float d = Vector3.Distance(
                transform.position,
                hero.transform.position
            );

            if (d < minDistance)
            {
                minDistance = d;
                nearest = hero;
            }
        }

        return nearest;
    }

   
    public void SetGridPosition(Vector2Int pos)
    {
        gridPosition = pos;
    }

    private void OnDestroy()
    {
        if (GridManager.Instance == null)
            return;

        Tile tile =
            GridManager.Instance.GetTile(gridPosition);

        if (tile != null)
            tile.IsOccupied = false;
    }
}