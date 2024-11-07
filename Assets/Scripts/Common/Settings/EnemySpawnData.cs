using System;
using UnityEngine;

namespace Asteroids.Common.Settings
{
    public interface IEnemySpawnData
    {
        IEnemySettings Enemy { get; }
        int Weight { get; }
    }

    [Serializable]
    public sealed class EnemySpawnData : IEnemySpawnData
    {
        [SerializeField]
        private EnemySettings _enemy;

        public IEnemySettings Enemy => _enemy;

        [field: SerializeField]
        public int Weight { get; private set; }
    }
}