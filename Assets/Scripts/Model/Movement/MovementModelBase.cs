using UnityEngine;

namespace Asteriods.Model.Movement
{
    public abstract class MovementModelBase : IMovementModel
    {
        public bool SeamlessMovement { get; }
        protected Vector2 _position;
        protected readonly float _speed;
        protected readonly Vector2 _direction;
        private readonly ISeamlessPositionHelper _seamlessPositionHelper;

        public MovementModelBase(Vector2 position, float speed, Vector2 direction, bool seamlessMovement, ISeamlessPositionHelper seamlessPositionHelper)
        {
            _position = position;
            _speed = speed;
            _direction = direction;
            _seamlessPositionHelper = seamlessPositionHelper;
            SeamlessMovement = seamlessMovement;
        }

        protected abstract void UpdatePositionInternal(float timeDelta);

        public Vector2 UpdatePosition(float timeDelta)
        {
            UpdatePositionInternal(timeDelta);
            if (SeamlessMovement)
                _position = _seamlessPositionHelper.UpdateSeamlessPosition(_position);
            return _position;
        }
    }
}