using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionEffect : MonoBehaviour
{
    public static SceneTransitionEffect Instance { get; private set; }

    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FadeIn());
    }

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOutIn(sceneName));
    }

    private IEnumerator FadeOutIn(string sceneName)
    {
        yield return StartCoroutine(FadeOut());

        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;
    }

    private IEnumerator FadeOut()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            float dt = Time.deltaTime > 0 ? Time.deltaTime : 0.01f;
            t += dt;
            SetAlpha(Mathf.Clamp01(t / fadeDuration));
            yield return null;
        }
    }

    private IEnumerator FadeIn()
    {
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Clamp01(1 - (t / fadeDuration));
            SetAlpha(alpha);
            yield return null;
        }
    }

    private void SetAlpha(float alpha)
    {
        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = alpha;
            fadeImage.color = c;
        }
    }
}
