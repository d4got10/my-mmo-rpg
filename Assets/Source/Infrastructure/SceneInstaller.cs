using Source.Networking;
using UnityEngine;
using Zenject;

namespace Source.Infrastructure
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private GameObject _networkPlayerPrefab;
        [SerializeField] private NetworkPlayerSpawnPoint _networkPlayerSpawnPoint;

        public override void InstallBindings()
        {
            InstallCamera();
            InstallNetworkPlayerFactory();
            InstallNetworkPlayerSpawnPoint();
        }

        private void InstallCamera()
        {
            Container
                .Bind<Camera>()
                .ToSelf()
                .FromInstance(_camera);
        }

        private void InstallNetworkPlayerSpawnPoint()
        {
            Container
                .Bind<NetworkPlayerSpawnPoint>()
                .ToSelf()
                .FromInstance(_networkPlayerSpawnPoint);
        }

        private void InstallNetworkPlayerFactory()
        {
            Container
                .Bind<INetworkPlayerFactory>()
                .To<DefaultNetworkPlayerFactory>()
                .AsSingle()
                .WithArguments(_networkPlayerPrefab)
                .NonLazy();
        }
    }
}