using Asteroids.Common.Observer;
using Asteroids.Controller.Common;
using Asteroids.Model.Ship;

namespace Asteroids.Controller.Game
{
    public interface IGameOverController
    {
        void GameOver();
    }

    internal sealed class GameOverController : IGameOverController
    {
        private readonly IResultListener _resultListener;
        private readonly IShipModel _shipModel;

        public GameOverController(IResultListener resultListener, IShipModel shipModel)
        {
            _resultListener = resultListener;
            _shipModel = shipModel;
        }

        public void GameOver()
        {
            _shipModel.Die();
            _resultListener.SendResult(ResultFactory.GetGameOverResult(_shipModel));
        }
    }
}