using UnityEngine;

[CreateAssetMenu(
    fileName = "LevelData",
    menuName = "Game/Level Data"
)]
public class LevelData : ScriptableObject
{
    [Header("Level")]
    public string levelName;
    public int levelNumber;

    [Header("Grid")]
    public int gridWidth = 8;
    public int gridHeight = 8;

    [Header("Boss")]
    public string bossName;
    public int bossHP = 20;

    [Range(0f, 1f)]
    public float accuracy = 0.65f;

    [Header("Boss Ability")]
    public bool useBarrier;
    public bool useLaser;
    public bool useSummon;
    public bool useRage;

    [Range(0f, 1f)]
    public float abilityHealthPercent = 0.5f;

    public int abilityCooldown = 2;

    [Header("Barrier")]
    public int barrierCount = 3;
    public int barrierDuration = 3;

    [Header("Laser")]
    public float laserWarningTime = 1f;
    public int laserDamage = 1;

    [Header("Summon")]
    public int summonCount = 2;
    public int maxMinion = 4;

}