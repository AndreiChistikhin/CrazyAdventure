using System;
using System.Collections.Generic;

namespace Configs
{
    [Serializable]
    public class EnemyPositions
    {
        public string SceneName;
        public List<EnemySpawner> EnemySpawner;
    }
}