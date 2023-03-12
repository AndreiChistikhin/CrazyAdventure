using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = "Configs/Windows", fileName = "windows")]
    public class WindowConfig : ScriptableObject
    {
        public List<WindowParameters> Windows = new List<WindowParameters>();
    }
}