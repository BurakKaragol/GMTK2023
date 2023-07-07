using UnityEngine;
using UnityEngine.EventSystems;

namespace MrLule.Managers.CursorMan
{
    public class CursorPointerTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("General:")]
        [Space(5)]
        [SerializeField] private string cursorName;

        private CursorManager cursorManager;
        private CursorType lastCursorType;

        private void Start()
        {
            cursorManager = FindObjectOfType<CursorManager>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            lastCursorType = cursorManager.GetActiveCursorType();
            cursorManager.SetCursor(cursorName);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            cursorManager.SetCursor(lastCursorType);
        }
    }
}
