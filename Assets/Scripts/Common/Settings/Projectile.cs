using UnityEngine;

namespace Asteroids.Common.Settings
{
    public interface IProjectile
    {
        string Key { get; }
        float Speed { get; }
        bool DestroyOnCollision { get; }
    }

    [CreateAssetMenu(menuName = "Asteroids/Projectile", fileName = "Projectile")]
    public sealed class Projectile : ScriptableObject, IProjectile
    {
        [field: SerializeField]
        public string Key { get; private set; }
        [field: SerializeField]
        public float Speed { get; private set; }
        [field: SerializeField]
        public bool DestroyOnCollision { get; private set; }
    }
}