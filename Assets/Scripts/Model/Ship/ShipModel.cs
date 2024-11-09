using Asteriods.Model;
using Asteriods.Model.Movement;
using Asteriods.Model.Weapons;
using Asteroids.Common;
using Asteroids.Common.Settings;
using UnityEngine;

namespace Asteroids.Model.Ship
{
    public interface IShipModel : IPositionProvider
    {
        float Speed { get; }
        float RotationAngle { get; }
        IWeaponModel PrimaryWeapon { get; }
        IWeaponModel SecondaryWeapon { get; }
        void UpdateShipMovement(float timeDelta, float rotationAxis, bool thrustEnabled);
    }

    public class ShipModel : IShipModel
    {
        public float Speed => _speedVector.magnitude;
        public float RotationAngle { get; private set; }
        public Vector2 Position { get; private set; }
        public IWeaponModel PrimaryWeapon { get; }
        public IWeaponModel SecondaryWeapon { get; }

        private Vector2 _speedVector;
        private readonly IShipSettings _shipSettings;
        private readonly IScreenBorderModel _screenBorderModel;

        public ShipModel(IShipSettings shipSettings, IScreenBorderModel screenBorderModel, IWeaponModelFactory weaponModelFactory)
        {
            _shipSettings = shipSettings;
            _screenBorderModel = screenBorderModel;
            PrimaryWeapon = weaponModelFactory.Create(shipSettings.PrimaryWeapon);
            SecondaryWeapon = weaponModelFactory.Create(shipSettings.SecondaryWeapon);
        }

        public void UpdateShipMovement(float timeDelta, float rotationAxis, bool thrustEnabled)
        {
            RotationAngle = (RotationAngle + rotationAxis * timeDelta * _shipSettings.RotationSpeed) % 360;

            if (thrustEnabled)
            {
                var accelerationVector = new Vector2(0, _shipSettings.AccelerationRate * timeDelta).Rotate(RotationAngle);
                _speedVector = Vector2.ClampMagnitude(_speedVector + accelerationVector, _shipSettings.MaxSpeed);
            }
            else
            {
                _speedVector = Vector2.Lerp(_speedVector, Vector2.zero, _shipSettings.DecelerationRate * timeDelta);
            }

            Position = _screenBorderModel.UpdateSeamlessPosition(Position + _speedVector);
        }
    }
}