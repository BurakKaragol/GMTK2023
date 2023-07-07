using DG.Tweening;
using MrLule.Attributes;
using MrLule.ExtensionMethods;
using MrLule.Managers.SceneTransitionMan;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

public class LoadingSceneSystem : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] public Image fillImage;
    [SerializeField] private TextMeshProUGUI loadingTip;
    [SerializeField] private float tipChangeTime = 10f;
    [SerializeField] private float tipFadeTime = 1f;
    [SerializeField] private float backgroundChangeTime = 20f;
    [SerializeField] private float backgroundFadeTime = 2f;
    [SerializeField] private LocalizedString localizedString;
    [SerializeField] private List<string> localizedTipKeys;
    [SerializeField] private List<Sprite> backgroundImages;
    [SerializeField] private float minimumLoadTime = 5f;

    [ShowOnly] public float waitProgress = 0f;
    private float waitStartTime = 0;

    private float lastTipChangeTime = 0f;
    private float lastBackgroundChangeTime = 0f;
    private bool isChangingTip = false;
    private bool isChangingBackground = false;
    private string newTip = "";
    private Sprite newBackground = null;

    private void Start()
    {
        waitStartTime = Time.time;
        loadingTip.SetText(GetLocalizedString(localizedTipKeys[Random.Range(0, localizedTipKeys.Count)]));
        if (backgroundImages.Count != 0)
        {
            backgroundImage.color = Color.white;
            backgroundImage.sprite = backgroundImages[Random.Range(0, backgroundImages.Count)];
        }
        else
        {
            backgroundImage.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
        }
        lastTipChangeTime = Time.time;
        lastBackgroundChangeTime = Time.time;
    }

    private void Update()
    {
        float progress = (Time.time - waitStartTime) / minimumLoadTime;
        waitProgress = progress >= 1f ? 1f : progress;
        float totalProgress = SceneTransitionManager.Instance.loadingProgress;
        fillImage.fillAmount = totalProgress;

        if (Time.time >= lastTipChangeTime + tipChangeTime && !isChangingTip)
        {
            isChangingTip = true;
            newTip = GetLocalizedString(localizedTipKeys[Random.Range(0, localizedTipKeys.Count)]);
            loadingTip.DOFade(0f, tipFadeTime).SetEase(Ease.Linear).OnComplete(() => loadingTip.SetText(newTip));
        }

        if (isChangingTip && loadingTip.text.Equals(newTip))
        {
            loadingTip.DOFade(1f, tipFadeTime).SetEase(Ease.Linear).OnComplete(() =>
            {
                isChangingTip = false;
                lastTipChangeTime = Time.time;
            });
        }

        if (backgroundImages.Count == 0)
        {
            return;
        }

        if (Time.time >= lastBackgroundChangeTime + backgroundChangeTime && !isChangingBackground)
        {
            isChangingBackground = true;
            newBackground = backgroundImages[Random.Range(0, backgroundImages.Count)];
            backgroundImage.DOFade(0f, backgroundFadeTime).SetEase(Ease.Linear).OnComplete(() => backgroundImage.sprite = newBackground);
        }

        if (newBackground == null)
        {
            return;
        }
        if (isChangingBackground && backgroundImage.sprite == newBackground)
        {
            backgroundImage.DOFade(1f, backgroundFadeTime).SetEase(Ease.Linear).OnComplete(() =>
            {
                isChangingBackground = false;
                lastBackgroundChangeTime = Time.time;
            });
        }
    }

    private string GetLocalizedString(string key)
    {
        localizedString.TableEntryReference = key;

        if (localizedString.GetLocalizedString().IsNotNullOrEmpty())
        {
            return localizedString.GetLocalizedString();
        }

        return "There should be a tip in your preffered language but something wrong happened ^_____^\nJust keep playing and try to have fun!";
    }
}
