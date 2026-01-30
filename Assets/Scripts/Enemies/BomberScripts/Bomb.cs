using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private Renderer bombRenderer;
    [SerializeField] private float blinkInterval = 0.2f;

    private Material bombMat;
    private bool isBlinking = false;

    void Start()
    {
        bombMat = new Material(bombRenderer.material);
        bombRenderer.material = bombMat;

        bombMat.EnableKeyword("_EMISSION");
        isBlinking = true;
        StartCoroutine(BlinkCoroutine());
    }

    private IEnumerator BlinkCoroutine()
    {
        bool blink = false;

        while (isBlinking)
        {
            Color emission = blink ? Color.white * 2f : Color.black;
            bombMat.SetColor("_EmissionColor", emission);
            blink = !blink;

            yield return new WaitForSeconds(blinkInterval);
        }
    }

    public void StopBlinking()
    {
        isBlinking = false;
    }
}
