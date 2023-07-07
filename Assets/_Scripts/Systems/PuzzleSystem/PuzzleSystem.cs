using System;
using UnityEngine;

namespace MrLule.Systems.PuzzleSystem
{
    [Serializable]
    public class PuzzleRequirements
    {
        public bool requiredState;
        public PuzzleComponent puzzleComponent;
    }

    public class PuzzleSystem : MonoBehaviour
    {
        [SerializeField] protected PuzzleRequirements[] puzzleRequirements;

        protected bool isPuzzleSolved = false;

        public virtual void Interact()
        {
            if (CheckPuzzleRequirements())
            {
                Solve();
            }
        }

        protected virtual void Solve()
        {
            isPuzzleSolved = true;
        }

        private bool CheckPuzzleRequirements()
        {
            foreach (PuzzleRequirements puzzleRequirement in puzzleRequirements)
            {
                bool activeState = puzzleRequirement.puzzleComponent.isActive;
                if (activeState != puzzleRequirement.requiredState)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
