using System;
using UnityEngine;

namespace Asteroids.View.View.MovingObjects
{
    internal interface IMovingObject
    {
        string Key { get;}
        public void UpdatePosition(Vector2 position);
        public void Show();
        public void Hide();
    }

    internal sealed class MovingObject : MonoBehaviour, IMovingObject
    {
        public string Key { get; private set; }
        [SerializeField]
        private RectTransform _rectTransform;

        private void Start()
        {
            _rectTransform = transform as RectTransform;
        }

        public void Setup(string key)
        {
            Key = key;
        }

        public void UpdatePosition(Vector2 position)
        {
            _rectTransform.anchoredPosition = position;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}