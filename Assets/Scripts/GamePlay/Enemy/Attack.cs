using System.Linq;
using GamePlay.HUD;
using UnityEngine;

namespace GamePlay.Enemy
{
    public class Attack : MonoBehaviour
    {
        [SerializeField] private EnemyAnimation _animator;
        [SerializeField] private float _attackCoolDown = 3f;
        [SerializeField] private float _cleavage = 0.5f;
        [SerializeField] private float _effectiveDistance = 0.5f;
        [SerializeField] private float _damage;

        private Transform _heroTransform;
        private float _currentAttackCoolDown;
        private bool _isAttacking;
        private int _layerMask;
        private Collider[] _hits = new Collider[1];
        private bool _attackRangeIsReached;

        public void Construct(Transform heroTransform, float damage)
        {
            _heroTransform = heroTransform;
            _damage = damage;
        }
        
        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer("Player");
        }

        private void Update()
        {
            UpdateCoolDown();

            if (!_isAttacking && CoolDownIsUp() && _attackRangeIsReached)
            {
                StartAttack();
            }
        }

        private void OnAttack()
        {
            if (Hit(out Collider hit))
            {
                hit.transform.GetComponent<IHealth>().TakeDamage(_damage);
            }
        }

        private void OnAttackEnded()
        {
            _currentAttackCoolDown = _attackCoolDown;
            _isAttacking = false;
        }

        public void EnableAttack()
        {
            _attackRangeIsReached = true;
        }

        public void DisableAttack()
        {
            _attackRangeIsReached = false;
        }

        private void UpdateCoolDown()
        {
            if (_currentAttackCoolDown > 0)
                _currentAttackCoolDown -= Time.deltaTime;
        }

        private bool CoolDownIsUp()
        {
            return _currentAttackCoolDown <= 0;
        }

        private void StartAttack()
        {
            transform.LookAt(_heroTransform);
            _animator.PlayAttack();

            _isAttacking = true;
        }

        private bool Hit(out Collider hit)
        {
            int hitsCount = Physics.OverlapSphereNonAlloc(StartPoint(), _cleavage, _hits, _layerMask);

            hit = _hits.FirstOrDefault();

            return hitsCount > 0;
        }

        private Vector3 StartPoint()
        {
            return new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) +
                   transform.forward * _effectiveDistance;
        }
    }
}