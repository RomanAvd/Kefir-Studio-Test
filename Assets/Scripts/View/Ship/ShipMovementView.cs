using System.Collections.Generic;
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

        [SerializeField]
        private List<ParticleSystem> _particleSystems;

        [Inject]
        private void Initialize(IResultObserver observer)
        {
            observer.Bind(this);
        }

        public void OnResultReceived(IShipMovementResult result)
        {
            _positionRectTransform.anchoredPosition = result.Position;
            _rotationRectTransform.localRotation = Quaternion.AngleAxis(result.Rotation, Vector3.forward);
            foreach (var particleSystem in _particleSystems)
            {
                if (result.ThrustEnabled && !particleSystem.isPlaying)
                    particleSystem.Play();
                else if (!result.ThrustEnabled && particleSystem.isPlaying)
                    particleSystem.Stop();
            }
        }
    }
}