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
            var result = new ShipMovementResult(_shipModel.RotationAngle, _shipModel.Position, _shipModel.Speed);
            Debug.Log($"rotation {_playerInput.GetRotationAxis()} thrust {_playerInput.ThrustPressed()} ship angle {_shipModel.RotationAngle} ship speed {_shipModel.Speed} position {_shipModel.Position}");
            _resultListener.SendResult(result);
        }
    }
}