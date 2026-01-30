using System.Collections;
using UnityEngine;

public class LightWizard : MonoBehaviour
{
    [Header("Attack Timing")]
    [SerializeField] private float attackInterval = 4f;
    [SerializeField] public float beamDelay = 5f;          // Efekt baþladýktan sonra kaç saniye sonra cast baþlasýn
    [SerializeField] public float warningBeamDelay = 1f;          // Efekt baþladýktan sonra kaç saniye sonra cast baþlasýn
    [SerializeField] private float castDuration = 10f;       // Cast animasyon süresi (ve beam açýk kalma süresi)
    [SerializeField] public float lookSpeed = 20f; // Derece/saniye

    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform warningBeamTransform;
    [SerializeField] private LightWizardBeam lightBeam;
    [SerializeField] private LightWizardWarningBeam warningBeam;
    [SerializeField] private LightWizardAnimation lightAnimation;
    [SerializeField] private AudioSource audioSource;

    private bool isDead = false;
    //private bool isBeaming = false;

    private Coroutine attackRoutine;

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        attackRoutine = StartCoroutine(AttackLoop());
    }

    IEnumerator AttackLoop()
    {
        while (!isDead)
        {
            // 1. IDLE bekleme
            lightAnimation.PlayIdle();
            //isBeaming = false;
            yield return new WaitForSeconds(attackInterval);

            if (isDead) yield break;


            // 2. Efekti erken baþlat (ýþýn animasyonundan önce çalýþmaya baþlasýn)
            lightBeam.EnableBeam();
            //isBeaming = true;

            audioSource.Play();

            yield return new WaitForSeconds(warningBeamDelay);
            warningBeam.EnableWarning();

            // 3. Iþýn animasyonuna geçmeden önce bekle (ýþýnýn "hazýrlýk süresi")
            yield return new WaitForSeconds(beamDelay);
            if (isDead) yield break;

            MatchRotationWithWarningBeam();
            // 4. CAST animasyonu baþlasýn
            lightAnimation.PlayCast();

            warningBeam.DisableWarning();

            float castElapsed = 0f;
            while (castElapsed < castDuration && !isDead)
            {
                SmoothLookAtPlayer();
                castElapsed += Time.deltaTime;
                yield return null;
            }

            // 5. Iþýn efektini kapat
            lightBeam.DisableBeam();
            lightAnimation.StopCast();
            audioSource.Stop();
        }
    }

    private void SmoothLookAtPlayer()
    {
        if (player == null) return;

        Vector3 direction = player.position - transform.position;
        direction.y = 0f;
        if (direction == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lookSpeed * Time.deltaTime);
    }

    public void MatchRotationWithWarningBeam()
    {
        transform.rotation = warningBeamTransform.rotation;
    }

    public void SetDead(bool dead)
    {
        isDead = dead;

        if (attackRoutine != null)
            StopCoroutine(attackRoutine);

        lightBeam.DisableBeam();
        warningBeam.DisableWarning();
    }
}
