using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MrLule.Managers.MessageMan
{
    public class MessagePanel : MonoBehaviour
    {
        [SerializeField] private float width = 400f;
        [SerializeField] private RectTransform progressImage;
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI contentText;
        [SerializeField] private Ease showEase = Ease.OutBack;
        [SerializeField] private Ease hideEase = Ease.InBack;
        [SerializeField] private float waitTime = 10f;
        [SerializeField] private Transform nextPosition;

        private float waitStartTime = float.PositiveInfinity;
        private bool waitTimeDone = false;

        private void LateUpdate()
        {
            if (waitTimeDone)
            {
                return;
            }

            progressImage.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width * ((Time.time - waitStartTime) / waitTime));

            if (Time.time >= waitStartTime + waitTime)
            {
                HideMessage();
                waitTimeDone = true;
            }
        }

        public void ShowMessage()
        {
            transform?.DOMoveX(width / 2, 0.5f).SetEase(showEase).OnComplete(() => waitStartTime = Time.time);
        }

        public void HideMessage()
        {
            transform?.DOMoveX(-(width / 2), 0.5f).SetEase(hideEase).OnComplete(CloseMessage);
        }

        public void CloseMessage()
        {
            FindObjectOfType<MessageManager>().CloseMessage(this);
            Destroy(gameObject, 1f);
        }

        public void SetTargetPosition(Vector3 target)
        {
            transform?.DOMoveY(target.y, 1f);
        }

        public void Initialize(string title, string content, Sprite sprite)
        {
            titleText.SetText(title);
            contentText.SetText(content);
            image.sprite = sprite;
            image.GetComponent<LayoutElement>().enabled = sprite != null;
        }

        public float GetHeight()
        {
            return GetComponent<RectTransform>().rect.height;
        }

        public Vector3 GetNextPosition()
        {
            return nextPosition.position;
        }
    }
}
