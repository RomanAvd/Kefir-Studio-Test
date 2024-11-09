using Asteroids.Common.MonoInjection;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.Controller.Common
{
    [RequireComponent(typeof(Button))]
    internal abstract class ButtonController : MonoBehaviour, IGameEntity
    {
        private void Start()
        {
            var button = GetComponent<Button>();
            button.onClick.AddListener(Do);
        }

        protected abstract void Do();
    }
}