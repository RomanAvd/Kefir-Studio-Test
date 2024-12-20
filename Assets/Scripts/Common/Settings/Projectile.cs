﻿using UnityEngine;

namespace Asteroids.Common.Settings
{
    public interface IProjectile
    {
        IMovingObjectSettings MovingObjectSettings { get; }
    }

    [CreateAssetMenu(menuName = "Asteroids/Projectile", fileName = "Projectile")]
    public sealed class Projectile : ScriptableObject, IProjectile
    {
        [SerializeField]
        private MovingObjectSettings _movingObjectSettings;

        public IMovingObjectSettings MovingObjectSettings => _movingObjectSettings;
    }
}