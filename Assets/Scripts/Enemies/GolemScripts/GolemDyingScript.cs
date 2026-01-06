using System.Collections;
using UnityEngine;

public class GolemDyingScript : MonoBehaviour
{
    [SerializeField] private int destroyTime = 3;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider hitbox;

    [SerializeField] private GolemMovement golemMovement;
    [SerializeField] private GolemAttack golemAttack;
    [SerializeField] private GolemAnimation golemAnimation;

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
        golemMovement.SetDead(true);

        // Beam efektini kapat
        golemAttack.SetDead(true);

        // Ölüm animasyonu oynat
        golemAnimation.PlayDeath();

        // Fizik iþlemleri
        rb.freezeRotation = true;
        rb.useGravity = false;

        // Collider'ý kapat
        Destroy(hitbox);

        yield return new WaitForSeconds(destroyTime);

        Destroy(gameObject);
    }
}
