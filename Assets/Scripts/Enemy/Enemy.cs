using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public enum EnemyType
{
   Default = 0,
   ToughOne = 1,
}

public abstract class Enemy : MonoBehaviour, IEnemy
{
   private EnemySpawner enemySpawner;
   public int Health { get; set; }

   private void OnEnable()
   {
      BlocksManager.Instance.BlockDestroyed += RecalculatePath;
   }

   private void Start()
   {
      Physics2D.IgnoreLayerCollision((int)Layer.Player, (int)Layer.Enemy);
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.K))
      {
         Die();
         enemySpawner.SpawnEnemyInRandomLocation();
      }
   }

   public virtual void Init()
   {
      enemySpawner = GetComponentInParent<EnemySpawner>();
   }

   public void TakeDamage(int damage)
   {
      Health -= damage;

      if (Health <= 0)
      {
         Die();
         enemySpawner.SpawnEnemyInRandomLocation();
      }
   }

   public void Die()
   {
      Destroy(gameObject);
   }
   
   private void RecalculatePath()
   {
      AstarPath.active.Scan();
   }
   
   private void OnDestroy()
   {
      BlocksManager.Instance.BlockDestroyed -= RecalculatePath;
   }
   
}
