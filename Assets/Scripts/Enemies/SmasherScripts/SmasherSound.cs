using UnityEngine;

public class SmasherSound : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource audioSource;

    [Header("Clips")]
    [SerializeField] private AudioClip warningClip;
    [SerializeField] private AudioClip impactClip;
    [SerializeField] private AudioClip explosionClip;

    public void PlayWarningSFX()
    {
        PlaySound(warningClip);
    }

    public void PlayImpactSFX()
    {
        PlaySound(impactClip);
    }
    public void PlayExplosionSFX()
    {
        PlaySound(explosionClip);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip == null || audioSource == null) return;
        audioSource.PlayOneShot(clip);
    }
}
