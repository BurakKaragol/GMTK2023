using UnityEngine;

namespace MrLule.Settings
{
    public class ShadowQualitySetting : SettingData<ShadowQuality>
    {
        public override void ApplyChanges()
        {
            base.ApplyChanges();
            QualitySettings.shadows = originalValue;
        }

        public override void InitializeOptions()
        {
            options = new ShadowQuality[3];
            options[0] = ShadowQuality.Disable;
            options[1] = ShadowQuality.HardOnly;
            options[2] = ShadowQuality.All;

            QualitySettings.shadows = originalValue;
        }

        protected override void LoadFromPlayerPrefs()
        {
            base.LoadFromPlayerPrefs();
            if (originalValueIndex == -1)
            {
                originalValue = selectedValue = QualitySettings.shadows;
                switch (originalValue)
                {
                    case ShadowQuality.Disable:
                        originalValueIndex = selectedValueIndex = 0;
                        break;
                    case ShadowQuality.HardOnly:
                        originalValueIndex = selectedValueIndex = 1;
                        break;
                    case ShadowQuality.All:
                        originalValueIndex = selectedValueIndex = 2;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
