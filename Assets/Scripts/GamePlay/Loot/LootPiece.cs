using System.Collections;
using Progress;
using TMPro;
using UnityEngine;

namespace GamePlay.Loot
{
    public class LootPiece : MonoBehaviour
    {
        [SerializeField] private GameObject _lootObject;
        [SerializeField] private TMP_Text _lootText;
        [SerializeField] private GameObject _pickUpPopUp;

        private Loot _loot;
        private bool _picked;
        private GameProgress _gameProgress;

        public void Construct(GameProgress gameProgress)
        {
            _gameProgress = gameProgress;
        }
        
        public void Initialize(Loot loot) => _loot = loot;

        private void OnTriggerEnter(Collider other) => PickUp();

        private void PickUp()
        {
            if(_picked)
                return;
            
            _picked = true;

            UpdateWorldData();
            HideSkull();
            ShowText();
            StartCoroutine(StartDestroyTimer());
        }

        private void UpdateWorldData() => _gameProgress.LootProgress.Collect(_loot);

        private void HideSkull() => _lootObject.SetActive(false);

        private void ShowText()
        {
            _lootText.text = $"{_loot.Value}";
            _pickUpPopUp.SetActive(true);
        }

        private IEnumerator StartDestroyTimer()
        {
            yield return new WaitForSeconds(1.5f);
            
            Destroy(gameObject);
        }
    }
}