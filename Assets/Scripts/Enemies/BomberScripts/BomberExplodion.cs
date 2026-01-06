using UnityEngine;

public class BomberExplosion : MonoBehaviour
{
    [Header("Explosion Settings")]
    [SerializeField] private float explosionRadius = 2f;
    [SerializeField] private LayerMask playerLayer;

    [Header("Components")]
    [SerializeField] private ParticleSystem explosionEffect;
    [SerializeField] private AudioSource explosionSound;
    [SerializeField] private BomberDyingScript bomberDyingScript;
    [SerializeField] private Transform player;

    private bool hasExploded = false;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        if (hasExploded || player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= explosionRadius)
        {
            Explode();
            bomberDyingScript.Dying();
        }
    }

    public void Explode()
    {
        hasExploded = true;

        if (explosionEffect != null)
        {
            explosionEffect.transform.parent = null;
            explosionEffect.Play();
        }

        if (explosionSound != null)
        {
            explosionSound.Play();
        }

        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius, playerLayer);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                GameOverManager.Instance.GameisOver();
                break;
            }
        }
    }


    // Patlama alanýný sahnede göster
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0.3f, 0f, 0.5f);
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
