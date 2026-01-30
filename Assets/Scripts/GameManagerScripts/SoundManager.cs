using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource audioSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip coinCollectClip;
    [SerializeField] private AudioClip succesfulMarketClip;
    [SerializeField] private AudioClip bulletHitClip;
    [SerializeField] private AudioClip levelUpClip;
    [SerializeField] private AudioClip gameOverClip;
    [SerializeField] private AudioClip fireballOutClip;
    [SerializeField] private AudioClip maxlevelchainClip;

    [Header("Settings")]
    [SerializeField] private Vector2 coinPitchRange = new Vector2(0.8f, 1f);
    [SerializeField] private Vector2 bulletHitPitchRange = new Vector2(0.8f, 1.2f);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayCoinCollectSound()
    {
        if (audioSource == null || coinCollectClip == null)
            return;

        audioSource.pitch = Random.Range(coinPitchRange.x, coinPitchRange.y);
        audioSource.PlayOneShot(coinCollectClip);
        audioSource.pitch = 1f;
    }

    public void PlaySuccesfulMarket()
    {
        if (audioSource == null||succesfulMarketClip == null)
            return;

        audioSource.PlayOneShot(succesfulMarketClip);
    }

    public void PlayBulletHit()
    {
        if (audioSource == null || bulletHitClip == null)
            return;

        audioSource.pitch = Random.Range(bulletHitPitchRange.x, bulletHitPitchRange.y);
        audioSource.PlayOneShot(bulletHitClip);
        audioSource.pitch = 1f;
    }

    public void PlayLevelUp()
    {
        if (audioSource == null || levelUpClip == null)
            return;

        audioSource.PlayOneShot(levelUpClip);
    }
    public void PlayGameOver()
    {
        if (audioSource == null || gameOverClip == null)
            return;

        audioSource.PlayOneShot(gameOverClip);
    }
    public void PlayFireballOver()
    {
        if (audioSource == null || fireballOutClip == null)
            return;

        audioSource.PlayOneShot(fireballOutClip);
    }
    public void PlayMaxlevelChain()
    {
        if (audioSource == null || maxlevelchainClip == null)
            return;

        audioSource.PlayOneShot(maxlevelchainClip);
    }
}
