using MrLule.General;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MrLule.Managers.InputMan
{
    public class InputManager : Manager
    {

        public override void OnEnable()
        {
            inputManager = this;
        }

        public override void OnDisable()
        {
            inputManager = null;
        }
    }
}
