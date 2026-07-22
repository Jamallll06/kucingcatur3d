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



    [Header("Boss Ability")]
    public bool useBarrier;
    public bool useLaser;
    public bool useSummon;
    public bool useRage;



    [Header("Barrier")]
    public int barrierCount = 3;
    public int barrierDuration = 3;



    [Header("Boss AI")]
    [Range(0, 1)]
    public float accuracy = 0.65f;

}