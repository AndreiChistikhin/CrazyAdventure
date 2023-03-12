using System;

namespace Progress
{
    [Serializable]
    public class WorldProgress
    {
        public string SceneToLoadName;
        public Vector3Data PositionOnScene;

        public WorldProgress()
        {
            PositionOnScene = new Vector3Data ();
        }
    }
}