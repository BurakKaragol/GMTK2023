using MrLule.ExtensionMethods;
using System.Collections.Generic;
using UnityEngine;

namespace MrLule.Managers.PlayerPrefsMan
{
    public class PlayerPrefsManager : Manager
    {
        public static List<string> intNames = new List<string>();
        public static List<string> floatNames = new List<string>();
        public static List<string> stringNames = new List<string>();
        public static List<string> boolNames = new List<string>();
        public static List<string> vector2Names = new List<string>();
        public static List<string> vector3Names = new List<string>();
        public static List<string> intArrayNames = new List<string>();
        public static List<string> floatArrayNames = new List<string>();
        public static List<string> boolArrayNames = new List<string>();
        public static List<string> keyCodeNames = new List<string>();

        #region Integer
        public static void SetInt(string key, int value)
        {
            if (!intNames.Contains(key))
            {
                intNames.Add(key);
            }
            PlayerPrefs.SetInt(key, value);
        }

        public static int GetInt(string key, int defaultValue = 0)
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }

        public static void DeleteInt(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }

        public static void ClearAllInt()
        {
            foreach (string name in intNames)
            {
                DeleteInt(name);
            }
        }
        #endregion

        #region Float
        public static void SetFloat(string key, float value)
        {
            if (!floatNames.Contains(key))
            {
                floatNames.Add(key);
            }
            PlayerPrefs.SetFloat(key, value);
        }

        public static float GetFloat(string key, float defaultValue = 0f)
        {
            return PlayerPrefs.GetFloat(key, defaultValue);
        }

        public static void DeleteFloat(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }

        public static void ClearAllFloat()
        {
            foreach (string name in floatNames)
            {
                DeleteFloat(name);
            }
        }
        #endregion

        #region String
        public static void SetString(string key, string value)
        {
            if (!stringNames.Contains(key))
            {
                stringNames.Add(key);
            }
            PlayerPrefs.SetString(key, value);
        }

        public static string GetString(string key, string defaultValue = "")
        {
            return PlayerPrefs.GetString(key, defaultValue);
        }

        public static void DeleteString(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }

        public static void ClearAllString()
        {
            foreach (string name in stringNames)
            {
                DeleteString(name);
            }
        }
        #endregion

        #region Bool
        public static void SetBool(string key, bool value)
        {
            if (!boolNames.Contains(key))
            {
                boolNames.Add(key);
            }
            PlayerPrefs.SetInt(key, value.ToIntWZero());
        }

        public static bool GetBool(string key, bool defaultValue = true)
        {
            return PlayerPrefs.GetInt(key, defaultValue.ToIntWZero()).ToBool();
        }

        public static void DeleteBool(string key)
        {
            DeleteInt(key);
        }

        public static void ClearAllBool()
        {
            foreach (string name in boolNames)
            {
                DeleteBool(name);
            }
        }
        #endregion

        #region Vector2
        public static void SetVector2(string key, Vector2 value)
        {
            if (!vector2Names.Contains(key))
            {
                vector2Names.Add(key);
            }
            SetFloat($"v2_{key}_x", value.x);
            SetFloat($"v2_{key}_y", value.y);
        }

        public static Vector2 GetVector2(string key)
        {
            return GetVector2(key, Vector2.zero);
        }

        public static Vector2 GetVector2(string key, Vector2 defaultValue)
        {
            Vector2 found;
            found.x = GetFloat($"v2_{key}_x", defaultValue.x);
            found.y = GetFloat($"v2_{key}_y", defaultValue.y);
            return found;
        }

        public static void DeleteVector2(string key)
        {
            DeleteFloat($"v2_{key}_x");
            DeleteFloat($"v2_{key}_y");
        }

        public static void ClearAllVector2()
        {
            foreach (string name in vector2Names)
            {
                DeleteVector2(name);
            }
        }
        #endregion

        #region Vector3
        public static void SetVector3(string key, Vector3 value)
        {
            if (!vector3Names.Contains(key))
            {
                vector3Names.Add(key);
            }
            SetFloat($"v3_{key}_x", value.x);
            SetFloat($"v3_{key}_y", value.y);
            SetFloat($"v3_{key}_z", value.z);
        }

        public static Vector3 GetVector3(string key)
        {
            return GetVector3(key, Vector3.zero);
        }

        public static Vector3 GetVector3(string key, Vector3 defaultValue)
        {
            Vector3 found;
            found.x = GetFloat($"v3_{key}_x", defaultValue.x);
            found.y = GetFloat($"v3_{key}_y", defaultValue.y);
            found.z = GetFloat($"v3_{key}_z", defaultValue.z);
            return found;
        }

        public static void DeleteVector3(string key)
        {
            DeleteFloat($"v3_{key}_x");
            DeleteFloat($"v3_{key}_y");
            DeleteFloat($"v3_{key}_z");
        }

        public static void ClearAllVector3()
        {
            foreach (string name in vector3Names)
            {
                DeleteVector3(name);
            }
        }
        #endregion

