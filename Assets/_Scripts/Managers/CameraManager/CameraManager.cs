using Cinemachine;
using System;
using UnityEngine;

namespace MrLule.Managers.CameraMan
{
    [Serializable]
    public class CameraData
    {
        public string cameraName;
        public CinemachineVirtualCamera camera;
    }

    public class CameraManager : Manager
    {
        [SerializeField] private CinemachineBrain mainCamera;
        [SerializeField] private CinemachineBlendDefinition defaultBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.EaseInOut, 2f);
        [SerializeField] private CinemachineImpulseSource impulseSource;
        [SerializeField] private CameraData[] cameras;

        public string activeCameraName { get; private set; }
        public CinemachineVirtualCamera activeCamera { get; private set; }

        public void ShakeCamera(float force = 0.5f, Vector3 velocity = default)
        {
            velocity = velocity == default ? new Vector3(1, 1, 0) : velocity;
            impulseSource.m_DefaultVelocity = velocity;
            impulseSource.GenerateImpulseWithForce(force);
        }

        public void SetActiveCamera(string name)
        {
            SetActiveCamera(name, defaultBlend);
        }

        public void SetActiveCamera(string name, CinemachineBlendDefinition.Style style)
        {
            SetActiveCamera(name, new CinemachineBlendDefinition(style, defaultBlend.BlendTime));
        }

        public void SetActiveCamera(string name, float time)
        {
            SetActiveCamera(name, new CinemachineBlendDefinition(defaultBlend.m_Style, time));
        }

        public void SetActiveCamera(string name, CinemachineBlendDefinition.Style style, float time)
        {
            SetActiveCamera(name, new CinemachineBlendDefinition(style, time));
        }

        public void SetActiveCamera(string name, CinemachineBlendDefinition blend)
        {
            if (TryGetCamera(name, out CameraData cameraData))
            {
                mainCamera.m_DefaultBlend = blend;
                SetAllPiorityZero();
                cameraData.camera.Priority = 10;
                activeCameraName = name;
                activeCamera = cameraData.camera;
            }
        }

        private void SetAllPiorityZero()
        {
            foreach (var camera in cameras)
            {
                camera.camera.Priority = 0;
            }
        }

        private CameraData GetCamera(string name)
        {
            CameraData camera = Array.Find(cameras, camera => camera.cameraName == name);
            return camera;
        }

        private bool TryGetCamera(string name, out CameraData cameraData)
        {
            cameraData = GetCamera(name);
            if (cameraData == null)
            {
                return false;
            }
            return true;
        }

        public override void OnEnable()
        {
            cameraManager = this;
        }

        public override void OnDisable()
        {
            cameraManager = null;
        }
    }
}
