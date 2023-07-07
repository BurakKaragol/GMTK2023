using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Tilemaps;
using UnityEngine;

namespace MrLule.RevealEffects
{
    public class RevealNumberCount : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private float waitBeforeStart = 1f;
        [SerializeField] private int countFrom = 0;
        [SerializeField] private int countTo = 99;
        [SerializeField] private float countTime = 2f;

        private void Start()
        {
            text.SetText(countFrom.ToString());
        }

        private void OnEnable()
        {
            StartCoroutine(Reveal());
        }

        private IEnumerator Reveal()
        {
            yield return new WaitForSeconds(waitBeforeStart);
            text.SetText(countFrom.ToString());

            float currentTime = 0;
            float totalTime = countTime;

            float startValue = countFrom;
            float endValue = countTo;

            while (currentTime < totalTime)
            {
                currentTime += Time.deltaTime;

                float t = currentTime / totalTime;
                t = 1 - Mathf.Pow(1 - t, 2);
                float currentValue = Mathf.Lerp(startValue, endValue, t);
                text.SetText(Mathf.RoundToInt(currentValue).ToString());

                yield return null;
            }

            text.SetText(countTo.ToString());
        }
    }
}
