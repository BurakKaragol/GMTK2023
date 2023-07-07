using MrLule.Managers.PlayerPrefsMan;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MrLule.Settings
{
    public class DisplayModeSetting : SettingData<FullScreenMode>
    {
        public override void ApplyChanges()
        {
            base.ApplyChanges();
            Screen.SetResolution(Screen.width, Screen.height, options[originalValueIndex]);
        }

        public override void InitializeOptions()
        {
            options = new FullScreenMode[3];
            options[0] = FullScreenMode.ExclusiveFullScreen;
            options[1] = FullScreenMode.Windowed;
            options[2] = FullScreenMode.FullScreenWindow;
            switch (Screen.fullScreenMode)
            {
                case FullScreenMode.ExclusiveFullScreen:
                    originalValueIndex = selectedValueIndex = 0;
                    break;
                case FullScreenMode.FullScreenWindow:
                    originalValueIndex = selectedValueIndex = 2;
                    break;
                case FullScreenMode.Windowed:
                    originalValueIndex = selectedValueIndex = 1;
                    break;
                default:
                    break;
            }
            Screen.SetResolution(Screen.width, Screen.height, options[originalValueIndex]);
        }

        protected override void LoadFromPlayerPrefs()
        {
            base.LoadFromPlayerPrefs();
            if (originalValueIndex == -1)
            {
                originalValue = selectedValue = Screen.fullScreenMode;
            }
        }
    }
}
