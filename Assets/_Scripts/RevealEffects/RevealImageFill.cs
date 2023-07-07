using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MrLule.RevealEffects
{
    public class RevealImageFill : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private float waitBeforeStart = 0f;
        [SerializeField] private float fillFrom = 0f;
        [SerializeField] private float fillTo = 1f;
        [SerializeField] private float fillTime = 2f;
        [SerializeField] private Ease ease = Ease.Linear;

        private void Start()
        {
            image.fillAmount = fillFrom;
        }

        private void OnEnable()
        {
            StartCoroutine(Reveal());
        }

        private IEnumerator Reveal()
        {
            yield return new WaitForSeconds(waitBeforeStart);
            image.fillAmount = fillFrom;
            image.DOFillAmount(fillTo, fillTime).SetEase(ease);
        }
    }
}
