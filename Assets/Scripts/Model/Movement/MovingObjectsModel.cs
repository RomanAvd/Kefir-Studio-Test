using System;
using System.Collections.Generic;
using Asteroids.Common.Settings;
using UnityEngine;

namespace Asteriods.Model.Movement
{
    public interface IMovingObjectsModel
    {
        bool TryGetObject(int id, out IMovingObject movingObject);
        IMovingObject Add(IMovingObjectSettings settings, Vector2 position, Vector2 direction);
        void Remove(int id);
    }

    public interface IUpdateMovingObjects
    {
        IEnumerable<IMovingObject> MovingObjects { get; }
        void Update(float timeDelta, out IEnumerable<int> removedObjects);

    }

    internal sealed class MovingObjectsModel : IMovingObjectsModel, IUpdateMovingObjects
    {
        private readonly IScreenBorderModel _screenBorderModel;
        private readonly IMovingObjectFactory _movingObjectFactory;
        public IEnumerable<IMovingObject> MovingObjects => _movingObjects.Values;
        private Dictionary<int, IMovingObjectInternal> _movingObjects;

        public MovingObjectsModel(IScreenBorderModel screenBorderModel, IMovingObjectFactory movingObjectFactory)
        {
            _screenBorderModel = screenBorderModel;
            _movingObjectFactory = movingObjectFactory;
            _movingObjects = new Dictionary<int, IMovingObjectInternal>();
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

        public void Update(float timeDelta, out IEnumerable<int> removedObjects)
        {
            var toRemove = new List<int>();
            removedObjects = toRemove;
            foreach (var movingObject in _movingObjects)
            {
                movingObject.Value.UpdatePosition(timeDelta);
                if (!movingObject.Value.SeamlessMovement && _screenBorderModel.CheckOutOfBounds(movingObject.Value.Position))
                {
                    toRemove.Add(movingObject.Key);
                }
            }

            foreach (var objectToRemove in toRemove)
            {
                _movingObjects.Remove(objectToRemove);
            }
        }
    }
}