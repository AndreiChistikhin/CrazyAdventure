﻿using UnityEngine;

namespace GamePlay.Hero
{
    public class HeroAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterController _characterController;

        private static readonly int Movement = Animator.StringToHash("Movement");
        private static readonly int IsAttackingTrigger = Animator.StringToHash("IsAttacking");
        private static readonly int Died = Animator.StringToHash("Died");
        private static readonly int GetHit = Animator.StringToHash("GetHit");
        

        private void Update()
        {
            _animator.SetFloat(Movement, _characterController.velocity.magnitude, 0.1f, Time.deltaTime);
        }

        public void PlayAttack()
        {
            _animator.SetTrigger(IsAttackingTrigger);
        }

        public void PlayDied()
        {
            _animator.SetTrigger(Died);
        }

        public void PlayGetHit()
        {
            _animator.SetTrigger(GetHit);
        }
    }
}