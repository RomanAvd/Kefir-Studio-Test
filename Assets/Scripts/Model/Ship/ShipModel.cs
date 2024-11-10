using Asteriods.Model;
using Asteriods.Model.Movement;
using Asteriods.Model.Weapons;
using Asteroids.Common;
using Asteroids.Common.Enums;
using Asteroids.Common.Settings;
using UnityEngine;

namespace Asteroids.Model.Ship
{
    public interface IShipModel : IPositionProvider
    {
        ShipStatus Status { get; }
        float Speed { get; }
        float RotationAngle { get; }
        IWeaponModel PrimaryWeapon { get; }
        IWeaponModel SecondaryWeapon { get; }
        Vector2 Position { get; }
        void UpdateShip(float timeDelta, float rotationAxis, bool thrustEnabled);
        void Die();
        void Reset();
        bool TryShootPrimary(out IProjectile projectile);
        bool TryShootSecondary(out IProjectile projectile);
    }

    public class ShipModel : IShipModel
    {
        public ShipStatus Status { get; private set; }
        public float Speed => _speedVector.magnitude;
        public float RotationAngle { get; private set; }
        public Vector2 Position { get; private set; }
        public IWeaponModel PrimaryWeapon => _primaryWeapon;
        public IWeaponModel SecondaryWeapon => _secondaryWeapon;
        private IWeaponModelInternal _primaryWeapon;
        private IWeaponModelInternal _secondaryWeapon;

        private Vector2 _speedVector;
        private float _invincibilityDuration;

        private readonly IShipSettings _shipSettings;
        private readonly IScreenBorderModel _screenBorderModel;

        public ShipModel(IShipSettings shipSettings, IScreenBorderModel screenBorderModel)
        {
            _shipSettings = shipSettings;
            _screenBorderModel = screenBorderModel;
            _primaryWeapon = new WeaponModel(shipSettings.PrimaryWeapon);
            _secondaryWeapon = new WeaponModel(shipSettings.SecondaryWeapon);
        }

        public void UpdateShip(float timeDelta, float rotationAxis, bool thrustEnabled)
        {
            UpdatePosition(timeDelta, rotationAxis, thrustEnabled);
            _primaryWeapon.Update();
            _secondaryWeapon.Update();
        }

        private void UpdatePosition(float timeDelta, float rotationAxis, bool thrustEnabled)
        {
            if (_invincibilityDuration > 0)
            {
                _invincibilityDuration -= timeDelta;
            }
            Status = _invincibilityDuration <= 0 ? ShipStatus.Default : ShipStatus.Invincible;

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

        public void Die()
        {
            Status = ShipStatus.Dead;
        }

        public void Reset()
        {
            Position = Vector2.zero;
            RotationAngle = 0;
            _speedVector = Vector2.zero;
            Status = ShipStatus.Invincible;
            _invincibilityDuration = _shipSettings.SpawnInvincibilityDuration;
            _primaryWeapon.Reset();
            _secondaryWeapon.Reset();
        }

        public bool TryShootPrimary(out IProjectile projectile)
        {
            var success = _primaryWeapon.TryShoot(out projectile);
            if (success)
                CancelInvincibility();
            return success;
        }

        public bool TryShootSecondary(out IProjectile projectile)
        {
            var success = _secondaryWeapon.TryShoot(out projectile);
            if (success)
                CancelInvincibility();
            return success;
        }

        private void CancelInvincibility()
        {
            if (Status != ShipStatus.Invincible)
                return;

            Status = ShipStatus.Default;
            _invincibilityDuration = 0;
        }

        public bool TryGetPosition(out Vector2 position)
        {
            position = Position;
            return Status == ShipStatus.Default;
        }
    }
}