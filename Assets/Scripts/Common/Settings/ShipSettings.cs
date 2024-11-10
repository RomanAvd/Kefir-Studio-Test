using System;
using UnityEngine;

namespace Asteroids.Common.Settings
{
    public interface IShipSettings
    {
        float MaxSpeed { get; }
        float AccelerationRate { get; }
        float DecelerationRate { get; }
        float RotationSpeed { get; }
        IWeaponSettings PrimaryWeapon { get; }
        IWeaponSettings SecondaryWeapon { get; }
        float SpawnInvincibilityDuration { get; }
    }

    [Serializable]
    public sealed class ShipSettings : IShipSettings
    {
        [SerializeField]
        private WeaponSettings _primaryWeapon;
        [SerializeField]
        private WeaponSettings _secondaryWeapon;
        [field: SerializeField]
        public float MaxSpeed { get; private set; }
        [field: SerializeField]
        public float AccelerationRate { get; private set; }
        [field: SerializeField]
        public float DecelerationRate { get; private set; }
        [field: SerializeField]
        public float RotationSpeed { get; private set; }
        [field: SerializeField]
        public float SpawnInvincibilityDuration { get; private set; }
        public IWeaponSettings PrimaryWeapon => _primaryWeapon;
        public IWeaponSettings SecondaryWeapon => _secondaryWeapon;
    }
}