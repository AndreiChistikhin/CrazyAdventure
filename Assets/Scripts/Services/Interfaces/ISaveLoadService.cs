using Progress;

namespace Services.Interfaces
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        GameProgress LoadProgress();
    }
}