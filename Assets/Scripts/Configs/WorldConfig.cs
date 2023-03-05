using UnityEngine;

namespace GamePlay.Configs
{
    [CreateAssetMenu(menuName = "Configs/World", fileName = "WorldConfig")]
    
    public class WorldConfig : ScriptableObject
    {
        public string InitialLevelName;
        public Vector3 InitialPosition;
    }
}