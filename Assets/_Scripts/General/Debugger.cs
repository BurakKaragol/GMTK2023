using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

namespace MrLule.General
{
    public class Debugger : MonoBehaviour
    {
        private static bool debug = true;
        private static bool warning = true;

        private static Dictionary<string, List<string>> logs = new Dictionary<string, List<string>> { };
        private static Dictionary<string, List<string>> warnings = new Dictionary<string, List<string>> { };

        #region Creating Objects
        #region Text
        public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3),
            int fontSize = 40, Color? color = null)
        {
            if (color == null)
            {
                color = Color.white;
            }
            return CreateWorldText(parent, text, localPosition, fontSize, (Color)color);
        }

        public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color,
            TextAnchor textAnchor = TextAnchor.MiddleCenter, TextAlignment textAllignment = TextAlignment.Center, int sortingOrder = 1)
        {
            GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
            Transform transform = gameObject.transform;
            transform.SetParent(parent, false);
            transform.localPosition = localPosition;
            TextMesh textMesh = gameObject.GetComponent<TextMesh>();
            textMesh.anchor = textAnchor;
            textMesh.alignment = textAllignment;
            textMesh.text = text;
            textMesh.fontSize = fontSize;
            textMesh.color = color;
            textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
            return textMesh;
        }
        #endregion

        #region Shapes
        public static GameObject CreateCube(string objectName, Transform parent, Vector3 localPosition, float size = 1, Color? color = null)
        {
            return CreatePrimitiveShape(objectName, parent, localPosition, PrimitiveType.Cube, size, color);
        }

        public static GameObject CreateSphere(string objectName, Transform parent, Vector3 localPosition, float size = 1, Color? color = null)
        {
            return CreatePrimitiveShape(objectName, parent, localPosition, PrimitiveType.Sphere, size, color);
        }

        public static GameObject CreateCylinder(string objectName, Transform parent, Vector3 localPosition, float size = 1, Color? color = null)
        {
            return CreatePrimitiveShape(objectName, parent, localPosition, PrimitiveType.Cylinder, size, color);
        }

        public static GameObject CreateCapsule(string objectName, Transform parent, Vector3 localPosition, float size = 1, Color? color = null)
        {
            return CreatePrimitiveShape(objectName, parent, localPosition, PrimitiveType.Capsule, size, color);
        }

        public static GameObject CreatePlane(string objectName, Transform parent, Vector3 localPosition, float size = 1, Color? color = null)
        {
            return CreatePrimitiveShape(objectName, parent, localPosition, PrimitiveType.Plane, size, color);
        }

        public static GameObject CreatePrimitiveShape(string objectName, Transform parent, Vector3 localPosition, PrimitiveType type, float size = 1, Color? color = null)
        {
            //color = color?? Color.white;
            if (color == null)
            {
                color = Color.white;
            }
            GameObject obj = GameObject.CreatePrimitive(type);
            obj.gameObject.name = objectName;
            obj.transform.SetParent(parent);
            obj.transform.localPosition = localPosition;
            obj.transform.localScale = Vector3.one * size;
            obj.GetComponent<MeshRenderer>().material.color = (Color)color;
            return obj;
        }

        public static GameObject CreateWorldSprite(string name, Sprite sprite, Vector3 position, Vector3 localScale, int sortingOrder, Color color)
        {
            return CreateWorldSprite(null, name, sprite, position, localScale, sortingOrder, color);
        }

        public static GameObject CreateWorldSprite(Transform parent, string name, Sprite sprite, Vector3 localPosition, Vector3 localScale, int sortingOrder, Color color)
        {
            GameObject gameObject = new GameObject(name, typeof(SpriteRenderer));
            Transform transform = gameObject.transform;
            transform.SetParent(parent, false);
            transform.localPosition = localPosition;
            transform.localScale = localScale;
            SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
            spriteRenderer.sortingOrder = sortingOrder;
            spriteRenderer.color = color;
            return gameObject;
        }
        #endregion
        #endregion

        #region Drawing
        /// <summary>
        /// REWORK
        /// </summary>
        /// <param name="text"></param>
        /// <param name="worldPos"></param>
        /// <param name="color"></param>
        /// <param name="fontSize"></param>
        /// <param name="alignment"></param>
        public static void DrawString(string text, Vector3 worldPos, Color? color = null, int fontSize = 12, TextAnchor alignment = TextAnchor.UpperCenter)
        {
            UnityEditor.Handles.BeginGUI();
            Color defaultColor = GUI.contentColor;
            if (color.HasValue)
                GUI.contentColor = color.Value;

            var view = UnityEditor.SceneView.currentDrawingSceneView;
            Vector3 screenPos = view.camera.WorldToScreenPoint(worldPos);

            screenPos.y = view.position.height - screenPos.y;
            float sizeMultiplier = 1.2f;
            Vector2 size = GUI.skin.label.CalcSize(new GUIContent(text));
            size *= sizeMultiplier;
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.fontSize = fontSize;
            style.alignment = alignment;
            style.fontStyle = FontStyle.Bold;
            style.normal.textColor = color.HasValue ? color.Value : defaultColor;

            float halfWidth = size.x / 2;
            float halfHeight = size.y / 2;

            float xMin = screenPos.x - halfWidth;
            float yMin = screenPos.y - halfHeight;

            GUI.Label(new Rect(xMin, yMin, size.x, size.y), text, style);
            GUI.contentColor = defaultColor;
            UnityEditor.Handles.EndGUI();
        }

        public static void DrawLine(Vector3 start, Vector3 end, Color? color = null, float duration = float.PositiveInfinity, bool depthTest = true)
        {
            if (color == null)
            {
                color = Color.white;
            }
            Debug.DrawLine(start, end, (Color)color, duration, depthTest);
        }

        public static void DrawRay(Vector3 start, Vector3 dir, Color? color = null, float duration = float.PositiveInfinity, bool depthTest = true)
        {
            if (color == null)
            {
                color = Color.white;
            }
            Debug.DrawRay(start, dir, (Color)color, duration, depthTest);
        }

        public static RectTransform DrawSprite(Color color, Transform parent, Vector2 pos, Vector2 size, string name = null)
        {
            RectTransform rectTransform = DrawSprite(null, color, parent, pos, size, name);
            return rectTransform;
        }

        public static RectTransform DrawSprite(Sprite sprite, Transform parent, Vector2 pos, Vector2 size, string name = null)
        {
            RectTransform rectTransform = DrawSprite(sprite, Color.white, parent, pos, size, name);
            return rectTransform;
        }

        public static RectTransform DrawSprite(Sprite sprite, Color color, Transform parent, Vector2 pos, Vector2 size, string name = null)
        {
            // Setup icon
            if (name == null || name == "") name = "Sprite";
            GameObject go = new GameObject(name, typeof(RectTransform), typeof(Image));
            RectTransform goRectTransform = go.GetComponent<RectTransform>();
            goRectTransform.SetParent(parent, false);
            goRectTransform.sizeDelta = size;
            goRectTransform.anchoredPosition = pos;

            Image image = go.GetComponent<Image>();
            image.sprite = sprite;
            image.color = color;

            return goRectTransform;
        }
        #endregion

        #region Gizmo
        public static void DrawRectangle(Vector3 position, Vector2 scale, Vector3 eulerAngles)
        {
            Quaternion rotation = Quaternion.Euler(eulerAngles);

            Vector2 halfExtents = scale * 0.5f;

            Vector3[] corners = new Vector3[4];
            corners[0] = position + rotation * new Vector3(-halfExtents.x, -halfExtents.y);
            corners[1] = position + rotation * new Vector3(halfExtents.x, -halfExtents.y);
            corners[2] = position + rotation * new Vector3(halfExtents.x, halfExtents.y);
            corners[3] = position + rotation * new Vector3(-halfExtents.x, halfExtents.y);

            Gizmos.DrawLine(corners[0], corners[1]);
            Gizmos.DrawLine(corners[1], corners[2]);
            Gizmos.DrawLine(corners[2], corners[3]);
            Gizmos.DrawLine(corners[3], corners[0]);
        }

        public static void DrawArrow(Vector3 startPos, Vector3 stopPos, Color? color = null, bool is2D = true, float arrowHeadSize = 100f, bool use3DVisual = false)
        {
            Color defaultColor = Gizmos.color;
            color = color == null ? Gizmos.color : color;
            Gizmos.color = (Color)color;
            Gizmos.DrawLine(startPos, stopPos);

            Vector3 direction = (stopPos - startPos).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction);

            Vector3 arrowHeadTip = stopPos - (direction * arrowHeadSize);
            Vector3 arrowHeadTop = arrowHeadTip - (rotation * Vector3.up * arrowHeadSize);
            Vector3 arrowHeadBottom = arrowHeadTip - (rotation * Vector3.down * arrowHeadSize);
            Vector3 arrowHeadRight = arrowHeadTip - (rotation * Vector3.left * arrowHeadSize);
            Vector3 arrowHeadLeft = arrowHeadTip - (rotation * Vector3.right * arrowHeadSize);

            Gizmos.DrawLine(stopPos, arrowHeadTop);
            Gizmos.DrawLine(stopPos, arrowHeadBottom);
            Gizmos.DrawLine(arrowHeadTop, arrowHeadBottom);

            if (!is2D)
            {
                Gizmos.DrawLine(stopPos, arrowHeadRight);
                Gizmos.DrawLine(stopPos, arrowHeadLeft);
                Gizmos.DrawLine(arrowHeadRight, arrowHeadLeft);
                if (use3DVisual)
                {
                    Gizmos.DrawLine(arrowHeadTop, arrowHeadRight);
                    Gizmos.DrawLine(arrowHeadRight, arrowHeadBottom);
                    Gizmos.DrawLine(arrowHeadBottom, arrowHeadLeft);
                    Gizmos.DrawLine(arrowHeadLeft, arrowHeadTop);
                }
            }
            Gizmos.color = defaultColor;
        }
        #endregion

        #region Logging
        public static void Log(string calledFrom, string message)
        {
            if (debug)
            {
                Debug.Log($"{calledFrom} : {message}");
            }

            SaveLog(calledFrom, message);
        }

        public static void LogWarning(string calledFrom, string message)
        {
            if (!debug)
            {
                return;
            }

            if (warning)
            {
                Debug.LogWarning($"{calledFrom} : {message}");
            }

            SaveWarning(calledFrom, message);
        }
        #endregion

        #region Saving Logs
        private static void SaveLog(string calledFrom, string message)
        {
            List<string> list;
            if (logs.ContainsKey(calledFrom))
            {
                logs[calledFrom].Add(message);
            }
            else
            {
                list = new List<string>();
                list.Add(message);
                logs.Add(calledFrom, list);
            }
        }

        private static void SaveWarning(string calledFrom, string message)
        {
            List<string> list;
            if (warnings.ContainsKey(calledFrom))
            {
                list = warnings[calledFrom];
                list.Add(message);
            }
            else
            {
                list = new List<string>();
                list.Add(message);
                warnings.Add(calledFrom, list);
            }
        }
        #endregion

        #region Print Logs
        public static void PrintLogs(string calledFrom = "All")
        {
            if (logs.ContainsKey(calledFrom))
            {
                Debug.Log($"Called From : {calledFrom}");
                foreach (string log in logs[calledFrom])
                {
                    Debug.Log($"    {log}");
                }
            }

            if (calledFrom == "All")
            {
                foreach (var key in logs.Keys)
                {
                    Debug.Log($"Called From : {key}");
                    foreach (string log in logs[key])
                    {
                        Debug.Log($"    {log}");
                    }
                }
            }
        }

        public static void PrintWarnings(string calledFrom = "All")
        {
            if (warnings.ContainsKey(calledFrom))
            {
                Debug.Log($"Called From : {calledFrom}");
                foreach (string log in warnings[calledFrom])
                {
                    Debug.Log($"    {log}");
                }
            }

            if (calledFrom == "All")
            {
                foreach (var key in warnings.Keys)
                {
                    Debug.Log($"Called From : {key}");
                    foreach (string log in warnings[key])
                    {
                        Debug.Log($"    {log}");
                    }
                }
            }
        }
        #endregion
    }
}
