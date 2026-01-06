using System.Collections;
using UnityEngine;

public class SmasherMovement : MonoBehaviour
{
    [Header("Timing Settings")]
    [SerializeField] private float waitTime = 2f;
    [SerializeField] private float jumpPrepDelay = 0.5f; // Zýplamadan önceki sabit duruþ süresi
    [SerializeField] private float jumpDuration = 0.6f;  // Zýplama süresi (biraz daha yavaþ)
    [SerializeField] private float indicatorDuration = 2f;

    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private SmasherAnimation smasherAnimation;
    [SerializeField] private SmasherEffects smasherEffects;
    [SerializeField] private SmasherSound smasherSound;
    [SerializeField] private Collider hitbox;

    //private bool isJumping = false;
    private Vector3 targetPosition;
    private bool isDead = false;
    private Coroutine jumpCoroutine;
    private GameObject currentIndicator;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        jumpCoroutine = StartCoroutine(JumpRoutine());

    }

    IEnumerator JumpRoutine()
    {
        while (true)
        {
            // 1. Bekleme süresi
            yield return new WaitForSeconds(waitTime);

            // 2. Hedef pozisyon al
            targetPosition = player.position;
            targetPosition.y = transform.position.y;

            // 3. Smasher hedefe dönsün
            Vector3 lookDir = targetPosition - transform.position;
            lookDir.y = 0f;
            if (lookDir != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(lookDir);

            // 4. Uyarý efekti oluþtur
            currentIndicator = smasherEffects.SpawnIndicator(targetPosition);


            // 5. Bekle (uyarý efekti süresi boyunca)
            yield return new WaitForSeconds(indicatorDuration);

            // 6. Animasyonu baþlat (hedefe saldýrý pozisyonu)
            smasherAnimation.PlayJump();

            // 7. Zýplama hazýrlýk animasyonu süresi (0.5s sabit duruþ)
            yield return new WaitForSeconds(jumpPrepDelay);

            // 8. Ýþareti yok et
            if (currentIndicator != null)
            {
                Destroy(currentIndicator);
                currentIndicator = null;
            }


            // 9. Zýplama (yatay hareket)
            //isJumping = true;
            hitbox.enabled = false;
            Vector3 start = transform.position;
            float elapsed = 0f;

            while (elapsed < jumpDuration)
            {
                transform.position = Vector3.Lerp(start, targetPosition, elapsed / jumpDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            // Zemin çatlama efekti oluþtur
            smasherEffects.SpawnImpactEffect(targetPosition, 2f);
            smasherSound.PlayImpactSFX();

            transform.position = targetPosition;
            hitbox.enabled = true;
            //isJumping = false;

            smasherAnimation.PlayIdle(); // Zýplama sonrasý idle’a dön
        }
    }

    public void SetDead(bool dead)
    {
        isDead = dead;

        if (jumpCoroutine != null)
        {
            StopCoroutine(jumpCoroutine);
            jumpCoroutine = null;
        }

        if (currentIndicator != null)
        {
            Destroy(currentIndicator);
            currentIndicator = null;
        }
    }

}
