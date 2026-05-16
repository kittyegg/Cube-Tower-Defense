using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private Tower _tower;
    [SerializeField] private Transform _menuPanel;

    private void OnEnable()
    {
        _tower.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _tower.OnGameOver -= OnGameOver;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Island");
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    private void OnGameOver()
    {
        Time.timeScale = 0f;
        _menuPanel.gameObject.SetActive(true);
    }
}
