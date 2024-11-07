using UnityEngine;

namespace Asteroids.Common.Settings
{
    public interface IEnemySettings
    {
        int Score { get; }
        IMovingObjectSettings MovingObjectSettings { get; }
    }

    [CreateAssetMenu(menuName = "Asteroids/Enemy", fileName = "Weapon")]
    public sealed class EnemySettings : ScriptableObject, IEnemySettings
    {
        [SerializeField]
        private MovingObjectSettings _movingObjectSettings;
        [field: SerializeField]
        public int Score { get; private set; }

        public IMovingObjectSettings MovingObjectSettings => _movingObjectSettings;
    }
}