        #region Int Array
        public static void SetIntArray(string key, int[] values)
        {
            if (!intArrayNames.Contains(key))
            {
                intArrayNames.Add(key);
            }
            SetInt($"ia_{key}_size", values.Length);
            for (int i = 0; i < values.Length; i++)
            {
                SetInt($"ia_{key}_{i}", values[i]);
            }
        }

        public static int[] GetIntArray(string key)
        {
            int size = GetInt($"ia_{key}_size");
            int[] @default = new int[size];
            for (int i = 0; i < size; i++)
            {
                @default[i] = 0;
            }
            return GetIntArray(key, @default);
        }

        public static int[] GetIntArray(string key, int[] defaultValues)
        {
            int size = GetInt($"ia_{key}_size", defaultValues.Length);
            int[] found = new int[size];
            for (int i = 0; i < size; i++)
            {
                found[i] = GetInt($"ia_{key}_{i}", defaultValues[i]);
            }
            return found;
        }

        public static void DeleteIntArray(string key)
        {
            for (int i = 0; i < GetInt($"ia_{key}_size"); i++)
            {
                DeleteInt($"ia_{key}_{i}");
            }
            DeleteInt($"ia_{key}_size");
        }

        public static void ClearAllIntArray()
        {
            foreach (string name in intArrayNames)
            {
                DeleteIntArray(name);
            }
        }
        #endregion

        #region Float Array
        public static void SetFloatArray(string key, float[] values)
        {
            if (!floatArrayNames.Contains(key))
            {
                floatArrayNames.Add(key);
            }
            SetInt($"fa_{key}_size", values.Length);
            for (int i = 0; i < values.Length; i++)
            {
                SetFloat($"fa_{key}_{i}", values[i]);
            }
        }

        public static float[] GetFloatArray(string key)
        {
            int size = GetInt($"fa_{key}_size");
            float[] @default = new float[size];
            for (int i = 0; i < size; i++)
            {
                @default[i] = 0;
            }
            return GetFloatArray(key, @default);
        }

        public static float[] GetFloatArray(string key, float[] defaultValues)
        {
            int size = GetInt($"fa_{key}_size", defaultValues.Length);
            float[] found = new float[size];
            for (int i = 0; i < size; i++)
            {
                found[i] = GetFloat($"fa_{key}_{i}", defaultValues[i]);
            }
            return found;
        }

        public static void DeleteFloatArray(string key)
        {
            for (int i = 0; i < GetInt($"fa_{key}_size"); i++)
            {
                DeleteFloat($"fa_{key}_{i}");
            }
            DeleteInt($"fa_{key}_size");
        }

        public static void ClearAllFloatArray()
        {
            foreach (string name in floatArrayNames)
            {
                DeleteFloatArray(name);
            }
        }
        #endregion

        #region Bool Array
        public static void SetBoolArray(string key, bool[] values)
        {
            if (!boolArrayNames.Contains(key))
            {
                boolArrayNames.Add(key);
            }
            SetInt($"ba_{key}_size", values.Length);
            for (int i = 0; i < values.Length; i++)
            {
                SetBool($"ba_{key}_{i}", values[i]);
            }
        }

        public static bool[] GetBoolArray(string key)
        {
            int size = GetInt($"ba_{key}_size");
            bool[] @default = new bool[size];
            for (int i = 0; i < size; i++)
            {
                @default[i] = true;
            }
            return GetBoolArray(key, @default);
        }

        public static bool[] GetBoolArray(string key, bool[] defaultValues)
        {
            int size = GetInt($"ba_{key}_size", defaultValues.Length);
            bool[] found = new bool[size];
            for (int i = 0; i < size; i++)
            {
                found[i] = GetBool($"ba_{key}_{i}", defaultValues[i]);
            }
            return found;
        }

        public static void DeleteBoolArray(string key)
        {
            for (int i = 0; i < GetInt($"ba_{key}_size"); i++)
            {
                DeleteBool($"ba_{key}_{i}");
            }
            DeleteInt($"ba_{key}_size");
        }

        public static void ClearAllBoolArray()
        {
            foreach (string name in boolArrayNames)
            {
                DeleteBoolArray(name);
            }
        }
        #endregion

        #region KeyCode
        public static void SetKeyCode(string key, KeyCode value)
        {
            if (!keyCodeNames.Contains(key))
            {
                keyCodeNames.Add(key);
            }
            SetInt($"keycode_{key}", (int)value);
        }

        public static KeyCode GetKeyCode(string key)
        {
            return GetKeyCode(key, KeyCode.None);
        }

        public static KeyCode GetKeyCode(string key, KeyCode defaultValue)
        {
            KeyCode found;
            found = (KeyCode)GetInt($"keycode_{key}", (int)defaultValue);
            return found;
        }

        public static void DeleteKeyCode(string key)
        {
            DeleteInt(key);
        }

        public static void ClearAllKeycode()
        {
            foreach (string name in keyCodeNames)
            {
                DeleteKeyCode(name);
            }
        }
        #endregion

        public static void Save()
        {
            PlayerPrefs.Save();
        }

        public static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }

        public override void OnEnable()
        {
            playerPrefsManager = this;
        }

        public override void OnDisable()
        {
            playerPrefsManager = null;
        }
    }
}
