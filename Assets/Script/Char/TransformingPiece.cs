using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CatForm
{
    King,
    Queen,
    Rook,
    Bishop,
    Knight,
    Pawn
}

public class TransformingPiece : ChessPiece
{
    [Header("Transformation")]
    [SerializeField] private CatForm currentForm = CatForm.King;

    [Header("Visual")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite kingSprite;
    [SerializeField] private Sprite queenSprite;
    [SerializeField] private Sprite rookSprite;
    [SerializeField] private Sprite bishopSprite;
    [SerializeField] private Sprite knightSprite;
    [SerializeField] private Sprite pawnSprite;

    [SerializeField] private Color kingColor = new Color(1f, 0.85f, 0.2f);
    [SerializeField] private Color queenColor = new Color(0.9f, 0.3f, 1f);
    [SerializeField] private Color rookColor = new Color(0.3f, 0.7f, 1f);
    [SerializeField] private Color bishopColor = new Color(0.5f, 0.2f, 0.8f);
    [SerializeField] private Color knightColor = new Color(1f, 0.45f, 0.1f);
    [SerializeField] private Color pawnColor = new Color(0.4f, 1f, 0.5f);

    [Header("Skill Cost")]
    [SerializeField] private int kingSkillCost = 2;
    [SerializeField] private int queenSkillCost = 3;
    [SerializeField] private int rookSkillCost = 2;
    [SerializeField] private int bishopSkillCost = 2;
    [SerializeField] private int knightSkillCost = 3;
    [SerializeField] private int pawnSkillCost = 1;

    public CatForm CurrentForm => currentForm;

    private int shieldCharges;
    private int armorCharges;

    protected override void Start()
    {
        base.Start();

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        ApplyFormVisual();
    }

    private void Update()
    {
        if (TurnManager.Instance == null ||
            !TurnManager.Instance.IsPlayerTurn)
            return;

        ChangeFormWithKeyboard();

        if (Input.GetKeyDown(KeyCode.Q))
            UseCurrentFormSkill();
    }

    public override List<Vector2Int> GetLegalMoves()
    {
        List<Vector2Int> moves = new List<Vector2Int>();

        switch (currentForm)
        {
            case CatForm.King:
                AddKingMoves(moves);
                break;

            case CatForm.Queen:
                AddSlidingMoves(moves, new[]
                {
                    Vector2Int.up,
                    Vector2Int.down,
                    Vector2Int.left,
                    Vector2Int.right,
                    new Vector2Int(1, 1),
                    new Vector2Int(1, -1),
                    new Vector2Int(-1, 1),
                    new Vector2Int(-1, -1)
                });
                break;

            case CatForm.Rook:
                AddSlidingMoves(moves, new[]
                {
                    Vector2Int.up,
                    Vector2Int.down,
                    Vector2Int.left,
                    Vector2Int.right
                });
                break;

            case CatForm.Bishop:
                AddSlidingMoves(moves, new[]
                {
                    new Vector2Int(1, 1),
                    new Vector2Int(1, -1),
                    new Vector2Int(-1, 1),
                    new Vector2Int(-1, -1)
                });
                break;

            case CatForm.Knight:
                AddKnightMoves(moves);
                break;

            case CatForm.Pawn:
                AddPawnMoves(moves);
                break;
        }

        return moves;
    }

    public override void TakeDamage(int damage)
    {
        if (shieldCharges > 0)
        {
            shieldCharges--;
            Debug.Log("King Shield menahan serangan.");
            return;
        }

        if (armorCharges > 0)
        {
            armorCharges--;
            damage = Mathf.Max(0, damage - 1);

            Debug.Log("Rook Armor mengurangi damage.");
        }

        base.TakeDamage(damage);
    }

    private void ChangeFormWithKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SetForm(CatForm.King);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            SetForm(CatForm.Queen);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            SetForm(CatForm.Rook);

        if (Input.GetKeyDown(KeyCode.Alpha4))
            SetForm(CatForm.Bishop);

        if (Input.GetKeyDown(KeyCode.Alpha5))
            SetForm(CatForm.Knight);

        if (Input.GetKeyDown(KeyCode.Alpha6))
            SetForm(CatForm.Pawn);
    }

    private void SetForm(CatForm newForm)
    {
        currentForm = newForm;

        ApplyFormVisual();

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayTransform();

        Debug.Log($"Hero Cat berubah menjadi {currentForm}.");
    }

    private void ApplyFormVisual()
    {
        if (spriteRenderer == null)
            return;

        switch (currentForm)
        {
            case CatForm.King:
                SetSpriteAndColor(kingSprite, kingColor);
                break;

            case CatForm.Queen:
                SetSpriteAndColor(queenSprite, queenColor);
                break;

            case CatForm.Rook:
                SetSpriteAndColor(rookSprite, rookColor);
                break;

            case CatForm.Bishop:
                SetSpriteAndColor(bishopSprite, bishopColor);
                break;

            case CatForm.Knight:
                SetSpriteAndColor(knightSprite, knightColor);
                break;

            case CatForm.Pawn:
                SetSpriteAndColor(pawnSprite, pawnColor);
                break;
        }
    }

