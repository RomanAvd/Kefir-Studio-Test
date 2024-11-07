using Asteriods.Model.Movement;

namespace Asteriods.Model.Enemies
{
    public interface IEnemy
    {
        IMovingObject MovingObject { get; }
        int Score { get; }
        int Id { get; }
    }

    internal sealed class Enemy : IEnemy
    {
        public IMovingObject MovingObject { get; }
        public int Score { get; }
        public int Id => MovingObject.Id;

        public Enemy(IMovingObject movingObject, int score)
        {
            MovingObject = movingObject;
            Score = score;
        }
    }
}