using Asteriods.Model;
using Asteriods.Model.Movement;
using UnityEngine;
using Asteroids.Common.MonoInjection;
using Asteroids.Common.Observer;
using Asteroids.Common.Settings;
using Asteroids.Controller.Input;
using Asteroids.Controller.Ship;
using Asteroids.Model.Ship;

namespace Asteroids.Controller
{
    internal sealed class GameController : MonoBehaviour
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

            _container.ResolveGameObject(_root, true);
        }

        private void InstallControllers()
        {
            _container.InstantiateAndBind<PlayerInput>();
            _container.InstantiateAndBind<PlayerShipController>();
        }
    }
}