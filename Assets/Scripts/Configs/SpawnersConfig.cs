using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = "Configs/Spawners", fileName = "Spawner")]
    public class SpawnersConfig : ScriptableObject
    {
        public List<LevelConfig> LevelConfigs;
    }
}