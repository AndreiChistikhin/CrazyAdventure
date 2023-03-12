using System.Linq;
using Configs;
using GamePlay.Hero;
using Infrasctructure.Extensions;
using Services;
using Services.Interfaces;
using States;
using UnityEngine;
using Zenject;

namespace GamePlay
{
    public class LevelTransferTrigger : MonoBehaviour
    {
        [SerializeField] private string _transfferTo;

        private IGameStateMachine _gameStateMachine;
        private IProgressService _progressService;
        private IConfigService _configService;
        private bool _triggered;

        [Inject]
        private void Construct(IGameStateMachine gameStateMachine, IProgressService progressService,
            IConfigService configService)
        {
            _configService = configService;
            _progressService = progressService;
            _gameStateMachine = gameStateMachine;
        }

        private async void OnTriggerEnter(Collider other)
        {
            if (_triggered)
                return;

            if (!other.gameObject.TryGetComponent(out PlayerMovement player))
                return;

            _progressService.GameProgress.WorldProgress.SceneToLoadName = _transfferTo;
            WorldConfig worldConfig = await _configService.ForWorld();
            _progressService.GameProgress.WorldProgress.PositionOnScene = worldConfig.StartingPoints
                .FirstOrDefault(x => x.LevelName == _transfferTo)
                ?.InitialPositionOnLevel.ToSerializedVector();
            _gameStateMachine.Enter<LoadLevelState>();
            _triggered = true;
        }
    }
}