using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace MrLule.Settings
{
    public class TriggerEventOnClick : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private UnityEvent onClick;

        public void OnPointerClick(PointerEventData eventData)
        {
            onClick?.Invoke();
        }
    }
}
