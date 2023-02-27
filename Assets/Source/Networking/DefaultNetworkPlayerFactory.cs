using UnityEngine;
using Zenject;

namespace Source.Networking
{
    public class DefaultNetworkPlayerFactory : INetworkPlayerFactory
    {
        public DefaultNetworkPlayerFactory(IInstantiator instantiator, GameObject playerPrefab)
        {
            _instantiator = instantiator;
            _playerPrefab = playerPrefab;
        }

        private readonly IInstantiator _instantiator;
        private readonly GameObject _playerPrefab;

        public GameObject Create(Transform parent) => _instantiator.InstantiatePrefab(_playerPrefab, parent);

        public GameObject Create(Vector3 position, Quaternion rotation, Transform parent) => 
            _instantiator.InstantiatePrefab(_playerPrefab, position, rotation, parent);
    }
}