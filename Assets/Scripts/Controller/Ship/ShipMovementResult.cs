using Asteroids.Common.Observer;
using UnityEngine;

namespace Asteroids.Controller.Ship
{
    public interface IShipMovementResult : IResult
    {
        float Speed { get; }
        float Rotation { get; }
        Vector2 Position { get; }
    }

    internal sealed class ShipMovementResult : IShipMovementResult
    {
        public float Speed { get; }
        public float Rotation { get; }
        public Vector2 Position { get; }

        public ShipMovementResult(float rotation, Vector2 position, float speed)
        {
            Rotation = rotation;
            Position = position;
            Speed = speed;
        }
    }
}