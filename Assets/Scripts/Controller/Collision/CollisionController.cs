using Asteriods.Model.Enemies;
using Asteriods.Model.Movement;
using Asteriods.Model.Score;
using Asteroids.Common.Observer;
using Asteroids.Controller.Common;

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

        public CollisionController(IResultListener listener,
                                   IEnemiesModel enemiesModel,
                                   IMovingObjectsModel movingObjectsModel,
                                   IScoreModel scoreModel)
        {
            _listener = listener;
            _enemiesModel = enemiesModel;
            _movingObjectsModel = movingObjectsModel;
            _scoreModel = scoreModel;
        }

        public void OnCollision(int id)
        {
            if (_enemiesModel.TryDestroy(id))
            {
                _listener.SendResult(ResultFactory.GetObjectDestroyedResult(id, _scoreModel));
                return;
            }

            if (!_movingObjectsModel.TryGetObject(id, out var movingObject) ||
                !movingObject.MovementModel.DestroyOnCollision)
                return;

            _movingObjectsModel.Remove(id);
            _listener.SendResult(ResultFactory.GetObjectDestroyedResult(id, _scoreModel));
        }
    }
}