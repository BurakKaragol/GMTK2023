using TMPro;
using UnityEngine;

namespace MrLule.Managers.DamagePopupMan
{
    public class DamagePopupText : MonoBehaviour
    {
        [SerializeField] private Gradient colorMap;

        private TextMeshPro textMesh;

        private void Awake()
        {
            textMesh = transform.GetComponent<TextMeshPro>();
        }

        private void AnimationFinishedTrigger()
        {
            Destroy(this.gameObject);
        }

        public void Setup(int damageAmount, float size, float gradient = 0f)
        {
            UnityEngine.Color color = colorMap.Evaluate(gradient);
            textMesh.color = color;
            textMesh.fontSize = size;
            textMesh.SetText(damageAmount.ToString());
        }

        public void Setup(int damageAmount, float size, UnityEngine.Color color)
        {
            textMesh.color = color == null ? UnityEngine.Color.white : color;
            textMesh.fontSize = size;
            textMesh.SetText(damageAmount.ToString());
        }

        public void SetOrderInLayer(int layer)
        {
            textMesh.sortingOrder = layer;
        }
    }
}
