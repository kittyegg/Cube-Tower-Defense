using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("Island");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
