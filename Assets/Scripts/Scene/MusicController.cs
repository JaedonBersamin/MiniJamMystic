using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField]
    private AudioClip mainMenuMusic;
    [SerializeField]
    private AudioClip backgroundMusic;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMainMenuMusic()
    {
        if (audioSource.clip != mainMenuMusic)
        {
            audioSource.clip = mainMenuMusic;
            audioSource.volume = 1.0f;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
    public void PlayBackgroundMusic()
    {
        if (audioSource.clip != backgroundMusic)
        {
            audioSource.clip = backgroundMusic;
            audioSource.volume = 1.0f;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}
