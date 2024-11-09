using Asteriods.Model.Enemies;
using Asteriods.Model.Movement;
using Asteriods.Model.Score;
using Asteroids.Common.Enums;
using Asteroids.Common.Observer;
using Asteroids.Controller.CommonResults;
using Asteroids.Model.Ship;
using UnityEngine;

namespace Asteroids.Controller
{
    public interface ICollisionController
    {
        void OnCollision(int id);
    }

    internal sealed class CollisionController : ICollisionController
    {
        private readonly IResultListener _listener;
        private readonly IEnemiesModel _enemiesModel;
        private readonly IMovingObjectsModel _movingObjectsModel;
        private readonly IShipModel _shipModel;
        private readonly IScoreModel _scoreModel;

        public CollisionController(IResultListener listener,
                                   IEnemiesModel enemiesModel,
                                   IMovingObjectsModel movingObjectsModel,
                                   IShipModel shipModel,
                                   IScoreModel scoreModel)
        {
            _listener = listener;
            _enemiesModel = enemiesModel;
            _movingObjectsModel = movingObjectsModel;
            _shipModel = shipModel;
            _scoreModel = scoreModel;
        }

        public void OnCollision(int id)
        {
            if (_enemiesModel.TryDestroy(id))
            {
                _listener.SendResult(new ObjectDestroyedResult(id));
                _listener.SendResult(new UpdateScoreResult(_scoreModel.Score));
                return;
            }

            if (!_movingObjectsModel.TryGetObject(id, out var movingObject) ||
                !movingObject.MovementModel.DestroyOnCollision)
                return;

            _movingObjectsModel.Remove(id);
            _listener.SendResult(new ObjectDestroyedResult(id));
        }
    }
}