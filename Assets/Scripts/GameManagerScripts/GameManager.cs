using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Debug / Reset")]
    [SerializeField] private bool resetAllData = false;
    [SerializeField] private bool resetMarketData = false;
    [SerializeField] private bool resetCurrentLevelData = false;


    private void Awake()
    {
        if (resetAllData)
            ResetPlayerData();

        if (resetMarketData)
            ResetMarketData();

        if(resetCurrentLevelData)
            ResetCurrentLevelData();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void ResetPlayerData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("All PlayerPrefs Data Has Been Reset.");
    }

    private void ResetMarketData()
    {
        PlayerPrefs.DeleteKey("WeaponUpgradePrice");
        PlayerPrefs.DeleteKey("SpeedUpgradePrice");
        PlayerPrefs.DeleteKey("SkillUpgradePrice");

        PlayerPrefs.DeleteKey("PlayerSpeedLevel");
        PlayerPrefs.DeleteKey("WeaponLevel");
        PlayerPrefs.DeleteKey("SkillLevel");
    }

    private void ResetCurrentLevelData()
    {
        PlayerPrefs.DeleteKey("CurrentLevel");
    }
}
