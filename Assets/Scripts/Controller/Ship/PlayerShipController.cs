using Asteriods.Model.Movement;
using Asteroids.Common;
using Asteroids.Common.Enums;
using Asteroids.Common.Interfaces;
using Asteroids.Common.Observer;
using Asteroids.Controller.Common;
using Asteroids.Controller.Input;
using Asteroids.Model.Ship;
using UnityEngine;

namespace Asteroids.Controller.Ship
{
    internal sealed class PlayerShipController : ITickable
    {
        private readonly IResultListener _resultListener;
        private readonly IShipModel _shipModel;
        private readonly IPlayerInput _playerInput;
        private readonly IMovingObjectsModel _movingObjectsModel;

        public PlayerShipController(IResultListener resultListener, IShipModel shipModel, IPlayerInput playerInput, IMovingObjectsModel movingObjectsModel)
        {
            _resultListener = resultListener;
            _shipModel = shipModel;
            _playerInput = playerInput;
            _movingObjectsModel = movingObjectsModel;
        }

        public void Tick(float deltaTime)
        {
            if (_shipModel.Status is ShipStatus.Dead or ShipStatus.None)
                return;

            _shipModel.UpdateShip(deltaTime, _playerInput.GetRotationAxis(), _playerInput.ThrustPressed());

            if (_playerInput.PrimaryPressed() && _shipModel.TryShootPrimary(out var projectile))
            {
                _movingObjectsModel.Add(projectile.MovingObjectSettings, _shipModel.Position, Vector2.up.Rotate(_shipModel.RotationAngle));
            }
            if (_playerInput.SecondaryPressed() && _shipModel.TryShootSecondary(out projectile))
            {
                _movingObjectsModel.Add(projectile.MovingObjectSettings, _shipModel.Position, Vector2.up.Rotate(_shipModel.RotationAngle));
            }

            var result = ResultFactory.GetUpdateShipResult(_shipModel);
            _resultListener.SendResult(result);
        }
    }
}