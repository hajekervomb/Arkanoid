using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
   [SerializeField] private GameObject enemyPrefab;
   [SerializeField] private SpriteRenderer backgroundSprite;
   [SerializeField] public Transform defaultTarget;

   private const float XBordersOfUnplayableArea = 120;
   private const float YBordersOfUnplayableArea = 10;

   private Vector3 defaultSpawnPoint = new Vector3(0,0,0);
   private float timeBeforeFirstSpawn = 5;

   private void Start()
   {
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
      GameObject enemyGameObject = Instantiate(enemyPrefab, GetRandomSpawnLocation(), quaternion.identity, this.gameObject.transform);
      IEnemy enemy = enemyGameObject.GetComponent<IEnemy>();
      enemy.Init();
   }

   private Vector3 GetRandomSpawnLocation()
   {
      Vector3 screenBounds = MiscTools.GetScreenBounds();

      float randomXCoord = Random.Range(-screenBounds.x + XBordersOfUnplayableArea, screenBounds.x - XBordersOfUnplayableArea);
      float randomYCoord = Random.Range(-screenBounds.y + YBordersOfUnplayableArea, screenBounds.y - YBordersOfUnplayableArea);
      
      return new Vector3(randomXCoord,randomYCoord, 0);
   }
}
