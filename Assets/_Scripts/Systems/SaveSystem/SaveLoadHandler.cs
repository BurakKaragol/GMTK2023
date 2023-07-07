using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using UnityEngine;
using MrLule.General;

namespace MrLule.Systems.SaveSystem
{
    public static class SaveLoadHandler
    {
        public static bool GetSaveName(string saveSlot, out string saveName)
        {
            string path = Application.persistentDataPath + "/saves/" + saveSlot;
            BinaryFormatter binaryFormatter = GetBinaryFormatter();

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                saveName = string.Empty;
                return false;
            }
            else
            {
                try
                {
                    string[] files = Directory.GetFiles(path);
                    if (files.Length > 1)
                    {
                        Debugger.LogWarning("SaveLoadHandler", $"Multiple files in the save folder {saveSlot}");
                    }
                    else if (files.Length == 0)
                    {
                        saveName = string.Empty;
                        return false;
                    }
                    else
                    {
                        string fileName = Path.GetFileNameWithoutExtension(files[0]);
                        saveName = fileName;
                        return true;
                    }
                }
                catch (System.Exception)
                {

                    throw;
                }
            }

            saveName = string.Empty;
            return false;
        }

        public static void SaveBinaryData(string saveSlot, string saveName, SaveData data)
        {
            string path = Application.persistentDataPath + "/saves/" + saveSlot + "/" + saveName + ".dat";
            BinaryFormatter binaryFormatter = GetBinaryFormatter();

            if (!Directory.Exists(Application.persistentDataPath + "/saves/" + saveSlot))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/saves/" + saveSlot);
            }

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            FileStream fileStream = new FileStream(path, FileMode.Create);

            binaryFormatter.Serialize(fileStream, data);
            fileStream.Close();
        }

        public static SaveData LoadBinaryData(string saveSlot, string saveName)
        {
            string path = Application.persistentDataPath + "/saves/" + saveSlot + "/" + saveName + ".dat";

            if (!File.Exists(path))
            {
                Debugger.LogWarning("SaveLoadHandler", "File doesn't exist" + saveName);
                return null;
            }
            BinaryFormatter binaryFormatter = GetBinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);

            try
            {
                SaveData data = binaryFormatter.Deserialize(fileStream) as SaveData;
                fileStream.Close();
                return data;
            }
            catch (System.Exception)
            {
                Debugger.LogWarning("SaveLoadHandler", "Error loading file at" + path);
                fileStream.Close();
                return null;
            }
        }

        public static bool DeleteBinaryData(string saveSlot, string saveName)
        {
            string path = Application.persistentDataPath + "/saves/" + saveSlot + "/" + saveName + ".dat";

            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            else
            {
                return false;
            }
        }

        private static BinaryFormatter GetBinaryFormatter()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            SurrogateSelector surrogateSelector = new SurrogateSelector();

            Vector3SerializationSurrogate vector3Surrogate = new Vector3SerializationSurrogate();
            QuaternionSerializationSurrogate quaternionSurrogate = new QuaternionSerializationSurrogate();

            surrogateSelector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), vector3Surrogate);
            surrogateSelector.AddSurrogate(typeof(Quaternion), new StreamingContext(StreamingContextStates.All), quaternionSurrogate);
            binaryFormatter.SurrogateSelector = surrogateSelector;

            return binaryFormatter;
        }
    }
}
