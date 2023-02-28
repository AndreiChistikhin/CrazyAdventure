using System;

[Serializable]
public class WorldProgress
{
    public string CurrentSceneName;
    public Vector3Data PositionOnScene;

    public WorldProgress()
    {
        PositionOnScene = new Vector3Data();
    }
}