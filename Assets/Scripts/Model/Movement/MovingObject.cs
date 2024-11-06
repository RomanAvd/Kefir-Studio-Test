using UnityEngine;

namespace Asteriods.Model.Movement
{
    public interface IMovingObject
    {
        int Id { get; }
        string ResourceKey { get; }
        Vector2 Position { get; }
    }

    internal interface IMovingObjectInternal : IMovingObject
    {
        void UpdatePosition(float timeDelta);
        bool SeamlessMovement { get; }
    }

    internal sealed class MovingObject : IMovingObjectInternal
    {
        public int Id { get; }
        public string ResourceKey => _resourceKey;
        public Vector2 Position { get; private set; }
        public bool SeamlessMovement => _movementModel.SeamlessMovement;

        private readonly string _resourceKey;
        private readonly IMovementModel _movementModel;

        public MovingObject(string resourceKey, IMovementModel movementModel)
        {
            Id = GetHashCode();
            _resourceKey = resourceKey;
            _movementModel = movementModel;
        }

        public void UpdatePosition(float timeDelta)
        {
            Position = _movementModel.UpdatePosition(timeDelta);
        }

    }
}