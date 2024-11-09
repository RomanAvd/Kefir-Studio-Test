using UnityEngine;

namespace Asteriods.Model.Movement
{
    public interface IMovementModel
    {
        Vector2 UpdatePosition(float timeDelta);
        public Vector2 Position { get; }
        bool SeamlessMovement { get; }
    }
}