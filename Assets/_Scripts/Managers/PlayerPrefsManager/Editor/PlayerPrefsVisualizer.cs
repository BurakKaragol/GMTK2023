using MrLule.Managers.PlayerPrefsMan;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class PlayerPrefsVisualizer : EditorWindow
{
    private string newIntegerName;
    private int newIntegerValue;
    private string newFloatName;
    private float newFloatValue;
    private string newStringName;
    private string newStringValue;
    private string newBoolName;
    private bool newBoolValue;
    private string newVector2Name;
    private Vector2 newVector2Value;
    private string newVector3Name;
    private Vector3 newVector3Value;
    private string newIntegerArrayName;
    private int[] newIntegerArrayValue;
    private string newFloatArrayName;
    private float[] newFloatArrayValue;
    private string newBoolArrayName;
    private bool[] newBoolArrayValue;
    private string newKeycodeName;
    private KeyCode newKeycodeValue;

    private bool intFoldout;
    private bool floatFoldout;
    private bool stringFoldout;
    private bool boolFoldout;
    private bool vector2Foldout;
    private bool vector3Foldout;
    private bool intArrayFoldout;
    private bool floatArrayFoldout;
    private bool boolArrayFoldout;
    private bool keycodeFoldout;

    [MenuItem("Tools/Player Prefs Visualizer")]
    public static void ShowWindow()
    {
        var window = EditorWindow.GetWindow<PlayerPrefsVisualizer>("Player Prefs Visualizer");

        window.Show();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Clear All PlayerPrefs"))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefsManager.intNames.Clear();
            PlayerPrefsManager.floatNames.Clear();
            PlayerPrefsManager.stringNames.Clear();
            PlayerPrefsManager.boolNames.Clear();
            PlayerPrefsManager.vector2Names.Clear();
            PlayerPrefsManager.vector3Names.Clear();
            PlayerPrefsManager.intArrayNames.Clear();
            PlayerPrefsManager.floatArrayNames.Clear();
            PlayerPrefsManager.boolArrayNames.Clear();
        }

        UpdateValues();
    }

    public void UpdateValues()
    {
        intFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(intFoldout, "Integers");
        if (intFoldout)
        {
            if (GUILayout.Button("Clear All"))
            {
                PlayerPrefsManager.ClearAllInt();
            }
            newIntegerName = EditorGUILayout.TextField("New name:", newIntegerName);
            newIntegerValue = EditorGUILayout.IntField("New value:", newIntegerValue);
            if (GUILayout.Button("Add New"))
            {
                PlayerPrefsManager.SetInt(newIntegerName, newIntegerValue);
            }
            for (int i = 0; i < PlayerPrefsManager.intNames.Count; i++)
            {
                string name = PlayerPrefsManager.intNames[i];
                if (name.Contains("v2_") || name.Contains("v3_") || name.Contains("ia_") || name.Contains("fa_") ||
                    name.Contains("ba_") || name.Contains("keycode_"))
                {
                    continue;
                }
                int value = PlayerPrefsManager.GetInt(name);
                EditorGUILayout.IntField(name, value);
            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        floatFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(floatFoldout, "Floats");
        if (floatFoldout)
        {
            if (GUILayout.Button("Clear All"))
            {
                PlayerPrefsManager.ClearAllFloat();
            }
            newFloatName = EditorGUILayout.TextField("New name:", newFloatName);
            newFloatValue = EditorGUILayout.FloatField("New value:", newFloatValue);
            if (GUILayout.Button("Add New"))
            {
                PlayerPrefsManager.SetFloat(newFloatName, newFloatValue);
            }
            for (int i = 0; i < PlayerPrefsManager.floatNames.Count; i++)
            {
                string name = PlayerPrefsManager.floatNames[i];
                if (name.Contains("v2_") || name.Contains("v3_") || name.Contains("ia_") || name.Contains("fa_") ||
                    name.Contains("ba_") || name.Contains("keycode_"))
                {
                    continue;
                }
                float value = PlayerPrefsManager.GetFloat(name);
                EditorGUILayout.FloatField(name, value);
            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        stringFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(stringFoldout, "Strings");
        if (stringFoldout)
        {
            if (GUILayout.Button("Clear All"))
            {
                PlayerPrefsManager.ClearAllString();
            }
            newStringName = EditorGUILayout.TextField("New name:", newStringName);
            newStringValue = EditorGUILayout.TextField("New value:", newStringValue);
            if (GUILayout.Button("Add New"))
            {
                PlayerPrefsManager.SetString(newStringName, newStringValue);
            }
            for (int i = 0; i < PlayerPrefsManager.stringNames.Count; i++)
            {
                string name = PlayerPrefsManager.stringNames[i];
                string value = PlayerPrefsManager.GetString(name);
                EditorGUILayout.TextField(name, value);
            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        boolFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(boolFoldout, "Bools");
        if (boolFoldout)
        {
            if (GUILayout.Button("Clear All"))
            {
                PlayerPrefsManager.ClearAllBool();
            }
            for (int i = 0; i < PlayerPrefsManager.boolNames.Count; i++)
            {
                string name = PlayerPrefsManager.boolNames[i];
                if (name.Contains("v2_") || name.Contains("v3_") || name.Contains("ia_") || name.Contains("fa_") ||
                    name.Contains("ba_") || name.Contains("keycode_"))
                {
                    continue;
                }
                bool value = PlayerPrefsManager.GetBool(name);
                EditorGUILayout.Toggle(name, value);
            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        vector2Foldout = EditorGUILayout.BeginFoldoutHeaderGroup(vector2Foldout, "Vector2s");
        if (vector2Foldout)
        {
            if (GUILayout.Button("Clear All"))
            {
                PlayerPrefsManager.ClearAllVector2();
            }
            newVector2Name = EditorGUILayout.TextField("New name:", newVector2Name);
            newVector2Value = EditorGUILayout.Vector2Field("New value:", newVector2Value);
            if (GUILayout.Button("Add New"))
            {
                PlayerPrefsManager.SetVector2(newVector2Name, newVector2Value);
            }
            for (int i = 0; i < PlayerPrefsManager.vector2Names.Count; i++)
            {
                string name = PlayerPrefsManager.vector2Names[i];
                Vector2 value = PlayerPrefsManager.GetVector2(name);
                EditorGUILayout.Vector2Field(name, value);
            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        vector3Foldout = EditorGUILayout.BeginFoldoutHeaderGroup(vector3Foldout, "Vector3s");
        if (vector3Foldout)
        {
            if (GUILayout.Button("Clear All"))
            {
                PlayerPrefsManager.ClearAllVector3();
            }
            newVector3Name = EditorGUILayout.TextField("New name:", newVector3Name);
            newVector3Value = EditorGUILayout.Vector3Field("New value:", newVector3Value);
            if (GUILayout.Button("Add New"))
            {
                PlayerPrefsManager.SetVector2(newVector2Name, newVector2Value);
            }
            for (int i = 0; i < PlayerPrefsManager.vector3Names.Count; i++)
            {
                string name = PlayerPrefsManager.vector3Names[i];
                Vector3 value = PlayerPrefsManager.GetVector3(name);
                EditorGUILayout.Vector3Field(name, value);
            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        intArrayFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(intArrayFoldout, "Int Arrays");
        if (intArrayFoldout)
        {
            if (GUILayout.Button("Clear All"))
            {
                PlayerPrefsManager.ClearAllIntArray();
            }
            for (int i = 0; i < PlayerPrefsManager.intArrayNames.Count; i++)
            {
                string name = PlayerPrefsManager.intArrayNames[i];
                int[] value = PlayerPrefsManager.GetIntArray(name);
                var rect = EditorGUILayout.BeginVertical();
                EditorGUI.DrawRect(rect, new Color(0.25f, 0.25f, 0.25f));
                EditorGUILayout.LabelField($"{name}");
                for (int j = 0; j < value.Length; j++)
                {
                    EditorGUILayout.IntField($"{j}", value[j]);
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.Space(5);
            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        floatArrayFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(floatArrayFoldout, "Float Arrays");
        if (floatArrayFoldout)
        {
            if (GUILayout.Button("Clear All"))
            {
                PlayerPrefsManager.ClearAllFloatArray();
            }
            for (int i = 0; i < PlayerPrefsManager.floatArrayNames.Count; i++)
            {
                string name = PlayerPrefsManager.floatArrayNames[i];
                float[] value = PlayerPrefsManager.GetFloatArray(name);
                var rect = EditorGUILayout.BeginVertical();
                EditorGUI.DrawRect(rect, new Color(0.25f, 0.25f, 0.25f));
                EditorGUILayout.LabelField($"{name}");
                for (int j = 0; j < value.Length; j++)
                {
                    EditorGUILayout.FloatField($"{j}", value[j]);
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.Space(5);
            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        boolArrayFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(boolArrayFoldout, "Bool Arrays");
        if (boolArrayFoldout)
        {
            if (GUILayout.Button("Clear All"))
            {
                PlayerPrefsManager.ClearAllBoolArray();
            }
            for (int i = 0; i < PlayerPrefsManager.boolArrayNames.Count; i++)
            {
                string name = PlayerPrefsManager.boolArrayNames[i];
                bool[] value = PlayerPrefsManager.GetBoolArray(name);
                var rect = EditorGUILayout.BeginVertical();
                EditorGUI.DrawRect(rect, new Color(0.25f, 0.25f, 0.25f));
                EditorGUILayout.LabelField($"{name}");
                for (int j = 0; j < value.Length; j++)
                {
                    EditorGUILayout.Toggle($"{j}", value[j]);
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.Space(5);
            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        keycodeFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(keycodeFoldout, "Keycodes");
        if (keycodeFoldout)
        {
            if (GUILayout.Button("Clear All"))
            {
                PlayerPrefsManager.ClearAllKeycode();
            }
            for (int i = 0; i < PlayerPrefsManager.keyCodeNames.Count; i++)
            {
                string name = PlayerPrefsManager.keyCodeNames[i];
                if (name.Contains("v2_") || name.Contains("v3_") || name.Contains("ia_") || name.Contains("fa_") || name.Contains("ba_"))
                {
                    continue;
                }
                KeyCode value = PlayerPrefsManager.GetKeyCode(name);
            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
    }
}
