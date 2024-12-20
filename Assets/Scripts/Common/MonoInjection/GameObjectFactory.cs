﻿using UnityEngine;

namespace Asteroids.Common.MonoInjection
{
    public interface IGameObjectFactory
    {
        T Instantiate<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent) where T : MonoBehaviour;
    }

    internal sealed class GameObjectFactory : IGameObjectFactory
    {
        private readonly IContainer _container;

        public GameObjectFactory(IContainer container)
        {
            _container = container;
        }

        public T Instantiate<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent) where T : MonoBehaviour
        {
            var instance = Object.Instantiate(prefab, position, rotation, parent);
            _container.ResolveGameObject(instance.gameObject, true);
            return instance;
        }
    }
}