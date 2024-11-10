using System;
using Asteroids.Common.Enums;
using Asteroids.Common.MonoInjection;
using Asteroids.Common.Observer;
using Asteroids.Controller.Game;
using Asteroids.Controller.Ship;
using UnityEngine;

namespace Asteroids.View.Ship
{
    internal sealed class ShipStatusView : MonoBehaviour, IResultReceiver<IUpdateShipStatusResult>
    {
        [SerializeField]
        private CanvasGroup _shipGroup;

        [SerializeField]
        private Collider2D _collider;

        private ShipStatus _current;
        private IGameOverController _gameOverController;

        [Inject]
        private void Initialize(IGameOverController gameOverController, IResultObserver resultObserver)
        {
            _gameOverController = gameOverController;
            resultObserver.Bind(this);
            _collider.enabled = false;
        }

        public void OnResultReceived(IUpdateShipStatusResult result)
        {
            if (_current == result.Status)
                return;

            _current = result.Status;
            gameObject.SetActive(true);
            switch (_current)
            {
                case ShipStatus.None:
                    gameObject.SetActive(false);
                    break;
                case ShipStatus.Default:
                    _collider.enabled = true;
                    _shipGroup.alpha = 1;
                    break;
                case ShipStatus.Invincible:
                    _shipGroup.alpha = 0.5f;
                    _collider.enabled = false;
                    break;
                case ShipStatus.Dead:
                    gameObject.SetActive(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (_current == ShipStatus.Default)
                _gameOverController.GameOver();
        }
    }
}