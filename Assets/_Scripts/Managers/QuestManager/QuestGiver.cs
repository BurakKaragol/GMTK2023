using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MrLule.General;

namespace MrLule.Managers.QuestMan
{
    public class QuestGiver : MonoBehaviour
    {
        [Header("Quests:")]
        [Space(5)]
        [SerializeField] private Quest[] quests;

        [Header("UI Elements:")]
        [Space(5)]
        [SerializeField] private GameObject questWindow;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private TextMeshProUGUI experienceText;
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private Button acceptButton;
        [SerializeField] private Button cancelButton;

        [Header("Animations:")]
        [Space(5)]
        [SerializeField] private bool animate = true;
        [SerializeField] private Animator questWindowAnimator;
        [SerializeField] private string questWindowBoolTrigger;
        [SerializeField] private float animationTime = 1f;

        private int progressIndex = -1;
        private int index = 0;

        public void OpenQuestWindow()
        {
            ImportQuest();
            StartCoroutine(OpenWindow());
        }

        public void NextQuest()
        {
            index = index + 1 >= quests.Length ? 0 : index + 1;
            ImportQuest();
        }

        public void AcceptQuest()
        {
            quests[index].isActive = true;
            QuestManager questManager = FindObjectOfType<QuestManager>();
            if (questManager.IsQuestCompleted(quests[index - 1 < 0 ? 0 : index - 1]))
            {
                questManager.AddNewQuest(quests[index]);
            }
            else
            {
                Debugger.LogWarning(this.GetType().ToString(), "Cannot take new quest (Previous quest is not completede)");
            }
            progressIndex = progressIndex + 1 >= quests.Length ? progressIndex : progressIndex + 1;
            CloseQuestWindow();
        }

        public void CancelQuest()
        {
            quests[index].isActive = false;
        }

        public void PreviousQuest()
        {
            index = index - 1 <= -1 ? quests.Length - 1 : index - 1;
            ImportQuest();
        }

        public void CloseQuestWindow()
        {
            StartCoroutine(CloseWindow());
        }

        private void ImportQuest()
        {
            if (index < progressIndex)
            {
                acceptButton.interactable = false;
                cancelButton.interactable = false;
            }
            else
            {
                acceptButton.interactable = true;
                cancelButton.interactable = true;
            }

            titleText.SetText(quests[index].title);
            descriptionText.SetText(quests[index].description);
            experienceText.SetText(quests[index].experienceReward.ToString());
            moneyText.SetText(quests[index].moneyReward.ToString());
        }

        IEnumerator OpenWindow()
        {
            questWindow.SetActive(true);
            if (animate)
            {
                questWindowAnimator.SetBool(questWindowBoolTrigger, true);
                yield return new WaitForSeconds(animationTime);
            }
        }

        IEnumerator CloseWindow()
        {
            if (animate)
            {
                questWindowAnimator.SetBool(questWindowBoolTrigger, false);
                yield return new WaitForSeconds(animationTime);
            }
            questWindow.SetActive(false);
        }
    }
}
