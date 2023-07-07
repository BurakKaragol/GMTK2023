using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MrLule.AudioVisualization
{
    [RequireComponent(typeof(Image))]
    public class AudioSyncColor : AudioSyncer
    {
        [SerializeField] private bool useGradient;
        [SerializeField] private Gradient beatGradient;
        [SerializeField] private Color[] beatColors;
        [SerializeField] private Color restColor;

        private int randomImage;
        private Image image;

        private void Start()
        {
            image = GetComponent<Image>();
        }

        public override void OnBeat()
        {
            base.OnBeat();

            Color color;
            if (useGradient)
            {
                color = beatGradient.Evaluate(Random.Range(0f, 1f));
            }
            else
            {
                color = RandomColor();
            }

            StopCoroutine("MoveToColor");
            StartCoroutine("MoveToColor", color);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (isBeat) return;

            image.color = Color.Lerp(image.color, restColor, restSmoothTime * Time.deltaTime);
        }

        private Color RandomColor()
        {
            if (beatColors == null || beatColors.Length == 0) return Color.white;
            randomImage = Random.Range(0, beatColors.Length);
            return beatColors[randomImage];
        }

        private IEnumerator MoveToColor(Color _target)
        {
            Color current = image.color;
            Color initial = current;
            float timer = 0;

            while (current != _target)
            {
                current = Color.Lerp(initial, _target, timer / timeToBeat);
                timer += Time.deltaTime;

                image.color = current;

                yield return null;
            }

            isBeat = false;
        }
    }
}
