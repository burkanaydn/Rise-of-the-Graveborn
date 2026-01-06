using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public static ItemSpawner Instance;

    [SerializeField] public GameObject[] playerEffects;

    [SerializeField] private GameObject[] itemPrefabs; // item prefabý
    [SerializeField] private float spawnRadius; // Rastgele spawnlanacak alanýn yarýçapý

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnItem()
    {
        if (itemPrefabs.Length == 0) return;

        Vector3 spawnPosition = GetRandomSpawnPosition();
        GameObject selectedItem = itemPrefabs[Random.Range(0, itemPrefabs.Length)];

        Instantiate(selectedItem, spawnPosition, Quaternion.identity);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector2 randomPoint = Random.insideUnitCircle * spawnRadius;
        return new Vector3(randomPoint.x, 0, randomPoint.y);
    }
}
