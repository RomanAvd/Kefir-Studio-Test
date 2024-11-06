using UnityEngine;

namespace Asteriods.Model.Movement
{
    public interface IPositionProvider
    {
        Vector2 Position { get; }
    }
}