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
        public int Score { get; private set; }

        public void AddScore(int score)
        {
            Score += score;
        }

        public void Reset()
        {
            Score = 0;
        }
    }
}