using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChessPiece : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private float attackRange = 1.3f;


    [Header("Position")]
    [SerializeField] protected Vector2Int startingPosition;
    [SerializeField] private float heightAboveTile = 0.65f;


    [Header("Movement")]
    [SerializeField] private float moveDuration = 0.2f;


    [Header("Health")]
    [SerializeField] private int health = 3;


    public bool IsMoving => isMoving;


    public Vector2Int CurrentPosition { get; private set; }


    private bool isMoving;


    private int maxHealth;


    public int CurrentHealth => health;
    public int MaxHealth => maxHealth;



    protected virtual void Awake()
    {
        maxHealth = health;
    }



    protected virtual void Start()
    {
        StartCoroutine(
            InitializePiece()
        );
    }



    private IEnumerator InitializePiece()
    {

        while (GridManager.Instance == null)
            yield return null;



        while (GridManager.Instance.Tiles == null)
            yield return null;



        CurrentPosition =
            startingPosition;



        transform.position =
            GetWorldPosition(
                CurrentPosition
            );



        Tile tile =
            GridManager.Instance.GetTile(
                CurrentPosition
            );



        if (tile != null)
            tile.IsOccupied = true;



        Debug.Log(
            "Hero Spawn : "
            + CurrentPosition
        );
    }

    protected virtual void OnMouseDown()
    {
        if (!isMoving)
            GridManager.Instance.SelectPiece(this);
    }





    public abstract List<Vector2Int> GetLegalMoves();





    public virtual bool TryMoveTo(Vector2Int targetPosition)
    {

        if (isMoving)
            return false;



        if (!GetLegalMoves().Contains(targetPosition))
            return false;



        Tile targetTile =
            GridManager.Instance.GetTile(targetPosition);



        // Cegah masuk tile kosong yang tidak valid
        if (targetTile == null)
            return false;



        // ==========================
        // BARIER CHECK
        // ==========================

        if (targetTile.IsBlocked)
        {
            Debug.Log(
                "Gerakan diblok oleh Barrier"
            );

            return false;
        }



        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayMove();



        StartCoroutine(
            MoveRoutine(
                targetPosition,
                true
            )
        );


        return true;
    }






    public virtual bool TryParryMoveTo(Vector2Int targetPosition)
    {

        if (isMoving)
            return false;


        if (!IsEmptyTile(targetPosition))
            return false;



        StartCoroutine(
            MoveRoutine(
                targetPosition,
                false
            )
        );


        return true;
    }








    public virtual void TakeDamage(int damage)
    {

        health -= damage;


        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayHit();



        if (CameraShake.Instance != null)
            CameraShake.Instance.Shake(
                0.15f,
                0.12f
            );



        Debug.Log(
            $"{gameObject.name} HP : {health}"
        );



        if (health > 0)
            return;



        Tile currentTile =
            GridManager.Instance.GetTile(CurrentPosition);



        if (currentTile != null)
            currentTile.IsOccupied = false;



        Debug.Log(
            $"{gameObject.name} kalah"
        );


        gameObject.SetActive(false);



        if (GameManager.Instance != null)
            GameManager.Instance.CheckGameOver();
    }









    private IEnumerator MoveRoutine(
        Vector2Int targetPosition,
        bool endPlayerTurn)
    {

        isMoving = true;



        Tile currentTile =
            GridManager.Instance.GetTile(CurrentPosition);



        if (currentTile != null)
            currentTile.IsOccupied = false;



        Vector3 startPosition =
            transform.position;



        Vector3 endPosition =
            GetWorldPosition(targetPosition);



        float elapsed = 0f;



        while (elapsed < moveDuration)
        {

            elapsed += Time.deltaTime;



            float progress =
                Mathf.Clamp01(
                    elapsed / moveDuration
                );


            progress =
                Mathf.SmoothStep(
                    0,
                    1,
                    progress
                );



            transform.position =
                Vector3.Lerp(
                    startPosition,
                    endPosition,
                    progress
                );


            yield return null;
        }




        transform.position =
            endPosition;



        CurrentPosition =
            targetPosition;




        Tile targetTile =
            GridManager.Instance.GetTile(CurrentPosition);



        if (targetTile != null)
            targetTile.IsOccupied = true;



        TryAttackBoss();



        isMoving = false;




        if (endPlayerTurn &&
           TurnManager.Instance != null)
        {
            TurnManager.Instance.EndPlayerTurn();
        }

    }










    protected bool IsEmptyTile(Vector2Int position)
    {

        Tile tile =
            GridManager.Instance.GetTile(position);



        if (tile == null)
            return false;



        if (tile.IsBlocked)
            return false;



        return !tile.IsOccupied;

    }








    protected Vector3 GetWorldPosition(
     Vector2Int position)
    {

        Tile tile =
            GridManager.Instance.GetTile(
                position
            );


        if (tile == null)
            return transform.position;



        return tile.transform.position +
               Vector3.up * heightAboveTile;

    }








    private void TryAttackBoss()
    {

        BossHealth boss =
            FindFirstObjectByType<BossHealth>();



        if (boss == null ||
           boss.IsDefeated)
            return;




        float distance =
            Vector3.Distance(
                transform.position,
                boss.transform.position
            );



        if (distance > attackRange)
            return;




        boss.TakeDamage(
            attackDamage
        );



        Debug.Log(
            $"{gameObject.name} menyerang Boss"
        );

    }









    public void Heal(int amount)
    {

        int before = health;



        health =
            Mathf.Min(
                health + amount,
                maxHealth
            );



        Debug.Log(
            $"{gameObject.name} heal {before} -> {health}"
        );

    }

}