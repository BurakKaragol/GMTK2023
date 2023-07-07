using UnityEngine;

/// <summary>
/// The extension methods are working like below:
/// 
/// public static Vector3 SetX(this Vector3 v, float value)
/// {
///     return new Vector3(value, v.y, v.z);
/// }
/// 
/// point = point.SetX(5);
/// 
/// TODO:
/// 
/// Add those extensions also:
/// 
/// public static void SetX(this ref Vector3 v, float value)
/// {
///     v = new Vector3(value, v.y, v.z);
/// }
/// 
/// that way we can use it as:
/// 
/// point.SetX(5);
/// 
/// </summary>

namespace MrLule.ExtensionMethods
{
    public static class ExtensionMethods
    {
        #region Bool Extensions
        #region Converting
        public static int ToIntWZero(this bool b)
        {
            return b ? 1 : 0;
        }

        public static int ToIntWMinusOne(this bool b)
        {
            return b ? 1 : -1;
        }
        #endregion
        #endregion

        #region Integer Extensions
        #region Clipping
        public static int AtLeast(this int i, int min)
        {
            return Mathf.Max(i, min);
        }

        public static int AtMost(this int i, int max)
        {
            return Mathf.Min(i, max);
        }
        #endregion

        #region Converting
        public static float ToFloat(this int i)
        {
            return (float)i;
        }

        public static bool ToBool(this int i)
        {
            return i == 1;
        }
        #endregion
        #endregion

        #region Float Extensions
        #region Rounding
        public static float Round(this float f, float size)
        {
            return Mathf.Round(f / size) * size;
        }
        #endregion

        #region Clipping
        public static float AtLeast(this float f, float min)
        {
            return Mathf.Max(f, min);
        }

        public static float AtMost(this float f, float max)
        {
            return Mathf.Min(f, max);
        }
        #endregion

        #region Converting
        public static int ToInt(this float f)
        {
            return Mathf.RoundToInt(f);
        }
        #endregion
        #endregion

        #region String Extensions
        public static bool IsNullOrEmpty(this string s)
        {
            return s == null || s == "";
        }

        public static bool IsNotNullOrEmpty(this string s)
        {
            return !s.IsNullOrEmpty();
        }
        #endregion

        #region Vector2 Extensions
        #region Rounding
        public static Vector2 Round(this Vector2 v)
        {
            v.x = Mathf.Round(v.x);
            v.y = Mathf.Round(v.y);
            return v;
        }

        public static Vector2 RoundTo(this Vector2 v, float size)
        {
            return (v / size).Round() * size;
        }
        #endregion

        #region Clipping
        public static Vector2 AtLeast(this Vector2 v, Vector2 min)
        {
            v.x = Mathf.Max(v.x, min.x);
            v.y = Mathf.Max(v.y, min.y);
            return v;
        }

        public static Vector2 AtMost(this Vector2 v, Vector2 max)
        {
            v.x = Mathf.Min(v.x, max.x);
            v.y = Mathf.Min(v.y, max.y);
            return v;
        }
        #endregion

        #region Set Value
        public static Vector2 SetX(this Vector2 v, float value)
        {
            return new Vector2(value, v.y);
        }

        public static Vector2 SetY(this Vector2 v, float value)
        {
            return new Vector2(v.x, value);
        }
        #endregion

        #region Move Value
        public static Vector2 FlipXY(this Vector2 v)
        {
            return new Vector2(v.y, v.x);
        }
        #endregion

        #region Inverting
        public static Vector2 Invert(this Vector2 v)
        {
            return new Vector2(v.x, v.y) * -1;
        }

        public static Vector2 InvertX(this Vector2 v)
        {
            return new Vector2(v.x * -1, v.y);
        }

        public static Vector2 InvertY(this Vector2 v)
        {
            return new Vector2(v.x, v.y * -1);
        }
        #endregion

        #region Converting
        public static Vector3 ToVector3(this Vector2 v)
        {
            return new Vector3(v.x, v.y);
        }
        #endregion
        #endregion

        #region Vector3 Extensions
        #region Rounding
        public static Vector3 Round(this Vector3 v)
        {
            v.x = Mathf.Round(v.x);
            v.y = Mathf.Round(v.y);
            v.z = Mathf.Round(v.z);
            return v;
        }

        public static Vector3 RoundTo(this Vector3 v, float size)
        {
            return (v / size).Round() * size;
        }
        #endregion

        #region Clipping
        public static Vector3 AtLeast(this Vector3 v, Vector3 min)
        {
            v.x = Mathf.Max(v.x, min.x);
            v.y = Mathf.Max(v.y, min.y);
            v.z = Mathf.Max(v.z, min.z);
            return v;
        }

