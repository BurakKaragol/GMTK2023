using DG.Tweening;
using UnityEngine;

namespace MrLule.Managers.ScreenBorderMan
{
    public class ScreenBorderManager : Manager
    {
        [SerializeField] private RectTransform effect;

        private bool isActive;
        private Vector2 startSize;
        private float currentValue = 0f;

        private void Start()
        {
            startSize = effect.sizeDelta;
        }

        public void SetValue(float value, float duration = 2f, Ease ease = Ease.Linear)
        {
            if (value >= 1)
            {
                Open(duration, ease);
            }
            else if (value <= 0)
            {
                Close(duration, ease);
            }
            else
            {
                currentValue = 1 - value;
                effect.DOSizeDelta(new Vector2(startSize.x - 400 * currentValue, startSize.y - 400 * currentValue), duration).SetEase(ease);
            }
        }

        public void Open(float duration = 2f, Ease ease = Ease.Linear)
        {
            if (isActive)
            {
                return;
            }
            isActive = true;
            effect.DOSizeDelta(new Vector2(startSize.x - 400, startSize.y - 400), duration).SetEase(ease);
        }

        public void Close(float duration = 2f, Ease ease = Ease.Linear)
        {
            if (!isActive)
            {
                return;
            }
            isActive = false;
            effect.DOSizeDelta(new Vector2(startSize.x, startSize.y), duration).SetEase(ease);
        }

        public override void OnEnable()
        {
            screenBorderManager = this;
        }

        public override void OnDisable()
        {
            screenBorderManager = null;
        }
    }
}
