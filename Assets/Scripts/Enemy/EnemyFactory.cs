using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    public GameObject GetEnemyInstance()
    {
        GameObject prefab = Instantiate(enemyPrefab);
        return prefab;
    }
}
