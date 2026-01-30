using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [Header("Level Info")]
    public int levelNumber; // Bu butonun temsil ettiði level
    [SerializeField] TextMeshProUGUI textMeshPro;

    [Header("Icons")]
    [SerializeField] private GameObject tickIcon;
    [SerializeField] private GameObject lockIcon;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }
    /// <summary>
    /// Level paneli açýldýðýnda çaðrýlýr. Level durumunu günceller.
    /// </summary>
    public void RefreshState(int currentLevel)
    {
        if (levelNumber < currentLevel)
        {
            // Level tamamlanmýþ
            tickIcon.SetActive(true);
            lockIcon.SetActive(false);
            button.interactable = false;
            textMeshPro.text = null;
        }
        else if (levelNumber == currentLevel)
        {
            // Þu anki level
            tickIcon.SetActive(false);
            lockIcon.SetActive(false);
            button.interactable = true;
            textMeshPro.text = "Level " + levelNumber;
        }
        else
        {
            // Level kilitli
            tickIcon.SetActive(false);
            lockIcon.SetActive(true);
            button.interactable = false;
            textMeshPro.text = null;
        }
    }

    public void OnButtonClicked()
    {
        GameSceneManager.Instance.LoadLevel(levelNumber);
    }

}
