using GamePlay.HUD;
using Progress;
using Services.Interfaces;
using UnityEngine;
using Zenject;

namespace GamePlay.Hero
{
    public class HeroAttack : MonoBehaviour, IProgressHandler
    {
        [SerializeField] private HeroAnimation _heroAnimation;
        [SerializeField] private CharacterController _controller;

        private IInputService _inputService;
        private static int _layerMask;
        private Collider[] _hits = new Collider[3];
        private PlayerProgress _playerProgress;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
            _layerMask = 1 << LayerMask.NameToLayer("Hittable");
        }

        private void Update()
        {
            if (_inputService == null)
                return;

            if (_inputService.IsAttacking)
                _heroAnimation.PlayAttack();
        }

        public void OnAttack()
        {
            for (int i = 0; i < Hit(); i++)
            {
                _hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(_playerProgress.Damage);
            }
        }

        public void LoadProgress(GameProgress gameProgress)
        {
            _playerProgress = gameProgress.PlayerProgress;
        }

        public void SaveProgress(GameProgress gameProgress)
        {
            gameProgress.PlayerProgress = _playerProgress;
        }
    
        private int Hit()
        {
            return Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward, _playerProgress.DamageRadius, _hits, _layerMask);
        }

        private Vector3 StartPoint()
        {
            return new Vector3(transform.position.x, _controller.center.y / 2, transform.position.z);
        }
    }
}
