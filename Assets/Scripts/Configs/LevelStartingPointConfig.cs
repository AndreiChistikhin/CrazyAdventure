using System;
using UnityEngine;

namespace Configs
{
    [Serializable]
    public class LevelStartingPointConfig
    {
        public string LevelName;
        public Vector3 InitialPositionOnLevel;
    }
}