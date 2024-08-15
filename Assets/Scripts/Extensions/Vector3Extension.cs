using UnityEngine;

namespace Extensions
{
    public static class Vector3Extension
    {
        public static Vector2Int ToVector2Int(this Vector3 vector3)
        {
            return new Vector2Int(Mathf.RoundToInt(vector3.x), Mathf.RoundToInt(vector3.y));
        }
    }
}