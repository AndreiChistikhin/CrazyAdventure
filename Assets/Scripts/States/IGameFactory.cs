using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public interface IGameFactory : IService
{
    List<IProgressHandler> ProgressHandlers { get; }
    void Warmup();
    void CleanUp();

    UniTask CreatePlayer();
}