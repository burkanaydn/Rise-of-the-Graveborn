using System.Collections;
using UnityEngine;

public class CoinItemScript : MonoBehaviour
{
    public int goldValue = 1;
    public float attractionDistance = 3f;
    public float minAttractionSpeed = 1f;
    public float maxAttractionSpeed = 10f;
    public Vector3 initialScale;

    [SerializeField] private AudioSource source;

    private Transform playerTransform;
    private bool isAttracted = false;
    private Vector3 originalLocalScale;

    void Awake()
    {
        originalLocalScale = transform.localScale;
    }

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogWarning("Player tag'ine sahip bir nesne bulunamadý! Coin çekme özelliði çalýþmayabilir.");
        }

        if (initialScale == Vector3.zero)
        {
            initialScale = originalLocalScale;
        }
        else
        {
            transform.localScale = initialScale;
        }
    }

    void OnEnable()
    {
        // Coin aktif hale geldiðinde tüm deðerleri sýfýrla
        isAttracted = false;
        transform.localScale = initialScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EconomyManager.Instance.AddGold(goldValue);
            SoundManager.Instance.PlayCoinCollectSound();
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerTransform != null && !isAttracted)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer <= attractionDistance)
            {
                isAttracted = true;
                PlayCoinAttractSound();
            }
        }

        if (isAttracted)
        {
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
            float currentDistance = Vector3.Distance(transform.position, playerTransform.position);
            float normalizedDistance = Mathf.Clamp01(Mathf.InverseLerp(0f, attractionDistance, currentDistance));
            float currentAttractionSpeed = Mathf.Lerp(maxAttractionSpeed, minAttractionSpeed, normalizedDistance);
            transform.position += directionToPlayer * currentAttractionSpeed * Time.deltaTime;
            transform.localScale = Vector3.Lerp(Vector3.zero, initialScale, normalizedDistance);
        }

        transform.position = new Vector3(transform.position.x, 0.3f, transform.position.z);
    }

    private void PlayCoinAttractSound()
    {
        source.pitch = Random.Range(0.8f, 1f);
        source.Play();
    }
}
