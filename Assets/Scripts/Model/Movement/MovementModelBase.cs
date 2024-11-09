using Asteroids.Common.Settings;
using UnityEngine;

namespace Asteriods.Model.Movement
{
    public abstract class MovementModelBase : IMovementModel
    {
        public Vector2 Position => _position;
        public bool SeamlessMovement { get; }
        public float Rotation { get; private set; }
        protected Vector2 _position;
        protected readonly float _speed;
        protected readonly Vector2 _direction;
        private bool _rotateTowardsDirection;
        private readonly ISeamlessPositionHelper _seamlessPositionHelper;

        public MovementModelBase(Vector2 position, Vector2 direction, IMovingObjectSettings settings, ISeamlessPositionHelper seamlessPositionHelper)
        {
            _position = position;
            _speed = Random.Range(settings.MinSpeed, settings.MaxSpeed);
            _direction = direction;
            _seamlessPositionHelper = seamlessPositionHelper;
            _rotateTowardsDirection = settings.RotateTowardsDirection;
            SeamlessMovement = settings.SeamlessMovement;
        }

        protected abstract void UpdatePositionInternal(float timeDelta);

        public Vector2 UpdatePosition(float timeDelta)
        {
            UpdatePositionInternal(timeDelta);
            if (_rotateTowardsDirection)
            {
                Rotation = Vector2.SignedAngle(Vector2.up, _direction);
            }
            if (SeamlessMovement)
                _position = _seamlessPositionHelper.UpdateSeamlessPosition(_position);
            return _position;
        }

    }
}