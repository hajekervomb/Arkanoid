using System;
using UnityEngine;

public enum EnemyType
{
   Fast = 0,
   Average = 1,
   Slow = 2,
}

public abstract class Enemy : MonoBehaviour, IEnemy
{
   private EnemySpawner enemySpawner;
   private MyAIPath myAIPath;
   private DestinationSwitcher destinationSwitcher;
   public int Health { get; set; }

   // TODO - объединить ивенты и методы (добавив параметр?)
   public event Action EnemyDied; // врага убили
   public event Action EnemyDestroyed; // враг дошел до точки назначения и уничтожился

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
         DestroyEnemy();
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
      OnEnemyDied();
      Destroy(gameObject);
   }

   private void OnEnemyDied()
   {
      EnemyDied?.Invoke();
   }

   public void DestroyEnemy()
   {
      OnEnemyDestroyed();
      Destroy(gameObject);
   }

   private void OnEnemyDestroyed()
   {
      EnemyDestroyed?.Invoke();
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
