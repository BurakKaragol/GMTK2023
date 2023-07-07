using UnityEngine;
using System;

namespace MrLule.Managers.AudioMan
{
    [Serializable]
    public enum AudioState
    {
        Stopped,
        Playing,
        Paused
    }

    [Serializable]
    public enum AudioType
    {
        Sound,
        Music,
        SoundEffect
    }

    [Serializable]
    public enum AudioPlayType
    {
        PlayOneShot,
        PlayLoop,
        PlayRandomArray
    }

    [Serializable]
    public class Audio
    {
        [Tooltip("Name of the sound. Play(name)")]
        public string name;

        [Tooltip("Audio Clip.")]
        public AudioClip[] clips;

        [Tooltip("Volume of the sound. You can set the volume of this specific sound.")]
        [Range(0f, 1f)]
        public float volume = 0.5f;

        [Tooltip("Pitch of the sound. Makes sound Thickens / Thins the sound.")]
        [Range(0.01f, 3f)]
        public float pitch = 1f;

        [Tooltip("Plays the sound right away")]
        public bool playOnAwake = false;

        public AudioPlayType playType;
        public AudioType audioType;

        [TextArea(3, 10)]
        public string subtitle;
        public float subtitleTime;

        [HideInInspector]
        public AudioState state;

        [HideInInspector]
        public AudioSource[] sources;
    }

    [Serializable]
    public class AudioHypes
    {
        public string name;

        public AudioHypeController audioHypeController;
    }
}
