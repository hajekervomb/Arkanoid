public interface IEnemy
{
    int Health { get; set; }
    void TakeDamage(int damage);
    void Die();
}
