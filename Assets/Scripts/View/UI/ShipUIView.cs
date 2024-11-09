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
            var angle = Mathf.Abs(result.Rotation) > 180 ?  360 - Mathf.Sign(result.Rotation) * result.Rotation : result.Rotation;
            _speedLabel.text = $"Speed: {result.Speed * 100:F1}";
            _angleLabel.text = $"Angle: {angle}";
            _coordinatesLabel.text = $"Position x:{result.Position.x:F1} y: {result.Position.y:F1}";
        }
    }
}