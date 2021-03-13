using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    public GameObject GetEnemyInstance()
    {
        GameObject enemyInstance = Instantiate(enemyPrefab);
        return enemyInstance;
    }
}
