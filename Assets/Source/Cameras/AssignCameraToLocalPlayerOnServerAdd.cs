using Cinemachine;
using Mirror;
using UnityEngine;

namespace Cameras
{
    public class AssignCameraToLocalPlayerOnServerAdd : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCameraBase _camera;
        
        
        public void OnServerAddedPlayer(GameObject player)
        {
            if (!IsLocalPlayer(player)) return;

            _camera.LookAt = player.transform;
            _camera.Follow = player.transform;
        }

        private bool IsLocalPlayer(GameObject player)
        {
            return player.TryGetComponent<NetworkBehaviour>(out var behaviour) 
                   && behaviour.isLocalPlayer;
        }
    }
}