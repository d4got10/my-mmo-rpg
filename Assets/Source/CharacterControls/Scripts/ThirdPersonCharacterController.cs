using System;
using Mirror;
using UnityEngine;
using Zenject;

namespace Source.CharacterControls
{
    public class ThirdPersonCharacterController : NetworkBehaviour
    {
        [SerializeField] private float _speed = 1f;
        [SerializeField] private Vector3 _groundCheckPosition = new(0, 0.5f, 0);
        [SerializeField] private float _groundCheckRadius = 1f;
        [SerializeField] private float _slopeCheckDistance = 0.5f;
        [SerializeField] private float _jumpVelocity = 2f;
        [SerializeField] private Transform _model;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private LayerMask _groundMask;

        private Vector3 ProjectedCameraForward => Vector3.ProjectOnPlane(_camera.forward, Vector3.up).normalized;
        private Vector3 ProjectedCameraRight => Vector3.ProjectOnPlane(_camera.right, Vector3.up).normalized;
        
        private Transform _camera;
        private Vector3 _velocity;
        
        [Inject]
        public void Construct(Camera camera)
        {
            _camera = camera.transform;
        }
        
        private void Update()
        {
            if (!isLocalPlayer) return;

            RotateModelTowardsCameraDirection();
            
            if (IsGrounded())
            {
                StopDownwardMovement();
                var movementInput = GetMovementInput();
                var worldDirection = ConvertInputToWorldDirection(movementInput);

                var isJumping = GetJumpInput();
                MoveRelatively(worldDirection);

                _velocity = AdjustVelocityToGroundSlope(_velocity);

                if (isJumping) 
                    ApplyJumpToVelocity();
            }
            else
            {
                ApplyGravity();
            }

            UpdatePosition();
        }

        private void ApplyJumpToVelocity()
        {
            if (_velocity.y < _jumpVelocity)
                _velocity.y = _jumpVelocity;
        }

        private void StopDownwardMovement()
        {
            if(_velocity.y < 0)
                _velocity.y = 0;
        }

        private void UpdatePosition()
        {
            _characterController.Move(_velocity * Time.deltaTime);
        }

        private void ApplyGravity()
        {
            _velocity += Physics.gravity * Time.deltaTime;
        }

        private void RotateModelTowardsCameraDirection()
        {
            var targetRotation = Quaternion.LookRotation(ProjectedCameraForward, Vector3.up);
            _model.rotation = targetRotation;
        }

        private void MoveRelatively(Vector3 direction)
        {
            var velocity = direction * _speed;
            velocity.y = _velocity.y;
            _velocity = velocity;
        }

        private Vector3 AdjustVelocityToGroundSlope(Vector3 velocity)
        {
            var ray = new Ray(transform.position, Vector3.down);

            if (Physics.Raycast(ray, out var hit, _slopeCheckDistance, _groundMask))
            {
                var slopeRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                var adjustedVelocity = slopeRotation * velocity;

                if (adjustedVelocity.y < 0)
                    return adjustedVelocity;
            }

            return velocity;
        }


        private bool IsGrounded() => Physics.CheckSphere(transform.position + _groundCheckPosition, _groundCheckRadius, _groundMask);

        private Vector3 ConvertInputToWorldDirection(Vector2 input) => ProjectedCameraForward * input.y + ProjectedCameraRight * input.x;

        private static Vector2 GetMovementInput()
        {
            var input = new Vector2(GetHorizontalInput(), GetVerticalInput());
            input = Vector2.ClampMagnitude(input, 1);
            return input;
        }



        private static bool GetJumpInput() => Input.GetKey(KeyCode.Space);
        private static float GetVerticalInput() => Input.GetAxis("Vertical");
        private static float GetHorizontalInput() => Input.GetAxis("Horizontal");
    }
}
