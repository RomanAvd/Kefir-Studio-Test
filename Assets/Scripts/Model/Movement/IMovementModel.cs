using UnityEngine;

namespace Asteriods.Model.Movement
{
    public interface IMovementModel
    {
        Vector2 UpdatePosition(float timeDelta);
        bool SeamlessMovement { get; }
    }
}