using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private List<AudioSource> audioSources;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Toggle muteToggle;


    private void Awake()
    {
        ResetVolume();
        ResetMute();
        ResetVolumeSlider();
        ResetMuteToggle();
    }

    public void Volume(float value)
    {
        PlayerPrefs.SetFloat("Volume", value);
        ResetVolume();
    }

    public void MuteVolume(bool state)
    {
        if (state) PlayerPrefs.SetString("Mute", "true");
        else PlayerPrefs.SetString("Mute", "false");
        ResetMute();
    }

    private void ResetVolume()
    {
        foreach (var audio in audioSources)
        {
            audio.volume = PlayerPrefs.GetFloat("Volume");
        }
    }

    private void ResetMute()
    {
        foreach (var audio in audioSources)
        {
            audio.mute = (PlayerPrefs.GetString("Mute") == "true");
        }
    }

    private void ResetVolumeSlider()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
    }

    private void ResetMuteToggle()
    {
        muteToggle.isOn = (PlayerPrefs.GetString("Mute") == "true");
    }

}
