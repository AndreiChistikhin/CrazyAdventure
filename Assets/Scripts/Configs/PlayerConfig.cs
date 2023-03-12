using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = "Configs/Player", fileName = "PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        public float MaxHealth;
        public float Damage;
    }
}