using UnityEngine;
using UnityEngine.SceneManagement;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager Instance { get; private set; }

    [SerializeField] private int currentGold = 0;
    private const string GoldKey = "PlayerGold";

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        LoadGold();
    }

    private void Start()
    {
        GlobalUIManager.Instance?.UpdateGoldText(currentGold);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GlobalUIManager.Instance?.UpdateGoldText(currentGold);
    }

    public void AddGold(int amount)
    {
        currentGold += amount;
        SaveGold();
        GlobalUIManager.Instance?.UpdateGoldText(currentGold);
    }

    public bool SpendGold(int amount)
    {
        if (currentGold >= amount)
        {
            currentGold -= amount;
            SaveGold();
            GlobalUIManager.Instance?.UpdateGoldText(currentGold);
            return true;
        }

        return false; // Yetersiz bakiye
    }

    public int GetGold()
    {
        return currentGold;
    }

    private void SaveGold()
    {
        PlayerPrefs.SetInt(GoldKey, currentGold);
        PlayerPrefs.Save();
    }

    private void LoadGold()
    {
        currentGold = PlayerPrefs.GetInt(GoldKey, 0);
        GlobalUIManager.Instance?.UpdateGoldText(currentGold);
    }
}
