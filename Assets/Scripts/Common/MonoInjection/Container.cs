using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Asteroids.Common.MonoInjection
{
    public interface IContainer
    {
        void ResolveGameObject(GameObject gameObject, bool includeChildren = false);
        void Resolve(object @object);
    }

    public sealed class Container : IContainer
    {
        private Dictionary<Type, object> _objects;

        public Container()
        {
            _objects = new Dictionary<Type, object>();
        }

        public void Bind<T>(T @object)
        {
            _objects.Add(typeof(T), @object);
        }

        public void ResolveGameObject(GameObject gameObject, bool includeChildren = false)
        {
            var entities = includeChildren
                ? gameObject.GetComponentsInChildren<IGameEntity>()
                : gameObject.GetComponents<IGameEntity>();

            foreach (var gameEntity in entities)
                Resolve(gameEntity);
        }

        public void Resolve(object @object)
        {
            var methods = @object
                          .GetType()
                          .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.SuppressChangeType)
                          .Where(m => m.GetCustomAttributes(typeof(Inject), true).Length > 0);

            foreach (var methodInfo in methods)
            {
                methodInfo.Invoke(@object, GetParameters(methodInfo).ToArray());
            }
        }

        private IEnumerable<object> GetParameters(MethodInfo methodInfo)
        {
            foreach (var parameterInfo in methodInfo.GetParameters())
            {
                if (!_objects.ContainsKey(parameterInfo.ParameterType))
                    throw new ArgumentNullException($"{parameterInfo.ParameterType} not found in container");

                yield return _objects[parameterInfo.ParameterType];
            }
        }
    }
}