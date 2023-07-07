using System;
using UnityEngine.Localization;

namespace MrLule.Settings
{
    public class LanguageSetting : SettingData<Locale>
    {
        public override void ApplyChanges()
        {
            base.ApplyChanges();
        }

        public override Locale GetOptionType(int index)
        {
            throw new System.NotImplementedException();
        }

        public override void InitializeOptions()
        {
            throw new NotImplementedException();
        }

        public override void SetOption(int index)
        {
            base.SetOption(index);
        }

        protected override void InitializeVisuals()
        {
            base.InitializeVisuals();
        }
    }
}
