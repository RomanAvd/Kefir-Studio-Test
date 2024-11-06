using UnityEngine;

namespace Asteriods.Model.Movement
{
    public sealed class TargetFollowMovement : MovementModelBase
    {
        private readonly IPositionProvider _target;

        public TargetFollowMovement(Vector2 position, float speed, Vector2 direction, bool seamlessMovement, IPositionProvider target) : base(position, speed, direction, seamlessMovement)
        {
            _target = target;
        }

        public override Vector2 UpdatePosition(float timeDelta)
        {
            var direction = (_target.Position - _position).normalized;
            _position += direction * _speed * timeDelta;
            return _position;
        }

    }
}