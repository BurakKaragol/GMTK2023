using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MrLule.UIElements
{
    /// <summary>
    /// Can be used in buttons.
    /// Add a empty gameobject as a child of the button and add the images you want to move as children of the empty gameobject.
    /// Add rect mask to the gameobject. And scale the inside images so that there are no empty corners on corners of the mask.
    /// Add this to the button and adress the parallax gameobject to the parallaxFolder variable.
    /// make image of the button transparent.
    /// </summary>
    public class ImageParallaxOnMouse : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField] private Transform parallexFolder;
        [SerializeField] private Vector2 parallaxEffect;
        [SerializeField] private bool mouseOnFocus = true;
        [SerializeField] private bool flipSolid = true;
        [SerializeField] private float lerpTime = 1f;

        private GameObject[] images;
        private bool pointerEntered = false;
        private Image image;
        private Vector2 size;
        private Vector3 mousePosition;
        private Vector3 mouseRelative;
        private Vector3 position;

        public void ButtonPressed()
        {
            for (int i = 0; i < images.Length; i++)
            {
                images[i].transform.DOPunchScale(Vector3.one * 0.1f, 0.2f, 1);
            }
        }

        void Start()
        {
            image = GetComponent<Image>();
            size = image.rectTransform.sizeDelta;
            images = new GameObject[parallexFolder.childCount];
            for (int i = 0; i < parallexFolder.childCount; i++)
            {
                images[i] = parallexFolder.GetChild(i).gameObject;
            }
        }

        void LateUpdate()
        {
            if (!pointerEntered)
            {
                return;
            }

            for (int i = 0; i < images.Length; i++)
            {
                mousePosition = Input.mousePosition;
                mouseRelative = new Vector3((mouseOnFocus ? (mousePosition.x - transform.position.x) : (transform.position.x - mousePosition.x)) / (size.x / 2),
                    (mouseOnFocus ? (mousePosition.y - transform.position.y) : (transform.position.y - mousePosition.y)) / (size.y / 2), 0);
                position = mouseRelative * parallaxEffect * (flipSolid ? (images.Length - 1 - i) : i);
                images[i].transform.DOMove(transform.position + position, lerpTime);
            }

            if ((Mathf.Abs(mousePosition.x - transform.position.x) >= size.x / 2) || (Mathf.Abs(mousePosition.y - transform.position.y) >= size.y / 2))
            {
                for (int i = 0; i < images.Length; i++)
                {
                    images[i].transform.DOMove(transform.position, lerpTime);
                }
                pointerEntered = false;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            pointerEntered = true;
        }
    }
}
