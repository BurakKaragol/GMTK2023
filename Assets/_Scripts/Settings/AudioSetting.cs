using MrLule.Managers.PlayerPrefsMan;
using UnityEngine;
using UnityEngine.UI;
using MrLule.Managers.AudioMan;

namespace MrLule.Settings
{
    public class AudioSetting : SettingData<float>
    {
        //[SerializeField] private Slider masterSlider;
        //[SerializeField] private Button masterMuteButton;

        //[SerializeField] private Slider soundSlider;
        //[SerializeField] private Button soundMuteButton;

        //[SerializeField] private Slider musicSlider;
        //[SerializeField] private Button musicMuteButton;

        //[SerializeField] private Slider sfxSlider;
        //[SerializeField] private Button sfxMuteButton;

        //private AudioManager audioManager;

        //public static float masterValue = 0.5f;
        //public static float soundValue = 0.5f;
        //public static float musicValue = 0.5f;
        //public static float sfxValue = 0.5f;

        //private bool masterMute = false;
        //private bool soundMute = false;
        //private bool musicMute = false;
        //private bool sfxMute = false;

        //protected override void Start()
        //{
        //    base.Start();

        //    audioManager = AudioManager.Instance;
        //}

        //protected override void FillAreas()
        //{
        //    base.FillAreas();

        //    FillMasterSlider();
        //    FillSoundSlider();
        //    FillMusicSlider();
        //    FillSfxSlider();
        //}

        //public void TestAudio(string audioName)
        //{
        //    audioManager.Play(audioName);
        //}

        //private void CheckAudioManager()
        //{
        //    if (audioManager == null)
        //    {
        //        audioManager = AudioManager.Instance;
        //    }
        //}

        //public void ToggleMasterButton()
        //{
        //    CheckAudioManager();
        //    masterMute = !masterMute;
        //    masterSlider.interactable = masterMute ? false : true;
        //    audioManager.masterVolume = masterMute ? 0f : masterValue;
        //    audioManager.SetMasterValues();
        //}

        //public void SetMasterSlider(float value)
        //{
        //    CheckAudioManager();
        //    masterValue = value;
        //    masterSlider.value = value;
        //    audioManager.masterVolume = masterValue;
        //    PlayerPrefsManager.SetFloat("masterVolume", masterValue);
        //    audioManager.SetMasterValues();
        //}

        //public void ToggleSoundButton()
        //{
        //    CheckAudioManager();
        //    soundMute = !soundMute;
        //    soundSlider.interactable = soundMute ? false : true;
        //    audioManager.soundVolume = soundMute ? 0f : soundValue;
        //    audioManager.SetSoundValues();
        //}

        //public void SetSoundSlider(float value)
        //{
        //    CheckAudioManager();
        //    soundValue = value;
        //    soundSlider.value = value;
        //    audioManager.soundVolume = soundValue;
        //    PlayerPrefsManager.SetFloat("soundVolume", soundValue);
        //    audioManager.SetSoundValues();
        //}

        //public void ToggleMusicButton()
        //{
        //    CheckAudioManager();
        //    musicMute = !musicMute;
        //    musicSlider.interactable = musicMute ? false : true;
        //    audioManager.musicVolume = musicMute ? 0f : musicValue;
        //    audioManager.SetMusicValues();
        //}

        //public void SetMusicSlider(float value)
        //{
        //    CheckAudioManager();
        //    musicValue = value;
        //    musicSlider.value = value;
        //    audioManager.musicVolume = musicValue;
        //    PlayerPrefsManager.SetFloat("musicVolume", musicValue);
        //    audioManager.SetMusicValues();
        //}

        //public void ToggleSfxButton()
        //{
        //    CheckAudioManager();
        //    sfxMute = !sfxMute;
        //    sfxSlider.interactable = sfxMute ? false : true;
        //    audioManager.sfxVolume = sfxMute ? 0f : sfxValue;
        //    audioManager.SetSfxValues();
        //}

        //public void SetSfxSlider(float value)
        //{
        //    CheckAudioManager();
        //    sfxValue = value;
        //    sfxSlider.value = value;
        //    audioManager.sfxVolume = sfxValue;
        //    PlayerPrefsManager.SetFloat("sfxVolume", sfxValue);
        //    audioManager.SetSfxValues();
        //}

        //private void FillMasterSlider()
        //{
        //    masterValue = PlayerPrefsManager.GetFloat("masterVolume", 0.5f);
        //    masterSlider.value = masterValue;
        //}

        //private void FillSoundSlider()
        //{
        //    soundValue = PlayerPrefsManager.GetFloat("soundVolume", 0.5f);
        //    soundSlider.value = soundValue;
        //}

        //private void FillMusicSlider()
        //{
        //    musicValue = PlayerPrefsManager.GetFloat("musicVolume", 0.5f);
        //    musicSlider.value = musicValue;
        //}

        //private void FillSfxSlider()
        //{
        //    sfxValue = PlayerPrefsManager.GetFloat("sfxVolume", 0.5f);
        //    sfxSlider.value = sfxValue;
        //}
        public override float GetOptionType(int index)
        {
            throw new System.NotImplementedException();
        }

        public override void InitializeOptions()
        {
            throw new System.NotImplementedException();
        }
    }
}
