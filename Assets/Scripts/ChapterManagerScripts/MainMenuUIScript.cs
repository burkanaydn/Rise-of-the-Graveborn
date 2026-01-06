using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenuUIScript : MonoBehaviour
{
    public void PlayButton()
    {
        GlobalUIManager.Instance.OpenSceneManagerPanel();
    }

    public void ExitGame()
    {
        GameSceneManager.Instance.ExitGame();
    }
}
