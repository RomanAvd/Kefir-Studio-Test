using Asteroids.Common.Enums;
using Asteroids.Common.MonoInjection;
using Asteroids.Common.Observer;
using UnityEngine;

namespace Asteroids.View.View.UI
{
    internal abstract class UIStateView : MonoBehaviour, IResultReceiver<IStateResult>
    {
        protected abstract GameState State { get; }

        [Inject]
        private void Initialize(IResultObserver observer)
        {
            observer.Bind(this);
        }

        public void OnResultReceived(IStateResult result)
        {
            gameObject.SetActive(State == result.State);
        }
    }
}