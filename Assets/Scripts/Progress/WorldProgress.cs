using System;

[Serializable]
public class WorldProgress
{
    private const string FirstLevelName = "FirstLevelScene";

    public string SceneToLoadName;
    public Vector3Data PositionOnScene;

    public WorldProgress()
    {
        PositionOnScene = new Vector3Data();
        SceneToLoadName = FirstLevelName;
    }
}