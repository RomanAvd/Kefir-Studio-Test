using System.Collections.Generic;
using Asteriods.Model.Movement;
using Asteroids.Common.Settings;

namespace Asteriods.Model.Enemies
{
    public interface IEnemy
    {
        IMovingObject MovingObject { get; }
        int Score { get; }
        int Id { get; }
    }

    internal interface IEnemyInternal : IEnemy
    {
        IReadOnlyList<IEnemySettings> NestedEnemies { get; }
    }

    internal sealed class Enemy : IEnemyInternal
    {
        public IMovingObject MovingObject { get; }
        public IReadOnlyList<IEnemySettings> NestedEnemies { get; }
        public int Score { get; }
        public int Id => MovingObject.Id;

        public Enemy(IMovingObject movingObject, int score, IEnumerable<IEnemySettings> nestedEnemies)
        {
            MovingObject = movingObject;
            Score = score;
            NestedEnemies = new List<IEnemySettings>(nestedEnemies);
        }
    }
}