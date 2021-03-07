public interface IEnemy
{
    int Health { get; set; }
    void Init();
    void TakeDamage(int damage);
    void Die();
}
