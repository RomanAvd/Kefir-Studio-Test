using Asteroids.Common.MonoInjection;
using Asteroids.Common.Observer;
using Asteroids.Controller.Ship;
using UnityEngine;

namespace Asteroids.View.Ship
{
    internal class ShipMovementView : MonoBehaviour, IResultReceiver<IShipMovementResult>
    {
        [SerializeField]
        private RectTransform _rotationRectTransform;

        [SerializeField]
        private RectTransform _positionRectTransform;

        [Inject]
        private void Initialize(IResultObserver observer)
        {
            observer.Bind(this);
        }

        public void OnResultReceived(IShipMovementResult result)
        {
            _positionRectTransform.anchoredPosition = result.Position;
            _rotationRectTransform.localRotation = Quaternion.AngleAxis(result.Rotation, Vector3.forward);
        }
    }
}