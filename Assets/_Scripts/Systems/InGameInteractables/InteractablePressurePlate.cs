using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MrLule.Interfaces;
using MrLule.Systems.PuzzleSystem;

namespace MrLule.Systems.InGameInteractables
{
    [RequireComponent(typeof(ChangeOnActive))]
    public class InteractablePressurePlate : MonoBehaviour, IInteractable
    {
        [SerializeField] private UnityEvent onTrigger;

        public bool isActive = false;

        private bool onArea = false;
        private ChangeOnActive changeOnActive;
        private PuzzleComponent puzzleComponent;

        private void Start()
        {
            changeOnActive = GetComponent<ChangeOnActive>();
            puzzleComponent = GetComponent<PuzzleComponent>();
        }

        public void AreaState(bool onArea)
        {
            this.onArea = onArea;
            changeOnActive.SetState(onArea);
            if (!onArea)
            {
                isActive = false;
                puzzleComponent.SetState(isActive);
            }
        }

        public void InAreaPercent(float percent)
        {

        }

        public void Interact()
        {
            isActive = true;
            puzzleComponent.SetState(isActive);
            onTrigger?.Invoke();
        }
    }
}
