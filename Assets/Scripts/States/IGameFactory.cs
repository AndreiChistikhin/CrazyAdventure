using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IGameFactory : IService
{
    List<IProgressHandler> ProgressHandlers { get; }
    void Warmup();
    void CleanUp();

    UniTask<GameObject> CreatePlayer();
}