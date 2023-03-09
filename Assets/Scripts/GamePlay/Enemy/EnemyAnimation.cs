using UnityEngine;

namespace GamePlay.Enemy
{
    public class EnemyAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private static readonly int DiedTrigger = Animator.StringToHash("Died");
        private static readonly int SpeedFloat = Animator.StringToHash("Movement");
        private static readonly int HitTrigger = Animator.StringToHash("GetHit");
        private static readonly int AttackTrigger = Animator.StringToHash("IsAttacking");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");


        private void Start()
        {
            Move(5);
        }

        public void Move(float speed)
        {
            _animator.SetBool(IsMoving, true);
            _animator.SetFloat(SpeedFloat, speed);
        }

        public void PlayDeath() => _animator.SetTrigger(DiedTrigger);
        
        public void PlayHit() => _animator.SetTrigger(HitTrigger);
        
        public void StopMoving() => _animator.SetBool(IsMoving, false);
        
        public void PlayAttack() => _animator.SetTrigger(AttackTrigger);
    }
}