using Asteroids.Common.Enums;
using Asteroids.Model.Ship;

namespace Asteriods.Model.Score
{
    public interface IScoreModel
    {
        int Score { get; }
        void AddScore(int score);
        void Reset();
    }

    internal sealed class ScoreModel : IScoreModel
    {
        private readonly IShipModel _shipModel;
        public int Score { get; private set; }

        public ScoreModel(IShipModel shipModel)
        {
            _shipModel = shipModel;
        }

        public void AddScore(int score)
        {
            if (_shipModel.Status != ShipStatus.Dead)
                Score += score;
        }

        public void Reset()
        {
            Score = 0;
        }
    }
}