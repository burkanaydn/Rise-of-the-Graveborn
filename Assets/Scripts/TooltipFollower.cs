using UnityEngine;

public class TooltipFollower : MonoBehaviour
{
    [SerializeField] private Vector2 offset = new Vector2(10f, -10f);

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        Vector2 mousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(),
            Input.mousePosition,
            null,
            out mousePos
        );

        rectTransform.anchoredPosition = mousePos + offset;
    }
}
