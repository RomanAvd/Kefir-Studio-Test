using System.Linq;
using Asteriods.Model.Movement;
using Asteroids.Common.Interfaces;
using Asteroids.Common.Observer;

namespace Asteroids.Controller.MovingObjects
{
    internal sealed class MovingObjectsController : ITickable
    {
        private readonly IUpdateMovingObjectsModel _movingObjectsModel;
        private readonly IResultListener _resultListener;

        public MovingObjectsController(IUpdateMovingObjectsModel movingObjectsModel, IResultListener resultListener)
        {
            _movingObjectsModel = movingObjectsModel;
            _resultListener = resultListener;
        }
        public void Tick(float deltaTime)
        {
            _movingObjectsModel.Update(deltaTime, out var removedObjects);
            var movingObjectsData =
                _movingObjectsModel.MovingObjects.Select(m => new MovingObjectData(m.Position, m.Id, m.ResourceKey));
            var result = new MovingObjectsResult(movingObjectsData, removedObjects);
            _resultListener.SendResult(result);
        }
    }
}