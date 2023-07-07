using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrLule.Attributes
{
    [System.Serializable]
    public class ShowOnlyAttribute : PropertyAttribute
    {

    }

#if UNITY_EDITOR
    [UnityEditor.CustomPropertyDrawer(typeof(ShowOnlyAttribute))]
    public class ShowValueDrawer : UnityEditor.PropertyDrawer
    {
        public override void OnGUI(Rect position, UnityEditor.SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;

            UnityEditor.EditorGUI.PropertyField(position, property, label);

            GUI.enabled = true;
        }
    }
#endif
}
