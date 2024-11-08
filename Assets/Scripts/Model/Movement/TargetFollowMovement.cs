using UnityEngine;

namespace Asteriods.Model.Movement
{
    public sealed class TargetFollowMovement : MovementModelBase
    {
        private readonly IPositionProvider _target;

        public TargetFollowMovement(Vector2 position, float speed, Vector2 direction, bool seamlessMovement,
                                    ISeamlessPositionHelper seamlessPositionHelper, IPositionProvider target)
            : base(position, speed, direction, seamlessMovement, seamlessPositionHelper)
        {
            _target = target;
        }

        protected override void UpdatePositionInternal(float timeDelta)
        {
            var direction = (_target.Position - _position).normalized;
            _position += direction * _speed * timeDelta;
        }
    }
}