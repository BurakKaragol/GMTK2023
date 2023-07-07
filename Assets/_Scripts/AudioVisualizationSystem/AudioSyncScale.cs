using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace MrLule.AudioVisualization
{
    [RequireComponent(typeof(Image))]
    public class AudioSyncScale : AudioSyncer
    {
        [SerializeField] private Vector3 beatScale = new Vector3(2, 2, 2);
        [SerializeField] private Vector3 restScale = new Vector3(1, 1, 1);
        [SerializeField] private bool randomize;

        public override void OnBeat()
        {
            base.OnBeat();

            StopCoroutine("MoveToScale");
            StartCoroutine("MoveToScale", beatScale);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (isBeat)
            {
                return;
            }

            transform.localScale = Vector3.Lerp(transform.localScale, restScale, restSmoothTime * Time.deltaTime);
        }

        private IEnumerator MoveToScale(Vector3 target)
        {
            Vector3 current = transform.localScale;
            Vector3 initial = current;
            float timer = 0;

            while (current != target)
            {
                current = Vector3.Lerp(initial, target * (randomize ? RandomSign() : 1), timer / timeToBeat);
                timer += Time.deltaTime;

                transform.localScale = current;

                yield return null;
            }
            isBeat = false;
        }

        private int RandomSign()
        {
            int random = Random.Range(0, 2);
            return random == 1 ? 1 : -1;
        }
    }
}
