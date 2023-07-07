using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrLule.AudioVisualization
{
    public class AudioSyncRotation : AudioSyncer
    {
        [SerializeField] private Vector3 beatRotation = new Vector3(0, 0, 5);
        [SerializeField] private Vector3 restRotation = Vector3.zero;
        [SerializeField] private bool randomize;

        private Quaternion realBeatRotation;
        private Quaternion realRestRotation;

        private void Awake()
        {
            realBeatRotation = transform.rotation * Quaternion.Euler(beatRotation);
            realRestRotation = transform.rotation * Quaternion.Euler(restRotation);
        }

        public override void OnBeat()
        {
            base.OnBeat();

            StopCoroutine("MoveToRotation");
            StartCoroutine("MoveToRotation", realBeatRotation);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (isBeat)
            {
                return;
            }

            transform.rotation = Quaternion.Lerp(transform.rotation, realRestRotation, restSmoothTime * Time.deltaTime);
        }

        private IEnumerator MoveToRotation(Quaternion target)
        {
            Quaternion current = transform.rotation;
            Quaternion initial = current;
            float timer = 0;

            while (current != target)
            {
                target.ToAngleAxis(out float angle, out Vector3 axis);
                current = Quaternion.Lerp(initial, Quaternion.Euler(axis * (randomize ? RandomSign() : 1)), timer / timeToBeat);
                timer += Time.deltaTime;

                transform.rotation = current;

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
