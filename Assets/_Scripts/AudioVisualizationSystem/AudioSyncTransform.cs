using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrLule.AudioVisualization
{
    public class AudioSyncTransform : AudioSyncer
    {
        [SerializeField] private Vector3 beatPosition = new Vector3(0, 5, 0);
        [SerializeField] private Vector3 restPosition = Vector3.zero;
        [SerializeField] private bool randomize;

        private Vector3 realBeatPosition;
        private Vector3 realRestPosition;

        private void Awake()
        {
            realBeatPosition = transform.position + beatPosition;
            realRestPosition = transform.position + restPosition;
        }

        public override void OnBeat()
        {
            base.OnBeat();

            StopCoroutine("MoveToPosition");
            StartCoroutine("MoveToPosition", realBeatPosition);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (isBeat)
            {
                return;
            }

            transform.position = Vector3.Lerp(transform.position, realRestPosition, restSmoothTime * Time.deltaTime);
        }

        private IEnumerator MoveToPosition(Vector3 target)
        {
            Vector3 current = transform.position;
            Vector3 initial = current;
            float timer = 0;

            while (current != target)
            {
                current = Vector3.Lerp(initial, target * (randomize ? RandomSign() : 1), timer / timeToBeat);
                timer += Time.deltaTime;

                transform.position = current;

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
