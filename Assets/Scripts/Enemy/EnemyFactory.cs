using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    public GameObject GetEnemyInstance(EnemyType type)
    {
        GameObject prefab = Instantiate(enemyPrefab);

        switch (type)
        {
            case EnemyType.Default:
                prefab.AddComponent<DefaultEnemy>();
                break;
            case EnemyType.ToughOne:
                prefab.AddComponent<ToughOneEnemy>();
                break;
            default:
                Debug.Log("Wrong enemy type");
                break;
        }

        return prefab;
    }
}
