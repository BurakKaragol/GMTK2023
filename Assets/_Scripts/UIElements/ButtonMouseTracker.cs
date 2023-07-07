using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MrLule.UIElements
{
    public class ButtonMouseTracker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Transform image;

        private Button button;
        private Vector3 mousePosition;
        private Vector2 buttonSize;
        private Vector2 imageSize;
        private Vector2 area;
        private Vector3 target;
        private bool isMouseOver = false;

        public void PressedButton()
        {
            // Button press interaction
        }

        private void Start()
        {
            button = GetComponent<Button>();
            buttonSize = button.GetComponent<RectTransform>().rect.size;
            imageSize = image.GetComponent<RectTransform>().rect.size;
            area = buttonSize - imageSize;
        }

        private void LateUpdate()
        {
            if (!isMouseOver)
            {
                return;
            }

            mousePosition = Input.mousePosition;
            Vector2 halfSize = area / 2;
            Vector2 mouseRequired = mousePosition - transform.position;
            target = transform.position + new Vector3(Mathf.Clamp(mouseRequired.x, -halfSize.x, halfSize.x), Mathf.Clamp(mouseRequired.y, -halfSize.y, halfSize.y), 0);
            image.DOMove(target, 1f);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isMouseOver = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            image.DOMove(transform.position, 1f);
            isMouseOver = false;
        }

        private void OnDrawGizmos()
        {
            Color startColor = Gizmos.color;
            button = GetComponent<Button>();
            buttonSize = button.GetComponent<RectTransform>().rect.size;
            imageSize = image.GetComponent<RectTransform>().rect.size;
            area = buttonSize - imageSize;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(target - transform.position, 2f);
            Gizmos.DrawWireCube(transform.position, area);
            Gizmos.color = startColor;
        }
    }
}
