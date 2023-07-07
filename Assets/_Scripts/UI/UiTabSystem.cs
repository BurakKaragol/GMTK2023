using MrLule.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MrLule.UI
{
    public class UiTabSystem : MonoBehaviour
    {
        [Header("General:")]
        [SerializeField] private bool resetPositionOnStart = true;
        [SerializeField] private GameObject[] panels;
        [SerializeField] private int selectedPanelIndex;

        [Header("Navigation:")]
        [Tooltip("Allow players to change between first-last panels")]
        [SerializeField] private bool continious = false;
        [SerializeField] private bool useNavigation = true;
        [SerializeField] private KeyCode nextKey;
        [SerializeField] private KeyCode previousKey;

        [Header("Tabs:")]
        [SerializeField] private Button previousTabButton;
        [SerializeField] private Button nextTabButton;
        [SerializeField] private Button[] tabButtons;

        private void Start()
        {
            for (int i = 0; i < panels.Length; i++)
            {
                panels[i].SetActive(i == selectedPanelIndex);

                if (resetPositionOnStart)
                {
                    panels[i].GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                }
            }

            if (!continious)
            {
                previousTabButton.interactable = selectedPanelIndex != 0;
                nextTabButton.interactable = selectedPanelIndex != panels.Length - 1;
            }
            else
            {
                previousTabButton.interactable = true;
                nextTabButton.interactable = true;
            }
        }

        private void LateUpdate()
        {
            if (!useNavigation)
            {
                return;
            }

            if (Input.GetKeyDown(nextKey))
            {
                NextTab();
            }
            else if (Input.GetKeyDown(previousKey))
            {
                PreviousTab();
            }
        }

        public void OpenTab(int index)
        {
            if (index < panels.Length)
            {
                selectedPanelIndex = index;
                SetActiveTab();
            }
            else
            {
                Debugger.LogWarning(this.GetType().ToString(), "Cannot open panel (Panel index is out of bounds.)");
            }
        }

        public void NextTab()
        {
            selectedPanelIndex = selectedPanelIndex + 1 >= panels.Length ? 0 : selectedPanelIndex + 1;
            SetActiveTab();
        }

        public void PreviousTab()
        {
            selectedPanelIndex = selectedPanelIndex - 1 < 0 ? panels.Length - 1 : selectedPanelIndex - 1;
            SetActiveTab();
        }

        private void SetActiveTab()
        {
            for (int i = 0; i < panels.Length; i++)
            {
                if (i == selectedPanelIndex)
                {
                    panels[i].SetActive(true);
                    tabButtons[i].interactable = false;
                }
                else
                {
                    panels[i].SetActive(false);
                    tabButtons[i].interactable = true;
                }
            }
            if (!continious)
            {
                previousTabButton.interactable = selectedPanelIndex != 0;
                nextTabButton.interactable = selectedPanelIndex != panels.Length - 1;
            }
            else
            {
                previousTabButton.interactable = true;
                nextTabButton.interactable = true;
            }
        }
    }
}
