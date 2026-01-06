using System.Collections;
using UnityEngine;

public class GolemAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float attackInterval = 5f;
    [SerializeField] private float attackDuration = 2f;
    [SerializeField] private float animDuration = 1.5f;

    [Header("References")]
    [SerializeField] private ParticleSystem attackFX;
    [SerializeField] private ParticleSystem warningFX;
    [SerializeField] private GolemMovement golemMovement;
    [SerializeField] private GolemAnimation golemAnimation;
    [SerializeField] private AudioSource audioSource;

    [Header("Material References")]
    [SerializeField] private Renderer warningRenderer; // Materyal atanacak renderer
    [SerializeField] private Material warningMat; // Materyal atanacak renderer
    [SerializeField] private float minAlpha = 0.1f;
    [SerializeField] private float maxAlpha = 0.5f;
    [SerializeField] private float blinkSpeed = 5f;


    private bool isWarningActive = false;

    private bool isDead = false;
    private Coroutine attackRoutine;

    void Start()
    {
        attackRoutine = StartCoroutine(AttackLoop());
    }

    void Update()
    {
        if (isWarningActive)
        {
            BlinkWarningEffect();
        }
    }

    private IEnumerator AttackLoop()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(attackInterval);

            if (isDead) yield break;

            // 1. Hareketi durdur
            golemMovement.SetCanMove(false);
            golemAnimation.StopWalk();

            if (warningFX != null)
            {
                warningFX.Play();
                isWarningActive = true;
            }

            // 2. Animasyonu baþlat
            golemAnimation.PlayAttack();

            yield return new WaitForSeconds(animDuration);

            if (warningFX != null)
            {
                warningFX.Clear();
                warningFX.Stop();
                isWarningActive = false;
            }

            // 3. FX baþlat
            if (attackFX != null)
            {
                attackFX.Play();
                audioSource.Play();
            }

            // 4. Saldýrý süresi kadar bekle
            yield return new WaitForSeconds(attackDuration);

            // 5. Hareketi devam ettir
            golemMovement.SetCanMove(true);
            golemAnimation.PlayWalk();
        }
    }

    private void BlinkWarningEffect()
    {
        if (warningMat == null) return;

        float alpha = Mathf.Lerp(minAlpha, maxAlpha, (Mathf.Sin(Time.time * blinkSpeed) + 1f) / 2f);
        Color color = warningMat.color;
        warningMat.color = new Color(color.r, color.g, color.b, alpha);
    }


    public void SetDead(bool dead)
    {
        isDead = dead;

        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
        }

        if (attackFX != null)
        {
            attackFX.Stop();
        }
    }
}
