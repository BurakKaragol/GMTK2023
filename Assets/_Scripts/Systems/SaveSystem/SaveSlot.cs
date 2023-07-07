using MrLule.Attributes;
using TMPro;
using UnityEngine;

namespace MrLule.Systems.SaveSystem
{
    public class SaveSlot : MonoBehaviour
    {
        [ShowOnly] private string defaultSaveName;
        [SerializeField] private string slotName;
        [SerializeField] private TextMeshProUGUI saveNameText;
        [SerializeField] private GameObject newSaveButton;
        [SerializeField] private TMP_InputField saveNameInputField;
        [SerializeField] private GameObject loadSaveButton;
        [SerializeField] private GameObject deleteSaveButton;

        private string saveName;

        public void SetLocalizedDefaultName(string name)
        {
            defaultSaveName = name;
        }

        private void Start()
        {
            UpdateSlot();
        }

        private void UpdateSlot()
        {
            if (SaveLoadHandler.GetSaveName(slotName, out string currentSaveName))
            {
                SetSlotState(true);
                saveName = currentSaveName;
                saveNameText.SetText(saveName);
            }
            else
            {
                SetSlotState(false);
                saveNameInputField.SetTextWithoutNotify(defaultSaveName);
                saveName = defaultSaveName;
            }
        }

        private void SetSlotState(bool hasSave)
        {
            if (hasSave)
            {
                saveNameText.gameObject.SetActive(true);
                newSaveButton.SetActive(false);
                saveNameInputField.gameObject.SetActive(false);
                loadSaveButton.SetActive(true);
                deleteSaveButton.SetActive(true);
            }
            else
            {
                saveNameText.gameObject.SetActive(false);
                newSaveButton.SetActive(true);
                saveNameInputField.gameObject.SetActive(true);
                loadSaveButton.SetActive(false);
                deleteSaveButton.SetActive(false);
            }
        }

        public void SetSaveName(string name)
        {
            saveName = name;
        }

        public void NewSave()
        {
            SaveSystem.Instance.NewGame(slotName, saveName);
        }

        public void LoadLevel()
        {
            SaveSystem.Instance.LoadGame(slotName, saveName);
        }

        public void DeleteSave()
        {
            SaveSystem.Instance.DeleteGame(slotName, saveName);
            UpdateSlot();
        }
    }
}
