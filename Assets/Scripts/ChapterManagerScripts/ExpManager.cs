using UnityEngine;

public class ExpManager : MonoBehaviour
{
    public static ExpManager Instance;

    [SerializeField] public int playerLevel = 1;
    [SerializeField] public int currentExp = 0;
    [SerializeField] public int expToNextLevel = 10; // Sonraki level için gerekli exp

    [SerializeField] private int expToNextLevelAdd = 5; // Sonrakþ levelde gerekecek exp artýþý

    private EnemySpawner enemySpawner => EnemySpawner.Instance;
    private ItemSpawner itemSpawner => ItemSpawner.Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void AddExp(int amount)
    {
        currentExp += amount;

        SoundManager.Instance.PlayBulletHit();

        if (currentExp >= expToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        playerLevel++;
        currentExp = 0;
        expToNextLevel += expToNextLevelAdd; // Her seviye atladýkça EXP gereksinimi artar.
        SoundManager.Instance.PlayLevelUp();

        if (enemySpawner != null)
        {
            enemySpawner.LevelUp(); // **EnemySpawner'a LevelUp sinyali gönder**
        }

        // Yeni levelde item spawnla
        if (itemSpawner != null)
        {
            itemSpawner.SpawnItem();
        }
    }
}
