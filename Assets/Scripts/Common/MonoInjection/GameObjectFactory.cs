using UnityEngine;

namespace Asteroids.Common.MonoInjection
{
    public interface IGameObjectFactory
    {
        T Instantitiate<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent) where T : MonoBehaviour;
    }

    public sealed class GameObjectFactory : IGameObjectFactory
    {
        private readonly IContainer _container;

        public GameObjectFactory(IContainer container)
        {
            _container = container;
        }

        public T Instantitiate<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent) where T : MonoBehaviour
        {
            var instance = Object.Instantiate(prefab, position, rotation, parent);
            _container.ResolveGameObject(instance.gameObject, true);
            return instance;
        }
    }
}