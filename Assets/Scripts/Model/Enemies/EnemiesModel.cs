﻿using System.Collections.Generic;
using Asteriods.Model.Movement;
using Asteroids.Common.Settings;
using Asteroids.Model.Ship;
using UnityEngine;

namespace Asteriods.Model.Enemies
{
    public interface IEnemiesModel
    {
        IEnumerable<IEnemy> Enemies { get; }
        int EnemyCount { get; }
        void Spawn(IEnemySettings enemySettings, Vector2 position, Vector2 direction);
        bool TryDestroy(int id);
    }

    internal sealed class EnemiesModel : IEnemiesModel
    {
        public IEnumerable<IEnemy> Enemies => _enemies.Values;
        public int EnemyCount => _enemies.Count;
        private readonly IMovingObjectsSpawner _movingObjectsModel;
        private readonly IShipModel _shipModel;
        private Dictionary<int, IEnemy> _enemies;

        public EnemiesModel(IMovingObjectsSpawner movingObjectsModel, IShipModel shipModel)
        {
            _movingObjectsModel = movingObjectsModel;
            _shipModel = shipModel;
            _enemies = new Dictionary<int, IEnemy>();
        }


        public void Spawn(IEnemySettings enemySettings, Vector2 position, Vector2 direction)
        {
            var movingObject = _movingObjectsModel.Add(enemySettings.MovingObjectSettings, position, direction);
            if (movingObject.MovementModel is ITargetFollowMovement targetFollowMovement)
                targetFollowMovement.SetTarget(_shipModel);
            var enemy = new Enemy(movingObject, enemySettings.Score);
            _enemies.Add(enemy.Id, enemy);
        }

        public bool TryDestroy(int id)
        {
            var success = _enemies.ContainsKey(id);
            if (_enemies.ContainsKey(id))
            {
                _movingObjectsModel.Remove(id);
                _enemies.Remove(id);
            }

            return success;
        }
    }
}