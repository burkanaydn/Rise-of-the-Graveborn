using System.Collections;
using UnityEngine;

public class LightWizardWarningBeam : MonoBehaviour
{
    [SerializeField] private ParticleSystem warningFX;

    [Header("Warning Alpha Oscillation")]
    [SerializeField] private Renderer warningRenderer;
    [SerializeField] private float minAlpha;
    [SerializeField] private float maxAlpha;
    [SerializeField] private float blinkSpeed;

    [SerializeField] private LightWizard lightWizard;

    private Transform player;
    private float lookSpeed;
    private float beamDelay;

    private Material warningMat;

    private Coroutine warningCoroutine;

    void Start()
    {
        warningMat = warningRenderer.material;

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        lookSpeed = lightWizard.lookSpeed;
        beamDelay = lightWizard.beamDelay;
    }

    void Update()
    {
        float alpha = Mathf.Lerp(minAlpha, maxAlpha, (Mathf.Sin(Time.time * blinkSpeed) + 1f) / 2f);
        Color col = warningMat.GetColor("_Color");
        warningMat.SetColor("_Color", new Color(col.r, col.g, col.b, alpha));
    }

    public void EnableWarning()
    {
        if (warningCoroutine != null)
            StopCoroutine(warningCoroutine);

        warningCoroutine = StartCoroutine(RotateDuringWarning());
    }

    public void DisableWarning()
    {
        if (warningCoroutine != null)
        {
            StopCoroutine(warningCoroutine);
            warningCoroutine = null;
        }

        if (warningFX != null)
        {
            warningFX.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        if (warningMat != null)
        {
            Color c = warningMat.color;
            warningMat.color = new Color(c.r, c.g, c.b, minAlpha);
        }
    }

    private IEnumerator RotateDuringWarning()
    {
        if (warningFX != null)
            warningFX.Play();

        float castElapsed = 0f;

        while (castElapsed < beamDelay)
        {
            SmoothLookAtPlayer();
            castElapsed += Time.deltaTime;
            yield return null;
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
}
