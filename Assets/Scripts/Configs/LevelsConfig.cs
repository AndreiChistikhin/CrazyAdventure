using System;
using System.Collections.Generic;

namespace Configs
{
    [Serializable]
    public class LevelConfig
    {
        public string SceneName;
        public List<EnemySpawner> EnemySpawner;
    }
}