using UnityEditor;
using UnityEngine;

namespace Infrasctructure.Tools
{
    public class Tools
    {
        [MenuItem("Tools/ClearPrefs")]
        public static void ClearPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}