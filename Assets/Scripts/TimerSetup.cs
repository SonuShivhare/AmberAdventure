using UnityEngine;
using UnityEngine.UI;

public class TimerSetup : MonoBehaviour
{
    [SerializeField] private Text scoreText;

    private void Update()
    {
        float timer = Time.time;
        float seconds = timer % 60;
        float minute = timer / 60;
        scoreText.text = minute.ToString("0") + ":" + seconds.ToString("0");
    }
}
