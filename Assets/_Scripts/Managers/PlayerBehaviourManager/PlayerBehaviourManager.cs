using UnityEngine;

namespace MrLule.Managers.PlayerBehaviourMan
{
    public class PlayerBehaviourManager : Manager
    {
        //Player controller
        /// <summary>
        /// control all player ınteractıons
        /// 
        /// </summary>
        //[SerializeField] 

        public bool canControl = true;

        public void StopPlayerInput()
        {
            canControl = false;
        }

        public void AllowPlayerInput()
        {
            canControl = true;
        }

        public override void OnEnable()
        {
            playerBehaviourManager = this;
        }

        public override void OnDisable()
        {
            playerBehaviourManager = null;
        }
    }
}
