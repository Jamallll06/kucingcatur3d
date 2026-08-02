using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossAttackPattern
{
    SingleTarget,
    Cross,
    Square,
    LaserRow,
    LaserColumn
}

public class BossAI : MonoBehaviour
{
    [Header("Boss Movement")]
    [SerializeField] private float moveDuration = 0.4f;
    [SerializeField] private float heightAboveTile = 0.8f;

    [Header("Attack")]
    [SerializeField] private float telegraphDuration = 1f;
    [SerializeField] private int damage = 1;

    [Header("Phase")]
    [Range(0f, 1f)]
    [SerializeField] private float phase2Threshold = 0.70f;

    [Range(0f, 1f)]
    [SerializeField] private float phase3Threshold = 0.35f;

    [SerializeField] private float attackDelayBetweenPatterns = 0.2f;

    private BossHealth bossHealth;
    private int previousPhase;

    [Header("Targeting")]
    [Range(0f, 1f)]
    [SerializeField] private float targetAccuracy = 0.65f;

    [SerializeField] private int missRadius = 1;

    [Header("Parry")]
    [SerializeField] private KeyCode parryKey = KeyCode.Space;
    [SerializeField] private float parryWindowDuration = 0.25f;

    private bool isParryWindowOpen;
    private bool parrySucceeded;

    private float damageMultiplier = 1f;
    private float speedMultiplier = 1f;
    private bool rageActivated;
    private Animator animator;

    private void Start()
    {
        bossHealth = GetComponent<BossHealth>();
        previousPhase = GetCurrentPhase();
        animator = GetComponent<Animator>();

        if (animator != null)
        {
            //animator.SetBool("Move", false);
        }
    }

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
        if (bossHealth == null)
            yield break;

        if (bossHealth.IsDefeated)
            yield break;

        int currentPhase = GetCurrentPhase();

        if (currentPhase != previousPhase)
        {
            previousPhase = currentPhase;

            Debug.Log($"Boss masuk Phase {currentPhase}!");

            HUDManager.Instance?.ShowMessage($"PHASE {currentPhase}");
        }

        // Aktifkan Rage lebih dulu jika sudah Phase 3
        if (currentPhase == 3)
        {
            ActivateRage();
        }

        // Deklarasikan attackCount SEBELUM dipakai
        int attackCount = (currentPhase == 3) ? 2 : 1;

        for (int i = 0; i < attackCount; i++)
        {
            yield return ExecuteSingleAttack();

            if (i < attackCount - 1)
            {
                yield return new WaitForSeconds(attackDelayBetweenPatterns);
            }
        }
    }

    private IEnumerator ExecuteSingleAttack()
    {
        ChessPiece targetPiece = FindNearestPiece();
        LookAtTarget(targetPiece);

        if (targetPiece == null)
            yield break;

        Vector2Int targetCenter =
            ChooseTargetPosition(targetPiece.CurrentPosition);

        BossAttackPattern pattern =
            GetPatternForCurrentPhase();

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

            if (EnergyManager.Instance != null)
                EnergyManager.Instance.AddEnergy(1);

            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayParry();
        }
        else
        {
            //animator?.SetTrigger("Attack");

            AudioManager.Instance?.PlayHit();

            DamageCharactersOnTiles(attackTiles);
        }

        GridManager.Instance.ClearHighlights();
    }

    private IEnumerator MoveToAttackPosition(Vector2Int targetPosition)
    {
        Tile targetTile = GridManager.Instance.GetTile(targetPosition);

        if (animator != null)
            //animator.SetBool("Move", true);

        if (targetTile == null)
            yield break;

        Vector3 startPosition = transform.position;
        Vector3 endPosition =
            targetTile.transform.position + Vector3.up * heightAboveTile;

        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime * speedMultiplier;

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

        if (animator != null)
            animator.SetBool("Move", false);

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
        float phaseAccuracy = targetAccuracy +
            (GetCurrentPhase() - 1) * 0.1f;

        if (Random.value <= Mathf.Clamp01(phaseAccuracy))
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

            case BossAttackPattern.LaserRow:
                for (int x = 0; x < GridManager.Instance.Width; x++)
                {
                    AddIfValid(
                        tiles,
                        new Vector2Int(x, center.y)
                    );
                }
                break;

            case BossAttackPattern.LaserColumn:
                for (int y = 0; y < GridManager.Instance.Height; y++)
                {
                    AddIfValid(
                        tiles,
                        new Vector2Int(center.x, y)
                    );
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
                piece.TakeDamage(
                    Mathf.RoundToInt(
                        damage * damageMultiplier
                    )
                );
        }
    }

    private int GetCurrentPhase()
    {
        if (bossHealth == null || bossHealth.MaxHealth <= 0)
            return 1;

        float healthPercent =
            (float)bossHealth.CurrentHealth / bossHealth.MaxHealth;

        if (healthPercent <= phase3Threshold)
            return 3;

        if (healthPercent <= phase2Threshold)
            return 2;

        return 1;
    }

    private BossAttackPattern GetPatternForCurrentPhase()
    {
        int phase = GetCurrentPhase();

        if (phase == 1)
        {
            return Random.value < 0.5f
                ? BossAttackPattern.SingleTarget
                : BossAttackPattern.Cross;
        }

        if (phase == 2)
        {
            int randomPattern = Random.Range(1, 4);

            return (BossAttackPattern)randomPattern;
        }

        int phase3Pattern = Random.Range(1, 5);

        return (BossAttackPattern)phase3Pattern;
    }

    public void SetAccuracy(float accuracy)
    {
        targetAccuracy = Mathf.Clamp01(accuracy);
    }

    public void ApplyLevelData(LevelData data)
    {
        if (data == null)
            return;

        damage = data.laserDamage;

        targetAccuracy = data.accuracy;

        telegraphDuration = data.laserWarningTime;
    }

    private void ActivateRage()
    {
        if (rageActivated)
            return;

        rageActivated = true;

        damageMultiplier = 2f;
        speedMultiplier = 1.4f;

        HUDManager.Instance?.ShowMessage("ENRAGED");
    }

    public IEnumerator BossTurn()
    {
        yield return ExecuteTurn();
    }

    private void LookAtTarget(ChessPiece target)
    {
        if (target == null)
            return;

        Vector3 dir =
            target.transform.position -
            transform.position;

        dir.y = 0;

        if (dir.sqrMagnitude > 0.01f)
        {
            transform.rotation =
                Quaternion.LookRotation(dir);
        }
    }

    public void ApplyRage(
    float damage,
    float speed)
    {
        damageMultiplier = damage;
        speedMultiplier = speed;

        rageActivated = true;

        HUDManager.Instance?.ShowMessage("ENRAGED");
    }
}