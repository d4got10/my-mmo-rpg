using UnityEngine;

namespace Source.Networking
{
    public class DisableOnNonLocalPlayer : MonoBehaviour
    {
        public void Disable() => gameObject.SetActive(false);
    }
}
