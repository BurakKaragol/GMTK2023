using Cinemachine;
using UnityEngine;

namespace MrLule.Managers.GameMan
{
    public class GameManager : Manager
    {
        [SerializeField]
        private Transform respawnPoint;
        [SerializeField]
        private GameObject player;
        [SerializeField]
        private float respawnTime;

        private float respawnTimeStart;

        private bool respawn;

        private CinemachineVirtualCamera cinemachineCamera;

        private void Start()
        {
            cinemachineCamera = GameObject.Find("Player Camera").GetComponent<CinemachineVirtualCamera>();
        }

        private void Update()
        {
            CheckRespawn();
        }
        public void Respawn()
        {
            respawnTimeStart = Time.time;
            respawn = true;
        }

        private void CheckRespawn()
        {
            if (Time.time >= respawnTimeStart + respawnTime && respawn)
            {
                var playerTemp = Instantiate(player, respawnPoint);
                cinemachineCamera.m_Follow = playerTemp.transform;
                respawn = false;
            }
        }

        public override void OnEnable()
        {
            gameManager = this;
        }

        public override void OnDisable()
        {
            gameManager = null;
        }
    }
}
