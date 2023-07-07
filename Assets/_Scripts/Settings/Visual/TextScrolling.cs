using Cinemachine;
using DG.Tweening;
using MrLule.Attributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MrLule.Settings
{
    public class TextScrolling : MonoBehaviour
    {
        [SerializeField] private int index = 0;
        [SerializeField] private float waitTime = 2f;
        [SerializeField] private float scrollSpeed = 20f;
        [SerializeField] private float fadeSpeed = 1f;

        private TextMeshProUGUI text;
        private Mask mask;
        private string currentText;
        private Vector3 startPosition;
        private Vector3 targetPosition;
        private Vector2 currentSize;
        private Vector2 prefferedSize;
        private Tweener tweener;
        private float waitStartTime = 0f;
        private bool isWaiting = false;
        private SettingExampleVisual exampleVisual;

        public bool isActive = false;

        private void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
            mask = GetComponentInParent<Mask>();
            startPosition = transform.localPosition;
            currentSize = mask.rectTransform.sizeDelta;
        }

        public void Activate()
        {
            isActive = true;
            CalculateMovementValues();
        }

        public void Deactivate()
        {
            isActive = false;
        }

        private void CalculateMovementValues()
        {
            prefferedSize = text.GetPreferredValues();
            if (currentSize.x < prefferedSize.x)
            {
                targetPosition = Vector3.left * (prefferedSize.x - currentSize.x);
                StartWaiting();
            }
        }

        private void LateUpdate()
        {
            if (!isActive)
            {
                return;
            }
            if (exampleVisual != null)
            {
                if (exampleVisual.currentExampleIndex != index)
                {
                    Deactivate();
                }
            }
            else
            {
                exampleVisual = FindObjectOfType<SettingExampleVisual>();
            }

            if (text.text != currentText)
            {
                ResetText();
            }

            if (isWaiting && Time.time >= waitStartTime + waitTime)
            {
                isWaiting = false;
                StartScrolling();
            }
        }

        private void StartScrolling()
        {
            transform.DOLocalMoveX(targetPosition.x, Mathf.Abs(targetPosition.x) / scrollSpeed).SetEase(Ease.Linear).
                OnComplete(() => ScrollingFinished());
        }

        private void ScrollingFinished()
        {
            text.DOFade(0f, fadeSpeed / 2).OnComplete(() => ResetText());
        }

        private void StartWaiting()
        {
            waitStartTime = Time.time;
            isWaiting = true;
        }

        private void ResetText()
        {
            currentText = text.text;
            transform.localPosition = startPosition;
            text.DOFade(1f, fadeSpeed / 2).OnComplete(() =>
            {
                StartWaiting();
                CalculateMovementValues();
            });
        }

        private void OnEnable()
        {
            exampleVisual = FindObjectOfType<SettingExampleVisual>();
        }
    }
}
