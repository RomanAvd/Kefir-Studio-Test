using UnityEngine;

namespace Asteriods.Model.Movement
{
    public sealed class LinearMovementModel : MovementModelBase
    {
        public LinearMovementModel(Vector2 position, float speed, Vector2 direction, bool seamlessMovement) : base(position, speed, direction, seamlessMovement)
        {
        }

        public override Vector2 UpdatePosition(float timeDelta)
        {
            _position += _direction.normalized * _speed * timeDelta;
            return _position;
        }
    }
}