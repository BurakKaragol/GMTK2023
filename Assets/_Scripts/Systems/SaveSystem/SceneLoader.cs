using UnityEngine;

namespace MrLule.Systems.SaveSystem
{
    public class SceneLoader : MonoBehaviour
    {
        private SaveData saveData;

        private void Awake()
        {
            saveData = SaveData.Instance;
        }
    }
}
