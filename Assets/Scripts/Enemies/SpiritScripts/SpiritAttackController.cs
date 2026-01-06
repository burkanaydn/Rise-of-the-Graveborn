using System.Collections;
using UnityEngine;

public class SpiritAttackController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float triggerDistance = 7f;

    [Header("Charge & Dash")]
    [SerializeField] private float chargeTime = 3f;
    [SerializeField] private float dashDistance = 12f;
    [SerializeField] private float dashDuration = 0.5f;
    [SerializeField] private float postDashWaitTime = 2f;

    [Header("Components")]
    [SerializeField] private SpiritMovement spiritMovement;
    [SerializeField] private SpiritAnimation spiritAnimation;
    [SerializeField] private SpiritSound spiritSound;
    [SerializeField] private GameObject dashIndicatorPrefab;

    private SpiritState currentState = SpiritState.Idle;
    private GameObject currentIndicator;
    private bool isDead = false;
    private Vector3 dashTargetPosition;


    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        spiritMovement.SetState(SpiritState.Running);
        spiritAnimation.SetState(SpiritState.Running);
        currentState = SpiritState.Running;
    }

    void Update()
    {
        if (isDead || currentState != SpiritState.Running) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= triggerDistance)
        {
            StartCoroutine(ChargeAndDashSequence());
        }
    }

    private IEnumerator ChargeAndDashSequence()
    {
        
        dashTargetPosition = player.position;

        currentState = SpiritState.Charging;
        spiritMovement.SetState(SpiritState.Idle);
        spiritAnimation.SetState(SpiritState.Charging);
        spiritSound.PlayChargeSFX();

        // Player'a dön
        Vector3 dir = dashTargetPosition - transform.position;
        dir.y = 0f;
        transform.rotation = Quaternion.LookRotation(dir);

        // Ok göstergesi
        Vector3 spawnPos = transform.position + Vector3.up * 0.5f;
        currentIndicator = Instantiate(dashIndicatorPrefab, spawnPos, Quaternion.LookRotation(dir));

        var ps = currentIndicator.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            ps.Simulate(5f, true, true);
            ps.Play();
        }

        yield return new WaitForSeconds(chargeTime);

        if (currentIndicator != null)
        {
            Destroy(currentIndicator);
            currentIndicator = null;
        }

        currentState = SpiritState.Dashing;
        spiritAnimation.SetState(SpiritState.Dashing);
        spiritSound.PlayDashSFX();

        Vector3 start = transform.position;
        Vector3 dashDir = (dashTargetPosition - start).normalized;
        Vector3 end = start + dashDir * dashDistance;

        float elapsed = 0f;
        while (elapsed < dashDuration)
        {
            transform.position = Vector3.Lerp(start, end, elapsed / dashDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = end;

        yield return new WaitForSeconds(postDashWaitTime);

        currentState = SpiritState.Running;
        spiritMovement.SetState(SpiritState.Running);
        spiritAnimation.SetState(SpiritState.Running);
    }

    public void SetDead(bool dead)
    {
        isDead = dead;
        StopAllCoroutines();
        currentState = SpiritState.Dead;

        spiritMovement.SetState(SpiritState.Dead);
        spiritAnimation.SetState(SpiritState.Dead);

        if (currentIndicator != null)
        {
            Destroy(currentIndicator);
            currentIndicator = null;
        }
    }
}
