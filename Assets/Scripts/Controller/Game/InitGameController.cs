using Asteriods.Model;
using Asteroids.Common.Enums;
using UnityEngine;
using Asteroids.Common.MonoInjection;
using Asteroids.Common.Observer;
using Asteroids.Common.Settings;
using Asteroids.Controller.Enemies;
using Asteroids.Controller.Input;
using Asteroids.Controller.MovingObjects;
using Asteroids.Controller.Ship;

namespace Asteroids.Controller.Game
{
    internal sealed class InitGameController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _root;
        [SerializeField]
        private GameSettings _gameSettings;

        private Container _container;

        private void Start()
        {
            _container = new Container();
            var observer = new Observer();
            _container.Bind(observer);
            _container.Bind(_gameSettings);

            ModelInstaller.InstallModels(_container, _gameSettings);
            InstallControllers();

            _container.ResolveGameObject(_root, true);

            observer.SendResult(new StateResult(GameState.MainMenu));
        }

        private void InstallControllers()
        {
            _container.InstantiateAndBind<PlayerInput>();
            _container.InstantiateAndBind<PlayerShipController>();
            _container.InstantiateAndBind<MovingObjectsController>();
            _container.InstantiateAndBind<EnemySpawnController>();
            _container.InstantiateAndBind<CollisionController>();
            _container.InstantiateAndBind<GameOverController>();
        }
    }
}