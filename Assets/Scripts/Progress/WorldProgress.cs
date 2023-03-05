using System;

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