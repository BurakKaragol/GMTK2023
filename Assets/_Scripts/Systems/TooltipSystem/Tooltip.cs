using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MrLule.General;
using System;

namespace MrLule.Systems.TooltipSystem
{
    [Serializable]
    public class TooltipContentData
    {
        public string name;
        public string data;
    }

    [ExecuteInEditMode()]
    public class Tooltip : MonoBehaviour
    {
        [Header("General:")]
        [Space(5)]
        [SerializeField] private TextMeshProUGUI headerText;
        [SerializeField] private TextMeshProUGUI contentText;
        [SerializeField] private LayoutElement layoutElement;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private int characterWrapLimit;

        private float headerLength;
        private float contentLength;

        private void Update()
        {
            Vector2 position = Input.mousePosition;

            float pivotX = position.x / Screen.width;
            float pivotY = position.y / Screen.height;

            rectTransform.pivot = new Vector2(pivotX, pivotY);
            transform.position = position;
        }

        public void SetText(TooltipContentData[] contents, string header = "")
        {
            if (string.IsNullOrEmpty(header))
            {
                contentText.gameObject.SetActive(false);
            }
            else
            {
                contentText.gameObject.SetActive(true);
            }

            if (headerText == null)
            {
                Debugger.LogWarning(this.GetType().ToString(), "Cannot set text (Header UI is null)");
                return;
            }

            if (contentText == null)
            {
                Debugger.LogWarning(this.GetType().ToString(), "Cannot set text (Content UI is null)");
                return;
            }

            string finalContent = "";
            foreach (TooltipContentData content in contents)
            {
                finalContent += $"<b>{content.name}</b> - {content.data}\n";
            }

            headerText.SetText(header);
            contentText.SetText(finalContent);

            CalculateSize();
        }

        private void CalculateSize()
        {
            headerLength = headerText.text.Length;
            contentLength = contentText.text.Length;

            layoutElement.enabled = headerLength > characterWrapLimit || contentLength > characterWrapLimit;
        }
    }
}
