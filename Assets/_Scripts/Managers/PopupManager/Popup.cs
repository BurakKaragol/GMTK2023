using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using MrLule.ExtensionMethods;

namespace MrLule.Managers.PopupMan
{
    public class Popup : MonoBehaviour
    {
        [SerializeField] private bool useBackgroundBlocker = true;
        [SerializeField] private RawImage backgroundBlocker;
        [SerializeField] private float fadeAmount = 0.2f;

        [SerializeField] private bool useShaderBackground = false;
        [SerializeField] private SpriteRenderer ShaderSpriteRenderer;
        [SerializeField] private string shaderFloatName;
        [SerializeField] private float blurAmount = 0.0025f;

        [Header("Tweening:")]
        [SerializeField] private Ease showEase = Ease.OutBack;
        [SerializeField] private Ease hideEase = Ease.InBack;
        [SerializeField] private float easeTime = 0.5f;

        [Header("Content:")]
        [SerializeField] private Image headerBackground;
        [SerializeField] private TextMeshProUGUI header;
        [SerializeField] private Image image;
        [SerializeField] private Image contentBackground;
        [SerializeField] private TextMeshProUGUI content;
        [SerializeField] private Image negativeButton;
        [SerializeField] private TextMeshProUGUI negativeButtonText;
        [SerializeField] private Image alternativeButton;
        [SerializeField] private TextMeshProUGUI alternativeButtonText;
        [SerializeField] private Image positiveButton;
        [SerializeField] private TextMeshProUGUI positiveButtonText;

        public bool isShowing = false;

        private PopupManager popupManager;

        private void Awake()
        {
            popupManager = FindObjectOfType<PopupManager>();
            backgroundBlocker.gameObject.SetActive(false);
            transform.localScale = Vector3.zero;
            gameObject.SetActive(false);
            popupManager.isPopupActive = false;
            isShowing = false;
        }

        public void NegativeAnswer()
        {
            popupManager.NegativeAnswer();
            HideWindow();
        }

        public void AlternativeAnswer()
        {
            popupManager.AlternativeAnswer();
            HideWindow();
        }

        public void PositiveAnswer()
        {
            popupManager.PositiveAnswer();
            HideWindow();
        }

        public void ShowWindow(Ease ease = Ease.Unset)
        {
            gameObject.SetActive(true);
            if (useBackgroundBlocker && backgroundBlocker != null)
            {
                backgroundBlocker.gameObject.SetActive(true);
                backgroundBlocker.DOFade(fadeAmount, easeTime);
            }
            else if (useShaderBackground && ShaderSpriteRenderer != null)
            {
                DOTween.To(() => ShaderSpriteRenderer.sharedMaterial.GetFloat(shaderFloatName), x => ShaderSpriteRenderer.sharedMaterial.SetFloat(shaderFloatName, x), blurAmount, easeTime);
            }
            transform.DOScale(1, easeTime).SetEase(ease == Ease.Unset ? showEase : ease).OnComplete(() =>
            {
                popupManager.isPopupActive = true;
                isShowing = true;
            });
        }

        public void HideWindow(Ease ease = Ease.Unset)
        {
            if (useBackgroundBlocker && backgroundBlocker != null)
            {
                backgroundBlocker.DOFade(0, easeTime).OnComplete(() => backgroundBlocker.gameObject.SetActive(false));
            }
            else if (useShaderBackground && ShaderSpriteRenderer != null)
            {
                DOTween.To(() => ShaderSpriteRenderer.sharedMaterial.GetFloat(shaderFloatName), x => ShaderSpriteRenderer.sharedMaterial.SetFloat(shaderFloatName, x), 0f, easeTime);
            }
            transform.DOScale(0, easeTime).SetEase(ease == Ease.Unset ? hideEase : ease).OnComplete(() =>
            {
                gameObject.SetActive(false);
                popupManager.isPopupActive = false;
                isShowing = false;
            });
        }

        public void Initialize(PopupContentSO content)
        {
            headerBackground.color = content.headerBackgroundColor;
            header.color = content.headerTextColor;
            header.SetText(content.header);
            image.color = content.imageTintColor;
            image.sprite = content.image;
            contentBackground.color = content.contentBackgroundColor;
            this.content.color = content.contentTextColor;
            this.content.SetText(content.content);
            negativeButton.color = content.negativeBackgroundColor;
            negativeButton.gameObject.SetActive(!content.negativeAnswer.IsNullOrEmpty());
            negativeButtonText.color = content.negativeTextColor;
            negativeButtonText.SetText(content.negativeAnswer);
            alternativeButton.color = content.alternativeBackgroundColor;
            alternativeButton.gameObject.SetActive(!content.alternativeAnswer.IsNullOrEmpty());
            alternativeButtonText.color = content.alternativeTextColor;
            alternativeButtonText.SetText(content.alternativeAnswer);
            positiveButton.color = content.positiveBackgroundColor;
            positiveButton.gameObject.SetActive(!content.positiveAnswer.IsNullOrEmpty());
            positiveButtonText.color = content.positiveTextColor;
            positiveButtonText.SetText(content.positiveAnswer);
        }
    }
}
