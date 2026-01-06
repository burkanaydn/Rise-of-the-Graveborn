using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance { get; private set; }

    [SerializeField] public bool isGameOver;

    [SerializeField] private AudioSource musicSource;
    private UIManager UIManager => UIManager.Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void GameisOver()
    {
        if (!isGameOver)
        {
            Debug.Log("Game Over");

            isGameOver = true;

            if (UIManager != null)
                UIManager.gameOverMenu.SetActive(true);

            Time.timeScale = 0f;

            musicSource.Stop();
            SoundManager.Instance.PlayGameOver();
        }
    }
}
