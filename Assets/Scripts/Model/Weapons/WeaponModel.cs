using System.Collections.Generic;
using System.Linq;
using Asteriods.Model.Movement;
using Asteroids.Common.Settings;
using UnityEngine;

namespace Asteriods.Model.Weapons
{
    public interface IWeaponModel
    {
        int Charges { get; }
        float ChargeCooldown { get; }
        float CooldownRemaining { get; }
        bool TryShoot(Vector2 position, Vector2 direction);
        void Update();
    }

    internal sealed class WeaponModel : IWeaponModel
    {
        public int Charges => _charges;
        public float ChargeCooldown => _weaponSettings.FireCooldown;
        public float CooldownRemaining => _chargeCooldownQueue.TryPeek(out var cooldown) ? cooldown - Time.time : 0;
        private readonly IWeaponSettings _weaponSettings;
        private readonly IMovingObjectsSpawner _movingObjectsModel;
        private int _charges;
        private Queue<float> _chargeCooldownQueue;

        public WeaponModel(IWeaponSettings weaponSettings, IMovingObjectsSpawner movingObjectsModel)
        {
            _weaponSettings = weaponSettings;
            _movingObjectsModel = movingObjectsModel;
            _chargeCooldownQueue = new Queue<float>(weaponSettings.Charges);
        }

        public bool TryShoot(Vector2 position, Vector2 direction)
        {
            Update();
            if (_charges > 0)
            {
                _charges--;
                _chargeCooldownQueue.Enqueue(_weaponSettings.FireCooldown + (_chargeCooldownQueue.Count > 0 ? _chargeCooldownQueue.Last() : Time.time));
                _movingObjectsModel.Add(_weaponSettings.Projectile.MovingObjectSettings, position, direction);
                return true;
            }

            return false;
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
    }
}