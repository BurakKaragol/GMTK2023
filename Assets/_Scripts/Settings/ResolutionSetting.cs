using MrLule.Attributes;
using UnityEngine;

namespace MrLule.Settings
{
    public class ResolutionSetting : SettingData<Resolution>
    {
        public override void ApplyChanges()
        {
            base.ApplyChanges();
            Screen.SetResolution(options[originalValueIndex].width, options[originalValueIndex].height, Screen.fullScreenMode);
        }

        public override void InitializeOptions()
        {
            options = Screen.resolutions;
            for (int i = 0; i < options.Length; i++)
            {
                if (options[i].width == originalValue.width && options[i].height == originalValue.height)
                {
                    originalValueIndex = selectedValueIndex = i;
                    break;
                }
            }
            strings = new string[options.Length];
            for (int i = 0; i < options.Length; i++)
            {
                strings[i] = (int)options[i].width + "X" + (int)options[i].height;
            }
            Screen.SetResolution(options[originalValueIndex].width, options[originalValueIndex].height, Screen.fullScreenMode);
        }

        protected override void LoadFromPlayerPrefs()
        {
            base.LoadFromPlayerPrefs();
            if (originalValueIndex == -1)
            {
                originalValue = selectedValue = Screen.currentResolution;
            }
        }
    }
}
