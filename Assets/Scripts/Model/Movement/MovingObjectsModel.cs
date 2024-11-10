using System;
using System.Collections.Generic;
using Asteroids.Common.Settings;
using UnityEngine;

namespace Asteriods.Model.Movement
{
    public interface IMovingObjectsModel : IMovingObjectsSpawner
    {
        IEnumerable<IMovingObject> MovingObjects { get; }
        bool TryGetObject(int id, out IMovingObject movingObject);
    }

    public interface IMovingObjectsSpawner
    {
        IMovingObject Add(IMovingObjectSettings settings, Vector2 position, Vector2 direction);
        void Remove(int id);
    }

    internal sealed class MovingObjectsModelModel : IMovingObjectsModel
    {
        private readonly IMovingObjectFactory _movingObjectFactory;
        public IEnumerable<IMovingObject> MovingObjects => _movingObjects.Values;
        private Dictionary<int, IMovingObject> _movingObjects;

        public MovingObjectsModelModel(IMovingObjectFactory movingObjectFactory)
        {
            _movingObjectFactory = movingObjectFactory;
            _movingObjects = new Dictionary<int, IMovingObject>();
        }

        public bool TryGetObject(int id, out IMovingObject movingObject)
        {
            bool success = _movingObjects.TryGetValue(id, out var movingObjectInternal);
            movingObject = movingObjectInternal;
            return success;
        }

        public IMovingObject Add(IMovingObjectSettings settings, Vector2 position, Vector2 direction)
        {
            var movingObject = _movingObjectFactory.Create(settings, position, direction);
            if (!_movingObjects.TryAdd(movingObject.Id, movingObject))
                throw new ArgumentException($"moving object with id {movingObject.Id} is already added");
            return movingObject;
        }

        public void Remove(int id)
        {
            _movingObjects.Remove(id);
        }
    }
}