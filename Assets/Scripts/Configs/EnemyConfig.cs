using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = "Configs/Spawners", fileName = "Spawner")]
    public class EnemyConfig : ScriptableObject
    {
        public int EnemyInitialHealth;
        public float EnemyDamage;
        public int MinimumLoot;
        public int MaximumLoot;
        public List<EnemyPositions> EnemyPositions;
    }
}