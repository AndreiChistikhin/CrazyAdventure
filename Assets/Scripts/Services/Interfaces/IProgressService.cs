using Progress;

namespace Services.Interfaces
{
    public interface IProgressService : IService
    {
        public GameProgress GameProgress { get; set; }
    }
}