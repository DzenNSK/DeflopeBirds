using Dzen.DeflopeBirds;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField] private ObstacleController obstaclePrefab;

    public override void InstallBindings()
    {
        Container.BindMemoryPool<ObstacleController, ObstacleController.Pool>()
            .FromComponentInNewPrefab(obstaclePrefab)
            .UnderTransformGroup("Obstacles");

        Container.Bind<UserInputService>().FromNewComponentOnRoot().AsSingle().NonLazy();
        Container.Bind<GameController>().FromNewComponentOnRoot().AsSingle().NonLazy();
        Container.Bind<SessionStatsController>().FromNewComponentOnRoot().AsSingle().NonLazy();
        Container.Bind<UserDataService>().FromNewComponentOnRoot() .AsSingle().NonLazy();
    }
}