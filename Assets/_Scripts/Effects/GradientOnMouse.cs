using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GradientOnMouse : MonoBehaviour, IPointerEnterHandler, IPointerMoveHandler, IPointerExitHandler
{
    [SerializeField] private Image image;
    [SerializeField] private Gradient tintColor;
    [SerializeField] private bool startColorGradient = false;
    [SerializeField] private bool triggerOnEnter = true;
    [SerializeField] private Ease enterEase = Ease.Linear;
    [SerializeField] private float enterEaseTime = 0.5f;
    [SerializeField] private bool triggerOnExit = true;
    [SerializeField] private Ease exitEase = Ease.Linear;
    [SerializeField] private float exitEaseTime = 1f;

    public bool onArea = false;

    private Color color;

    private void Start()
    {
        if (startColorGradient)
        {
            image.color = tintColor.Evaluate(0f);
        }
        color = image.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onArea = true;
        if (!triggerOnEnter)
        {
            return;
        }
        image.DOGradientColor(tintColor, enterEaseTime).SetEase(enterEase);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        onArea = true;
        if (!triggerOnEnter)
        {
            return;
        }
        image.DOGradientColor(tintColor, enterEaseTime).SetEase(enterEase);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onArea = false;
        if (!triggerOnExit)
        {
            return;
        }
        image.DOGradientColor(tintColor, exitEaseTime).SetEase(exitEase);
    }
}
