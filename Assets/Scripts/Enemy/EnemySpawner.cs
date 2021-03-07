using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
   [SerializeField] private EnemyFactory enemyFactory;

   private Array enemyTypeEnumValues;
   private float timeBeforeFirstSpawn = 5;

   private void Start()
   {
      enemyTypeEnumValues = Enum.GetValues(typeof(EnemyType));
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
      Vector3 randomLocation = (Vector3)MyPathUtilities.GetValidRandomNode().position;
      SpawnEnemy(GetRandomEnemyType(), randomLocation);
   }

   private EnemyType GetRandomEnemyType()
   {
      EnemyType randomEnemy = (EnemyType)enemyTypeEnumValues.GetValue(Random.Range(0, enemyTypeEnumValues.Length));
      return randomEnemy;
   }
}
