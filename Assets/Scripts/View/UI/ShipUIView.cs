using Asteroids.Common.MonoInjection;
using Asteroids.Common.Observer;
using Asteroids.Controller.Ship;
using TMPro;
using UnityEngine;

namespace Asteroids.View.View.UI
{
    internal sealed class ShipUIView : MonoBehaviour, IResultReceiver<IShipMovementResult>
    {
        [SerializeField]
        private TextMeshProUGUI _speedLabel;
        [SerializeField]
        private TextMeshProUGUI _coordinatesLabel;
        [SerializeField]
        private TextMeshProUGUI _angleLabel;
        private TextMeshProUGUI _laserChargesLabel;

        [Inject]
        private void Initialize(IResultObserver observer)
        {
            observer.Bind(this);
        }

        public void OnResultReceived(IShipMovementResult result)
        {
            var shipOrientation = Mathf.Sign(Mathf.Sin(result.Rotation * Mathf.Deg2Rad));
            var angle = Mathf.Abs(result.Rotation) > 180 ? result.Rotation + shipOrientation * 360 : result.Rotation;
            _speedLabel.text = $"Speed: {result.Speed * 100:F1}";
            _angleLabel.text = $"Angle: {angle:F0}";
            _coordinatesLabel.text = $"Position x:{result.Position.x:F1} y: {result.Position.y:F1}";
        }
    }
}