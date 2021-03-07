using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    public GameObject GetEnemyInstance(EnemyType type)
    {
        GameObject prefab = Instantiate(enemyPrefab);

        switch (type)
        {
            case EnemyType.Fast:
                prefab.AddComponent<FastEnemy>();
                break;
            case EnemyType.Average:
                prefab.AddComponent<AverageEnemy>();
                break;
            case EnemyType.Slow:
                prefab.AddComponent<SlowEnemy>();
                break;

            default:
                Debug.Log("Wrong enemy type");
                break;
        }

        return prefab;
    }
}
