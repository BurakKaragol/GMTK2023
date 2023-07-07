using MrLule.ExtensionMethods;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MrLule.Managers.AudioMan
{
    public class AudioManager : Manager
    {
        [Header("Subtitle System:")]
        [SerializeField] private bool useSubtitles = false;
        [Header("Sounds:")]
        [SerializeField] private Audio[] audios;
        [Header("Audio Hype Controllers:")]
        [SerializeField] private AudioHypes[] audioHypecontrollers;

        [HideInInspector]
        public static Dictionary<string, AudioSource> audioSources = new Dictionary<string, AudioSource>();

        [HideInInspector]
        public List<Audio> soundAudios = new List<Audio>();
        [HideInInspector]
        public List<Audio> musicAudios = new List<Audio>();
        [HideInInspector]
        public List<Audio> sfxAudios = new List<Audio>();

        [Header("Volumes:")]
        public float masterVolume = 0.5f;
        public float soundVolume = 0.5f;
        public float musicVolume = 0.5f;
        public float sfxVolume = 0.5f;

        public static AudioManager Instance;
        private AudioSubtitleSystem audioSubtitleSystem;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
            if (useSubtitles)
            {
                audioSubtitleSystem = GetComponent<AudioSubtitleSystem>();
            }

            DontDestroyOnLoad(gameObject);
            CreateAudioSources();
        }

        #region Audio Functions
        public void Play(string name)
        {
            Audio audio = Array.Find(audios, audio => audio.name == name);
            if (audio != null)
            {
                if (audioSources.ContainsKey(name))
                {
                    if (useSubtitles)
                    {
                        if (audio.subtitle.IsNotNullOrEmpty())
                        {
                            audioSubtitleSystem.ShowSubtitle(audio.subtitle, audio.subtitleTime);
                        }
                    }
                    audioSources[name].Play();
                }
            }
        }

        public void PlayRandom(string name)
        {
            Audio audio = Array.Find(audios, audio => audio.name == name);
            if (audio != null && audio.playType == AudioPlayType.PlayRandomArray)
            {
                int randomIndex = UnityEngine.Random.Range(0, audio.sources.Length);
                if (useSubtitles)
                {
                    if (audio.subtitle.IsNotNullOrEmpty())
                    {
                        audioSubtitleSystem.ShowSubtitle(audio.subtitle, audio.subtitleTime);
                    }
                }
                audio.sources[randomIndex].Play();
            }
        }

        public void Pause(string name)
        {
            if (audioSources.ContainsKey(name))
            {
                audioSources[name].Pause();
            }
        }

        public void Stop(string name)
        {
            if (audioSources.ContainsKey(name))
            {
                audioSources[name].Stop();
            }
        }

        public void SetMasterValues()
        {
            SetSoundValues();
            SetMusicValues();
            SetSfxValues();
        }

        public void SetSoundValues()
        {
            foreach (Audio audio in soundAudios)
            {
                foreach (AudioSource source in audio.sources)
                {
                    source.volume = CalculateVolume(audio);
                }
            }
        }

        public void SetMusicValues()
        {
            foreach (Audio audio in musicAudios)
            {
                foreach (AudioSource source in audio.sources)
                {
                    source.volume = CalculateVolume(audio);
                }
            }
        }

        public void SetSfxValues()
        {
            foreach (Audio audio in sfxAudios)
            {
                foreach (AudioSource source in audio.sources)
                {
                    source.volume = CalculateVolume(audio);
                }
            }
        }

        public void CreateAudioSources()
        {
            foreach (Audio audio in audios)
            {
                audio.sources = new AudioSource[audio.clips.Length];
                int index = 0;
                foreach (AudioClip clip in audio.clips)
                {
                    AudioSource source = gameObject.AddComponent<AudioSource>();
                    source.clip = clip;
                    source.playOnAwake = audio.playOnAwake;
                    source.volume = CalculateVolume(audio);
                    source.pitch = audio.pitch;
                    source.loop = audio.playType == AudioPlayType.PlayLoop;
                    audio.sources[index++] = source;
                    audioSources.Add(audio.name, source);
                    switch (audio.audioType)
                    {
                        case AudioType.Sound:
                            soundAudios.Add(audio);
                            break;
                        case AudioType.Music:
                            musicAudios.Add(audio);
                            break;
                        case AudioType.SoundEffect:
                            sfxAudios.Add(audio);
                            break;
                    }
                }
            }
        }

        public float CalculateVolume(Audio audio)
        {
            switch (audio.audioType)
            {
                case AudioType.Sound:
                    return masterVolume * soundVolume * audio.volume;
                case AudioType.Music:
                    return masterVolume * musicVolume * audio.volume;
                case AudioType.SoundEffect:
                    return masterVolume * sfxVolume * audio.volume;
            }
            return 0f;
        }
        #endregion

        #region Audio Hype Controller Functions
        public void SetAudioHype(string name, float value)
        {
            for (int i = 0; i < audioHypecontrollers.Length; i++)
            {
                if (audioHypecontrollers[i].name == name)
                {
                    audioHypecontrollers[i].audioHypeController.SetHype(value);
                }
            }
        }
        #endregion

        public override void OnEnable()
        {
            audioManager = this;
        }

        public override void OnDisable()
        {
            audioManager = null;
        }
    }
}
