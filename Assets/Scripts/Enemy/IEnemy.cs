using System;

public interface IEnemy
{
    event EventHandler<MyEventArgs> EnemyKilled;
    event EventHandler<MyEventArgs> EnemyHitTheRacket;
    event EventHandler<MyEventArgs> EnemyEscaped;
    
    int Health { get; set; }
    int Speed { get; set; }
    void Init(EnemyType enemyType);
    void TakeDamage(int damage);
    void Die();
    void DestroyEnemy();
}
