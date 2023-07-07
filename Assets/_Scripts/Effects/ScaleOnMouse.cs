using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScaleOnMouse : MonoBehaviour, IPointerEnterHandler, IPointerMoveHandler, IPointerExitHandler
{
    [SerializeField] private Transform scaleObj;
    [SerializeField] private Vector3 scaleVector = Vector3.one;
    [SerializeField] private Ease enterEase = Ease.Linear;
    [SerializeField] private float enterEaseTime = 0.5f;
    [SerializeField] private Ease exitEase = Ease.Linear;
    [SerializeField] private float exitEaseTime = 1f;

    public bool onArea = false;

    private Vector3 scale;

    private void Start()
    {
        scale = scaleObj.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onArea = true;
        scaleObj.DOScale(scaleVector, enterEaseTime).SetEase(enterEase);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        onArea = true;
        scaleObj.DOScale(scaleVector, enterEaseTime).SetEase(enterEase);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onArea = false;
        scaleObj.DOScale(scale, exitEaseTime).SetEase(exitEase);
    }
}
