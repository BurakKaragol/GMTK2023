using MrLule.Interfaces;
using UnityEngine;

namespace MrLule.Managers.DialogueMan
{
    public class DialogueTrigger : MonoBehaviour, IInteractable
    {
        [Header("Dialogue:")]
        [SerializeField] DialogueNodeGraph dialogue;

        private bool objectInArea = false;

        public void TriggerDialogue()
        {
            FindObjectOfType<DialogueManager>().ImportAndStartDialogue(dialogue);
        }

        public void AreaState(bool onArea)
        {
            objectInArea = onArea;
        }

        public void Interact()
        {
            TriggerDialogue();
        }

        public void InAreaPercent(float percent)
        {
            throw new System.NotImplementedException();
        }
    }
}
