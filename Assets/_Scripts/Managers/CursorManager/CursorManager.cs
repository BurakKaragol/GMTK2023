using MrLule.General;
using System;
using UnityEngine;

namespace MrLule.Managers.CursorMan
{
    [System.Serializable]
    public enum Hotspot
    {
        TopLeft,
        TopMiddle,
        TopRight,
        MiddleLeft,
        Center,
        MiddleRight,
        BottomLeft,
        BottomMiddle,
        BottomRight,
        Custom
    }

    [System.Serializable]
    public class CursorType
    {
        public string cursorName;
        public Texture2D cursorTexture;
        public Hotspot hotspotLocation;
        public Vector2 cursorHotspot;
    }

    public class CursorManager : Manager
    {
        [Header("General:")]
        [Space(5)]
        [SerializeField] private string defaultCursorName = "Deafult";
        [SerializeField] private bool isShowing = true;

        [Header("CursorTypes:")]
        [Space(5)]
        [SerializeField] CursorType[] cursors;

        private string activeCursorName;

        public static CursorManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            SetCursor(defaultCursorName);

        }

        public void SetCursor(string cursorName)
        {
            if (cursorName == activeCursorName)
            {
                Debugger.LogWarning(this.GetType().ToString(), "Cannot set cursor (cursor already selected)");
                return;
            }

            CursorType cursor = GetCursor(cursorName);
            Vector2 hotspot = CalculateHotspot(cursor);
            Cursor.SetCursor(cursor.cursorTexture, hotspot, CursorMode.Auto);
            activeCursorName = cursor.cursorName;
        }

        public void SetCursor(CursorType cursorType)
        {
            CursorType cursor = cursorType;
            Vector2 hotspot = CalculateHotspot(cursor);
            Cursor.SetCursor(cursor.cursorTexture, hotspot, CursorMode.Auto);
            activeCursorName = cursor.cursorName;
        }

        public CursorType GetActiveCursorType()
        {
            return GetCursor(activeCursorName);
        }

        public void SetCursorMode(bool show)
        {
            if (show)
            {
                ShowCursor();
            }
            else
            {
                HideCursor();
            }
        }

        public void HideCursor()
        {
            if (isShowing)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                isShowing = false;
            }
            else
            {
                Debugger.LogWarning(this.GetType().ToString(), "Cannot hide cursor (cursor is already hidden)");
            }
        }

        public void ShowCursor()
        {
            if (!isShowing)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                isShowing = true;
            }
            else
            {
                Debugger.LogWarning(this.GetType().ToString(), "Cannot show cursor (cursor is already visible)");
            }
        }

        private Vector2 CalculateHotspot(CursorType cursor)
        {
            if (cursor.hotspotLocation != Hotspot.Custom)
            {
                float width = cursor.cursorTexture.width;
                float height = cursor.cursorTexture.height;
                switch (cursor.hotspotLocation)
                {
                    case Hotspot.TopLeft:
                        return new Vector2(0, 0);
                    case Hotspot.TopMiddle:
                        return new Vector2(width / 2, 0);
                    case Hotspot.TopRight:
                        return new Vector2(width, 0);
                    case Hotspot.MiddleLeft:
                        return new Vector2(0, height / 2);
                    case Hotspot.Center:
                        return new Vector2(width / 2, height / 2);
                    case Hotspot.MiddleRight:
                        return new Vector2(width, height / 2);
                    case Hotspot.BottomLeft:
                        return new Vector2(0, height);
                    case Hotspot.BottomMiddle:
                        return new Vector2(width / 2, height);
                    case Hotspot.BottomRight:
                        return new Vector2(width, height);
                }
                return Vector2.zero;
            }
            else
            {
                return cursor.cursorHotspot;
            }
        }

        private CursorType GetCursor(string cursorName)
        {
            CursorType cursor = Array.Find(cursors, cursor => cursor.cursorName == cursorName);
            if (cursor == null)
            {
                Debugger.LogWarning(this.GetType().ToString(), "Cannot find the cursor (Cursor name is wrong)");
                return cursors[0];
            }
            return cursor;
        }

        public override void OnEnable()
        {
            cursorManager = this;
        }

        public override void OnDisable()
        {
            cursorManager = null;
        }
    }
}
