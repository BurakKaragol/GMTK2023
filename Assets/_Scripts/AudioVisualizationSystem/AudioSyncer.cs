using UnityEngine;


namespace MrLule.AudioVisualization
{
    public class AudioSyncer : MonoBehaviour
    {
        [Range(0.1f, 100f)]
        [SerializeField] private float bias = 5f;
        [SerializeField] private float timeStep = 0.15f;
        [SerializeField] protected float timeToBeat = 0.05f;
        [SerializeField] protected float restSmoothTime = 2;

        private float previousAudioValue;
        private float audioValue;
        private float timer;

        protected bool isBeat;

        public virtual void OnBeat()
        {
            timer = 0;
            isBeat = true;
        }

        public virtual void OnUpdate()
        {
            previousAudioValue = audioValue;
            audioValue = AudioSpectrum.spectrumValue;

            if (previousAudioValue > bias && audioValue <= bias)
            {
                if (timer > timeStep)
                {
                    OnBeat();
                }
            }

            if (previousAudioValue <= bias && audioValue > bias)
            {
                if (timer > timeStep)
                {
                    OnBeat();
                }
            }

            timer += Time.deltaTime;
        }

        private void Update()
        {
            OnUpdate();
        }
    }
}
