using UnityEngine;

namespace MrLule.Managers.DamagePopupMan
{
    public class DamagePopupManager : Manager
    {
        [Header("General:")]
        [SerializeField] private GameObject damagePopupTextGreenToRed;
        [SerializeField] private GameObject damagePopupTextWhiteToRed;

        [Header("Controls:")]
        [SerializeField] private float comboAddUp = 0.2f;
        [SerializeField] private float comboSizeUpOnComplete = 1f;
        [SerializeField] private float comboTimeup = 2f;

        private int lastLayerOrder = 0;
        private float comboStartTime;
        private float gradientComboValue = 0;
        private float gradientSizeValue = 5;
        private bool isCombo = false;

        public DamagePopupText CreatePopupTextCombo(Vector3 position, int damageAmount)
        {
            if (!isCombo)
            {
                comboStartTime = Time.time;
                isCombo = true;
            }

            if (gradientComboValue >= 1f + comboAddUp)
            {
                isCombo = false;
                gradientComboValue = 0;
                gradientSizeValue = 5;
            }

            DamagePopupText popupText = CreatePopupText(position, damageAmount, gradientComboValue, gradientSizeValue, damagePopupTextWhiteToRed);
            Debug.Log($"{lastLayerOrder} | {gradientComboValue} | {gradientSizeValue}");
            gradientComboValue += comboAddUp;

            if (gradientComboValue >= 1f)
            {
                gradientSizeValue += comboSizeUpOnComplete;
            }

            return popupText;
        }

        public DamagePopupText CreatePopupText(Vector3 position, int damageAmount, float gradient, float size = 5, GameObject damagePopupText = null)
        {
            GameObject popupTransform = Instantiate(damagePopupText == null ? damagePopupTextGreenToRed : damagePopupText, position, Quaternion.identity);
            DamagePopupText popupText = popupTransform.GetComponent<DamagePopupText>();
            popupText.Setup(damageAmount, size, gradient);
            popupText.SetOrderInLayer(lastLayerOrder++);
            return popupText;
        }

        public DamagePopupText CreatePopupText(Vector3 position, int damageAmount, Color color, float size = 5)
        {
            GameObject popupTransform = Instantiate(damagePopupTextGreenToRed, position, Quaternion.identity);
            DamagePopupText popupText = popupTransform.GetComponent<DamagePopupText>();
            popupText.Setup(damageAmount, size, color);
            popupText.SetOrderInLayer(lastLayerOrder++);
            return popupText;
        }

        private void Update()
        {
            if (isCombo && Time.time >= comboStartTime + comboTimeup)
            {
                isCombo = false;
                gradientComboValue = 0;
                gradientSizeValue = 5;
            }
        }

        public override void OnEnable()
        {
            damagePopupManager = this;
        }

        public override void OnDisable()
        {
            damagePopupManager = null;
        }
    }
}
