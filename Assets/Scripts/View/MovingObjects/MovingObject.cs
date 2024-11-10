using Asteroids.Common.MonoInjection;
using Asteroids.Controller;
using UnityEngine;

namespace Asteroids.View.MovingObjects
{
    internal interface IMovingObject : IGameEntity
    {
        string Key { get;}
        public void UpdatePosition(Vector2 position, float rotation);
        public void Show();
        public void Hide();
        public void SetId(int id);
    }

    internal sealed class MovingObject : MonoBehaviour, IMovingObject
    {
        public string Key { get; private set; }
        [SerializeField]
        private RectTransform _rectTransform;

        [SerializeField]
        private RectTransform _rotationTransform;

        private ICollisionController _collisionController;
        private int _id;

        [Inject]
        private void Initialize(ICollisionController collisionController)
        {
            _collisionController = collisionController;
        }

        private void Start()
        {
            _rectTransform = transform as RectTransform;
        }

        public void Setup(string key)
        {
            Key = key;
        }

        public void UpdatePosition(Vector2 position,float rotation)
        {
            _rectTransform.anchoredPosition = position;
            _rotationTransform.localRotation = Quaternion.AngleAxis(rotation, Vector3.forward);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetId(int id)
        {
            _id = id;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            _collisionController.OnCollision(_id);
        }
    }
}