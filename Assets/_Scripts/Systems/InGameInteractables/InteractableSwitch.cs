using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MrLule.Interfaces;
using MrLule.Systems.PuzzleSystem;

namespace MrLule.Systems.InGameInteractables
{
    [RequireComponent(typeof(ChangeOnActive))]
    public class InteractableSwitch : MonoBehaviour, IInteractable
    {
        [SerializeField] private UnityEvent onTrigger;

        private bool isActive = false;
        private ChangeOnActive changeOnActive;
        private PuzzleComponent puzzleComponent;

        private void Start()
        {
            changeOnActive = GetComponent<ChangeOnActive>();
            puzzleComponent = GetComponent<PuzzleComponent>();
        }

        public void AreaState(bool onArea)
        {

        }

        public void InAreaPercent(float percent)
        {

        }

        public void Interact()
        {
            isActive = !isActive;
            puzzleComponent.SetState(isActive);
            changeOnActive.SetState(isActive);
        }
    }
}
