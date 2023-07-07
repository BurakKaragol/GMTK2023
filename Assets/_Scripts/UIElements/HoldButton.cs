using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MrLule.UIElements
{
    public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public float fillTime = 1f;
        public float minValue = 0f;
        public float maxValue = 1f;
        public Image buttonImage;
        public UnityEvent onFill;

        private float currentValue = 0f;
        private bool isFilling = false;
        private bool isFilled = false;

        private void Start()
        {
            buttonImage = GetComponent<Image>();
        }

        private void Update()
        {
            if (isFilling)
            {
                currentValue += Time.deltaTime / fillTime;
                currentValue = Mathf.Clamp(currentValue, minValue, maxValue);

                if (buttonImage != null)
                {
                    buttonImage.fillAmount = currentValue;
                }

                if (currentValue >= maxValue && !isFilled)
                {
                    onFill?.Invoke();
                    isFilled = true;
                    isFilling = false;
                    currentValue = 0;
                }
            }
            else
            {
                currentValue -= Time.deltaTime / fillTime;
                currentValue = Mathf.Clamp(currentValue, minValue, maxValue);

                if (buttonImage != null)
                {
                    buttonImage.fillAmount = currentValue;
                }
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isFilling = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isFilling = false;
        }
    }
}
