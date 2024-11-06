using UnityEngine;

namespace Asteriods.Model.Movement
{
    public abstract class MovementModelBase : IMovementModel
    {
        public bool SeamlessMovement { get; }
        protected Vector2 _position;
        protected float _speed;
        protected Vector2 _direction;

        public MovementModelBase(Vector2 position, float speed, Vector2 direction, bool seamlessMovement)
        {
            _position = position;
            _speed = speed;
            _direction = direction;
            SeamlessMovement = seamlessMovement;
        }

        public abstract Vector2 UpdatePosition(float timeDelta);
    }
}