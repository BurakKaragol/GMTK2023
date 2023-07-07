using Cinemachine;
using MrLule.Managers.CameraMan;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LookatCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private string mainCameraName = "Main";
    [SerializeField] private float transitionTime = 2f;
    [SerializeField] private float waitTime = 5f;
    [SerializeField] private UnityEvent onCameraFocused;

    private float startTime = 0f;
    private bool isFocusing = false;
    private bool isFocused = false;

    private void Update()
    {
        if (!isFocusing)
        {
            return;
        }

        if (Time.time >= startTime + transitionTime && !isFocused)
        {
            isFocused = true;
            onCameraFocused?.Invoke();
        }

        if (Time.time >= startTime + transitionTime + waitTime)
        {
            isFocusing = false;
            FindObjectOfType<CameraManager>().SetActiveCamera(mainCameraName);
        }
    }

    public void OnCameraActivatedEvent(ICinemachineCamera to, ICinemachineCamera from)
    {
        if (to.Name == virtualCamera.Name)
        {
            startTime = Time.time;
            isFocusing = true;
        }
    }

    public void LookAtPosition(Vector3 position)
    {
        transform.position = position;
    }
}
