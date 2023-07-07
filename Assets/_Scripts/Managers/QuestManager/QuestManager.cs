using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MrLule.Managers.QuestMan
{
    /// <summary>
    /// REMAKE
    /// using scaleable ui and custom scripts that adds itselft to the player
    /// and when the player continues the quest checks if the requirement satisfied.
    /// use events in player to trigger and those scripts and also analytics manager
    /// can listen to this event
    /// </summary>
    public class QuestManager : Manager
    {
        [Header("UI Elements:")]
        [Space(5)]
        [SerializeField] private GameObject questsPanel;
        [SerializeField] private TextMeshProUGUI questTitleText;
        [SerializeField] private TextMeshProUGUI questDescriptionText;
        [SerializeField] private TextMeshProUGUI questGoalText;
        [SerializeField] private TextMeshProUGUI questGoalRequiredText;
        [SerializeField] private TextMeshProUGUI questExperienceText;
        [SerializeField] private TextMeshProUGUI questMoneyText;
        [SerializeField] private Button claimButton;

        [Header("Animations:")]
        [Space(5)]
        [SerializeField] private bool animate = true;
        [SerializeField] private Animator questPanelAnimator;
        [SerializeField] private string questPanelBoolTrigger;
        [SerializeField] private float animationTime = 1f;

        private Quest[] quests;
        private Quest[] completedQuests = new Quest[0];
        private int index = 0;

        public void OpenQuestsPanel()
        {
            ImportQuests(quests[index]);
            StartCoroutine(OpenWindow());
        }

        public void NextQuest()
        {
            index = index + 1 >= quests.Length ? 0 : index + 1;
            ImportQuests(quests[index]);
        }

        public void AddNewQuest(Quest quest)
        {
            AddQuest(quest);
        }

        public bool IsQuestCompleted(Quest quest)
        {
            bool found = false;
            foreach (Quest q in quests)
            {
                if (q == quest)
                {
                    found = true;
                    break;
                }
                else
                {
                    found = false;
                }
            }
            return found;
        }

        public void ClaimQuestReward()
        {
            CompleteQuest();
            // CLAIMING THE REWARD
        }

        public void PreviousQuest()
        {
            index = index - 1 <= -1 ? quests.Length - 1 : index - 1;
            ImportQuests(quests[index]);
        }

        public void CloseQuestsPanel()
        {
            StartCoroutine(CloseWindow());
        }

        private void AddQuest(Quest newQuest)
        {
            Quest[] temp = new Quest[quests.Length + 1];
            for (int i = 0; i < quests.Length; i++)
            {
                temp[i] = quests[i];
            }
            temp[quests.Length] = newQuest;
            quests = new Quest[temp.Length];
            for (int i = 0; i < temp.Length; i++)
            {
                quests[i] = temp[i];
            }
        }

        private void CompleteQuest()
        {
            Quest[] temp = new Quest[quests.Length];
            Quest[] completed = new Quest[quests.Length];
            for (int i = 0; i < completedQuests.Length; i++)
            {
                completed[i] = completedQuests[i];
            }
            completedQuests = new Quest[completed.Length + 1];
            for (int i = 0; i < completed.Length; i++)
            {
                completedQuests[i] = completed[i];
            }
            completedQuests[completedQuests.Length - 1] = quests[index];
            for (int i = 0; i < quests.Length; i++)
            {
                temp[i] = quests[i];
            }
            completedQuests[completedQuests.Length - 1] = quests[index];
            quests[index] = null;
            quests = new Quest[temp.Length - 1];
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i] == null)
                {
                    continue;
                }

                quests[i] = temp[i];
            }
        }

        private void ImportQuests(Quest quest)
        {
            questTitleText.SetText(quest.title);
            questDescriptionText.SetText(quest.description);
            questGoalText.SetText(quest.goal.goalType.ToString());
            questGoalRequiredText.SetText(quest.goal.requiredAmount.ToString());
            questExperienceText.SetText(quest.experienceReward.ToString());
            questMoneyText.SetText(quest.moneyReward.ToString());
            claimButton.interactable = quest.goal.IsReached();
        }

        IEnumerator OpenWindow()
        {
            questsPanel.SetActive(true);
            if (animate)
            {
                questPanelAnimator.SetBool(questPanelBoolTrigger, true);
                yield return new WaitForSeconds(animationTime);
            }
        }

        IEnumerator CloseWindow()
        {
            if (animate)
            {
                questPanelAnimator.SetBool(questPanelBoolTrigger, false);
                yield return new WaitForSeconds(animationTime);
            }
            questsPanel.SetActive(false);
        }

        public override void OnEnable()
        {
            questManager = this;
        }

        public override void OnDisable()
        {
            questManager = null;
        }
    }
}
