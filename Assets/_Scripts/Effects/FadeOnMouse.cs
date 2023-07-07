using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FadeOnMouse : MonoBehaviour, IPointerEnterHandler, IPointerMoveHandler, IPointerExitHandler
{
    [SerializeField] Image image;
    [SerializeField] private float fadeAmount = 1f;
    [SerializeField] private Ease enterEase = Ease.Linear;
    [SerializeField] private float enterEaseTime = 0.5f;
    [SerializeField] private Ease exitEase = Ease.Linear;
    [SerializeField] private float exitEaseTime = 1f;

    public bool onArea = false;

    private float fade;

    private void Start()
    {
        fade = image.color.a;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onArea = true;
        image.DOFade(fadeAmount, enterEaseTime).SetEase(enterEase);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        onArea = true;
        image.DOFade(fadeAmount, enterEaseTime).SetEase(enterEase);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onArea = false;
        image.DOFade(fade, exitEaseTime).SetEase(exitEase);
    }
}
