using UnityEngine;
using UnityEngine.Events;

namespace MrLule.Managers.PopupMan
{
    [CreateAssetMenu(fileName = "PopupContent", menuName = "Popup Manager/Popup Content")]
    public class PopupContentSO : ScriptableObject
    {
        [Header("General:")]
        public Color popupWindowBackground = Color.white;

        [Header("Header:")]
        public Color headerBackgroundColor = Color.white;
        public Color headerTextColor = Color.black;
        [Tooltip("Leave empty for not using the header.")]
        public string header = "Header";

        [Header("Image:")]
        public Color imageTintColor = Color.white;
        [Tooltip("Leave empty for not using the image.")]
        public Sprite image;

        [Header("Content:")]
        public Color contentBackgroundColor = Color.white;
        public Color contentTextColor = Color.black;
        [Tooltip("Leave empty for not using the content.")]
        public string content = "Lorem ipsum falan filan";

        [Header("Negative Answer:")]
        public Color negativeBackgroundColor = Color.gray;
        public Color negativeTextColor = Color.black;
        [Tooltip("Leave empty for not using the negative answer.")]
        public string negativeAnswer = "No";

        [Header("Alternative Answer:")]
        public Color alternativeBackgroundColor = Color.gray;
        public Color alternativeTextColor = Color.black;
        [Tooltip("Leave empty for not using the alternative answer.")]
        public string alternativeAnswer = "Maybe";

        [Header("Positive Answer:")]
        public Color positiveBackgroundColor = Color.gray;
        public Color positiveTextColor = Color.black;
        [Tooltip("Leave empty for not using the positive answer.")]
        public string positiveAnswer = "Yes";
    }
}
