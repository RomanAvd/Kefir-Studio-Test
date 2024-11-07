using UnityEngine;

namespace Asteroids.Controller.MovingObjects
{
    public struct MovingObjectData
    {
        public int Id { get; }
        public Vector2 Position { get; }
        public string ResourceKey { get; }

        public MovingObjectData(Vector2 position, int id, string resourceKey)
        {
            Position = position;
            Id = id;
            ResourceKey = resourceKey;
        }
    }
}