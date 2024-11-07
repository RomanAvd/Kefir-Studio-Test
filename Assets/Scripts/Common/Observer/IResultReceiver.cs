using Asteroids.Common.MonoInjection;

namespace Asteroids.Common.Observer
{
    public interface IResultReceiver<T> : IGameEntity where T : IResult
    {
        void OnResultReceived(T result);
    }
}