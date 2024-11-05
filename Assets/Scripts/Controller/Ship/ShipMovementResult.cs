using Asteroids.Common.Observer;
using UnityEngine;

namespace Asteroids.Controller.Ship
{
    public interface IShipMovementResult : IResult
    {
        Vector2 SpeedVector { get; }
        float Rotation { get; }
    }

    internal sealed class ShipMovementResult : IShipMovementResult
    {
        public Vector2 SpeedVector { get; }
        public float Rotation { get; }

        public ShipMovementResult(Vector2 speedVector, float rotation)
        {
            SpeedVector = speedVector;
            Rotation = rotation;
        }
    }
}