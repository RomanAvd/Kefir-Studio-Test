using System;
using Asteroids.Common.Enums;
using Asteroids.Common.Settings;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteriods.Model.Movement
{
    internal interface IMovingObjectFactory
    {
        IMovingObject Create(IMovingObjectSettings settings, Vector2 position, Vector2 direction);
    }

    internal sealed class MovingObjectFactory : IMovingObjectFactory
    {
        private readonly ISeamlessPositionHelper _seamlessPositionHelper;

        public MovingObjectFactory(ISeamlessPositionHelper seamlessPositionHelper)
        {
            _seamlessPositionHelper = seamlessPositionHelper;
        }

        public IMovingObject Create(IMovingObjectSettings settings, Vector2 position, Vector2 direction)
        {
            var speed = Random.Range(settings.MinSpeed, settings.MaxSpeed);
            IMovementModel movementModel = settings.Type switch
            {
                MovementType.None => throw new NotImplementedException($"Invalid movement type {settings.Type}"),
                MovementType.Linear => new LinearMovementModel(position, direction, settings, _seamlessPositionHelper),
                MovementType.Follow => new TargetFollowMovement(position, Vector2.zero, settings, _seamlessPositionHelper),
                _ => throw new ArgumentOutOfRangeException(nameof(settings.Type), settings.Type, null)
            };

            return new MovingObject(settings.ResourceKey, movementModel);
        }
    }
}