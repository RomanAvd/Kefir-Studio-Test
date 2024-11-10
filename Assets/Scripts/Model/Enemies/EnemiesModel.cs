using System.Collections.Generic;
using Asteriods.Model.Movement;
using Asteriods.Model.Score;
using Asteroids.Common.Settings;
using Asteroids.Model.Ship;
using UnityEngine;

namespace Asteriods.Model.Enemies
{
    public interface IEnemiesModel
    {
        int EnemyCount { get; }
        void Spawn(IEnemySettings enemySettings, Vector2 position, Vector2 direction);
        bool TryDestroy(int id);
    }

    internal sealed class EnemiesModel : IEnemiesModel
    {
        public int EnemyCount => _enemies.Count;
        private readonly IMovingObjectsSpawner _movingObjectsModel;
        private readonly IShipModel _shipModel;
        private readonly IScoreModel _scoreModel;
        private Dictionary<int, IEnemyInternal> _enemies;

        public EnemiesModel(IMovingObjectsSpawner movingObjectsModel, IShipModel shipModel, IScoreModel scoreModel)
        {
            _movingObjectsModel = movingObjectsModel;
            _shipModel = shipModel;
            _scoreModel = scoreModel;
            _enemies = new Dictionary<int, IEnemyInternal>();
        }


        public void Spawn(IEnemySettings enemySettings, Vector2 position, Vector2 direction)
        {
            var movingObject = _movingObjectsModel.Add(enemySettings.MovingObjectSettings, position, direction);
            if (movingObject.MovementModel is ITargetFollowMovement targetFollowMovement)
                targetFollowMovement.SetTarget(_shipModel);
            var enemy = new Enemy(movingObject, enemySettings.Score, enemySettings.NestedEnemies);
            _enemies.Add(enemy.Id, enemy);
        }

        public bool TryDestroy(int id)
        {
            var success = _enemies.ContainsKey(id);
            if (!_enemies.TryGetValue(id, out var enemy))
                return success;

            _scoreModel.AddScore(_enemies[id].Score);
            _movingObjectsModel.Remove(id);
            _enemies.Remove(id);
            foreach (var nestedEnemy in enemy.NestedEnemies)
            {
                Spawn(nestedEnemy, enemy.MovingObject.MovementModel.Position, Random.insideUnitCircle);
            }

            return success;
        }
    }
}