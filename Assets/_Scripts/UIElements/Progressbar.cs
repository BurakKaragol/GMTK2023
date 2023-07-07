using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MrLule.UIElements
{
    [ExecuteInEditMode]
    public class Progressbar : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI text;
        [Range(0f, 1f)]
        public float value = 0.5f;

        private void Start()
        {
            image.fillAmount = value;
            text.SetText($"%{(int)value}");
        }

        private void Update()
        {
            image.fillAmount = value;
            text.SetText($"%{(int)(value * 100)}");
        }
    }
}
