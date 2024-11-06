using System;
using Asteroids.Common.Enums;
using Asteroids.Common.Settings;
using Asteroids.Model.Ship;
using UnityEngine;

namespace Asteriods.Model.Movement
{
    internal interface IMovingObjectFactory
    {
        IMovingObjectInternal Create(IMovingObjectSettings settings, Vector2 position, Vector2 direction);
    }

    internal sealed class MovingObjectFactory : IMovingObjectFactory
    {
        private readonly IShipModel _shipModel;

        public MovingObjectFactory(IShipModel shipModel)
        {
            _shipModel = shipModel;
        }

        public IMovingObjectInternal Create(IMovingObjectSettings settings, Vector2 position, Vector2 direction)
        {
            IMovementModel movementModel = settings.Type switch
            {
                MovementType.None => throw new NotImplementedException($"Invalid movement type {settings.Type}"),
                MovementType.Linear => new LinearMovementModel(position, settings.Speed, direction, settings.SeamlessMovement),
                MovementType.Follow => new TargetFollowMovement(position, settings.Speed, Vector2.zero, settings.SeamlessMovement, _shipModel),
                _ => throw new ArgumentOutOfRangeException(nameof(settings.Type), settings.Type, null)
            };

            return new MovingObject(settings.ResourceKey, movementModel);
        }
    }
}