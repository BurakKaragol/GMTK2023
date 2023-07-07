using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class LocalRotateOnMouse : MonoBehaviour, IPointerEnterHandler, IPointerMoveHandler, IPointerExitHandler
{
    [SerializeField] private Transform rotateObj;
    [SerializeField] private Vector3 rotationVector = Vector3.zero;
    [SerializeField] private Ease enterEase = Ease.Linear;
    [SerializeField] private float enterEaseTime = 0.5f;
    [SerializeField] private Ease exitEase = Ease.Linear;
    [SerializeField] private float exitEaseTime = 1f;

    public bool onArea = false;

    private Vector3 rotation;

    private void Start()
    {
        rotation = rotateObj.localRotation.eulerAngles;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onArea = true;
        rotateObj.DOLocalRotate(rotation + rotationVector, enterEaseTime).SetEase(enterEase);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        onArea = true;
        rotateObj.DOLocalRotate(rotation + rotationVector, enterEaseTime).SetEase(enterEase);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onArea = false;
        rotateObj.DOLocalRotate(rotation, exitEaseTime).SetEase(exitEase);
    }
}
