using Configs;
using Cysharp.Threading.Tasks;
using GamePlay.Hero;
using Infrasctructure;
using Infrasctructure.Extensions;
using Infrasctructure.Requests;
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
        private IServerRequester _serverRequester;
        private ITimeChecker _timeChecker;
        private bool _triggered;

        [Inject]
        private void Construct(IGameStateMachine gameStateMachine, IProgressService progressService,
            IConfigService configService, IServerRequester serverRequester,ITimeChecker timeChecker)
        {
            _timeChecker = timeChecker;
            _serverRequester = serverRequester;
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
            
            _triggered = true;

            SendLevelTimeToDb();

            await LoadNewLevel();
        }

        private void SendLevelTimeToDb()
        {
            int time = _timeChecker.GetTime();
            _timeChecker.StopTimer();
            _serverRequester.Post<string>(ServerAPI.TimeAPi, new TimeRequest {Time = time});
        }

        private async UniTask LoadNewLevel()
        {
            _progressService.GameProgress.WorldProgress.SceneToLoadName = _transfferTo;
            LevelStartingPoint levelStartConfig = await _configService.ForLevel(_transfferTo);
            _progressService.GameProgress.WorldProgress.PositionOnScene =
                levelStartConfig.InitialPositionOnLevel.ToSerializedVector();
            _gameStateMachine.Enter<LoadLevelState>();
        }
    }
}