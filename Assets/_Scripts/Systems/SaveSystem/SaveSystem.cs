using MrLule.Managers.SceneTransitionMan;
using UnityEngine;

namespace MrLule.Systems.SaveSystem
{
    public class SaveSystem : MonoBehaviour
    {
        public static SaveSystem Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SaveGame(string saveSlot, string saveName)
        {
            SaveData saveData = SaveData.Instance;

            SaveLoadHandler.SaveBinaryData(saveSlot, saveName, saveData);
        }

        public void LoadGame(string saveSlot, string saveName)
        {
            SaveData saveData = SaveLoadHandler.LoadBinaryData(saveSlot, saveName);

            if (saveData != null)
            {
                saveData = SaveLoadHandler.LoadBinaryData(saveSlot, saveName);
                SceneTransitionManager.Instance.NextScene();
            }
        }

        public void NewGame(string saveSlot, string saveName)
        {
            SaveData.Instance.ResetAllSaveData();
            SaveData newSaveData = SaveData.Instance;

            if (newSaveData != null)
            {
                SaveLoadHandler.SaveBinaryData(saveSlot, saveName, newSaveData);
                SceneTransitionManager.Instance.NextScene();
            }
        }

        public void DeleteGame(string saveSlot, string saveName)
        {
            SaveData.Instance.ResetAllSaveData();
            SaveData saveData = SaveData.Instance;

            if (saveData != null)
            {
                SaveLoadHandler.DeleteBinaryData(saveSlot, saveName);
            }
        }
    }
}
