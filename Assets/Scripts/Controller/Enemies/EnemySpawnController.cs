using System.Linq;
using Asteriods.Model;
using Asteriods.Model.Enemies;
using Asteroids.Common;
using Asteroids.Common.Interfaces;
using Asteroids.Common.Settings;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids.Controller.Enemies
{
    internal sealed class EnemySpawnController : ITickable
    {
        private readonly IEnemySpawnSettings _enemySpawnSettings;
        private readonly IEnemiesModel _enemiesModel;
        private readonly IScreenBorderModel _screenBorderModel;
        private float _newxtSpawnTime;

        public EnemySpawnController(IGameSettings gameSettings, IEnemiesModel enemiesModel, IScreenBorderModel screenBorderModel)
        {
            _enemySpawnSettings = gameSettings.SpawnSettings;
            _enemiesModel = enemiesModel;
            _screenBorderModel = screenBorderModel;
            _newxtSpawnTime = Time.time;
        }

        public void Tick(float deltaTime)
        {
            var minSpawnDelta = _enemySpawnSettings.MinimumSpawnedEnemies - _enemiesModel.EnemyCount;
            if (minSpawnDelta > 0)
            {
                for (int i = 0; i < minSpawnDelta; i++)
                {
                    SpawnRandomEnemy();
                }
            }

            if (Time.time > _newxtSpawnTime && _enemiesModel.EnemyCount < _enemySpawnSettings.SpawnLimit)
            {
                SpawnRandomEnemy();
            }
        }

        private void SpawnRandomEnemy()
        {
            _enemiesModel.Spawn(RollEnemy(), _screenBorderModel.GetRandomOffscreenPosition(), RandomHelper.RandomNormalizedVector());
            _newxtSpawnTime = Time.time + _enemySpawnSettings.SpawnDelay;
        }

        private IEnemySettings RollEnemy()
        {
            var weightSum = _enemySpawnSettings.Enemies.Sum(e => e.Weight);
            var rnd = Random.Range(0, weightSum);
            foreach (var enemy in _enemySpawnSettings.Enemies)
            {
                if (rnd < enemy.Weight)
                    return enemy.Enemy;

                rnd -= enemy.Weight;
            }

            return null;
        }
    }
}