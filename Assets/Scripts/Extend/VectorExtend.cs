using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityEngine
{
    public static class VectorExtend
    {
        public static Vector2 Clamp(this Vector2 vector2, Vector2 value, Vector2 min, Vector2 max)
        {
            value.x = Mathf.Clamp(value.x, min.x, max.x);
            value.y = Mathf.Clamp(value.y, min.y, max.y);

            return value;
        }

        public static Vector3 Clamp(this Vector3 vector3,  Vector3 value, Vector3 min, Vector3 max)
        {
            value.x = Mathf.Clamp(value.x, min.x, max.x);
            value.y = Mathf.Clamp(value.y, min.y, max.y);
            value.z = Mathf.Clamp(value.z, min.z, max.z);

            return value;
        }
    }
}
