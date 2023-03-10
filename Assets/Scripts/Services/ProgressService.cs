using System;

namespace Services
{
    [Serializable]
    public class ProgressService : IProgressService
    {
        public GameProgress GameProgress { get; set; }
    }
}