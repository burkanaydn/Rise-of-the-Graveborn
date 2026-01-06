using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Menu Panels")]
    [SerializeField] public GameObject gameOverMenu;
    [SerializeField] private GameObject escapeMenuPanel;
    [SerializeField] private GameObject optionsMenuPanel;

    [Header("Sound Settings")]
    [SerializeField] public AudioSource musicSource;
    [SerializeField] private AudioMixer audioixer;
    [SerializeField] private Slider generalSlider;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private TextMeshProUGUI generalVolumeText;
    [SerializeField] private TextMeshProUGUI soundVolumeText;
    [SerializeField] private TextMeshProUGUI musicVolumeText;

    [Header("LevelBar")]
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] public Image fillImage;

    public bool isEscapeMenuOpen { get; private set; }


    private ExpManager Exp => ExpManager.Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (gameOverMenu != null)
        {
            gameOverMenu.SetActive(false);
        }
        if (escapeMenuPanel != null)
        {
            escapeMenuPanel.SetActive(false);
        }

        SetGeneralVolume(PlayerPrefs.GetFloat("GeneralVolume", 1f));
        SetSFXVolume(PlayerPrefs.GetFloat("SFXVolume", 0.5f));
        SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 0.5f));
    }

    void Update()
    {
        if (Exp == null) return;

        levelText.text = Exp.playerLevel.ToString();
        expText.text = $"{Exp.currentExp} / {Exp.expToNextLevel}";

        fillImage.fillAmount = (float)Exp.currentExp / Exp.expToNextLevel;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionsMenuPanel.activeSelf)
            {
                optionsMenuPanel.SetActive(false);
                OpenEscapeMenu();
            }
            else if (GlobalUIManager.Instance.isMarketOpen)
            {
                GlobalUIManager.Instance.CloseMarket();
            }

            else if (isEscapeMenuOpen)
            {
                CloseEscapeMenu();
            }

            else
            {
                OpenEscapeMenu();
            }
        }
    }

    public void RestartButton()
    {
        GameSceneManager.Instance.RestartCurrentLevel();
    }

    public void OpenEscapeMenu()
    {
        if (GameOverManager.Instance.isGameOver)
            return;

        if (GlobalUIManager.Instance.isMarketOpen)
        {
            Debug.LogWarning("Market menu is open, cannot open Escape Menu directly.");
            return;
        }

        isEscapeMenuOpen = true;
        escapeMenuPanel.SetActive(true);

        musicSource.Pause();
        optionsMenuPanel.SetActive(false);
        Time.timeScale = 0f;
    }

    public void CloseEscapeMenu()
    {
        isEscapeMenuOpen = false;
        escapeMenuPanel.SetActive(false);

        if (!optionsMenuPanel.activeSelf)
        {
            musicSource.UnPause();
            Time.timeScale = 1f;
        }
        else if (!optionsMenuPanel.activeSelf) 
        {
            musicSource.UnPause();
            Time.timeScale = 1f;
        }
    }

    public void ResumeGame()
    {
        isEscapeMenuOpen = false;
        escapeMenuPanel.SetActive(false);
        musicSource.UnPause();
        Time.timeScale = 1f;
    }

    public void BackToMainMenu()
    {
        GameSceneManager.Instance.LoadMainMenu();
    }

    public void ExitGame()
    {
        GameSceneManager.Instance.ExitGame();
    }

    public void SetGeneralVolume(float volume)
    {
        AudioListener.volume = volume;
        generalSlider.value = volume;
        generalVolumeText.text = ((int)(volume * 100)).ToString();
        PlayerPrefs.SetFloat("GeneralVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        float dB = (volume > 0) ? Mathf.Log10(volume) * 20 : -80f;
        audioixer.SetFloat("SFXParameters", dB);
        soundSlider.value = volume;
        soundVolumeText.text = ((int)(volume * 100)).ToString();
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        float dB = (volume > 0) ? Mathf.Log10(volume) * 20 : -80f;
        audioixer.SetFloat("MusicParameters", dB);
        musicSlider.value = volume;
        musicVolumeText.text = ((int)(volume * 100)).ToString();
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
}
