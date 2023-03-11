using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Windows", fileName = "windows")]
public class WindowConfig : ScriptableObject
{
    public List<WindowParameters> Windows = new List<WindowParameters>();
}

[Serializable]
public class WindowParameters
{
    public WindowId WindowId;
    public GameObject WindowPrefab;
}

public enum WindowId
{
    Unknown,
    LoseGame,
    WinGame
}
