using System;
using System.Collections.Generic;
using Asteriods.Model.Model;
using Asteroids.Common.Interfaces;
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
        private List<ITickable> _tickables;

        private void Start()
        {
            _container = new Container();
            _tickables = new List<ITickable>();
            _container.Bind(new GameObjectFactory(_container));
            var observer = new Observer();

            var model = new ShipModel(_gameSettings.ShipSettings);
            _container.Bind(model);
            var input = new PlayerInput(_gameSettings.PlayerInput);
            var shipcontroller = new PlayerShipController(observer, model, input);
            _tickables.Add(shipcontroller);
            _tickables.Add(observer);
            _container.Bind(input);
            _container.Bind<IResultObserver>(observer);
            _container.Bind(shipcontroller);

            InitializeModels();
            InitializeControllers();

            _container.ResolveGameObject(_root, true);
        }

        private void InitializeModels()
        {
            var modelInstaller = new ModelInstaller();
            //modelInstaller.InstallModels(_container, _gameSettings);
        }

        private void InitializeControllers()
        {
        }

        private void Update()
        {
            _tickables.ForEach(t => t?.Tick(Time.deltaTime));
        }
    }
}