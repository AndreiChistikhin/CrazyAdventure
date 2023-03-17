using System;
using GamePlay.Hero;
using UnityEngine;
using UnityEngine.AI;

namespace GamePlay.Enemy
{
    public class AnimateAgent : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private EnemyAnimation _animator;

        private const float MinimalVelocity = 0.1f;
        private float _cachedVelocity;
        
        private void Update()
        {
            if (!VelocityHasChanged(_agent.velocity.magnitude))
                return;
            
            if (ShouldShowMovement())
                _animator.Move(_agent.velocity.magnitude);
            else
                _animator.StopMoving();
        }

        private bool VelocityHasChanged(float newVelocity)
        {
            if (Math.Abs(_cachedVelocity - newVelocity) < Constants.Epsilon)
                return false;

            _cachedVelocity = newVelocity;
            return true;
        }
        
        private bool ShouldShowMovement()
        {
            return _agent.velocity.magnitude > MinimalVelocity && _agent.remainingDistance > _agent.radius;
        }
    }
}
