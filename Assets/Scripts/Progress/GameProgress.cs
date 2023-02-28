using System;

[Serializable]
public class GameProgress
{
    public PlayerProgress PlayerProgress;
    public WorldProgress WorldProgress;
    public EnemyProgress EnemyProgress;
    public LootProgress LootProgress;

    public GameProgress()
    {
        PlayerProgress = new PlayerProgress();
        WorldProgress = new WorldProgress();
        EnemyProgress = new EnemyProgress();
        LootProgress = new LootProgress();
    }
}