using Asteriods.Model.Enemies;
using Asteriods.Model.Movement;
using Asteriods.Model.Score;
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
        private readonly IScoreModel _scoreModel;
        private readonly IShipModel _shipModel;

        public CollisionController(IResultListener listener,
                                   IEnemiesModel enemiesModel,
                                   IMovingObjectsModel movingObjectsModel,
                                   IScoreModel scoreModel,
                                   IShipModel shipModel)
        {
            _listener = listener;
            _enemiesModel = enemiesModel;
            _movingObjectsModel = movingObjectsModel;
            _scoreModel = scoreModel;
            _shipModel = shipModel;
        }

        public void OnCollision(int id)
        {
            Debug.Log($"collision {id}");
            if (_enemiesModel.TryDestroy(id))
            {
                Debug.Log("enemy");
                _listener.SendResult(new UpdateScoreResult(_scoreModel.Score));
                _listener.SendResult(new ObjectDestroyedResult(id));
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