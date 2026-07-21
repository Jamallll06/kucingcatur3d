using UnityEngine;


[CreateAssetMenu(
    fileName = "LevelData",
    menuName = "ChessCat/Level Data"
)]
public class LevelData : ScriptableObject
{

    [Header("Level Info")]
    public string levelName;

    public int levelNumber;


    [Header("Scene")]
    public string sceneName;



    [Header("Boss Settings")]
    public string bossName;

    public int bossHP;


    [Header("Boss Ability")]
    public bool useBarrier;
    public bool useSummon;
    public bool useRage;


    [Header("Grid")]
    public int gridWidth = 8;
    public int gridHeight = 8;

}