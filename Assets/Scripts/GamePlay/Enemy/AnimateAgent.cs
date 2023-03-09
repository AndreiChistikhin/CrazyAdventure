using GamePlay.Enemy;
using UnityEngine;
using UnityEngine.AI;

public class AnimateAgent : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private EnemyAnimation _animator;

    private const float MinimalVelocity = 0.1f;

    private void Update()
    {
        if (ShouldMove())
            _animator.Move(_agent.velocity.magnitude);
        else
            _animator.StopMoving();
    }

    private bool ShouldMove()
    {
        return _agent.velocity.magnitude > MinimalVelocity && _agent.remainingDistance > _agent.radius;
    }
}
