using DG.Tweening;
using MrLule.Managers.MainMenuMan;
using UnityEngine;
using UnityEditor;
using MrLule.General;

namespace MrLule.UI
{
    public class UIPanel : MonoBehaviour
    {
        [SerializeField] private bool showOnStart = false;

        [Header("Tweening")]
        [SerializeField] private Ease showEase = Ease.Linear;
        [SerializeField] private float showEaseTime = 1f;
        [SerializeField] private Ease hideEase = Ease.Linear;
        [SerializeField] private float hideEaseTime = 1f;

        private Vector3 startPosition;
        [SerializeField] private Vector3 targetPosition;

        [SerializeField] private float startFadeAmount = 0;
        [SerializeField] private float targetFadeAmount = 1;

        private Vector3 startRotation;
        [SerializeField] private Vector3 targetRotation;

        private Vector3 startScale = Vector3.one;
        [SerializeField] private Vector3 targetScale = Vector3.one;

        private MainMenuManager mainMenuManager;
        private CanvasGroup canvasGroup;
        private bool isShowing = false;

        public void Start()
        {
            mainMenuManager = FindObjectOfType<MainMenuManager>();
            canvasGroup = GetComponent<CanvasGroup>();

            startPosition = transform.position;
            startRotation = transform.rotation.eulerAngles;
            startScale = transform.localScale;

            gameObject.SetActive(false);
            if (showOnStart)
            {
                gameObject.SetActive(true);
                transform.position = startPosition + targetPosition;
                transform.rotation = Quaternion.Euler(targetRotation);
                transform.localScale = targetScale;
                canvasGroup.alpha = targetFadeAmount;
                isShowing = true;
            }
        }

        public void ShowPanel()
        {
            if (isShowing)
            {
                return;
            }
            gameObject.SetActive(true);
            transform.DOMove(startPosition + targetPosition, showEaseTime).SetEase(showEase);
            transform.DORotate(targetRotation, showEaseTime).SetEase(showEase);
            transform.DOScale(targetScale, showEaseTime).SetEase(showEase);
            canvasGroup.DOFade(targetFadeAmount, showEaseTime).SetEase(showEase);
            isShowing = true;
        }

        public void HidePanel()
        {
            if (!isShowing)
            {
                return;
            }
            transform.DOMove(startPosition, hideEaseTime).SetEase(hideEase);
            transform.DORotate(startRotation, hideEaseTime).SetEase(hideEase);
            transform.DOScale(startScale, hideEaseTime).SetEase(hideEase);
            canvasGroup.DOFade(startFadeAmount, hideEaseTime).SetEase(hideEase).OnComplete(() => gameObject.SetActive(false));
            isShowing = false;
        }

        private void OnDrawGizmos()
        {
            Rect rect = GetComponent<RectTransform>().rect;
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, transform.position + targetPosition);
            if (Selection.activeGameObject == gameObject)
            {
                Gizmos.color = Color.red;
                Debugger.DrawRectangle(transform.position, transform.localScale * rect.size, transform.rotation.eulerAngles);
                Gizmos.color = Color.green;
                Debugger.DrawRectangle(transform.position + targetPosition, targetScale * rect.size, transform.rotation.eulerAngles + targetRotation);
            }
            Gizmos.color = Color.white;
        }
    }
}
