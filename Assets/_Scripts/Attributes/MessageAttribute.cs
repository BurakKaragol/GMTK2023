using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MrLule.Attributes
{
    public class MessageAttribute : PropertyAttribute
    {
        public string message;

        public MessageAttribute(string message)
        {
            this.message = message;
        }
    }

    [CustomPropertyDrawer(typeof(MessageAttribute))]
    public class MessageDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            MessageAttribute messageAttribute = attribute as MessageAttribute;

            float messageHeight = GUI.skin.label.CalcHeight(new GUIContent(messageAttribute.message), position.width);

            Rect messageRect = new Rect(position.x, position.y, position.width, messageHeight);

            EditorGUI.LabelField(messageRect, messageAttribute.message, EditorStyles.helpBox);

            Rect propertyRect = new Rect(position.x, position.y + messageHeight, position.width, position.height - messageHeight);

            EditorGUI.PropertyField(propertyRect, property, label, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            MessageAttribute messageAttribute = attribute as MessageAttribute;

            float propertyHeight = EditorGUI.GetPropertyHeight(property, label, true);
            float messageHeight = GUI.skin.label.CalcHeight(new GUIContent(messageAttribute.message), EditorGUIUtility.currentViewWidth);

            return propertyHeight + messageHeight + EditorGUIUtility.standardVerticalSpacing;
        }
    }
}
