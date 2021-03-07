using System;

public interface IEnemy
{
    event Action EnemyDied;
    event Action EnemyDestroyed;
    
    int Health { get; set; }
    void Init();
    void TakeDamage(int damage);
    void Die();
    void DestroyEnemy();
}
