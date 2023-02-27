using UnityEngine;

namespace Source.Networking
{
    public interface INetworkPlayerFactory
    {
        GameObject Create(Transform parent);
        GameObject Create(Vector3 position, Quaternion rotation, Transform parent);
    }
}