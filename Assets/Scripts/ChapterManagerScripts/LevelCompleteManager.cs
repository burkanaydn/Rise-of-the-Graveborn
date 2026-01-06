using UnityEngine;

public class LevelCompleteManager : MonoBehaviour
{
    public static LevelCompleteManager Instance { get; private set; }

    [Header("Sound")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource fireworksSource;

    [Header("Prefabs")]
    [SerializeField] private GameObject nextLevelPrefab;
    [SerializeField] private GameObject[] fireworksPrefabs;

    [Header("SpawnPoints")]
    [SerializeField] private Transform nextLevelSpawnPoint;
    [SerializeField] private Transform[] fireworksSpawnPoints;

    [SerializeField] private int fireworksToSpawn;

    [Header("Level Complete State")]
    public bool levelIsOver = false;

    [Header("Direction Arrow")]
    [SerializeField] private GameObject directionArrow;
    [SerializeField] private Transform playerTransform;

    private void Awake()
    {
        // Singleton kur
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void Update()
    {
        if (levelIsOver && directionArrow != null && playerTransform != null && nextLevelSpawnPoint != null)
        {
            if (!directionArrow.activeSelf)
                directionArrow.SetActive(true);

            directionArrow.transform.position = playerTransform.position;

            Vector3 direction = nextLevelSpawnPoint.position - playerTransform.position;
            directionArrow.transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    /// <summary>
    /// Call this method when player reaches level 10.
    /// Spawns next level prefab and several fireworks at predefined positions.
    /// </summary>
    public void HandleLevelComplete()
    {
        levelIsOver = true;
        musicSource.Stop();

        // Next Level prefabýný spawn et
        if (nextLevelPrefab != null && nextLevelSpawnPoint != null)
        {
            if (GameEndingScript.Instance != null)
            {
                GameEndingScript.Instance.GameEnding();
            }

            Instantiate(nextLevelPrefab, nextLevelSpawnPoint.position, nextLevelSpawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("Next Level prefab veya spawn point eksik!");
        }

        // Havaifiþekleri spawn et
        if (fireworksPrefabs != null && fireworksPrefabs.Length > 0 && fireworksSpawnPoints != null)
        {
            int count = Mathf.Min(fireworksToSpawn, fireworksSpawnPoints.Length);

            for (int i = 0; i < count; i++)
            {
                if (fireworksSpawnPoints[i] != null)
                {
                    GameObject selectedPrefab = fireworksPrefabs[i % fireworksPrefabs.Length];

                    Instantiate(selectedPrefab, fireworksSpawnPoints[i].position, fireworksSpawnPoints[i].rotation);
                    fireworksSource.Play();
                }
            }
        }
        else
        {
            Debug.LogWarning("Fireworks prefabs veya spawn noktalarý eksik!");
        }
    }
}
