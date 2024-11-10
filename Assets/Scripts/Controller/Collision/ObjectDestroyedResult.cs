namespace Asteroids.Controller
{
    public interface IObjectDestroyedResult : IUpdateScoreResult
    {
        int Id { get; }
    }

    internal sealed class ObjectDestroyedResult : IObjectDestroyedResult
    {
        public int Id { get; }
        public int TotalScore { get; }

        public ObjectDestroyedResult(int id, int totalScore)
        {
            Id = id;
            TotalScore = totalScore;
        }

    }
}