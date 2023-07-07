using UnityEngine;

namespace MrLule.Managers.QuestMan
{
    [System.Serializable]
    public class Quest
    {
        public bool isActive;

        public string title;
        [TextArea(3, 10)]
        public string description;
        public int experienceReward;
        public int moneyReward;

        public QuestGoal goal;
    }
}
