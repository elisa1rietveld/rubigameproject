using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSettingsManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer; // Assign your Audio Mixer in the Inspector
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private const string MUSIC_VOLUME_PARAM = "MusicVolume";
    private const string SFX_VOLUME_PARAM = "SFXVolume";

    private void Start()
    {
        // Load saved volume settings (if any)
        if (PlayerPrefs.HasKey(MUSIC_VOLUME_PARAM))
        {
            float savedMusicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME_PARAM);
            musicSlider.value = savedMusicVolume;
            SetMusicVolume(savedMusicVolume);
        }

        if (PlayerPrefs.HasKey(SFX_VOLUME_PARAM))
        {
            float savedSfxVolume = PlayerPrefs.GetFloat(SFX_VOLUME_PARAM);
            sfxSlider.value = savedSfxVolume;
            SetSFXVolume(savedSfxVolume);
        }

        // Add listeners for real-time updates
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat(MUSIC_VOLUME_PARAM, volume);
        PlayerPrefs.SetFloat(MUSIC_VOLUME_PARAM, volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat(SFX_VOLUME_PARAM, volume);
        PlayerPrefs.SetFloat(SFX_VOLUME_PARAM, volume);
    }

    private void OnDestroy()
    {
        // Save volume settings when the object is destroyed
        PlayerPrefs.Save();
    }
}
