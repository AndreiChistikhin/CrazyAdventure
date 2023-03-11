using UnityEditor;
using UnityEngine;

namespace Infrasctructure.Tools
{
    public static class Tools
    {
        [MenuItem("Tools/ClearPrefs")]
        public static void ClearPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}