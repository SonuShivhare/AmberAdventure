using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEvents : MonoBehaviour
{
    public GameObject gamePanel;
    public GameObject pausePanel;
    public Text text;

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

    private void Update()
    {
        float timer = Time.time;
        float seconds = timer % 60;
        float minute = timer / 60;
        text.text = minute.ToString("0") + ":" + seconds.ToString("0") ;
    }
}
