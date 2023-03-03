using Services.Interfaces;
using UnityEngine;
using Zenject;

namespace Hero
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _movementSpeed;

        private IInputService _inputService;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Update()
        {
            if (_inputService == null)
                return;
            
            if (_inputService.Axis.magnitude <= Constants.Epsilon)
                return;

            Vector3 movementDirection = _inputService.Axis;
            movementDirection.Normalize();
            transform.forward = movementDirection;

            movementDirection += Physics.gravity;
            _characterController.Move(movementDirection * Time.deltaTime * _movementSpeed);
        }
    }

    public static class Constants
    {
        public const float Epsilon = 0.001f;
    }
}