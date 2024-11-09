using Asteroids.Common;
using Asteroids.Common.Interfaces;
using Asteroids.Common.Observer;
using Asteroids.Controller.Input;
using Asteroids.Model.Ship;
using UnityEditorInternal;
using UnityEngine;

namespace Asteroids.Controller.Ship
{
    internal sealed class PlayerShipController : ITickable
    {
        private readonly IResultListener _resultListener;
        private readonly IShipModel _shipModel;
        private readonly IPlayerInput _playerInput;

        public PlayerShipController(IResultListener resultListener, IShipModel shipModel, IPlayerInput playerInput)
        {
            _resultListener = resultListener;
            _shipModel = shipModel;
            _playerInput = playerInput;
        }

        public void Tick(float deltaTime)
        {
            _shipModel.UpdateShipMovement(deltaTime, _playerInput.GetRotationAxis(), _playerInput.ThrustPressed());
            _shipModel.PrimaryWeapon.Update();
            _shipModel.SecondaryWeapon.Update();

            if (_playerInput.PrimaryPressed())
            {
                _shipModel.PrimaryWeapon.TryShoot(_shipModel.Position, Vector2.up.Rotate(_shipModel.RotationAngle));
            }
            if (_playerInput.SecondaryPressed())
            {
                _shipModel.SecondaryWeapon.TryShoot(_shipModel.Position, Vector2.up.Rotate(_shipModel.RotationAngle));
            }

            var result = new ShipMovementResult(_shipModel.RotationAngle, _shipModel.Position, _shipModel.Speed);
            _resultListener.SendResult(result);
        }
    }
}