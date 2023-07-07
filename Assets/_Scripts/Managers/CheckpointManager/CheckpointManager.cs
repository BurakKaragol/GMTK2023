using System;
using UnityEngine;

namespace MrLule.Managers.CheckpointMan
{
    [Serializable]
    public class CheckpointData
    {
        public Checkpoint checkpoint;
        public bool state;
    }

    public class CheckpointManager : Manager
    {
        [SerializeField] private CheckpointData[] checkpoints;

        private int lastActivatedCheckpointIndex = 0;

        public void SetCheckpointState(Checkpoint checkpoint, bool state)
        {
            for (int i = 0; i < checkpoints.Length; i++)
            {
                if (checkpoints[i].checkpoint == checkpoint)
                {
                    checkpoints[i].state = state;
                }
            }
        }

        public void ActivateCheckpoint(Checkpoint checkpoint)
        {
            for (int i = 0; i < checkpoints.Length; i++)
            {
                if (checkpoints[i].checkpoint == checkpoint)
                {
                    checkpoints[i].state = checkpoint.isActivated;
                    lastActivatedCheckpointIndex = i;
                }
            }
        }

        public Transform GetLastSpawnPosition()
        {
            return checkpoints[lastActivatedCheckpointIndex].checkpoint.GetSpawnPoint();
        }

        public override void OnEnable()
        {
            checkpointManager = this;
        }

        public override void OnDisable()
        {
            checkpointManager = null;
        }
    }
}
