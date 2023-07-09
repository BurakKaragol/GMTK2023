using System;
using UnityEngine;
using MrLule.General;
using MrLule.Managers.AudioMan;

public class AudioManager : MonoBehaviour
{
    [Header("Sounds:")]
    [Space(5)]
    [SerializeField] private Sound[] sounds;

    [Header("Sound Names:")]
    [Space(5)]
    [SerializeField] private string background = "background";
    [SerializeField] private string general = "volume";
    [SerializeField] private string music = "bgVolume";

    public static AudioManager Instance;

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
        DontDestroyOnLoad(gameObject);
        CreateAudioSources();
    }

    private void Start()
    {
        UpdateSounds();
        Play(background);
    }

    public void Play(string name)
    {
        Sound s = GetSound(name);
        if (s == null)
        {
            Debugger.LogWarning(this.GetType().ToString(), $"Cannot play (Wrong sound name {name})");
            return;
        }

        s.source.Play();
        s.isPlaying = true;
    }

    public void Stop(string name)
    {
        Sound s = GetSound(name);
        if (s == null)
        {
            Debugger.LogWarning(this.GetType().ToString(), $"Cannot stop (Wrong sound name {name})");
            return;
        }

        if (s.isPlaying)
        {
            s.source.Stop();
            s.isPlaying = false;
        }
        else
        {
            Debugger.LogWarning(this.GetType().ToString(), $"Sound already stopped {name}");
        }
    }

    public void Pause(string name)
    {
        Sound s = GetSound(name);
        if (s == null)
        {
            Debugger.LogWarning(this.GetType().ToString(), $"Cannot pause (Wrong sound name {name})");
            return;
        }

        if (s.isPlaying)
        {
            s.source.Pause();
            s.isPaused = true;
        }
        else
        {
            Debugger.LogWarning(this.GetType().ToString(), $"Cannot pause (Song is not playing {name})");
        }
    }

    public void UnPause(string name)
    {
        Sound s = GetSound(name);
        if (s == null)
        {
            Debugger.LogWarning(this.GetType().ToString(), $"Cannot unpause (Wrong sound name {name})");
            return;
        }

        if (s.isPaused)
        {
            s.source.UnPause();
            s.isPaused = false;
        }
        else
        {
            Debugger.LogWarning(this.GetType().ToString(), $"Cannot unpause (Song is not paused {name})");
        }
    }

    public void PlayFromArray(string[] names)
    {
        int temp = UnityEngine.Random.Range(0, names.Length);
        Play(names[temp]);
    }

    public void SetGeneralVolume(float volume)
    {
        PlayerPrefs.SetFloat(general, volume);
        UpdateSounds();
    }

    public void SetBGMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat(music, volume);
        UpdateSounds();
    }

    private Sound GetSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        return s;
    }

    private void CreateAudioSources()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void UpdateSounds()
    {
        foreach (Sound sound in sounds)
        {
            if (sound.name == background)
            {
                sound.source.volume = sound.volume * PlayerPrefs.GetFloat(music, 0.5f);
            }
            else
            {
                sound.source.volume = sound.volume * PlayerPrefs.GetFloat(general, 0.5f);
            }
        }
    }
}
