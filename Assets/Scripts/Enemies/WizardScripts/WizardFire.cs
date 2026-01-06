using System.Collections;
using UnityEngine;

public class WizardFire : MonoBehaviour
{
    [SerializeField] private GameObject fireballPrefab; // Ateþ topu Prefab'ý
    [SerializeField] private Transform firePoint; // Ateþ topunun çýkýþ noktasý
    [SerializeField] public float fireRate; // Ateþ topu atýþ hýzý (saniyede kaç ateþ topu)
    [SerializeField] private Transform targetPoint; // Player'ýn target Transform'u
    private float fireAnimSecond = 1f; //Ateþ topunun animasyonun kaçýncý saniyesinde atýlacaðý

    private float nextFireTime = 0f; // sayaç
    [SerializeField] private WizardAnimation wizardAnimation;
    private bool isFiring = false;

    public bool keepFire; //Ateþ etmeye devam et aç kapat

    private void Start()
    {
        // Player'ýn Transform'unu bul
        if (targetPoint == null)
        {
            targetPoint = GameObject.FindGameObjectWithTag("Target").transform;
        }
    }

    void Update()
    {
        // Belirli aralýklarla ateþ topu at
        if (Time.time >= nextFireTime && keepFire)
        {
            StartCoroutine(Fire());
            nextFireTime = Time.time + 1f / fireRate; // Sonraki atýþ zamanýný ayarla
        }
    }

    IEnumerator Fire()
    {
        if (isFiring) yield break; //Þu an ateþ etmiyorsa devam et
        isFiring = true;

        // Animasyon hýzýný hesapla: 
        // Örneðin, fireRate = 2 ise animasyon 2x hýzlý oynasýn.
        float animationSpeed = fireRate;
        wizardAnimation.SetFireSpeed(animationSpeed);

        // Ateþ topu atma animasyonunu oynat
        wizardAnimation.FireAnim();

        yield return new WaitForSeconds(fireAnimSecond / animationSpeed);

        if (!keepFire) yield break; //Ateþ etmeye devam etmesi gerekmiyorsa dur

        // Ateþ topu örneði oluþtur
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);

        // player'ýn pozisyonunu al
        Vector3 shootDirection = (targetPoint.position - firePoint.position).normalized;

        // Ateþ topunu player pozisyonuna döndür
        fireball.transform.rotation = Quaternion.LookRotation(shootDirection);

        isFiring = false;
    }
}