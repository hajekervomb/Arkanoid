using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
   public int Health { get; set; } = 100;
   
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
}
