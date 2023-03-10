using System.Collections.Generic;
using System.Threading.Tasks;
using Configs;
using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IGameFactory : IService
{
    List<IProgressHandler> ProgressHandlers { get; }
    void Warmup();
    void CleanUp();

    UniTask<GameObject> CreatePlayer();
    UniTask<GameObject> CreateHUD();

    UniTask CreateEnemy(EnemySpawner spawner);
    UniTask<LootPiece> CreateLoot();
}