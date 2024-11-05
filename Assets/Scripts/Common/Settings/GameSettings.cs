using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids.Common.Settings
{
    public interface IGameSettings
    {
        InputActionAsset PlayerInput { get; }
        IShipSettings ShipSettings { get; }
    }

    [CreateAssetMenu(menuName = "Asteroids/GameSettings", fileName = "Game Settings")]
    public sealed class GameSettings : ScriptableObject, IGameSettings
    {
        [field: SerializeField]
        public InputActionAsset PlayerInput { get; private set; }
        [SerializeField]
        private ShipSettings _shipSettings;

        public IShipSettings ShipSettings => _shipSettings;

    }
}