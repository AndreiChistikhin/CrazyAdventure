namespace Services
{
    public interface IProgressService : IService
    {
        public GameProgress GameProgress { get; set; }
    }
}