using UnityEngine;
using Zenject;

public class LevelSceneInstaller : MonoInstaller
{
    [SerializeField] private GameData gameDataSO;
    [SerializeField] private GameObject pathfindingManagerPrefab;
    [SerializeField] private GameObject enemySpawnerPrefab;
    
    public override void InstallBindings()
    {
        Container.Bind<GameData>().FromInstance(gameDataSO).AsSingle().NonLazy();
        Container.Bind<AstarPath>().FromComponentInNewPrefab(pathfindingManagerPrefab).AsSingle().NonLazy();
        Container.Bind<EnemySpawner>().FromComponentInNewPrefab(enemySpawnerPrefab).AsSingle().NonLazy();
    }
}