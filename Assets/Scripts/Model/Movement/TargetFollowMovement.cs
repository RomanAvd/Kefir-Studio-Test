using Asteroids.Common;
using UnityEngine;

namespace Asteriods.Model.Movement
{
    public interface ITargetFollowMovement
    {
        void SetTarget(IPositionProvider target);
    }

    public sealed class TargetFollowMovement : MovementModelBase, ITargetFollowMovement
    {
        private IPositionProvider _target;
        private Vector2 _randomDirection;

        public TargetFollowMovement(Vector2 position, float speed, Vector2 direction, bool seamlessMovement,
                                    ISeamlessPositionHelper seamlessPositionHelper)
            : base(position, speed, direction, seamlessMovement, seamlessPositionHelper)
        {
            _randomDirection = RandomHelper.RandomNormalizedVector();
        }

        protected override void UpdatePositionInternal(float timeDelta)
        {
            var direction = _target != null ? (_target.Position - _position).normalized : _randomDirection;
            _position += direction * _speed * timeDelta;
        }

        public void SetTarget(IPositionProvider target)
        {
            _target = target;
        }
    }
}