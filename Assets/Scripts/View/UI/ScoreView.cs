using Asteroids.Common.MonoInjection;
using Asteroids.Common.Observer;
using Asteroids.Controller;
using TMPro;
using UnityEngine;

namespace Asteroids.View.UI
{
    internal sealed class ScoreView : MonoBehaviour, IResultReceiver<IUpdateScoreResult>
    {
        [SerializeField]
        private TextMeshProUGUI _text;

        [Inject]
        private void Initialize(IResultObserver observer)
        {
            observer.Bind(this);
        }

        public void OnResultReceived(IUpdateScoreResult result)
        {
            _text.text = result.TotalScore.ToString();
        }
    }
}