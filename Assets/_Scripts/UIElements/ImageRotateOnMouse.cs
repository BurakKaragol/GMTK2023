using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MrLule.UIElements
{
    public class ImageRotateOnMouse : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField] private Vector2 maxRotation;
        [SerializeField] private bool mousePositionUp = true;

        private bool pointerEntered = false;
        private Image image;
        private Vector2 size;
        private Vector3 mousePosition;
        private Vector3 mouseRelative;
        private Vector3 rotation;

        void Start()
        {
            image = GetComponent<Image>();
            size = image.rectTransform.sizeDelta;
        }

        void Update()
        {
            if (!pointerEntered)
            {
                return;
            }

            mousePosition = Input.mousePosition;
            mouseRelative = new Vector3((mousePositionUp ? (mousePosition.x - transform.position.x) : (transform.position.x - mousePosition.x)) / (size.x / 2),
                (mousePositionUp ? (mousePosition.y - transform.position.y) : (transform.position.y - mousePosition.y)) / (size.y / 2), 0);
            rotation = mouseRelative * maxRotation;
            transform.DORotate(rotation, 1f);

            if ((Mathf.Abs(mousePosition.x - transform.position.x) >= size.x / 2) || (Mathf.Abs(mousePosition.y - transform.position.y) >= size.y / 2))
            {
                transform.DORotate(Vector3.zero, 1f);
                pointerEntered = false;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            pointerEntered = true;
        }
    }
}
