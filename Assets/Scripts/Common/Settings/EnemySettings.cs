using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Common.Settings
{
    public interface IEnemySettings
    {
        int Score { get; }
        IMovingObjectSettings MovingObjectSettings { get; }
        IReadOnlyList<IEnemySettings> NestedEnemies { get; }
    }

    [CreateAssetMenu(menuName = "Asteroids/Enemy", fileName = "Weapon")]
    public sealed class EnemySettings : ScriptableObject, IEnemySettings
    {
        [SerializeField]
        private MovingObjectSettings _movingObjectSettings;
        [field: SerializeField]
        public int Score { get; private set; }
        [SerializeField]
        private List<EnemySettings> _nestedEnemies;
        public IReadOnlyList<IEnemySettings> NestedEnemies => _nestedEnemies;
        public IMovingObjectSettings MovingObjectSettings => _movingObjectSettings;
    }
}