using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using MrLule.Interfaces;

namespace MrLule.Systems.InteractionSystem
{
    [RequireComponent(typeof(IInteractable))]
    [RequireComponent(typeof(Collider))]
    public class AreaCheckInteractor3D : MonoBehaviour
    {
        [Header("General:")]
        [TagField]
        [SerializeField] private string compareTag;
        [TagField]
        [SerializeField] private string altCompareTag;

        [Header("Interaction:")]
        [SerializeField] private bool useKeyPress = true;
        [SerializeField] private KeyCode activationKey = KeyCode.E;
        [Tooltip("User is required to press the specified key the amount specified. If not checked keypress is enough.")]
        [SerializeField] private bool activateOvertime = false;
        [SerializeField] private float waitTime = 3f;

        [Header("Visual:")]
        [SerializeField] private bool useVisual;
        [SerializeField] private GameObject interactionKey;
        [SerializeField] private Slider percentSlider;

        private IInteractable interactable;

        private bool onArea = false;
        private bool isPressingKey = false;
        private float pressStartTime = float.PositiveInfinity;
        private float areaEnterTime = float.PositiveInfinity;

        private void Start()
        {
            interactable = GetComponent<IInteractable>();
        }

        private void Update()
        {
            if (!onArea)
            {
                return;
            }

            if (useKeyPress)
            {
                if (activateOvertime)
                {
                    if (Input.GetKeyDown(activationKey))
                    {
                        isPressingKey = true;
                        pressStartTime = Time.time;
                    }
                    if (Input.GetKeyDown(activationKey))
                    {
                        isPressingKey = false;
                        pressStartTime = 0;
                    }
                    if (isPressingKey)
                    {
                        interactable.InAreaPercent((Time.time - pressStartTime) / waitTime);
                        if (Time.time >= pressStartTime + waitTime)
                        {
                            interactable.Interact();
                        }
                    }
                }
                else
                {
                    if (Input.GetKeyDown(activationKey))
                    {
                        interactable.Interact();
                    }
                }
            }
            else
            {
                if (activateOvertime)
                {
                    interactable.InAreaPercent((Time.time - areaEnterTime) / waitTime);
                    if (Time.time >= areaEnterTime + waitTime)
                    {
                        interactable.Interact();
                    }
                }
                else
                {
                    interactable.Interact();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(compareTag) || other.CompareTag(altCompareTag))
            {
                onArea = true;
                areaEnterTime = Time.time;
                if (useVisual)
                {
                    interactionKey.SetActive(true);
                }
                interactable?.AreaState(onArea);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(compareTag) || other.CompareTag(altCompareTag))
            {
                onArea = false;
                areaEnterTime = float.PositiveInfinity;
                if (useVisual)
                {
                    interactionKey.SetActive(false);
                }
                interactable?.AreaState(onArea);
            }
        }
    }
}
