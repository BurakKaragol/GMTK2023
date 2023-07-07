using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveOnMouse : MonoBehaviour, IPointerEnterHandler, IPointerMoveHandler, IPointerExitHandler
{
    [SerializeField] private Transform moveObj;
    [SerializeField] private Vector3 movementVector = Vector3.zero;
    [SerializeField] private Ease enterEase = Ease.Linear;
    [SerializeField] private float enterEaseTime = 0.5f;
    [SerializeField] private Ease exitEase = Ease.Linear;
    [SerializeField] private float exitEaseTime = 1f;

    public bool onArea = false;

    private Vector3 position;

    private void Start()
    {
        position = moveObj.position;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onArea = true;
        moveObj.DOMove(position + movementVector, enterEaseTime).SetEase(enterEase);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        onArea = true;
        moveObj.DOMove(position + movementVector, enterEaseTime).SetEase(enterEase);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onArea = false;
        moveObj.DOMove(position, exitEaseTime).SetEase(exitEase);
    }
}
