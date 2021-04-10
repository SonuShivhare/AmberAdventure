using UnityEngine;
using UnityEngine.UI;

public class ButtonEvents : MonoBehaviour
{
    public GameObject gamePanel;
    public GameObject pausePanel;

    public void PauseButton()
    {
        Time.timeScale = 0.0f;
        gamePanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void ResumeButton()
    {
        Time.timeScale = 1.0f;
        gamePanel.SetActive(true);
        pausePanel.SetActive(false);
    }
}
