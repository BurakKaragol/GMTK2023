using System;
using UnityEngine;

namespace MrLule.Managers.AchievemenMan
{
    [CreateAssetMenu(fileName = "Achievement", menuName = "Achievement Manager/Achievement")]
    public class AchievementDataSO : ScriptableObject
    {
        public string achievementName;
        public string title;
        [TextArea(3, 10)]
        public string description;
        public Sprite sprite;
        public bool isUnlocked;
    }
}
