using UnityEngine;
using TMPro;
using System.Collections;

public class ScrollTextVertical : MonoBehaviour
{
    public float height = 800f;
    public float scrollSpeed = 30f;
    public float delayTime = 2f;

    private RectTransform rectTransform;
    private TextMeshProUGUI textMeshPro;
    private float contentHeight;
    private Coroutine scrollCoroutine;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        contentHeight = textMeshPro.preferredHeight;
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, -height);
    }

    private void OnEnable()
    {
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, -height);
        scrollCoroutine = StartCoroutine(Scroll());
    }

    private void OnDisable()
    {
        if (scrollCoroutine != null)
            StopCoroutine(scrollCoroutine);
    }

    private IEnumerator Scroll()
    {
        float startPosition = rectTransform.anchoredPosition.y;
        float endPosition = contentHeight + rectTransform.rect.height;

        while (true)
        {
            rectTransform.anchoredPosition += new Vector2(0f, scrollSpeed * Time.deltaTime);

            if (rectTransform.anchoredPosition.y >= endPosition)
            {
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, startPosition);
                yield return new WaitForSeconds(delayTime);
            }

            yield return null;
        }
    }
}
