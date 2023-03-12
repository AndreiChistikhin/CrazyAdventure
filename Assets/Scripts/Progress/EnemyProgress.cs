using System;
using System.Collections.Generic;

namespace Progress
{
    [Serializable]
    public class EnemyProgress
    {
        public List<string> ClearedSpawners = new List<string>();

        public event Action EnemyWasKilled;
    
        public void AddKilledEnemy(string enemyID)
        {
            ClearedSpawners.Add(enemyID);
            EnemyWasKilled?.Invoke();
        }
    }
}