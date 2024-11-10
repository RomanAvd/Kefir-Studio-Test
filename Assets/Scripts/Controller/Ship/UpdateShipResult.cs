using Asteroids.Common.Enums;
using Asteroids.Controller.CommonResults;
using UnityEngine;

namespace Asteroids.Controller.Ship
{
    public interface IUpdateShipResult : IUpdateShipStatusResult, IShipMovementResult, IUpdateWeaponResult
    {

    }

    internal sealed class UpdateShipResult : IUpdateShipResult
    {
        public float Speed { get; }
        public float Rotation { get; }
        public Vector2 Position { get; }
        public bool ThrustEnabled { get; }
        public ShipStatus Status { get; }
        public int Charges { get; }
        public float Cooldown { get; }
        public float CooldownRemaining { get; }

        public UpdateShipResult(float rotation,
                                Vector2 position,
                                float speed,
                                ShipStatus status,
                                int charges,
                                float cooldown,
                                float cooldownRemaining,
                                bool thrustEnabled)
        {
            Rotation = rotation;
            Position = position;
            Speed = speed;
            Status = status;
            Charges = charges;
            Cooldown = cooldown;
            CooldownRemaining = cooldownRemaining;
            ThrustEnabled = thrustEnabled;
        }
    }
}