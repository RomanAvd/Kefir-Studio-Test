using UnityEngine;

namespace Asteriods.Model.Movement
{
    public sealed class LinearMovementModel : MovementModelBase
    {
        public LinearMovementModel(Vector2 position, float speed, Vector2 direction, bool seamlessMovement, ISeamlessPositionHelper seamlessPositionHelper) : base(position, speed, direction, seamlessMovement, seamlessPositionHelper)
        {
        }

        protected override void UpdatePositionInternal(float timeDelta)
        {
            _position += _direction.normalized * _speed * timeDelta;
        }
    }
}