using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }

    [SerializeField] private GameObject[] warriorPrefabs;
    [SerializeField] private GameObject[] wizardPrefabs;

    [Header("Level Info")]
    [SerializeField] private int currentLevel = 1;
    [SerializeField] private int maxLevel = 10;
    [SerializeField] int activeEnemies = 0;

    [Header("Difficulty")]
    [SerializeField] private int maxEnemies;
    [SerializeField] private int maxEnemiesAdd;
    [SerializeField] private float spawnRate;
    [SerializeField] private float spawnRateMultiplier = 0.9f;
    [SerializeField] private int warriorToSpawn;
    [SerializeField] private int wizardToSpawn;

    [Header("Spawn Distance Settings")]
    [SerializeField] private float maxSpawnDistance;
    [SerializeField] private float minSpawnDistance;


    private GameObject player;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemies());

        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void LevelUp()
    {
        currentLevel++;
        spawnRate *= spawnRateMultiplier;
        maxEnemies += maxEnemiesAdd;

        warriorToSpawn = Mathf.Min(warriorToSpawn + 1, warriorPrefabs.Length);
        wizardToSpawn = Mathf.Min(wizardToSpawn + 1, wizardPrefabs.Length);

        if (currentLevel == maxLevel)
        {
            LevelCompleteManager.Instance.HandleLevelComplete();
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);

            if (activeEnemies < maxEnemies)
            {
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {
        int enemiesLeft = maxEnemies - activeEnemies;

        int totalToSpawn = warriorToSpawn + wizardToSpawn;

        if (totalToSpawn == 0 || enemiesLeft <= 0)
            return;

        // Oransal olarak daðýt
        float warriorRatio = (float)warriorToSpawn / totalToSpawn;
        float wizardRatio = (float)wizardToSpawn / totalToSpawn;

        int warriorsToSpawnNow = Mathf.Min(Mathf.RoundToInt(enemiesLeft * warriorRatio), warriorToSpawn);
        int wizardsToSpawnNow = Mathf.Min(enemiesLeft - warriorsToSpawnNow, wizardToSpawn);

        // Spawn warriors
        for (int i = 0; i < warriorsToSpawnNow; i++)
        {
            Vector3 pos = GetRandomSpawnPosition();
            Instantiate(warriorPrefabs[Random.Range(0, warriorToSpawn)], pos, Quaternion.identity);
            activeEnemies++;
        }

        // Spawn wizards
        for (int i = 0; i < wizardsToSpawnNow; i++)
        {
            Vector3 pos = GetRandomSpawnPosition();
            Instantiate(wizardPrefabs[Random.Range(0, wizardToSpawn)], pos, Quaternion.identity);
            activeEnemies++;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (player == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(player.transform.position, minSpawnDistance);

        Vector3 fixedSpawnCenter = Vector3.zero;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(fixedSpawnCenter, maxSpawnDistance);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 fixedSpawnCenter = Vector3.zero;
        Vector3 playerPosition = player.transform.position;
        Vector3 spawnPosition;

        do
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;

            float spawnDistance = Random.Range(minSpawnDistance, maxSpawnDistance);

            spawnPosition = fixedSpawnCenter + new Vector3(randomDirection.x, 0, randomDirection.y) * spawnDistance;
        }
        while (Vector3.Distance(spawnPosition, playerPosition) < minSpawnDistance);

        return spawnPosition;
    }

    public void EnemyDefeated()
    {
        activeEnemies--;
    }
}
