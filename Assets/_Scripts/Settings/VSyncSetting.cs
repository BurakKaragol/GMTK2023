using MrLule.Managers.PlayerPrefsMan;
using UnityEngine;
using UnityEngine.UI;

namespace MrLule.Settings
{
    public enum VSyncMode
    {
        Disabled = 0,
        Enabled = 1,
        Adaptive = -1
    }

    public class VSyncSetting : SettingData<VSyncMode>
    {
        public override void ApplyChanges()
        {
            base.ApplyChanges();
            QualitySettings.vSyncCount = (int)originalValue;
        }

        public override void InitializeOptions()
        {
            options = new VSyncMode[3];
            options[0] = VSyncMode.Disabled;
            options[1] = VSyncMode.Enabled;
            options[2] = VSyncMode.Adaptive;
            QualitySettings.vSyncCount = (int)originalValue;
        }

        protected override void LoadFromPlayerPrefs()
        {
            base.LoadFromPlayerPrefs();
            if (originalValueIndex == -1)
            {
                originalValue = selectedValue = (VSyncMode)QualitySettings.vSyncCount;
                originalValueIndex = selectedValueIndex = originalValue == VSyncMode.Disabled ? 0 : originalValue == VSyncMode.Enabled ? 1 : 2;
            }
        }
    }
}
