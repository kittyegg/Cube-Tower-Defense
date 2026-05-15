using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Island");
    }

    public void GitHubLinkClick() =>
        Application.OpenURL("https://github.com/ryzij/Cube-Tower-Defense");

    public void GiteaLinkClick() =>
        Application.OpenURL("https://gitea.aloegames.com/ryzij/Cube-Tower-Defense");
}
