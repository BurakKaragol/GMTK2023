using UnityEngine;
using MrLule.Interfaces;
using MrLule.Managers.PlayerPrefsMan;

namespace MrLule.Managers.CheckpointMan
{
    public class Checkpoint : MonoBehaviour, IInteractable
    {
        [Header("General:")]
        [SerializeField] private int checkpointIndex;
        [SerializeField] private Transform spawnPoint;
        public bool isActivated = false;

        [Header("Animation:")]
        [SerializeField] private bool useAnimation;
        [SerializeField] private Animator animator;
        [SerializeField] private string animatorOnAreaBool;
        [SerializeField] private string animatorIsActiveBool;
        [SerializeField] private string animatorAreaTimeFloat;

        private CheckpointManager checkpointManager;

        private bool onArea = false;
        private float areaPercent = 0f;

        private void Awake()
        {
            checkpointManager = FindObjectOfType<CheckpointManager>();
            if (!isActivated)
            {
                isActivated = PlayerPrefsManager.GetBool($"Checkpoint_{checkpointIndex}", false);
            }
            areaPercent = isActivated ? 1f : 0f;
            checkpointManager.SetCheckpointState(this, isActivated);
        }

        public Transform GetSpawnPoint()
        {
            return spawnPoint;
        }

        public void Interact()
        {
            checkpointManager.ActivateCheckpoint(this);
            isActivated = !isActivated;
            if (!useAnimation)
            {
                return;
            }
            animator.SetBool(animatorIsActiveBool, isActivated);
            checkpointManager.SetCheckpointState(this, isActivated);
        }

        public void AreaState(bool onArea)
        {
            this.onArea = onArea;
            if (!useAnimation)
            {
                return;
            }
            animator.SetBool(animatorOnAreaBool, this.onArea);
        }

        public void InAreaPercent(float percent)
        {
            areaPercent = percent;
            if (!useAnimation)
            {
                return;
            }
            animator.SetFloat(animatorAreaTimeFloat, areaPercent);
        }
    }
}
