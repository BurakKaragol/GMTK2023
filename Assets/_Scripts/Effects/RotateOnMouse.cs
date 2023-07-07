using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateOnMouse : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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
        rotation = rotateObj.rotation.eulerAngles;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onArea = true;
        rotateObj.DORotate(rotation + rotationVector, enterEaseTime).SetEase(enterEase);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onArea = false;
        rotateObj.DORotate(rotation, exitEaseTime).SetEase(exitEase);
    }
}
