using System;
using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
   [SerializeField] private EnemyFactory enemyFactory;

   private GridGraph activeGrid;
   private Array enumValues;
   private Vector3 defaultSpawnPoint = new Vector3(0,0,0);
   private float timeBeforeFirstSpawn = 5;

   private void Start()
   {
      activeGrid = AstarPath.active.data.gridGraph;
      enumValues = Enum.GetValues(typeof(EnemyType));
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
      GridNode randomNode;
      
      while (true)
      {
         randomNode = activeGrid.nodes[Random.Range(0, activeGrid.nodes.Length)];

         if (randomNode.Walkable)
            break;
      }

      Debug.LogError($"Is nod traversable: {randomNode.Walkable}");
      return (Vector3) randomNode.position;
   }

   private EnemyType GetRandomEnemyType()
   {
      EnemyType randomEnemy = (EnemyType)enumValues.GetValue(Random.Range(0, enumValues.Length));
      return randomEnemy;
   }
}
