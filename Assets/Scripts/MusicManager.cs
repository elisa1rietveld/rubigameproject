using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private AudioSource audioSource;

    public AudioMixerGroup musicMixerGroup; 
    public AudioClip startMenuMusic;
    public AudioClip optionsMenuMusic;
    public AudioClip gameplayMusic;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // keeps this object alive across scenes
        }
        else
        {
            Destroy(gameObject); // prevents multiple instances
            return;
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.loop = true; 
        audioSource.outputAudioMixerGroup = musicMixerGroup;

        // Add listener for scene changes
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioClip newClip = null;

        if (scene.name == "StartMenu")
        {
            newClip = startMenuMusic;
        }
        else if (scene.name == "Options")
        {
            newClip = optionsMenuMusic;
        }
        else
        {
            newClip = gameplayMusic;
        }

        // change music only if it's different from the current playing one
        if (audioSource.clip != newClip)
        {
            audioSource.clip = newClip;
            audioSource.Play();
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