        public static Vector3 AtMost(this Vector3 v, Vector3 max)
        {
            v.x = Mathf.Min(v.x, max.x);
            v.y = Mathf.Min(v.y, max.y);
            v.z = Mathf.Min(v.z, max.z);
            return v;
        }
        #endregion

        #region Set Value
        public static Vector3 SetX(this Vector3 v, float x)
        {
            return new Vector3(x, v.y, v.z);
        }

        public static Vector3 SetY(this Vector3 v, float y)
        {
            return new Vector3(v.x, y, v.z);
        }

        public static Vector3 SetZ(this Vector3 v, float z)
        {
            return new Vector3(v.x, v.y, z);
        }

        public static Vector3 SetXY(this Vector3 v, float x, float y)
        {
            return new Vector3(x, y, v.z);
        }

        public static Vector3 SetXZ(this Vector3 v, float x, float z)
        {
            return new Vector3(x, v.y, z);
        }

        public static Vector3 SetYZ(this Vector3 v, float y, float z)
        {
            return new Vector3(v.x, y, z);
        }
        #endregion

        #region Move Value
        public static Vector3 YToZ(this Vector3 v)
        {
            return new Vector3(v.x, 0, v.y);
        }

        public static Vector3 ZToY(this Vector3 v)
        {
            return new Vector3(v.x, v.z, 0);
        }
        #endregion

        #region Inverting
        public static Vector3 Invert(this Vector3 v)
        {
            return new Vector3(v.x, v.y, v.z) * -1;
        }

        public static Vector3 InvertX(this Vector3 v)
        {
            return new Vector3(v.x * -1, v.y, v.z);
        }

        public static Vector3 InvertY(this Vector3 v)
        {
            return new Vector3(v.x, v.y * -1, v.z);
        }

        public static Vector3 InvertZ(this Vector3 v)
        {
            return new Vector3(v.x, v.y, v.z * -1);
        }
        #endregion

        #region Converting
        public static Vector3 ToVector2(this Vector3 v)
        {
            return new Vector2(v.x, v.y);
        }
        #endregion
        #endregion

        #region Quaternion Extensions
        #region Rounding
        public static Quaternion Round(this Quaternion q)
        {
            q.x = Mathf.Round(q.x);
            q.y = Mathf.Round(q.y);
            q.z = Mathf.Round(q.z);
            q.w = Mathf.Round(q.w);
            return q;
        }
        #endregion

        #region Clipping
        public static Quaternion AtLeast(this Quaternion q, Quaternion min)
        {
            q.x = Mathf.Max(q.x, min.x);
            q.y = Mathf.Max(q.y, min.y);
            q.z = Mathf.Max(q.z, min.z);
            q.w = Mathf.Max(q.w, min.w);
            return q;
        }

        public static Quaternion AtMost(this Quaternion q, Quaternion max)
        {
            q.x = Mathf.Min(q.x, max.x);
            q.y = Mathf.Min(q.y, max.y);
            q.z = Mathf.Min(q.z, max.z);
            q.w = Mathf.Min(q.w, max.w);
            return q;
        }
        #endregion

        #region Set Value
        public static Quaternion SetX(this Quaternion q, float value)
        {
            return new Quaternion(value, q.y, q.z, q.w);
        }

        public static Quaternion SetY(this Quaternion q, float value)
        {
            return new Quaternion(q.x, value, q.z, q.w);
        }

        public static Quaternion SetZ(this Quaternion q, float value)
        {
            return new Quaternion(q.x, q.y, value, q.w);
        }

        public static Quaternion SetW(this Quaternion q, float value)
        {
            return new Quaternion(q.x, q.y, q.z, value);
        }
        #endregion

        #region Inverting
        public static Quaternion Invert(this Quaternion q)
        {
            Vector3 v = q.eulerAngles;
            v.Invert();
            return Quaternion.Euler(v);
        }

        public static Quaternion InvertX(this Quaternion q)
        {
            Vector3 v = q.eulerAngles;
            v.InvertX();
            return Quaternion.Euler(v);
        }

        public static Quaternion InverY(this Quaternion q)
        {
            Vector3 v = q.eulerAngles;
            v.InvertY();
            return Quaternion.Euler(v);
        }

        public static Quaternion InvertZ(this Quaternion q)
        {
            Vector3 v = q.eulerAngles;
            v.InvertZ();
            return Quaternion.Euler(v);
        }
        #endregion
        #endregion
    }
}
