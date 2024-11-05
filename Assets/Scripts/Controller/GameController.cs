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
            _container.Bind(new GameObjectFactory(_container));
            var observer = new Observer();

            var model = new ShipModel(_gameSettings.ShipSettings);
            _container.Bind(model);
            var input = new PlayerInput(_gameSettings.PlayerInput);
            var shipController = new PlayerShipController(observer, model, input);


            _container.Bind(input);
            _container.Bind(observer);
            _container.Bind(shipController);

            _container.ResolveGameObject(_root, true);
        }
    }
}