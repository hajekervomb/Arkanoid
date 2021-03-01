using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
   [SerializeField] private EnemyFactory enemyFactory;
   [SerializeField] public Transform defaultTarget;

   private Array enumValues;

   private Vector3 screenBounds;
   private const float XBordersOfUnplayableArea = 120;
   private const float YBordersOfUnplayableArea = 10;

   private Vector3 defaultSpawnPoint = new Vector3(0,0,0);
   private float timeBeforeFirstSpawn = 5;

   private void Start()
   {
      enumValues = Enum.GetValues(typeof(EnemyType));
      screenBounds = MiscTools.GetScreenBounds();
      Invoke(nameof(SpawnEnemyInRandomLocation), timeBeforeFirstSpawn);
   }

   public void SpawnEnemy(EnemyType type, Vector3 spawnLocation)
   {
      GameObject enemyGameObject = enemyFactory.GetEnemyInstance(type);
      enemyGameObject.transform.position = spawnLocation;
      enemyGameObject.transform.parent = this.gameObject.transform;
         
      IEnemy enemy = enemyGameObject.GetComponent<IEnemy>();
      enemy.Init();
   }

   public void SpawnEnemyInRandomLocation()
   {
      Vector3 randomLocation = GetRandomSpawnLocation();
      SpawnEnemy(GetRandomEnemyType(), randomLocation);
   }

   private Vector3 GetRandomSpawnLocation()
   {
      float randomXCoord = Random.Range(-screenBounds.x + XBordersOfUnplayableArea, screenBounds.x - XBordersOfUnplayableArea);
      float randomYCoord = Random.Range(-screenBounds.y + YBordersOfUnplayableArea, screenBounds.y - YBordersOfUnplayableArea);
      
      return new Vector3(randomXCoord,randomYCoord, 0);
   }

   private EnemyType GetRandomEnemyType()
   {
      EnemyType randomEnemy = (EnemyType)enumValues.GetValue(Random.Range(0, enumValues.Length));
      return randomEnemy;
   }
}
