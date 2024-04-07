using UnityEngine.SceneManagement;
using VDFramework;

public class GameRewind : BetterMonoBehaviour
{
    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
