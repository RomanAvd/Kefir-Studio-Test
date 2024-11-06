using Asteroids.Common.MonoInjection;
using Asteroids.Common.Observer;
using Asteroids.Controller.Ship;
using UnityEngine;

namespace Asteroids.View.Ship
{
    internal class ShipView : MonoBehaviour, IResultReceiver<IShipMovementResult>, IGameEntity
    {
        [SerializeField]
        private RectTransform _rotationRectTransform;

        [Inject]
        private void Initialize(IResultObserver observer)
        {
            observer.Bind(this);
        }

        public void OnResultReceived(IShipMovementResult result)
        {
            (transform as RectTransform).anchoredPosition = result.Position;
            _rotationRectTransform.localRotation = Quaternion.AngleAxis(result.Rotation, Vector3.forward);
        }
    }
}