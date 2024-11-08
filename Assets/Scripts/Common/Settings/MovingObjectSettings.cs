using System;
using Asteroids.Common.Enums;
using UnityEngine;

namespace Asteroids.Common.Settings
{
    public interface IMovingObjectSettings
    {
        string ResourceKey { get; }
        float MinSpeed { get; }
        float MaxSpeed { get; }
        MovementType Type { get; }
        bool SeamlessMovement { get; }
    }

    [Serializable]
    public sealed class MovingObjectSettings : IMovingObjectSettings
    {
        [field: SerializeField]
        public string ResourceKey { get; private set; }
        [field: SerializeField]
        public float MinSpeed { get; private set; }
        [field: SerializeField]
        public float MaxSpeed { get; private set; }
        [field: SerializeField]
        public MovementType Type { get; private set; }
        [field: SerializeField]
        public bool DestroyOnCollision { get; private set; }
        [field: SerializeField]
        public bool SeamlessMovement { get; private set; }
    }
}