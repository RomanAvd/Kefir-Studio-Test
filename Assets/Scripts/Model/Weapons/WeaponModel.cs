using System.Collections.Generic;
using System.Linq;
using Asteroids.Common.Settings;
using UnityEngine;

namespace Asteriods.Model.Weapons
{
    public interface IWeaponModel
    {
        int Charges { get; }
        float ChargeCooldown { get; }
        float CooldownRemaining { get; }
    }

    internal interface IWeaponModelInternal : IWeaponModel
    {
        bool TryShoot(out IProjectile projectile);
        void Update();
        void Reset();
    }

    internal sealed class WeaponModel : IWeaponModelInternal
    {
        public int Charges => _charges;
        public float ChargeCooldown => _weaponSettings.FireCooldown;
        public float CooldownRemaining => _chargeCooldownQueue.TryPeek(out var cooldown) ? cooldown - Time.time : 0;
        private readonly IWeaponSettings _weaponSettings;
        private int _charges;
        private Queue<float> _chargeCooldownQueue;

        public WeaponModel(IWeaponSettings weaponSettings)
        {
            _weaponSettings = weaponSettings;
            _chargeCooldownQueue = new Queue<float>(weaponSettings.Charges);
        }

        public bool TryShoot(out IProjectile projectile)
        {
            Update();
            projectile = default;
            if (_charges <= 0)
                return false;

            _charges--;
            _chargeCooldownQueue.Enqueue(_weaponSettings.FireCooldown + (_chargeCooldownQueue.Count > 0 ? _chargeCooldownQueue.Last() : Time.time));
            projectile = _weaponSettings.Projectile;
            return true;

        }

        public void Update()
        {
            if (_charges == 0 && _chargeCooldownQueue.Count == 0)
            {
                for (int i = 0; i < _weaponSettings.Charges; i++)
                {
                    _chargeCooldownQueue.Enqueue(Time.time + _weaponSettings.FireCooldown * (i+1));
                }
            }

            if (_charges >= _weaponSettings.Charges)
                return;

            if (!_chargeCooldownQueue.TryPeek(out var cooldown))
                return;

            if (!(cooldown <= Time.time))
                return;

            _chargeCooldownQueue.Dequeue();
            _charges++;
        }

        public void Reset()
        {
            _chargeCooldownQueue.Clear();
            _charges = 0;
        }
    }
}