using MrLule.Attributes;
using UnityEngine;

namespace MrLule.Settings
{
    public enum QualityEnum
    {
        Lowest = 0,
        Low = 1,
        Medium = 2,
        High = 3,
        VeryHigh = 4,
        Epic = 5
    }

    public class GraphicsQualitySetting : SettingData<QualityEnum>
    {
        public override void ApplyChanges()
        {
            base.ApplyChanges();
            QualitySettings.SetQualityLevel((int)originalValue);
        }

        public override void InitializeOptions()
        {
            options = new QualityEnum[6];
            for (int i = 0; i < options.Length; i++)
            {
                if (QualitySettings.GetQualityLevel() == i)
                {
                    originalValueIndex = selectedValueIndex = i;
                }
                options[i] = (QualityEnum)i;
            }
            QualitySettings.SetQualityLevel((int)originalValue);
        }

        protected override void LoadFromPlayerPrefs()
        {
            base.LoadFromPlayerPrefs();
            if (originalValueIndex == -1)
            {
                originalValueIndex = selectedValueIndex = QualitySettings.GetQualityLevel();
            }
        }
    }
}
