using UnityEngine;

namespace Source.Networking
{
    public class NetworkPlayerSpawnPoint : MonoBehaviour
    {
        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;
    }
}