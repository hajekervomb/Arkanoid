using System;

public interface IEnemy
{
    event Action EnemyDied;
    event Action EnemyDestroyed;
    
    int Health { get; set; }
    int Speed { get; set; }
    void Init(EnemyType enemyType);
    void TakeDamage(int damage);
    void Die();
    void DestroyEnemy();
}
