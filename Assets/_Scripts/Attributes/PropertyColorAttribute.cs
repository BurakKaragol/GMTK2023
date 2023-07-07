using System;
using UnityEditor;
using UnityEngine;

namespace MrLule.Attributes
{
    public class PropertyColorAttribute : PropertyAttribute
    {
        public Color propertyColor;

        public PropertyColorAttribute(string hexCode)
        {
            if (ColorUtility.TryParseHtmlString( hexCode, out Color color))
            {
                propertyColor = color;
            }
        }

        public PropertyColorAttribute(int red, int green, int blue)
        {
            propertyColor = new Color(red / 255f, green / 255f, blue / 255f);
        }

        public PropertyColorAttribute(float red, float green, float blue)
        {
            propertyColor = new Color(red, green, blue);
        }
    }

    [CustomPropertyDrawer(typeof(PropertyColorAttribute))]
    public class PropertyColorAttributePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var attr = attribute as PropertyColorAttribute;

            if (attr.propertyColor != null)
            {
                Color originalColor = GUI.color;
                GUI.color = attr.propertyColor;
                EditorGUI.PropertyField(position, property, label, true);
                GUI.color = originalColor;
            }
        }
    }
}
