using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;
using System.Collections.Generic;

namespace MrLule.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class InspectorButtonAttribute : PropertyAttribute
    {
        public string ButtonName { get; private set; }
        public object[] Parameters { get; private set; }

        public InspectorButtonAttribute(string buttonName)
        {
            ButtonName = buttonName;
            Parameters = new object[0];
        }

        public InspectorButtonAttribute(string buttonName, params object[] parameters)
        {
            ButtonName = buttonName;
            Parameters = parameters;
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class InspectorButtonAttributeEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var methods = target.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var method in methods)
            {
                var attributes = method.GetCustomAttributes(typeof(InspectorButtonAttribute), true);
                if (attributes.Length > 0)
                {
                    var attribute = (InspectorButtonAttribute)attributes[0];
                    var buttonName = attribute.ButtonName;

                    if (GUILayout.Button(buttonName))
                    {
                        method.Invoke(target, attribute.Parameters);
                    }
                }
            }

            base.OnInspectorGUI();
        }
    }
#endif
}
