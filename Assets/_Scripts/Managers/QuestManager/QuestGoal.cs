using MrLule.General;

namespace MrLule.Managers.QuestMan
{
    public enum GoalType
    {
        Kill,
        Collect,
        Reach
    }

    [System.Serializable]
    public class QuestGoal
    {
        public GoalType goalType;

        public int currentAmount;
        public int requiredAmount;

        public bool IsReached()
        {
            return currentAmount >= requiredAmount;
        }

        public void KillEnemy()
        {
            if (goalType == GoalType.Kill)
            {
                currentAmount++;
            }
            else
            {
                Debugger.LogWarning(this.GetType().ToString(), "Cannot kill (Wrong goal type)");
            }
        }

        public void CollectItem()
        {
            if (goalType == GoalType.Collect)
            {
                currentAmount++;
            }
            else
            {
                Debugger.LogWarning(this.GetType().ToString(), "Cannot collect (Wrong goal type)");
            }
        }

        public void ReachGoal()
        {
            if (goalType == GoalType.Reach)
            {
                currentAmount++;
            }
            else
            {
                Debugger.LogWarning(this.GetType().ToString(), "Cannot reach (Wrong goal type)");
            }
        }
    }
}