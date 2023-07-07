using MrLule.General;
using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace MrLule.Settings
{
    public class AntiAliasingSetting : SettingData<AntialiasingMode>
    {
        public override void ApplyChanges()
        {
            base.ApplyChanges();
            QualitySettings.antiAliasing = GetAntiAliasing((int)originalValue);
        }

        private int GetAntiAliasing(int mode)
        {
            switch (mode)
            {
                case 0:
                    return 0;
                case 1:
                    return 2;
                case 2:
                    return 4;
                case 3:
                    return 8;
                default:
                    return 0;
            }
        }

        public override AntialiasingMode GetOptionType(int index)
        {
            return (AntialiasingMode)index;
        }

        public override void InitializeOptions()
        {
            options = (AntialiasingMode[])System.Enum.GetValues(typeof(AntialiasingMode));
            originalValue = selectedValue = (AntialiasingMode)QualitySettings.antiAliasing;
            originalValueIndex = selectedValueIndex = (int)originalValue;
        }

        protected override void LoadFromPlayerPrefs()
        {
            base.LoadFromPlayerPrefs();
            if (originalValueIndex == -1)
            {
                originalValue = selectedValue = (AntialiasingMode)QualitySettings.antiAliasing;
            }
        }
    }
}
