using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint; // Merminin çýkýþ noktasý
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float fireRate = 5f;
    [SerializeField] private float bulletLifetime = 3f;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private LayerMask aimLayerMask; // Sadece hedef alýnacak layer
    [SerializeField] private float minDistanceToShoot = 1f;

    [Header("Cursor Settings")]
    [SerializeField] private Texture2D blueCursor;      // Mavi cursor
    [SerializeField] private Texture2D redCursor;    // Kýrmýzý cursor
    [SerializeField] private Vector2 cursorHotspot = Vector2.zero;
    private enum CursorState { Red, Blue }
    private CursorState currentCursorState = CursorState.Blue;


    private float nextFireTime = 0f;

    private void Update()
    {
        CheckCursor();
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }


    void Shoot()
    {
        if (LevelCompleteManager.Instance.levelIsOver)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 shootDirection = Vector3.zero;

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, aimLayerMask))
        {
            Vector3 targetPoint = hit.point;

            float distance = Vector3.Distance(firePoint.position, targetPoint);
            if (distance < minDistanceToShoot)
                return;

            shootDirection = (targetPoint - firePoint.position).normalized;
        }
        else
        {
            shootDirection = firePoint.forward;
        }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(shootDirection));
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.velocity = shootDirection * bulletSpeed;

        Destroy(bullet, bulletLifetime);

        if (audioSource && shootSound)
        {
            audioSource.PlayOneShot(shootSound);
        }

        if (playerAnimator != null)
        {
            playerAnimator.Play("Shoot", -1, 0f);
        }
    }

    void CheckCursor()
    {
        if (LevelCompleteManager.Instance.levelIsOver)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, aimLayerMask))
        {
            float distance = Vector3.Distance(firePoint.position, hit.point);
            if (distance < minDistanceToShoot)
            {
                if (currentCursorState != CursorState.Red)
                {
                    Cursor.SetCursor(redCursor, cursorHotspot, CursorMode.Auto);
                    currentCursorState = CursorState.Red;
                }
            }
            else
            {
                if (currentCursorState != CursorState.Blue)
                {
                    Cursor.SetCursor(blueCursor, cursorHotspot, CursorMode.Auto);
                    currentCursorState = CursorState.Blue;
                }
            }
        }
        else
        {
            if (currentCursorState != CursorState.Blue)
            {
                Cursor.SetCursor(blueCursor, cursorHotspot, CursorMode.Auto);
                currentCursorState = CursorState.Blue;
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        if (firePoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(firePoint.position, minDistanceToShoot);
        }
    }
}
