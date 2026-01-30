using UnityEngine;

public class SpiritSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [Header("SFX Clips")]
    [SerializeField] private AudioClip chargeSFX;
    [SerializeField] private AudioClip dashSFX;

    public void PlayChargeSFX()
    {
        PlaySFX(chargeSFX);
    }

    public void PlayDashSFX()
    {
        PlaySFX(dashSFX);
    }

    private void PlaySFX(AudioClip clip)
    {
        if (clip == null || audioSource == null) return;

        audioSource.PlayOneShot(clip);
    }
}
