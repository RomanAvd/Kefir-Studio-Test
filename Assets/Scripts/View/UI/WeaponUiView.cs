using Asteroids.Common.MonoInjection;
using Asteroids.Common.Observer;
using Asteroids.Controller;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.View.UI
{
    internal sealed class WeaponUiView : MonoBehaviour, IResultReceiver<IUpdateWeaponResult>
    {
        [SerializeField]
        private TextMeshProUGUI _cooldownText;
        [SerializeField]
        private TextMeshProUGUI _chargesText;
        [SerializeField]
        private Image _fillImage;

        [SerializeField]
        private GameObject _cooldownGroup;

        [Inject]
        private void Initialize(IResultObserver observer)
        {
            observer.Bind(this);
        }

        public void OnResultReceived(IUpdateWeaponResult result)
        {
            _cooldownGroup.SetActive(result.CooldownRemaining > 0);
            _fillImage.fillAmount = (result.Cooldown - result.CooldownRemaining) / result.Cooldown;
            _cooldownText.text = result.CooldownRemaining.ToString("F1");
            _chargesText.text = $"X {result.Charges}";
        }
    }
}