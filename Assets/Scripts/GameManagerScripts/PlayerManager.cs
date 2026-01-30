using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Vector3 spawnPosition = new Vector3(0f, -0.3472875f, 0f);

    private GameObject currentPlayer;

    private WeaponManager weaponManager;
    private PlayerSpeedUpgradeManager playerSpeedUpgradeManager;
    private SkillManager skillManager;

    [SerializeField] private int speedLevel = 0;
    [SerializeField] private int weaponLevel = 0;
    [SerializeField] private int skillLevel = 0;

    public bool SkillMaxLevel => skillMaxLevel;
    [SerializeField] private bool skillMaxLevel = false;

    public bool WeaponMaxLevel => weaponMaxLevel;
    [SerializeField] private bool weaponMaxLevel = false;

    private const string SpeedKey = "PlayerSpeedLevel";
    private const string WeaponKey = "WeaponLevel";
    private const string SkillKey = "SkillLevel";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        speedLevel = PlayerPrefs.GetInt(SpeedKey, 0);
        weaponLevel = PlayerPrefs.GetInt(WeaponKey, 0);
        skillLevel = PlayerPrefs.GetInt(SkillKey, 0);
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
        if (scene.name == "MainScene") return;

        if (currentPlayer == null)
        {
            SpawnPlayer();
        }
    }

    private void SpawnPlayer()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            currentPlayer = GameObject.FindGameObjectWithTag("Player"); // mevcut player'a referans ver
        }
        else
        {
            currentPlayer = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        }

        weaponManager = currentPlayer.GetComponent<WeaponManager>();
        playerSpeedUpgradeManager = currentPlayer.GetComponent<PlayerSpeedUpgradeManager>();
        skillManager = currentPlayer.GetComponent<SkillManager>();


        // Sahnedeki player'a hafýzadaki upgrade seviyelerini uygula
        weaponManager.SetWeaponLevel(weaponLevel);
        playerSpeedUpgradeManager.SetPlayerSpeed(speedLevel);
        skillManager.SetSkillLevel(skillLevel);
    }

    // MARKETTEN ÇAÐRILACAK
    public void UpgradePlayerSpeed()
    {
        speedLevel++;
        PlayerPrefs.SetInt(SpeedKey, speedLevel);
        PlayerPrefs.Save();

        if (playerSpeedUpgradeManager != null)
        {
            playerSpeedUpgradeManager.SetPlayerSpeed(1);
        }
    }

    public void UpgradeWeapon()
    {
        weaponLevel++;
        PlayerPrefs.SetInt(WeaponKey, weaponLevel);
        PlayerPrefs.Save();

        if (weaponManager != null)
        {
            weaponManager.SwitchToNextWeapon();
        }
    }

    public void UpgradeSkill()
    {
        skillLevel++;
        PlayerPrefs.SetInt(SkillKey, skillLevel);
        PlayerPrefs.Save();

        if (skillManager != null)
        {
            skillManager.SwitchToNextSkill();
        }
    }

    public void SetSkillMaxLevel(bool isMax)
    {
        skillMaxLevel = isMax;
    }
    public void SetWeaponMaxLevel(bool isMax)
    {
        weaponMaxLevel = isMax;
    }

    public GameObject GetCurrentPlayer() => currentPlayer;
    public int GetSpeedLevel() => speedLevel;
    public int GetWeaponLevel() => weaponLevel;

    public int GetSkillLevel() => skillLevel;
}
