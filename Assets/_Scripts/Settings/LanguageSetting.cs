using System;
using System.Runtime.CompilerServices;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace MrLule.Settings
{
    public class LanguageSetting : SettingData<Locale>
    {
        public LocalizationSettings localizationSettings;

        public override void ApplyChanges()
        {
            base.ApplyChanges();
            localizationSettings.SetSelectedLocale(options[originalValueIndex]);
        }

        public override Locale GetOptionType(int index)
        {
            return options[index];
        }

        public override void InitializeOptions()
        {
            options = LocalizationSettings.AvailableLocales.Locales.ToArray();
            for (int i = 0; i < options.Length; i++)
            {
                if (options[i].LocaleName == strings[i])
                {
                    originalValueIndex = selectedValueIndex = i;
                    break;
                }
            }
            localizationSettings.SetSelectedLocale(options[originalValueIndex]);
        }

        protected override void LoadFromPlayerPrefs()
        {
            base.LoadFromPlayerPrefs();
            if (originalValueIndex == -1)
            {
                originalValue = selectedValue = LocalizationSettings.AvailableLocales.Locales.ToArray()[1];
            }
        }
    }
}
