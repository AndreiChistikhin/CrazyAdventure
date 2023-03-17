using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = "Configs/World", fileName = "WorldConfig")]
    public class LevelStartConfig : ScriptableObject
    {
        public string InitialLevelName;
        public List<LevelStartingPoint> StartingPoints;
    }
}