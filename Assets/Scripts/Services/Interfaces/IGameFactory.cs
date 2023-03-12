using System.Collections.Generic;
using Configs;
using Cysharp.Threading.Tasks;
using GamePlay.Loot;
using Progress;
using UnityEngine;

namespace Services.Interfaces
{
    public interface IGameFactory : IService
    {
        List<IProgressHandler> ProgressHandlers { get; }
        void Warmup();
        void CleanUp();

        UniTask<GameObject> CreatePlayer();
        UniTask<GameObject> CreateHUD();
        UniTask<LootPiece> CreateLoot();
        UniTask CreateEnemy(EnemySpawner enemySpawner, string enemySpawnerEnemyId);
    }
}