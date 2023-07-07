using DG.Tweening;
using MrLule.ExtensionMethods;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace MrLule.Settings
{
    [Serializable]
    public class ExampleVisual
    {
        public string localizationKey;
        public Sprite exampleVisual;
    }

    public class SettingExampleVisual : MonoBehaviour
    {
        [SerializeField] private LocalizedString localizedString;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private Image exampleImage;
        [SerializeField] private ExampleVisual[] examples;
        [SerializeField] private float changeTime = 1f;

        public int currentExampleIndex = 0;
        private float halfTime;

        private void Start()
        {
            halfTime = changeTime / 2;
            exampleImage.sprite = examples[currentExampleIndex].exampleVisual;
            descriptionText.SetText(GetLocalizedString(examples[currentExampleIndex].localizationKey));
        }

        public void SetActiveExample(int index)
        {
            if (examples[index].exampleVisual != null)
            {
                exampleImage.DOFade(0f, halfTime).SetEase(Ease.InOutSine).OnComplete(() =>
                {
                    exampleImage.sprite = examples[index].exampleVisual;
                    exampleImage.DOFade(1f, halfTime).SetEase(Ease.InOutSine);
                });
            }
            descriptionText.DOFade(0f, halfTime).SetEase(Ease.InOutSine).OnComplete(() =>
            {
                descriptionText.SetText(GetLocalizedString(examples[index].localizationKey));
                descriptionText.DOFade(1f, halfTime).SetEase(Ease.InOutSine);
            });
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
}
