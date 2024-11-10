using Asteroids.Common.Observer;
using UnityEngine;

namespace Asteroids.Controller.Ship
{
    public interface IShipMovementResult : IResult
    {
        float Speed { get; }
        float Rotation { get; }
        Vector2 Position { get; }
        bool ThrustEnabled { get; }
    }
}