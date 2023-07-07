using DG.Tweening;
using MrLule.Attributes;
using MrLule.General;
using MrLule.Managers.MainMenuMan;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MainMenuPanel : MonoBehaviour
{
    [SerializeField] private bool showGizmosOnSelect = false;
    [SerializeField] private bool resetPositionOnStart = true;
    [SerializeField] private bool showOnStart = false;
    [SerializeField] private GameObject onActiveButton;

    [Header("Tweening")]
    [SerializeField] private Ease showEase = Ease.Linear;
    [SerializeField] private float showEaseTime = 1f;
    [SerializeField] private Ease hideEase = Ease.Linear;
    [SerializeField] private float hideEaseTime = 1f;

    private Vector3 startPosition = Vector3.zero;
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

        if (resetPositionOnStart)
        {
            ResetPosition();
        }

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

    private void ResetPosition()
    {
        Vector3 origin = new Vector3(Screen.width, Screen.height, 0) / 2;
        transform.position = origin - targetPosition;
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
        EventSystem.current.SetSelectedGameObject(onActiveButton);
        isShowing = true;
        mainMenuManager.openPanel = this;
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
        if (showGizmosOnSelect)
        {
            return;
        }
        DrawGizmos();
    }

    private void OnDrawGizmosSelected()
    {
        if (!showGizmosOnSelect)
        {
            return;
        }
        DrawGizmos();
    }

    private void DrawGizmos()
    {
        float multiplier = 1.25f;
        Rect rect = GetComponent<RectTransform>().rect;
        if (startPosition == Vector3.zero)
        {
            if (showOnStart)
            {
                Debugger.DrawArrow(transform.position - targetPosition, transform.position, Color.blue);
                Gizmos.color = Color.red;
                Debugger.DrawRectangle(transform.position - targetPosition, startScale * rect.size, startRotation);
                Debugger.DrawString($"{gameObject.name} Hide Position\n" +
                    $"Alpha: {startFadeAmount}",
                    transform.position - targetPosition + Vector3.down * Screen.height * multiplier, Gizmos.color);
                Gizmos.color = Color.green;
                Debugger.DrawRectangle(transform.position, targetScale * rect.size, startRotation + targetRotation);
                Debugger.DrawString($"{gameObject.name} Show Position\n" +
                    $"Alpha: {startFadeAmount}",
                    transform.position + Vector3.down * Screen.height * multiplier, Gizmos.color);
            }
            else
            {
                Debugger.DrawArrow(transform.position, transform.position + targetPosition, Color.blue);
                Gizmos.color = Color.red;
                Debugger.DrawRectangle(transform.position, startScale * rect.size, startRotation);
                Debugger.DrawString($"{gameObject.name} Hide Position\n" +
                    $"Alpha: {startFadeAmount}",
                    transform.position + Vector3.down * Screen.height * multiplier, Gizmos.color);
                Gizmos.color = Color.green;
                Debugger.DrawRectangle(transform.position + targetPosition, targetScale * rect.size, startRotation + targetRotation);
                Debugger.DrawString($"{gameObject.name} Show Position\n" +
                    $"Alpha: {startFadeAmount}",
                    transform.position + targetPosition + Vector3.down * Screen.height * multiplier, Gizmos.color);
            }
        }
        else
        {
            if (isShowing)
            {
                Debugger.DrawArrow(startPosition, startPosition + targetPosition, Color.blue);
            }
            else
            {
                Debugger.DrawArrow(startPosition + targetPosition, startPosition, Color.blue);
            }
            Gizmos.color = Color.red;
            Debugger.DrawRectangle(startPosition, startScale * rect.size, startRotation);
            Debugger.DrawString($"{gameObject.name} Hide Position\n" +
                $"Alpha: {startFadeAmount}",
                startPosition + Vector3.down * Screen.height * multiplier, Gizmos.color);
            Gizmos.color = Color.green;
            Debugger.DrawRectangle(startPosition + targetPosition, targetScale * rect.size, startRotation + targetRotation);
            Debugger.DrawString($"{gameObject.name} Show Position\n" +
                $"Alpha: {startFadeAmount}",
                startPosition + targetPosition + Vector3.down * Screen.height * multiplier, Gizmos.color);
        }
        Gizmos.color = Color.white;
    }
}
