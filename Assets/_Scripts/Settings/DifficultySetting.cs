using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using MrLule.Managers.PlayerPrefsMan;
using MrLule.Managers.DifficultyMan;

namespace MrLule.Settings
{
    [Serializable]
    public enum DifficultyEnum
    {
        Easiest,
        Easy,
        Normal,
        Hard,
        Hardest
    }

    public class DifficultySetting : SettingData<DifficultyEnum>
    {
        public override void ApplyChanges()
        {
            base.ApplyChanges();
        }

        public override DifficultyEnum GetOptionType(int index)
        {
            throw new NotImplementedException();
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
