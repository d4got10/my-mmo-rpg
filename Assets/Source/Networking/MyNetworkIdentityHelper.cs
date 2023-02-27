using Mirror;

namespace Source.Networking
{
    public class MyNetworkIdentityHelper : NetworkBehaviour
    {
        private void Start()
        {
            if (!isLocalPlayer)
            {
                foreach (var component in GetComponentsInChildren<DisableOnNonLocalPlayer>())
                {
                    component.Disable();
                }
            }
        }
    }
}