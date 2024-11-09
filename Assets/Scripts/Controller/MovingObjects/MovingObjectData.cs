using UnityEngine;

namespace Asteroids.Controller.MovingObjects
{
    public struct MovingObjectData
    {
        public int Id { get; }
        public Vector2 Position { get; }
        public float Rotation { get; }
        public string ResourceKey { get; }

        public MovingObjectData(Vector2 position, float rotation, int id, string resourceKey)
        {
            Position = position;
            Rotation = rotation;
            Id = id;
            ResourceKey = resourceKey;
        }
    }
}