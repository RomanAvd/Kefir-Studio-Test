using UnityEngine;

namespace Asteroids.Common
{
    public static class RandomHelper
    {
        public static Vector2 RandomNormalizedVector()
        {
            return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }
    }
}