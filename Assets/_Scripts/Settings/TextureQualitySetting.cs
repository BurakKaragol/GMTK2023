using UnityEngine;

namespace MrLule.Settings
{
    public class TextureQualitySetting : SettingData<AnisotropicFiltering>
    {
        public override void ApplyChanges()
        {
            base.ApplyChanges();
            QualitySettings.anisotropicFiltering = originalValue;
        }

        public override void InitializeOptions()
        {
            options = (AnisotropicFiltering[])System.Enum.GetValues(typeof(AnisotropicFiltering));
            originalValue = selectedValue = QualitySettings.anisotropicFiltering;
        }

        protected override void LoadFromPlayerPrefs()
        {
            base.LoadFromPlayerPrefs();
            if (originalValueIndex == -1)
            {
                originalValue = selectedValue = QualitySettings.anisotropicFiltering;
                originalValueIndex = selectedValueIndex = ActiveIndex();
            }
        }

        private int ActiveIndex()
        {
            switch (originalValue)
            {
                case AnisotropicFiltering.Disable:
                    return 0;
                case AnisotropicFiltering.Enable:
                    return 1;
                case AnisotropicFiltering.ForceEnable:
                    return 2;
                default:
                    return 0;
            }
        }
    }
}
