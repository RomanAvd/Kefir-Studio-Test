using System;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Common.Settings
{
    public interface IEnemySpawnSettings
    {
        float SpawnDelay { get; }
        int SpawnLimit { get; }
        int MinimumSpawnedEnemies { get; }
        IReadOnlyList<IEnemySpawnData> Enemies { get; }
    }

    [Serializable]
    public sealed class EnemySpawnSettings : IEnemySpawnSettings
    {
        [field: SerializeField]
        public float SpawnDelay { get; private set; }

        [field: SerializeField]
        public int SpawnLimit { get; private set; }
        [field: SerializeField]
        public int MinimumSpawnedEnemies { get; private set; }

        public IReadOnlyList<IEnemySpawnData> Enemies => _enemies;

        [SerializeField]
        private List<EnemySpawnData> _enemies;
    }
}