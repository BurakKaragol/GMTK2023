using Cinemachine;
using MrLule.Interfaces;
using UnityEngine;

namespace MrLule.Managers.CameraMan
{
    public class CameraTrigger : MonoBehaviour, IInteractable
    {
        [Header("General:")]
        [SerializeField] private bool triggerOnArea = false;
        [SerializeField] private string cameraName;
        [SerializeField] private CinemachineBlendDefinition blend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.EaseInOut, 2f);

        private CameraManager cameraManager;
        private string previousCameraName;
        private bool isActivated = false;

        private void Start()
        {
            cameraManager = FindObjectOfType<CameraManager>();
        }

        public void AreaState(bool onArea)
        {
            if (!triggerOnArea)
            {
                return;
            }

            if (onArea)
            {
                SetCamera();
            }
            else
            {
                SetLastCamera();
            }
        }

        public void SetCamera()
        {
            previousCameraName = cameraManager.activeCameraName;
            cameraManager.SetActiveCamera(cameraName, blend);
            isActivated = true;
        }

        public void SetLastCamera()
        {
            cameraManager.SetActiveCamera(previousCameraName, blend);
            isActivated = false;
        }

        public void InAreaPercent(float percent)
        {
            throw new System.NotImplementedException();
        }

        public void Interact()
        {
            if (isActivated)
            {
                SetLastCamera();
            }
            else
            {
                SetCamera();
            }
        }
    }
}
