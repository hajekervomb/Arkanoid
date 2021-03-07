using System;
using UnityEngine;

public enum EnemyType
{
   Default = 0,
   ToughOne = 1,
}

public abstract class Enemy : MonoBehaviour, IEnemy
{
   private EnemySpawner enemySpawner;
   private MyAIPath myAIPath;
   private DestinationSwitcher destinationSwitcher;
   public int Health { get; set; }

   private void OnEnable()
   {
      BlocksManager.Instance.BlockDestroyed += RecalculatePath;

      // Т.к. блоки инстанируются в рантайме (а не сразу лежат на сцене), нужно сканить при спавне
      RecalculatePath();
   }

   // TODO - убрать. Сейчас нужно только для тестов
   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.K))
      {
         Die();
      }
   }

   public virtual void Init()
   {
      enemySpawner = GetComponentInParent<EnemySpawner>();
      myAIPath =  GetComponentInParent<MyAIPath>();
      myAIPath.TargetReachedEvent += CheckMainDestinationPoint;
      destinationSwitcher =  GetComponentInParent<DestinationSwitcher>();
   }

   // Проверка дошел ли враг именно до своего основного пункта назначения
   private void CheckMainDestinationPoint()
   {
      if (destinationSwitcher.IsEnemyReachedMainDestinationPoint())
      {
         Die();
      }
   }

   public void TakeDamage(int damage)
   {
      Health -= damage;

      if (Health <= 0)
      {
         Die();
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
