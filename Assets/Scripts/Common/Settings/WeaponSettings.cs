using UnityEngine;

namespace Asteroids.Common.Settings
{
    public interface IWeaponSettings
    {
        IProjectile Projectile { get; }
        float FireCooldown { get; }
        int Charges { get; }
    }

    [CreateAssetMenu(menuName = "Asteroids/Weapon", fileName = "Weapon")]
    public sealed class WeaponSettings : ScriptableObject, IWeaponSettings
    {
        [SerializeField]
        private Projectile _projectile;
        [field: SerializeField]
        public float FireCooldown { get; private set; }
        [field: SerializeField]
        public int Charges { get; private set; }

        public IProjectile Projectile => _projectile;
    }
}