using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpOnMouse : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private Transform moveObj;
    [SerializeField] private Vector3 movementVector = Vector3.zero;
    [SerializeField] private Ease enterEase = Ease.Linear;
    [SerializeField] private float enterEaseTime = 0.5f;

    private Vector3 position;

    private void Start()
    {
        position = moveObj.position;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        moveObj.DOPath(new Vector3[] { position + movementVector, position }, enterEaseTime, PathType.CatmullRom).SetEase(enterEase);
    }
}
