using UnityEngine;
using UnityEngine.Events;
using MrLule.Interfaces;

namespace MrLule.Systems.InGameInteractables
{
    public class InteractableButton : MonoBehaviour, IInteractable
    {
        [SerializeField] private UnityEvent onTrigger;

        private ChangeOnActive changeOnActive;

        private void Start()
        {
            changeOnActive = GetComponent<ChangeOnActive>();
        }

        public void AreaState(bool onArea)
        {
            changeOnActive.SetState(onArea);
        }

        public void InAreaPercent(float percent)
        {

        }

        public void Interact()
        {
            onTrigger?.Invoke();
        }
    }
}
