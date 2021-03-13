using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameData", menuName = "GameData")]
public class GameData : ScriptableObject
{
    [Header("Enemies")]
    public int maxEnemyCount;
    public float enemySpawnDelay;
    public float timeBeforeFirstSpawn;
}
