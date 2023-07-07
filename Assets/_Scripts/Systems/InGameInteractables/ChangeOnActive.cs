using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrLule.Systems.InGameInteractables
{
    public class ChangeOnActive : MonoBehaviour
    {
        [SerializeField] private GameObject deactiveState;
        [SerializeField] private GameObject activeState;

        [SerializeField] private bool useAnimation;
        [SerializeField] private string animationStateName;

        private bool isActive = false;
        private Animator animator;

        private void Start()
        {
            TryGetComponent<Animator>(out animator);
        }

        public void ToggleState()
        {
            isActive = !isActive;
            UpdateState();
        }

        public void SetState(bool state)
        {
            isActive = state;
            UpdateState();
        }

        public void UpdateState()
        {
            if (useAnimation)
            {
                animator?.SetBool(animationStateName, isActive);
            }
            else
            {
                deactiveState.SetActive(isActive);
                activeState.SetActive(isActive);
            }
        }
    }
}
