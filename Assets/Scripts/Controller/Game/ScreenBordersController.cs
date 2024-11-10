using Asteriods.Model;
using Asteroids.Common.MonoInjection;
using UnityEngine;

namespace Asteroids.Controller.Game
{
    internal class ScreenBordersController : MonoBehaviour, IGameEntity
    {
        [SerializeField]
        private RectTransform _gameRectTransform;

        private IScreenBorderModel _screenBorderModel;

        [Inject]
        private void Initialize(IScreenBorderModel screenBorderModel)
        {
            _screenBorderModel = screenBorderModel;
            _screenBorderModel.SetBorders(_gameRectTransform.rect);
        }
    }
}