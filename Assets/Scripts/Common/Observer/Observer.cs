using System;
using System.Collections.Generic;
using System.Linq;
using Asteroids.Common.Interfaces;

namespace Asteroids.Common.Observer
{
    public interface IResultObserver
    {
        void Bind<T>(IResultReceiver<T> receiver) where T : IResult;
        void UnBind<T>(IResultReceiver<T> receiver) where T : IResult;
    }

    internal delegate void OnResultReceivedHandler<in T>(T result) where T : IResult;

    public sealed class Observer : IResultListener, IResultObserver, ITickable
    {
        private Dictionary<Type, Dictionary<int,object>> _receivers;
        private Queue<Action> _invokeQueue;
        private Queue<Action> _nextTickQueue;

        public Observer()
        {
            _receivers = new Dictionary<Type, Dictionary<int, object>>();
            _invokeQueue = new Queue<Action>();
            _nextTickQueue = new Queue<Action>();
        }

        public void SendResult<T>(T result) where T : IResult
        {
            _nextTickQueue.Enqueue(() => Invoke(result));
        }

        public void Bind<T>(IResultReceiver<T> receiver) where T : IResult
        {
            var type = typeof(T);
            if (!_receivers.ContainsKey(type))
                _receivers.Add(type, new Dictionary<int,object>());

            var hash = receiver.GetHashCode();
            if (!_receivers[type].TryAdd(hash, new OnResultReceivedHandler<T>(receiver.OnResultReceived)))
            {
                throw new Exception($"{nameof(receiver)} is already added");
            }
        }

        public void UnBind<T>(IResultReceiver<T> receiver) where T : IResult
        {
            var type = typeof(T);
            if (!_receivers.ContainsKey(type))
                return;

            var hash = receiver.GetHashCode();
            _receivers[type].Remove(hash);
        }

        private void Invoke<T>(T result) where T : IResult
        {
            var receivers = _receivers
                            .Where(r => r.Key.IsAssignableFrom(typeof(T)))
                            .SelectMany(r => r.Value.Values);
            foreach (var receiver in receivers)
            {
                (receiver as OnResultReceivedHandler<T>)?.Invoke(result);
            }
        }

        public void Tick(float deltaTime)
        {
            while (_nextTickQueue.TryDequeue(out var action))
                _invokeQueue.Enqueue(action);

            while (_invokeQueue.TryDequeue(out var action))
                action.Invoke();
        }
    }
}