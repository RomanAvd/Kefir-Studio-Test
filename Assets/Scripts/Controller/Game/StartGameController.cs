using Asteriods.Model.Score;
using Asteroids.Common.MonoInjection;
using Asteroids.Common.Observer;
using Asteroids.Controller.Common;
using Asteroids.Model.Ship;

namespace Asteroids.Controller.Game
{
    internal sealed class StartGameController : ButtonController
    {
        private IScoreModel _scoreModel;
        private IShipModel _shipModel;
        private IResultListener _listener;

        [Inject]
        private void Initialize(IResultListener listener, IShipModel shipModel, IScoreModel scoreModel)
        {
            _listener = listener;
            _shipModel = shipModel;
            _scoreModel = scoreModel;
        }

        protected override void Do()
        {
            _scoreModel.Reset();
            _shipModel.Reset();
            _listener.SendResult(new StartGameResult());
        }
    }
}