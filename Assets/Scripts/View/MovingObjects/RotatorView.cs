using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids.View.View.MovingObjects
{
    internal sealed class RotatorView : MonoBehaviour
    {
        [SerializeField]
        private Transform _transform;

        [SerializeField]
        private float _minAngle;

        [SerializeField]
        private float _maxAngle;

        private float _angle;
        private float _delta;
        private void Start()
        {
            _delta = Random.Range(_minAngle, _maxAngle) * Mathf.Sign(Random.Range(-1,1));
        }

        private void Update()
        {
            _angle += _delta * Time.deltaTime;
            _transform.localRotation = Quaternion.AngleAxis(_angle, Vector3.forward);
        }
    }
}