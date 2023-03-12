using System;
using Progress;
using Services.Interfaces;

namespace Services
{
    [Serializable]
    public class ProgressService : IProgressService
    {
        public GameProgress GameProgress { get; set; }
    }
}