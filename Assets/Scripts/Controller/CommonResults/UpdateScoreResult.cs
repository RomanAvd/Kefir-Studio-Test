using Asteroids.Common.Observer;

namespace Asteroids.Controller
{
    public interface IUpdateScoreResult : IResult
    {
        int TotalScore { get; }
    }
}