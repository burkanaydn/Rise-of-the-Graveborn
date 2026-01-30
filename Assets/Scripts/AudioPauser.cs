using UnityEngine;
public class AudioPauser : MonoBehaviour
{
    private AudioSource audioSource;
    private bool wasPaused = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Time.timeScale == 0f && audioSource.isPlaying)
        {
            audioSource.Pause();
            wasPaused = true;
        }
        else if (Time.timeScale > 0f && wasPaused)
        {
            audioSource.UnPause();
            wasPaused = false;
        }
    }
}
