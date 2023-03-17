using System;
using UnityEngine;

namespace Configs
{
    [Serializable]
    public class LevelStartingPoint
    {
        public string LevelName;
        public Vector3 InitialPositionOnLevel;
    }
}