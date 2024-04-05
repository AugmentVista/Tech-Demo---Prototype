using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public UIManager uimanager;
    public AudioClip backgroundMusic;
    public AudioClip actionMusic;

    private AudioSource audioSource;
    private float speedOverTime; 
    private float underSpeedTime;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (backgroundMusic == null || actionMusic == null)
        {
            Debug.LogError("Music isn't working. Missing file: " + (backgroundMusic == null ? "backgroundMusic" : "actionMusic"));
            return;
        }
        PlayBackgroundMusic();
    }

    private void FixedUpdate()
    {
        if (uimanager.speed >= 18)
        {
            speedOverTime += Time.fixedDeltaTime;
            underSpeedTime = 0f;
            if (speedOverTime >= 0.5f && audioSource.clip != actionMusic)
            {
                PlayActionMusic();
            }
        }
        else if (underSpeedTime >= 0.5f && uimanager.speed < 5 && audioSource.clip != backgroundMusic)
        {
            speedOverTime = 0f;
            underSpeedTime += Time.fixedDeltaTime;
            if (audioSource.clip != backgroundMusic)
            {
                PlayBackgroundMusic();
            }
        }
    }
    private void PlayBackgroundMusic()
    {
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.Play();
    }
    private void PlayActionMusic()
    {
        audioSource.clip = actionMusic;
        audioSource.loop = true;
        audioSource.Play();
    }
}