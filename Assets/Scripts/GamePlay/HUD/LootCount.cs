using System;
using Progress;
using TMPro;
using UnityEngine;

namespace GamePlay.HUD
{
    public class LootCount : MonoBehaviour, IProgressHandler
    {
        [SerializeField] private TMP_Text _text;
        
        private GameProgress _gameProgress;

        public void Construct(GameProgress gameProgress)
        {
            _gameProgress = gameProgress;
            _gameProgress.LootProgress.Changed += UpdateCounter;
            UpdateCounter();
        }

        public void LoadProgress(GameProgress progress)
        {
            _text.text = progress.LootProgress.LootCount.ToString();
        }

        public void SaveProgress(GameProgress progress)
        {
            progress.LootProgress.LootCount = Int32.Parse(_text.text);
        }

        private void UpdateCounter()
        {
            _text.text = $"{_gameProgress.LootProgress.LootCount}";
        }
    }
}
