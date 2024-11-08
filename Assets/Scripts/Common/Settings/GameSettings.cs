using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids.Common.Settings
{
    public interface IGameSettings
    {
        InputActionAsset PlayerInput { get; }
        IShipSettings ShipSettings { get; }
        IEnemySpawnSettings SpawnSettings { get; }
    }

    [CreateAssetMenu(menuName = "Asteroids/GameSettings", fileName = "Game Settings")]
    public sealed class GameSettings : ScriptableObject, IGameSettings
    {
        [field: SerializeField]
        public InputActionAsset PlayerInput { get; private set; }
        [SerializeField]
        private ShipSettings _shipSettings;

        [SerializeField]
        private EnemySpawnSettings _enemySpawnSettings;

        public IShipSettings ShipSettings => _shipSettings;
        public IEnemySpawnSettings SpawnSettings => _enemySpawnSettings;
    }
}