using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private int maxLevel = 10;         // En yüksek level
    [SerializeField] private int currentLevel = 1;
    [SerializeField] private string mainMenuScene = "MainScene";
    [SerializeField] private string levelScenePrefix = "Level";


    private const string CurrentLevelKey = "CurrentLevel";

    private void Awake()
    {
        // Singleton kontrolü
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        LoadCurrentLevel();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Yeni sahne yüklenince paneli kapat
        GlobalUIManager.Instance?.CloseSceneManagerPanel();
    }

    /// <summary>
    /// PlayerPrefs'ten currentLevel bilgisini yükler
    /// </summary>
    private void LoadCurrentLevel()
    {
        currentLevel = PlayerPrefs.GetInt(CurrentLevelKey, 1);
        currentLevel = Mathf.Clamp(currentLevel, 1, maxLevel);
    }

    /// <summary>
    /// currentLevel bilgisini PlayerPrefs'e kaydeder
    /// </summary>
    private void SaveCurrentLevel()
    {
        PlayerPrefs.SetInt(CurrentLevelKey, currentLevel);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Oyun baþlatýldýðýnda çaðrýlýr. Main menüdeki Play butonuna baðlanýr.
    /// </summary>
    public void StartGame()
    {
        LoadLevel(currentLevel);
    }

    /// <summary>
    /// Belirli bir leveli yükler
    /// </summary>
    public void LoadLevel(int level)
    {
        level = Mathf.Clamp(level, 1, maxLevel);
        currentLevel = level;

        string sceneName = levelScenePrefix + level;
        SceneTransitionEffect.Instance?.FadeToScene(sceneName);
    }

    /// <summary>
    /// Oyuncu leveli bitirdiðinde çaðrýlýr. currentLevel bir artýrýlýr.
    /// </summary>
    public void CompleteLevel()
    {
        currentLevel++;

        if (currentLevel > maxLevel)
        {
            currentLevel = maxLevel;
            Debug.Log("Tebrikler! Oyunun sonuna ulaþtýn.");
            LoadMainMenu();
            return;
        }

        SaveCurrentLevel();
        GlobalUIManager.Instance.OpenSceneManagerPanel();
        Debug.Log($"Level tamamlandý. Yeni Level: {currentLevel}");
    }

    /// <summary>
    /// Ana menüyü yükler
    /// </summary>
    public void LoadMainMenu()
    {
        SceneTransitionEffect.Instance?.FadeToScene(mainMenuScene);
    }

    /// <summary>
    /// Mevcut leveli yeniden yükler (restart)
    /// </summary>
    public void RestartCurrentLevel()
    {
        string sceneName = levelScenePrefix + currentLevel;
        SceneTransitionEffect.Instance?.FadeToScene(sceneName);
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }
    public void ExitGame()
    {
        Debug.Log("Oyun Kapatýlýyor...");
        Application.Quit(); // Oyunu kapatýr
    }
}
