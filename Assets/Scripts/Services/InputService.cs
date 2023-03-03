using Services.Interfaces;
using UnityEngine;

namespace Services
{
    public class InputService : IInputService
    {
        public bool IsAttacking => _playerInput.Player.Hit.triggered;
        public Vector2 Axis => _playerInput.Player.Move.ReadValue<Vector2>();

        private PlayerControl _playerInput;

        public InputService()
        {
            _playerInput = new PlayerControl();
            _playerInput.Enable();
        }
    }
}