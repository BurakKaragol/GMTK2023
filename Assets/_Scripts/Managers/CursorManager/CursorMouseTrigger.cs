using UnityEngine;

namespace MrLule.Managers.CursorMan
{
    public class CursorMouseTrigger : MonoBehaviour
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

        private void OnMouseEnter()
        {
            lastCursorType = cursorManager.GetActiveCursorType();
            cursorManager.SetCursor(cursorName);
        }

        private void OnMouseExit()
        {
            cursorManager.SetCursor(lastCursorType);
        }
    }
}
