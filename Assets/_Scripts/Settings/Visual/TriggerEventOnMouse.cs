using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace MrLule.Settings
{
    public class TriggerEventOnMouse : MonoBehaviour, IPointerEnterHandler
    {
        public UnityEvent onMouseEnter;

        public void OnPointerEnter(PointerEventData eventData)
        {
            onMouseEnter?.Invoke();
        }
    }
}
