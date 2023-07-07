using UnityEngine;

namespace MrLule.AudioVisualization
{
    public class AudioSpectrum : MonoBehaviour
    {
        [SerializeField] private FFTWindow window = FFTWindow.Hamming;

        public float[] spectrumSamples = new float[256];
        public static float spectrumValue { get; private set; }

        private void Start()
        {
            spectrumSamples = new float[128];
        }

        private void Update()
        {
            AudioListener.GetSpectrumData(spectrumSamples, 0, window);

            if (spectrumSamples != null && spectrumSamples.Length > 0)
            {
                spectrumValue = spectrumSamples[0] * 100;
            }
        }
    }
}
