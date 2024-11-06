using System;
using System.Collections.Generic;
using Asteroids.Common.Interfaces;
using Asteroids.Common.MonoInjection;
using UnityEngine;

namespace Asteroids.View.View
{
    internal sealed class MainTicker : MonoBehaviour, IGameEntity
    {
        private List<ITickable> _tickables;

        [Inject]
        private void Initialize(IEnumerable<ITickable> tickables)
        {
            _tickables.Clear();
            _tickables.AddRange(tickables);
        }

        private void Awake()
        {
            _tickables = new List<ITickable>();
        }

        private void Update()
        {
            _tickables.ForEach(t => t?.Tick(Time.deltaTime));
        }
    }
}