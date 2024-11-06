using Asteroids.Common.Settings;
using UnityEngine.InputSystem;

namespace Asteroids.Controller.Input
{
    internal interface IPlayerInput
    {
        float GetRotationAxis();
        bool ThrustPressed();
        bool PrimaryPressed();
        bool SecondaryPressed();
    }

    internal sealed class PlayerInput : IPlayerInput
    {
        private readonly InputActionAsset _inputActionAsset;
        private InputAction _thrustAction;
        private InputAction _rotationAction;
        private InputAction _primaryWeaponAction;
        private InputAction _secondaryWeaponAction;

        public PlayerInput(IGameSettings settings)
        {
            _inputActionAsset = settings.PlayerInput;
            _inputActionAsset.Enable();
            _thrustAction = _inputActionAsset.FindAction("Thrust");
            _rotationAction = _inputActionAsset.FindAction("Rotation");
            _primaryWeaponAction = _inputActionAsset.FindAction("Primary");
            _secondaryWeaponAction = _inputActionAsset.FindAction("Secondary");
        }

        public float GetRotationAxis()
        {
            return _rotationAction.ReadValue<float>();
        }

        public bool ThrustPressed()
        {
            return _thrustAction.IsPressed();
        }

        public bool PrimaryPressed()
        {
            return _primaryWeaponAction.IsPressed();
        }

        public bool SecondaryPressed()
        {
            return _secondaryWeaponAction.IsPressed();
        }
    }
}