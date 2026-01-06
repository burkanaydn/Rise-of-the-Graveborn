using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GlobalUIManager : MonoBehaviour
{
    public static GlobalUIManager Instance { get; private set; }

    [Header("Gold")]
    [SerializeField] private TextMeshProUGUI goldText;

    [Header("Market UI")]
    [SerializeField] private GameObject marketPanel;
    [SerializeField] public TextMeshProUGUI weaponPriceText;
    [SerializeField] public TextMeshProUGUI speedPriceText;
    [SerializeField] public TextMeshProUGUI skillPriceText;
    public Color affordableColor = Color.white;
    public Color notAffordableColor = Color.red;
    public bool isMarketOpen { get; private set; }

    [Header("Upgrade Levels")]
    [SerializeField] private TextMeshProUGUI weaponLevelText;
    [SerializeField] private TextMeshProUGUI speedLevelText;
    [SerializeField] private TextMeshProUGUI skillLevelText;

    [Header("Skill UI")]
    [SerializeField] private UnityEngine.UI.Image skillIconImage;
    [SerializeField] private Sprite skillIcon_Level0;
    [SerializeField] private Sprite skillIcon_Level1;
    [SerializeField] private GameObject skillMaxLevelPanel;
    [SerializeField] private GameObject weaponMaxLevelPanel;

    [Header("Skill Panel UI")]
    [SerializeField] private GameObject skillCooldownPanel;
    [SerializeField] private GameObject skillCooldownLockPanel;
    [SerializeField] private UnityEngine.UI.Image skillCooldownPanelIconImage;
    [SerializeField] private TextMeshProUGUI skillCooldownText;

    [Header("Info Panels")]
    [SerializeField] private GameObject weaponInfoPanel;
    [SerializeField] private GameObject speedInfoPanel;
    [SerializeField] private GameObject skillInfoPanel_Level0;
    [SerializeField] private GameObject skillInfoPanel_Level1;

    [Header("Scene Manager Panel")]
    [SerializeField] private GameObject sceneManagerPanel;
    [SerializeField] private LevelButton[] levelButtons;


    private MarketManager marketManager;
    private PlayerManager playerManager;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        marketManager = FindObjectOfType<MarketManager>();
        playerManager = FindObjectOfType<PlayerManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseMarket();
        }
    }

    public void UpdateGoldText(int amount)
    {
        if (goldText != null)
            goldText.text = amount.ToString();
    }

    public void OpenMarket()
    {
        if (UIManager.Instance != null)
        {
            if (UIManager.Instance.isEscapeMenuOpen)
            {
                UIManager.Instance.CloseEscapeMenu();
            }

        }

        UpdateMarketPrices();
        UpdateUpgradeLevels();
        UpdateSkillIcon();
        marketManager.CheckUpgradePrices();

        isMarketOpen = true;
        marketPanel.SetActive(true);

        UIManager.Instance.musicSource.Pause();
        Time.timeScale = 0f;
    }

    public void CloseMarket()
    {
        marketPanel.SetActive(false);
        isMarketOpen = false;

        if (!UIManager.Instance.isEscapeMenuOpen && !GameOverManager.Instance.isGameOver)
        {
            UIManager.Instance.musicSource.UnPause();
            Time.timeScale = 1f;
        }
    }

    public void OpenSceneManagerPanel()
    {
        if (sceneManagerPanel != null)
        {
            sceneManagerPanel.SetActive(true);

            int currentLevel = GameSceneManager.Instance.GetCurrentLevel();

            foreach (var levelButton in levelButtons)
            {
                levelButton.RefreshState(currentLevel);
            }

            Time.timeScale = 0f;
        }
        else
        {
            Debug.LogWarning("Scene Manager Panel is not assigned in GlobalUIManager.");
        }
    }

    public void CloseSceneManagerPanel()
    {
        if (sceneManagerPanel != null)
        {
            sceneManagerPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    private void UpdateMarketPrices()
    {
        if (marketManager == null) return;

        weaponPriceText.text = marketManager.GetWeaponPrice().ToString();
        speedPriceText.text = marketManager.GetSpeedPrice().ToString();
        skillPriceText.text = marketManager.GetSkillPrice().ToString();
    }

    private void UpdateUpgradeLevels()
    {
        if (playerManager == null) return;

        weaponLevelText.text = playerManager.GetWeaponLevel().ToString();
        speedLevelText.text = playerManager.GetSpeedLevel().ToString();
        skillLevelText.text = playerManager.GetSkillLevel().ToString();
    }

    private void UpdateSkillIcon()
    {
        if (playerManager == null) return;

        int skillLevel = playerManager.GetSkillLevel();

        if (skillLevel == 0)
            skillIconImage.sprite = skillIcon_Level0;
        else
            skillIconImage.sprite = skillIcon_Level1;
    }

    public void ShowSkillInfoPanel()
    {
        if (playerManager == null) return;

        int skillLevel = playerManager.GetSkillLevel();

        if (skillLevel == 0)
        {
            skillInfoPanel_Level0.SetActive(true);
            skillInfoPanel_Level1.SetActive(false);
        }
        else
        {
            skillInfoPanel_Level0.SetActive(false);
            skillInfoPanel_Level1.SetActive(true);
        }
    }

    public void HideSkillInfoPanel()
    {
        skillInfoPanel_Level0.SetActive(false);
        skillInfoPanel_Level1.SetActive(false);
    }

    public void ShowWeaponInfoPanel()
    {
        weaponInfoPanel.SetActive(true);
    }

    public void HideWeaponInfoPanel()
    {
        weaponInfoPanel.SetActive(false);
    }

    public void ShowSpeedInfoPanel()
    {
        speedInfoPanel.SetActive(true);
    }

    public void HideSpeedInfoPanel()
    {
        speedInfoPanel.SetActive(false);
    }

    public void ShowSkillMaxLevelPanel()
    {
        skillMaxLevelPanel.SetActive(true);
        SoundManager.Instance.PlayMaxlevelChain();
    }

    public void ShowWeaponMaxLevelPanel()
    {
        weaponMaxLevelPanel.SetActive(true);
        SoundManager.Instance.PlayMaxlevelChain();
    }

    public void ShowSkillCooldownPanel()
    {
        if (skillCooldownPanel != null)
            skillCooldownPanel.SetActive(true);

        if (playerManager == null) return;

        int skillLevel = playerManager.GetSkillLevel();

        if (skillLevel == 0)
            HideSkillCooldownPanel();

        if (skillLevel == 1)
            skillCooldownPanelIconImage.sprite = skillIcon_Level0;
        else if (skillLevel == 2 || skillLevel == 3)
            skillCooldownPanelIconImage.sprite = skillIcon_Level1;
    }

    public void HideSkillCooldownPanel()
    {
        if (skillCooldownPanel != null)
            skillCooldownPanel.SetActive(false);
    }

    public void UpdateSkillCooldownPanel(float skillCooldown)
    {
        int cooldownInt = Mathf.CeilToInt(skillCooldown);
        skillCooldownText.text = cooldownInt.ToString();
    }

    public void ShowSkillCooldownLock()
    {
        skillCooldownLockPanel.SetActive(true);
    }

    public void HideSkillCooldownLock()
    {
        skillCooldownLockPanel.SetActive(false);
    }

    public void SetPriceColor(TextMeshProUGUI priceText, bool affordable)
    {
        if (affordable)
            priceText.color = affordableColor;
        else
            priceText.color = notAffordableColor;
    }
}
