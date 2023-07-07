using UnityEngine;
using UnityEngine.UI;

namespace MrLule.Features
{
    public class FeatureToUI : MonoBehaviour
    {
        [SerializeField] private Feature feature;

        [Header("UI Elements:")]
        [Space(5)]
        [SerializeField] private bool useSlider;
        [SerializeField] private Slider slider;

        private void Start()
        {
            if (useSlider)
            {
                slider.maxValue = feature.Maximum;
                slider.value = feature.Current;
            }
        }

        private void LateUpdate()
        {
            UpdateSlider();
        }

        private void UpdateSlider()
        {
            if (useSlider)
            {
                slider.value = feature.GetCurrentPercent();
            }
        }
    }
}
