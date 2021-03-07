using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
   [SerializeField] private EnemyFactory enemyFactory;

   [SerializeField] private List<GameObject> spawnPointGameObjects = new List<GameObject>();
   private readonly List<Vector3> spawnPointPositions = new List<Vector3>();

   [SerializeField] private float timeBeforeFirstSpawn = 5;
   [SerializeField] private float enemySpawnDelay = 30;

   private Array enemyTypeEnumValues;

   private void Start()
   {
      enemyTypeEnumValues = Enum.GetValues(typeof(EnemyType));
      FillSpawnPointPositionList();
      InvokeRepeating(nameof(SpawnEnemyInRandomSpawnPosition), timeBeforeFirstSpawn, enemySpawnDelay);
   }

   public void SpawnEnemy(EnemyType type, Vector3 spawnLocation)
   {
      GameObject enemyGameObject = enemyFactory.GetEnemyInstance(type);
      enemyGameObject.transform.position = spawnLocation;
      enemyGameObject.transform.parent = this.gameObject.transform;
         
      IEnemy enemy = enemyGameObject.GetComponent<IEnemy>();
      enemy.Init();
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
      foreach (GameObject spawnPointGameObject in spawnPointGameObjects)
      {
         spawnPointPositions.Add(spawnPointGameObject.transform.position);
      }
   }
}
