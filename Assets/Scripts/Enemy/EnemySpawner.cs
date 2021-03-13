using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
   [SerializeField] private EnemyFactory enemyFactory;
   [SerializeField] public List<EnemyTypeSO> enemyTypesSOs = new List<EnemyTypeSO>();

   private GameObject[] spawnPointGameObjects;
   private readonly List<Vector3> spawnPointPositions = new List<Vector3>();

   private GameData gameData;

   private float timeBeforeFirstSpawn;
   private float enemySpawnDelay;
   private int maxEnemyCount;

   private int currentEnemyCount = 0;

   private Array enemyTypeEnumValues;

   private void Start()
   {
      Init();
      FillSpawnPointPositionList();
      InvokeRepeating(nameof(SpawnEnemyInRandomSpawnPosition), timeBeforeFirstSpawn, enemySpawnDelay);
   }

   [Inject]
   private void Construct(GameData gameData)
   {
      this.gameData = gameData;
   }

   private void Init()
   {
      timeBeforeFirstSpawn = gameData.timeBeforeFirstSpawn;
      enemySpawnDelay = gameData.enemySpawnDelay;
      maxEnemyCount = gameData.maxEnemyCount;
      
      enemyTypeEnumValues = Enum.GetValues(typeof(EnemyType));
   }

   public void SpawnEnemy(EnemyType enemyType, Vector3 spawnLocation)
   {
      if (currentEnemyCount >= maxEnemyCount)
         return;
      
      GameObject enemyGameObject = enemyFactory.GetEnemyInstance();
      enemyGameObject.transform.position = spawnLocation;
      enemyGameObject.transform.parent = this.gameObject.transform;
      IEnemy enemy = enemyGameObject.GetComponent<IEnemy>();

      enemy.Init(enemyType);
      
      enemy.EnemyHitTheRacket += DecreaseEnemyCount;
      enemy.EnemyHitTheRacket += GameManager.Instance.ChangeScore;
      
      enemy.EnemyKilled += DecreaseEnemyCount;
      enemy.EnemyKilled += GameManager.Instance.ChangeScore;

      enemy.EnemyEscaped += DecreaseEnemyCount;

      currentEnemyCount++;
      Debug.LogError($"Current enemy count: {currentEnemyCount}");
   }

   private void DecreaseEnemyCount(object sender, MyEventArgs args)
   {
      currentEnemyCount--;
      Debug.LogError($"Current enemy count: {currentEnemyCount}");
   }

   public void SpawnEnemyInRandomSpawnPosition()
   {
      Vector3 spawnPosition = GetRandomSpawnPointPosition();
      SpawnEnemy(GetRandomEnemyType(), spawnPosition);
   }
   
   public void SpawnEnemyInRandomLocation()
   {
      Vector3 randomLocation = (Vector3)MyPathUtilities.GetValidRandomNode().position;
      SpawnEnemy(GetRandomEnemyType(), randomLocation);
   }

   private EnemyType GetRandomEnemyType()
   {
      EnemyType randomEnemy = (EnemyType)enemyTypeEnumValues.GetValue(Random.Range(0, enemyTypeEnumValues.Length));
      return randomEnemy;
   }

   private Vector3 GetRandomSpawnPointPosition()
   {
      return spawnPointPositions[Random.Range(0, spawnPointPositions.Count)];
   }

   private void FillSpawnPointPositionList()
   {
      spawnPointGameObjects = GameObject.FindGameObjectsWithTag("SpawnPoint");
      
      foreach (GameObject spawnPointGameObject in spawnPointGameObjects)
      {
         spawnPointPositions.Add(spawnPointGameObject.transform.position);
      }
   }
}
