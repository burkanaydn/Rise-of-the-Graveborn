using System.Collections;
using UnityEngine;

public class LightWizardDyingScript : MonoBehaviour
{
    [SerializeField] private int destroyTime = 3;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider hitbox;

    [SerializeField] private LightWizard lightWizard;
    [SerializeField] private LightWizardAnimation lightAnimation;
    [SerializeField] private AudioSource audioSource;


    private void Awake()
    {
        if (GameSceneManager.Instance.GetCurrentLevel() >= 7)
        {
            destroyTime = 1;
        }
    }

    public void Dying()
    {
        StartCoroutine(DyingEnum());
    }

    IEnumerator DyingEnum()
    {
        // Beam döngüsünü durdur
        lightWizard.SetDead(true);

        // Ölüm animasyonu oynat
        lightAnimation.PlayDeath();

        audioSource.Stop();

        // Fizik iþlemleri
        rb.freezeRotation = true;
        rb.useGravity = false;

        // Collider'ý kapat
        Destroy(hitbox);

        yield return new WaitForSeconds(destroyTime);

        Destroy(gameObject);
    }
}
