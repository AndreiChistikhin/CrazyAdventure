using UnityEngine.SceneManagement;

public class LoadProgressState : IState
{
    public void Enter()
    {
        SceneManager.LoadScene("FirstLevelScene");
    }
}