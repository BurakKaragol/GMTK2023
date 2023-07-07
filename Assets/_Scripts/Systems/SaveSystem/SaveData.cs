using System.Numerics;

namespace MrLule.Systems.SaveSystem
{
    [System.Serializable]
    public class SaveData
    {
        public static SaveData Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SaveData();
                }
                return _instance;
            }
        }
        private static SaveData _instance;

        // reset all save date and make a default save data
        public void ResetAllSaveData()
        {

        }

        public Vector3 pozisyon;
    }
}
