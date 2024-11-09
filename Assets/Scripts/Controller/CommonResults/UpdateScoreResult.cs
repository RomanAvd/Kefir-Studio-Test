using Asteroids.Common.Observer;

namespace Asteroids.Controller.CommonResults
{
    public interface IUpdateScoreResult : IResult
    {
        int TotalScore { get; }
    }

    internal sealed class UpdateScoreResult : IUpdateScoreResult
    {
        public int TotalScore { get; }

        public UpdateScoreResult(int totalScore)
        {
            TotalScore = totalScore;
        }
    }
}