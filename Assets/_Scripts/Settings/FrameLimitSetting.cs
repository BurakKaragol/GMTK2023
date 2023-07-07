using MrLule.Managers.PlayerPrefsMan;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MrLule.Settings
{
    [Serializable]
    public enum FrameLimitEnum
    {
        FPSNoLimit = 0,
        FPS30 = 30,
        FPS60 = 60,
        FPS90 = 90,
        FPS120 = 120,
        FPS144 = 144,
        FPS240 = 240
    }

    public class FrameLimitSetting : SettingData<FrameLimitEnum>
    {
        public override void ApplyChanges()
        {
            base.ApplyChanges();
        }

        public override FrameLimitEnum GetOptionType(int index)
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
