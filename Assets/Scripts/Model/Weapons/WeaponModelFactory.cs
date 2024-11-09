using Asteriods.Model.Movement;
using Asteroids.Common.Settings;

namespace Asteriods.Model.Weapons
{
    public interface IWeaponModelFactory
    {
        IWeaponModel Create(IWeaponSettings settings);
    }

    internal sealed class WeaponModelFactory : IWeaponModelFactory
    {
        private readonly IMovingObjectsSpawner _movingObjectsSpawner;

        public WeaponModelFactory(IMovingObjectsSpawner movingObjectsSpawner)
        {
            _movingObjectsSpawner = movingObjectsSpawner;
        }

        public IWeaponModel Create(IWeaponSettings settings)
        {
            return new WeaponModel(settings, _movingObjectsSpawner);
        }
    }
}