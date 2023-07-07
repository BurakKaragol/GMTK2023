using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MrLule.Managers.MainMenuMan
{
    [Serializable]
    public class PanelData
    {
        public string panelName;
        public MainMenuPanel panel;
    }

    public class MainMenuManager : Manager
    {
        [SerializeField] private List<PanelData> panels = new List<PanelData>();

        public string openPanelName;
        public MainMenuPanel openPanel;

        public void OpenPanel(string panelName)
        {
            foreach (var panel in panels)
            {
                if (panel.panelName == panelName)
                {
                    panel.panel.ShowPanel();
                    openPanel = panel.panel;
                    openPanelName = panelName;
                }
                else
                {
                    panel.panel.HidePanel();
                }
            }
        }

        public void ReturnActiveButton(GameObject button)
        {
            EventSystem.current.SetSelectedGameObject(button);
        }

        public override void OnEnable()
        {
            mainMenuManager = this;
        }

        public override void OnDisable()
        {
            mainMenuManager = null;
        }

        public void OpenURL(string url)
        {
            Application.OpenURL(url);
        }

        public void ExitGame()
        {
            Debug.Log("Application Quit");
            Application.Quit();
        }
    }
}
