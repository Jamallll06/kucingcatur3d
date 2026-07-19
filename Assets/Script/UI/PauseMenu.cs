using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    private bool isPaused;

    private void Start()
    {
        pausePanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        Time.timeScale = isPaused ? 0f : 1f;

        pausePanel.SetActive(isPaused);
    }

    public void ResumeGame()
    {
        if (!isPaused)
            return;

        TogglePause();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        GameManager.Instance.RestartGame();
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;

        GameManager.Instance.QuitGame();
    }
}