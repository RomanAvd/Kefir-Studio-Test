using UnityEngine;

namespace Asteriods.Model.Movement
{
    public interface IPositionProvider
    {
        bool TryGetPosition(out Vector2 position);
    }
}