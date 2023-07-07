using MrLule.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

namespace MrLule.Managers.DifficultyMan
{
    public class DifficultyManager : Manager
    {
        public static DifficultyManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                if (instance == this)
                {
                    return;
                }
                else
                {
                    Destroy(this);
                }
            }
        }

        public void SetDifficulty(int difficultyIndex)
        {

        }

        public override void OnEnable()
        {
            difficultyManager = this;
        }

        public override void OnDisable()
        {
            difficultyManager = null;
        }
    }
}
