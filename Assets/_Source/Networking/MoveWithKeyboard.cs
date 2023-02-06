using Mirror;
using UnityEngine;

namespace Source.Networking
{
    public class MoveWithKeyboard : NetworkBehaviour
    {
        [SerializeField] private float _speed = 1f;
        
        private void Update()
        {
            if (!isLocalPlayer) return;
            
            var direction = new Vector3(GetHorizontalInput(), GetVerticalInput());

            transform.position += direction * _speed * Time.deltaTime;
        }


        private static float GetVerticalInput() => Input.GetAxis("Vertical");
        private static float GetHorizontalInput() => Input.GetAxis("Horizontal");
    }
}
