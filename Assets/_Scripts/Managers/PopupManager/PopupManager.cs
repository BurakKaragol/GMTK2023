using UnityEngine;
using UnityEngine.Events;

namespace MrLule.Managers.PopupMan
{
    public class PopupManager : Manager
    {
        [SerializeField] private Popup popup;

        public bool isPopupActive = false;

        private UnityEvent onNegativeAnswer = new UnityEvent();
        private UnityEvent onAlternativeAnswer = new UnityEvent();
        private UnityEvent onPositiveAnswer = new UnityEvent();

        private PopupContentSO currentpopupContent = null;

        public bool ShowPopup(PopupContentSO content, UnityAction negative = null, UnityAction alternative = null, UnityAction positive = null)
        {
            if (isPopupActive || popup.isShowing)
            {
                return false;
            }
            currentpopupContent = content;
            popup.Initialize(currentpopupContent);
            if (negative != null)
            {
                onNegativeAnswer.AddListener(negative);
            }
            if (alternative != null)
            {
                onAlternativeAnswer.AddListener(alternative);
            }
            if (positive != null)
            {
                onPositiveAnswer.AddListener(positive);
            }
            popup.ShowWindow();
            return true;
        }

        public void NegativeAnswer()
        {
            onNegativeAnswer?.Invoke();
            ClearListeners();
        }

        public void AlternativeAnswer()
        {
            onAlternativeAnswer?.Invoke();
            ClearListeners();
        }

        public void PositiveAnswer()
        {
            onPositiveAnswer?.Invoke();
            ClearListeners();
        }

        private void ClearListeners()
        {
            onNegativeAnswer.RemoveAllListeners();
            onAlternativeAnswer.RemoveAllListeners();
            onPositiveAnswer.RemoveAllListeners();
        }

        public override void OnEnable()
        {
            popupManager = this;
        }

        public override void OnDisable()
        {
            popupManager = null;
        }
    }
}
