using GamePlay.Enemy;
using Infrasctructure.Extensions;
using Services.Interfaces;
using UnityEngine;

namespace GamePlay.Loot
{
    public class LootSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyDeath _enemyDeath;
        
        private IGameFactory _factory;
        private int _lootmax;
        private int _lootMin;

        public void Construct(IGameFactory factory)
        {
            _factory = factory;
        }
        
        private void Start()
        {
            _enemyDeath.OnDeath += SpawnLoot;
        }

        public void SetLoot(int min, int max)
        {
            _lootMin = min;
            _lootmax = max;
        }

        private async void SpawnLoot()
        {
            LootPiece loot = await _factory.CreateLoot();
            loot.transform.position = transform.position.AddY(1);

            Loot lootItem = new Loot()
            {
                Value = Random.Range(_lootMin, _lootmax)
            };
            
            loot.Initialize(lootItem);
        }
    }
}