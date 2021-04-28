using UnityEngine;

public class ButtonEvents : MonoBehaviour
{
    public void PauseButton()
    {
        Time.timeScale = 0.0f;
    }

    public void ResumeButton()
    {
        Time.timeScale = 1.0f;
    }

    public void setActiveTrue(GameObject @object)
    {
        @object.SetActive(true);
    }

    public void setActiveFalse(GameObject @object)
    {
        @object.SetActive(false);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
