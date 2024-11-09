using Asteroids.Common.Settings;
using UnityEngine;

namespace Asteriods.Model.Movement
{
    public sealed class LinearMovementModel : MovementModelBase
    {
        public LinearMovementModel(Vector2 position, Vector2 direction, IMovingObjectSettings settings, ISeamlessPositionHelper seamlessPositionHelper) : base(position, direction, settings, seamlessPositionHelper)
        {
        }

        protected override void UpdatePositionInternal(float timeDelta)
        {
            _position += _direction * _speed * timeDelta;
        }
    }
}