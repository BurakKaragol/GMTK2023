using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResizeOnMouse : MonoBehaviour, IPointerEnterHandler, IPointerMoveHandler, IPointerExitHandler
{
    [SerializeField] private LayoutElement resizeObj;
    [SerializeField] private Vector2 resizeVector = Vector2.one;
    [SerializeField] private Ease enterEase = Ease.Linear;
    [SerializeField] private float enterEaseTime = 0.5f;
    [SerializeField] private Ease exitEase = Ease.Linear;
    [SerializeField] private float exitEaseTime = 1f;

    public bool onArea = false;

    private Vector2 size;

    private void Start()
    {
        size = new Vector2(resizeObj.preferredWidth, resizeObj.preferredHeight);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onArea = true;
        resizeObj.DOPreferredSize(resizeVector, enterEaseTime).SetEase(enterEase);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        onArea = true;
        resizeObj.DOPreferredSize(resizeVector, enterEaseTime).SetEase(enterEase);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onArea = false;
        resizeObj.DOPreferredSize(size, exitEaseTime).SetEase(exitEase);
    }
}
