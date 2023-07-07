using MrLule.Managers.PlayerPrefsMan;
using System;
using UnityEngine;

namespace MrLule.Managers.AchievemenMan
{
    public class AchievementManager : Manager
    {
        [SerializeField] private AchievementDataSO[] achievements;

        private void Start()
        {
            GetAchievementStates();
        }

        private void GetAchievementStates()
        {
            for (int i = 0; i < achievements.Length; i++)
            {
                bool achievementState = PlayerPrefsManager.GetBool($"achievements_{i}");
                achievements[i].isUnlocked = achievementState;
            }
        }

        private void UpdateAchievements()
        {
            for (int i = 0; i < achievements.Length; i++)
            {
                if (achievements[i].isUnlocked)
                {
                    PlayerPrefsManager.SetBool($"achievements_{i}", true);
                }
                else
                {
                    PlayerPrefsManager.SetBool($"achievements_{i}", false);
                }
            }
        }

        public void UnlockAchievement(string name)
        {
            if (TryGetAchievement(name, out AchievementDataSO achievementData))
            {
                achievementData.isUnlocked = true;
                UpdateAchievements();
                messageManager.NewMessage(achievementData.title, achievementData.description, achievementData.sprite);
            }
        }

        private AchievementDataSO GetAchievementData(string name)
        {
            AchievementDataSO achievement = Array.Find(achievements, achievement => achievement.achievementName == name);
            return achievement;
        }

        private bool TryGetAchievement(string name, out AchievementDataSO achievementData)
        {
            achievementData = GetAchievementData(name);
            if (achievementData == null)
            {
                return false;
            }
            return true;
        }

        public override void OnEnable()
        {
            achievementManager = this;
        }

        public override void OnDisable()
        {
            achievementManager = null;
        }
    }
}
