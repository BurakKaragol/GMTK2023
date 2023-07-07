using UnityEngine;
using UnityEngine.InputSystem;

namespace MrLule.Settings
{
    public class InputSetting : SettingData<int>
    {
        public override void ApplyChanges()
        {
            base.ApplyChanges();
        }

        public override int GetOptionType(int index)
        {
            throw new System.NotImplementedException();
        }

        public override void InitializeOptions()
        {
            throw new System.NotImplementedException();
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
