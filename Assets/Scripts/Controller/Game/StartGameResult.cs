using Asteroids.Common.Enums;
using Asteroids.Common.Observer;

namespace Asteroids.Controller.Game
{
    public interface IStartGameResult : IStateResult, IUpdateScoreResult
    {

    }

    internal sealed class StartGameResult : StateResult, IStartGameResult
    {
        public int TotalScore { get; }

        public StartGameResult(int totalScore) : base(GameState.Game)
        {
            TotalScore = totalScore;
        }
    }
}