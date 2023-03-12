using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = "Configs/World", fileName = "WorldConfig")]
    public class WorldConfig : ScriptableObject
    {
        public string InitialLevelName;
        public List<LevelStartingPointConfig> StartingPoints;
    }
}