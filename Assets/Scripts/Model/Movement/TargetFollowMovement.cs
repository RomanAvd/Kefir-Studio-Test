using Asteroids.Common;
using Asteroids.Common.Settings;
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

        public TargetFollowMovement(Vector2 position, Vector2 direction, IMovingObjectSettings settings, ISeamlessPositionHelper seamlessPositionHelper) : base(position, direction, settings, seamlessPositionHelper)
        {
            _randomDirection = Random.insideUnitCircle;
        }

        protected override void UpdatePositionInternal(float timeDelta)
        {
            _direction = _target.TryGetPosition(out var position) ? (position - _position).normalized : _randomDirection;
            _position += _direction * _speed * timeDelta;
        }

        public void SetTarget(IPositionProvider target)
        {
            _target = target;
        }
    }
}