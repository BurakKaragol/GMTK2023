using System;
using System.Collections.Generic;
using UnityEngine;

namespace MrLule.Managers.AudioMan
{
    [Serializable]
    public class AudioMixType
    {
        public AudioClip clip;
        public AnimationCurve volumeCurve;
        [Range(0f, 1f)]
        public float volume;
        public AnimationCurve pitchCurve;
        [Range(-3f, 3f)]
        public float pitch;
        public AnimationCurve stereoCurve;
        [Range(-1f, 1f)]
        public float stereo;
        public AudioSource source;
    }

    [Serializable]
    public class HypePrefix
    {
        public string hypeName;
        public float value;

        public HypePrefix(string name, float value)
        {
            hypeName = name;
            this.value = value;
        }
    }

    public class AudioHypeController : MonoBehaviour
    {
        [SerializeField]
        List<HypePrefix> prefixes = new List<HypePrefix>()
        {
            new HypePrefix("noHype", 0f),
            new HypePrefix("lowHype", 0.25f),
            new HypePrefix("normalHype", 0.5f),
            new HypePrefix("highHype", 0.75f),
            new HypePrefix("fullHype", 1f)
        };
        [SerializeField] List<AudioMixType> audios;
        [SerializeField] private double startDelay = 0.1d;
        [Range(0f, 1f)]
        [SerializeField] private float hype = 0.5f;
        [SerializeField] private float hypeLerpSpeed = 0.05f;

        private float generalVolume = .5f;
        private float musicVolume = .5f;
        private float lerpStartTime = 0f;
        private AudioManager audioManager;

        private void Start()
        {
            audioManager = AudioManager.Instance;
            if (audioManager != null)
            {
                generalVolume = audioManager.masterVolume;
                musicVolume = audioManager.musicVolume;
            }
            CreateAudioSources();
            StartPlaying();
        }

        private void Update()
        {
            if (isLerping)
            {
                float timeSinceLerpStarted = Time.time - lerpStartTime;
                float timeNeeded = Mathf.Abs(targetValue - lerpStartValue) / hypeLerpSpeed;
                float percentageComplete = timeSinceLerpStarted / timeNeeded;

                if (percentageComplete >= 1)
                {
                    hype = targetValue;
                    isLerping = false;
                }
                else
                {
                    hype = Mathf.Lerp(lerpStartValue, targetValue, percentageComplete);
                }

                UpdateHype();
            }
        }

        #region HypeControl
        public void HypeUp(float amount)
        {
            SetHype(hype + amount);
        }

        public void HypeDown(float amount)
        {
            SetHype(hype - amount);
        }

        public void SetHypePrefix(string name)
        {
            bool foundPrefix = false;
            foreach (HypePrefix prefix in prefixes)
            {
                if (prefix.hypeName == name)
                {
                    SetHype(prefix.value);
                    foundPrefix = true;
                    break;
                }
            }

            if (!foundPrefix)
            {
                Debug.LogWarning($"Could not find a hype prefix with the name '{name}'.");
            }
        }

        public void SetHype(float amount)
        {
            if (amount >= 1)
            {
                HypeLerp(1);
            }
            else if (amount <= 0)
            {
                HypeLerp(0);
            }
            else
            {
                HypeLerp(amount);
            }
        }

        private float lerpStartValue;
        private float targetValue;
        private bool isLerping = false;
        private void HypeLerp(float amount)
        {
            lerpStartTime = Time.time;
            lerpStartValue = hype;
            targetValue = amount;
            isLerping = true;
        }

        private void UpdateHype()
        {
            if (audioManager == null)
            {
                audioManager = AudioManager.Instance;
            }
            generalVolume = audioManager.masterVolume;
            musicVolume = audioManager.musicVolume;
            foreach (var audio in audios)
            {
                audio.source.volume = audio.volume * audio.volumeCurve.Evaluate(hype) * generalVolume * musicVolume;
                audio.source.pitch = audio.pitch * audio.pitchCurve.Evaluate(hype);
                audio.source.panStereo = audio.stereo * audio.stereoCurve.Evaluate(hype);
            }
        }
        #endregion

        private void StartPlaying()
        {
            double startTime = AudioSettings.dspTime + startDelay;
            foreach (var audio in audios)
            {
                audio.source.PlayScheduled(startTime);
            }
        }

        private void CreateAudioSources()
        {
            foreach (AudioMixType audio in audios)
            {
                AudioSource source = gameObject.AddComponent<AudioSource>();
                source.clip = audio.clip;
                source.volume = audio.volume * audio.volumeCurve.Evaluate(hype);
                source.pitch = audio.pitch * audio.pitchCurve.Evaluate(hype);
                source.panStereo = audio.stereo * audio.stereoCurve.Evaluate(hype);
                source.playOnAwake = false;
                source.loop = true;
                audio.source = source;
            }
        }
    }
}
