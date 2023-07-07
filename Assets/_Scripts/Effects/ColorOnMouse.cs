using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorOnMouse : MonoBehaviour, IPointerEnterHandler, IPointerMoveHandler, IPointerExitHandler
{
    [SerializeField] private Image image;
    [SerializeField] private Color tintColor = Color.white;
    [SerializeField] private Ease enterEase = Ease.Linear;
    [SerializeField] private float enterEaseTime = 0.5f;
    [SerializeField] private Ease exitEase = Ease.Linear;
    [SerializeField] private float exitEaseTime = 1f;

    public bool onArea = false;

    private Color color;

    private void Start()
    {
        color = image.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onArea = true;
        image.DOBlendableColor(tintColor, enterEaseTime).SetEase(enterEase);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        onArea = true;
        image.DOBlendableColor(tintColor, enterEaseTime).SetEase(enterEase);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onArea = false;
        image.DOBlendableColor(color, exitEaseTime).SetEase(exitEase);
    }
}
