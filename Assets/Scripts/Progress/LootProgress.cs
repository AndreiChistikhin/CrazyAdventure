using System;
using GamePlay.Loot;

namespace Progress
{
    [Serializable]
    public class LootProgress
    {
        public int LootCount;

        public event Action Changed;

        public void Collect(Loot loot)
        {
            LootCount += loot.Value;
            Changed?.Invoke();
        }
    }
}