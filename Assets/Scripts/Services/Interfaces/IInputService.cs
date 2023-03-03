using UnityEngine;

namespace Services.Interfaces
{
    public interface IInputService : IService
    {
        public bool IsAttacking { get; }
        public Vector2 Axis { get; }
    }
}