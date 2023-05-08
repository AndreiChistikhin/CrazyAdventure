using Cysharp.Threading.Tasks;
using Services.Interfaces;

namespace Services
{
    public interface ITimeChecker : IService
    {
        UniTask StartTimer();
        int GetTime();
        void StopTimer();
    }
}