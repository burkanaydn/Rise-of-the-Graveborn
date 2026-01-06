using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] public int highLevel;
    [SerializeField] public int highExp;

    [SerializeField] private bool resetAllData;

    void Start()
    {
        if (resetAllData)
        {
            ResetAllData();
            resetAllData = false;
        }

        highLevel = PlayerPrefs.GetInt("HighLevel", 0);
        highExp = PlayerPrefs.GetInt("HighExp", 0);
    }

    public void SaveScore()
    {
        var playerExpManager = ExpManager.Instance;

        if (playerExpManager == null)
        {
            Debug.LogWarning("PlayerExpManager referansý bulunamadý!");
            return;
        }

        if (playerExpManager.playerLevel > highLevel)
        {
            highLevel = playerExpManager.playerLevel;
            highExp = playerExpManager.currentExp;
            PlayerPrefs.SetInt("HighLevel", highLevel);
            PlayerPrefs.SetInt("HighExp", highExp);
            PlayerPrefs.Save();
        }
        else if (playerExpManager.playerLevel == highLevel && playerExpManager.currentExp > highExp)
        {
            highExp = playerExpManager.currentExp;
            PlayerPrefs.SetInt("HighExp", highExp);
            PlayerPrefs.Save();
        }
    }

    public void ResetAllData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("Tüm PlayerPrefs verileri sýfýrlandý.");
        highLevel = 0;
        highExp = 0;
    }
}