    private void SetSpriteAndColor(Sprite newSprite, Color newColor)
    {
        // Jika belum memiliki sprite khusus,
        // karakter tetap berubah warna sebagai placeholder.
        if (newSprite != null)
            spriteRenderer.sprite = newSprite;

        spriteRenderer.color = newColor;
    }

    private void UseCurrentFormSkill()
    {
        if (EnergyManager.Instance == null)
            return;

        switch (currentForm)
        {
            case CatForm.King:
                if (EnergyManager.Instance.TrySpend(kingSkillCost))
                {
                    shieldCharges = 1;
                    EndTurnAfterSkill("King Shield aktif.");
                }
                break;

            case CatForm.Queen:
                UseDamageSkill(
                    queenSkillCost,
                    3,
                    4.5f,
                    "Queen Laser"
                );
                break;

            case CatForm.Rook:
                if (EnergyManager.Instance.TrySpend(rookSkillCost))
                {
                    armorCharges = 2;
                    EndTurnAfterSkill("Rook Armor aktif.");
                }
                break;

            case CatForm.Bishop:
                UseBishopBlink();
                break;

            case CatForm.Knight:
                UseDamageSkill(
                    knightSkillCost,
                    2,
                    2.2f,
                    "Knight Pounce"
                );
                break;

            case CatForm.Pawn:
                if (EnergyManager.Instance.TrySpend(pawnSkillCost))
                {
                    Heal(2);
                    EndTurnAfterSkill("Pawn Heal digunakan.");
                }
                break;
        }
    }

    private void UseDamageSkill(
        int energyCost,
        int damage,
        float range,
        string skillName)
    {
        BossHealth boss = FindFirstObjectByType<BossHealth>();

        if (boss == null || boss.IsDefeated)
            return;

        float distance = Vector3.Distance(
            transform.position,
            boss.transform.position
        );

        if (distance > range)
        {
            Debug.Log($"{skillName} terlalu jauh dari Boss.");
            return;
        }

        if (!EnergyManager.Instance.TrySpend(energyCost))
            return;

        boss.TakeDamage(damage);
        EndTurnAfterSkill($"{skillName} mengenai Boss.");
    }

    private void UseBishopBlink()
    {
        Vector2Int safePosition = FindNearestEmptyTile();

        if (safePosition == CurrentPosition)
        {
            Debug.Log("Tidak ada tile kosong untuk Bishop Blink.");
            return;
        }

        if (!EnergyManager.Instance.TrySpend(bishopSkillCost))
            return;

        if (TryParryMoveTo(safePosition))
            StartCoroutine(EndTurnAfterBlink());
    }

    private IEnumerator EndTurnAfterBlink()
    {
        yield return new WaitForSeconds(0.25f);

        TurnManager.Instance.EndPlayerTurn();
    }

    private void EndTurnAfterSkill(string message)
    {
        Debug.Log(message);
        TurnManager.Instance.EndPlayerTurn();
    }

    private Vector2Int FindNearestEmptyTile()
    {
        Vector2Int bestPosition = CurrentPosition;
        float closestDistance = float.MaxValue;

        for (int x = 0; x < GridManager.Instance.Width; x++)
        {
            for (int y = 0; y < GridManager.Instance.Height; y++)
            {
                Vector2Int position = new Vector2Int(x, y);
                Tile tile = GridManager.Instance.GetTile(position);

                if (tile == null || tile.IsOccupied)
                    continue;

                float distance = Vector2Int.Distance(
                    CurrentPosition,
                    position
                );

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    bestPosition = position;
                }
            }
        }

        return bestPosition;
    }

    private void AddKingMoves(List<Vector2Int> moves)
    {
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
    }

    private void AddSlidingMoves(
        List<Vector2Int> moves,
        Vector2Int[] directions)
    {
        foreach (Vector2Int direction in directions)
        {
            Vector2Int target = CurrentPosition + direction;

            while (true)
            {
                Tile tile = GridManager.Instance.GetTile(target);

                if (tile == null || tile.IsOccupied)
                    break;

                moves.Add(target);
                target += direction;
            }
        }
    }

    private void AddKnightMoves(List<Vector2Int> moves)
    {
        Vector2Int[] knightMoves =
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

        foreach (Vector2Int move in knightMoves)
        {
            Vector2Int target = CurrentPosition + move;

            if (IsEmptyTile(target))
                moves.Add(target);
        }
    }

    private void AddPawnMoves(List<Vector2Int> moves)
    {
        Vector2Int target = CurrentPosition + Vector2Int.up;

        if (IsEmptyTile(target))
            moves.Add(target);
    }
}
