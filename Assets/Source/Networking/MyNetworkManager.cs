using System;
using Mirror;
using UnityEngine;
using Zenject;

namespace Source.Networking
{
    public class MyNetworkManager : NetworkManager
    {
        public event Action<GameObject> ServerAddedPlayer; 


        private INetworkPlayerFactory _networkPlayerFactory;
        private NetworkPlayerSpawnPoint _spawnPoint;
        
        [Inject]
        public void Construct(INetworkPlayerFactory networkPlayerFactory)
        {
            _networkPlayerFactory = networkPlayerFactory;
            var point = new GameObject("SpawnPoint");
            _spawnPoint = point.AddComponent<NetworkPlayerSpawnPoint>();
        }
        
        
        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            // var player = 
            //     _networkPlayerFactory.Create(_spawnPoint.Position, _spawnPoint.Rotation, _spawnPoint.transform);
            //
            // player.name = $"{playerPrefab.name} [connId={conn.connectionId}]";
            // NetworkServer.AddPlayerForConnection(conn, player);
            // Debug.Log("Server added player");
            // ServerAddedPlayer?.Invoke(player);
            //NetworkClient.RegisterHandler();

            GameObject player = Instantiate(playerPrefab, _spawnPoint.Position, _spawnPoint.Rotation);

            player.name = $"{playerPrefab.name} [connId={conn.connectionId}]";
            NetworkServer.AddPlayerForConnection(conn, player);
        }
        

        public override void OnClientConnect()
        {
            base.OnClientConnect();
            Debug.Log("Client connected");
            
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
