using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
   [SerializeField] private GameObject enemyPrefab;
   [SerializeField] public Transform defaultTarget;

   private Vector3 screenBounds;
   private const float XBordersOfUnplayableArea = 120;
   private const float YBordersOfUnplayableArea = 10;

   private Vector3 defaultSpawnPoint = new Vector3(0,0,0);
   private float timeBeforeFirstSpawn = 5;

   private void Start()
   {
      screenBounds = MiscTools.GetScreenBounds();
      Invoke(nameof(SpawnEnemyInRandomLocation), timeBeforeFirstSpawn);
   }

   public void SpawnEnemy(Vector3 spawnLocation)
   {
      GameObject enemyGameObject = Instantiate(enemyPrefab, spawnLocation, quaternion.identity, this.gameObject.transform);
      IEnemy enemy = enemyGameObject.GetComponent<IEnemy>();
      enemy.Init();
   }

   public void SpawnEnemyInRandomLocation()
   {
      Vector3 randomLocation = GetRandomSpawnLocation();
      SpawnEnemy(randomLocation);
   }

   private Vector3 GetRandomSpawnLocation()
   {
      float randomXCoord = Random.Range(-screenBounds.x + XBordersOfUnplayableArea, screenBounds.x - XBordersOfUnplayableArea);
      float randomYCoord = Random.Range(-screenBounds.y + YBordersOfUnplayableArea, screenBounds.y - YBordersOfUnplayableArea);
      
      return new Vector3(randomXCoord,randomYCoord, 0);
   }
}
