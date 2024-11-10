using Asteroids.Common.Settings;
using UnityEngine;

namespace Asteriods.Model.Movement
{
    public abstract class MovementModelBase : IMovementModel
    {
        public Vector2 Position => _position;
        public bool SeamlessMovement { get; }
        public bool DestroyOnCollision { get; }
        public float Rotation { get; private set; }

        protected Vector2 _position;
        protected Vector2 _direction;
        private bool _rotateTowardsDirection;

        protected readonly float _speed;
        private readonly ISeamlessPositionHelper _seamlessPositionHelper;

        public MovementModelBase(Vector2 position, Vector2 direction, IMovingObjectSettings settings, ISeamlessPositionHelper seamlessPositionHelper)
        {
            _position = position;
            _speed = Random.Range(settings.MinSpeed, settings.MaxSpeed);
            _direction = direction;
            _seamlessPositionHelper = seamlessPositionHelper;
            _rotateTowardsDirection = settings.RotateTowardsDirection;
            SeamlessMovement = settings.SeamlessMovement;
            DestroyOnCollision = settings.DestroyOnCollision;
        }

        protected abstract void UpdatePositionInternal(float timeDelta);

        public void UpdatePosition(float timeDelta)
        {
            UpdatePositionInternal(timeDelta);
            if (_rotateTowardsDirection)
            {
                Rotation = Vector2.SignedAngle(Vector2.up, _direction);
            }
            if (SeamlessMovement)
                _position = _seamlessPositionHelper.UpdateSeamlessPosition(_position);
        }
    }
}