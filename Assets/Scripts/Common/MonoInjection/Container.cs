using System;
using System.Collections;
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
        void Bind(object @object);
        T InstantiateAndBind<T>() where T : class;
    }

    public sealed class Container : IContainer
    {
        private Dictionary<Type, HashSet<object>> _objects;

        public Container()
        {
            _objects = new Dictionary<Type, HashSet<object>>();
            Bind(new GameObjectFactory(this));
        }

        public void Bind(object @object)
        {
            foreach (var type in @object.GetType().GetInterfaces())
            {
                if (!_objects.ContainsKey(type))
                    _objects.Add(type, new HashSet<object>());

                _objects[type].Add(@object);
            }
        }

        public T InstantiateAndBind<T>() where T : class
        {
            var ctorParams = typeof(T).GetConstructors().Select(c => c.GetParameters()).First(p => p.Length > 0);
            var instance = (T)Activator.CreateInstance(typeof(T), GetParameters(ctorParams).ToArray());
            Bind(instance);
            return instance;
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
                          .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                          .Where(m => m.GetCustomAttributes(typeof(Inject), true).Length > 0);

            foreach (var methodInfo in methods)
            {
                methodInfo.Invoke(@object, GetParameters(methodInfo.GetParameters()).ToArray());
            }
        }

        private IEnumerable<object> GetParameters(ParameterInfo[] parameterInfos)
        {
            foreach (var parameterInfo in parameterInfos)
            {
                if (parameterInfo.ParameterType.IsGenericType && parameterInfo.ParameterType.GetGenericTypeDefinition().IsAssignableFrom(typeof(IEnumerable<>)))
                {
                    var paramType = parameterInfo.ParameterType.GetGenericArguments()[0];
                    if (!_objects.ContainsKey(paramType))
                        throw new ArgumentNullException($"{parameterInfo.ParameterType} not found in container");

                    Type listType = typeof(List<>).MakeGenericType(paramType);
                    var instance = (IList)Activator.CreateInstance(listType);
                    foreach (var obj in _objects[paramType])
                        instance.Add(obj);
                    yield return instance;
                }
                else
                {
                    if (!_objects.ContainsKey(parameterInfo.ParameterType))
                        throw new ArgumentNullException($"{parameterInfo.ParameterType} not found in container");

                    yield return _objects[parameterInfo.ParameterType].Single();
                }
            }
        }
    }
}