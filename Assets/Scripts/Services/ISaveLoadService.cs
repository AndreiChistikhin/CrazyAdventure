namespace Services
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        GameProgress LoadProgress();
    }
}