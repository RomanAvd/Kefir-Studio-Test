using UnityEngine;

namespace Asteriods.Model.Movement
{
    public interface IMovementModel
    {
        void UpdatePosition(float timeDelta);
        public Vector2 Position { get; }
        float Rotation { get; }
        bool SeamlessMovement { get; }
        bool DestroyOnCollision { get; }
    }
}