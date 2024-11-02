using UnityEngine;
using Asteroids.Common.MonoInjection;

namespace Asteroids.Controller
{
    internal sealed class GameController : MonoBehaviour
    {
        private Container _container;

        [SerializeField]
        private GameObject root;

        private void Start()
        {
            _container = new Container();


            _container.ResolveGameObject(root, true);
        }
    }
}