using Infrasctructure.Extensions;
using UnityEngine;

namespace Services
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string SaveInfoKey = "SaveStats";
        private readonly IProgressService _progressService;
        private readonly IGameFactory _gameFactory;

        public SaveLoadService(IProgressService progressService, IGameFactory gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
        }

        public void SaveProgress()
        {
            foreach (IProgressHandler progressHandler in _gameFactory.ProgressHandlers)
            {
                progressHandler.SaveProgress(_progressService.GameProgress);
            }

            PlayerPrefs.SetString(SaveInfoKey, _progressService.GameProgress.Serialize());
        }

        public GameProgress LoadProgress()
        {
            return PlayerPrefs.GetString(SaveInfoKey).Deserialize<GameProgress>();
        }
    }
}