using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageFillOnMouse : MonoBehaviour, IPointerEnterHandler, IPointerMoveHandler, IPointerExitHandler
{
    [SerializeField] private Image fillImg;
    [SerializeField] private float fillAmount = 1f;
    [SerializeField] private Ease enterEase = Ease.Linear;
    [SerializeField] private float enterEaseTime = 0.5f;
    [SerializeField] private Ease exitEase = Ease.Linear;
    [SerializeField] private float exitEaseTime = 1f;

    public bool onArea = false;

    private float fill;

    private void Start()
    {
        fill = fillImg.fillAmount;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onArea = true;
        fillImg.DOFillAmount(fill + fillAmount, enterEaseTime).SetEase(enterEase);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        onArea = true;
        fillImg.DOFillAmount(fill + fillAmount, enterEaseTime).SetEase(enterEase);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onArea = false;
        fillImg.DOFillAmount(fill, exitEaseTime).SetEase(exitEase);
    }
}
