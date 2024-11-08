using System.Collections.Generic;
using Asteroids.Common.MonoInjection;
using Asteroids.Common.Observer;
using Asteroids.Controller.MovingObjects;
using Asteroids.View.ResourceLoading;
using UnityEngine;
using UnityEngine.Pool;

namespace Asteroids.View.View.MovingObjects
{
    internal sealed class MovingObjectsView : MonoBehaviour, IResultReceiver<IMovingObjectsResult>
    {
        private Dictionary<string, ObjectPool<IMovingObject>> _pools;
        private Dictionary<int, IMovingObject> _activeObjects;
        private IGameObjectFactory _gameObjectFactory;

        private void Start()
        {
            _pools = new Dictionary<string, ObjectPool<IMovingObject>>();
            _activeObjects = new Dictionary<int, IMovingObject>();
        }

        [Inject]
        private void Initialize(IGameObjectFactory gameObjectFactory, IResultObserver observer)
        {
            _gameObjectFactory = gameObjectFactory;
            observer.Bind(this);
        }

        public void OnResultReceived(IMovingObjectsResult result)
        {
            foreach (var removedObject in result.RemovedObjects)
            {
                if (_activeObjects.TryGetValue(removedObject, out var viewToRemove))
                {
                    _pools[viewToRemove.Key].Release(viewToRemove);
                }

                _activeObjects.Remove(removedObject);
            }

            foreach (var activeObject in result.MovingObjects)
            {
                if (_activeObjects.ContainsKey(activeObject.Id))
                {
                    _activeObjects[activeObject.Id].UpdatePosition(activeObject.Position);
                    continue;
                }

                if (!_pools.ContainsKey(activeObject.ResourceKey))
                {
                    var pool = new ObjectPool<IMovingObject>(() =>CreateInstance(activeObject.ResourceKey),
                        (m) => m.Show(),
                        (m) => m.Hide()
                        );
                    _pools.Add(activeObject.ResourceKey, pool);
                }

                var instance = _pools[activeObject.ResourceKey].Get();
                instance.UpdatePosition(activeObject.Position);
                _activeObjects.Add(activeObject.Id, instance);
            }

        }

        private IMovingObject CreateInstance(string resourceKey)
        {
            var prefab = ResourceLoader.LoadMovingObject(resourceKey);
            var instance = _gameObjectFactory.Instantitiate(prefab, Vector3.zero, Quaternion.identity, transform);
            instance.Setup(resourceKey);
            return instance;
        }
    }
}