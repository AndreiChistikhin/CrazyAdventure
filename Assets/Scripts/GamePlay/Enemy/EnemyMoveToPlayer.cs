using UnityEngine;
using UnityEngine.AI;

namespace GamePlay.Enemy
{
    public class EnemyMoveToPlayer : Follow
    {
        [SerializeField] private NavMeshAgent _agent;

        private Transform _heroTransform;

        private const float MinimalDistance = 1;

        public void Construct(Transform heroTransform)
        {
            _heroTransform = heroTransform;
        }
        
        private void Update()
        {
            if (_heroTransform != null && HeroNotReached())
                _agent.destination = _heroTransform.position;
        }
        
        private bool HeroNotReached() =>
            Vector3.Distance(_agent.transform.position, _heroTransform.position) >= MinimalDistance;
    }
}