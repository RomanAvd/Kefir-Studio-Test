using System;
using UnityEngine;

namespace Asteroids.Common
{
    public static class Extensions
    {
        public static Vector2 Rotate(this Vector2 origin, float angle)
        {
            var rad = Mathf.Deg2Rad * angle;
            var x = origin.x * Mathf.Cos(rad) - origin.y * Mathf.Sin(rad);
            var y = origin.x * Mathf.Sin(rad) + origin.y * Mathf.Cos(rad);
            return new Vector2(x, y);
        }
    }
}