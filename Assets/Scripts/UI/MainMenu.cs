using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Island");
    }

    public void SourceCodeLinkClick() =>
        Application.OpenURL("https://github.com/ryzij/Cube-Tower-Defense");
}
