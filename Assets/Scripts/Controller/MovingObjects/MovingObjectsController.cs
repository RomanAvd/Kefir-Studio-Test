using System.Collections.Generic;
using System.Linq;
using Asteriods.Model;
using Asteriods.Model.Movement;
using Asteroids.Common.Interfaces;
using Asteroids.Common.Observer;

namespace Asteroids.Controller.MovingObjects
{
    internal sealed class MovingObjectsController : ITickable
    {
        private readonly IMovingObjectsModel _movingObjectsModel;
        private readonly IScreenBorderModel _screenBorderModel;
        private readonly IResultListener _resultListener;
        private List<int> _toRemoveCache;

        public MovingObjectsController(IMovingObjectsModel movingObjectsModel, IScreenBorderModel screenBorderModel, IResultListener resultListener)
        {
            _movingObjectsModel = movingObjectsModel;
            _screenBorderModel = screenBorderModel;
            _resultListener = resultListener;
            _toRemoveCache = new List<int>();
        }
        public void Tick(float deltaTime)
        {
            _toRemoveCache.Clear();
            foreach (var movingObject in _movingObjectsModel.MovingObjects)
            {
                movingObject.MovementModel.UpdatePosition(deltaTime);
                if (!movingObject.MovementModel.SeamlessMovement && _screenBorderModel.CheckOutOfBounds(movingObject.MovementModel.Position))
                {
                    _toRemoveCache.Add(movingObject.Id);
                }
            }

            foreach (var objectToRemove in _toRemoveCache)
            {
                _movingObjectsModel.Remove(objectToRemove);
            }

            var movingObjectsData =
                _movingObjectsModel.MovingObjects.Select(m => new MovingObjectData(m.MovementModel.Position, m.MovementModel.Rotation, m.Id, m.ResourceKey));
            var result = new MovingObjectsResult(movingObjectsData, _toRemoveCache);
            _resultListener.SendResult(result);
        }
    }
}