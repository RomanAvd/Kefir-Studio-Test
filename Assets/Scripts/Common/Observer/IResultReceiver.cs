namespace Asteroids.Common.Observer
{
    public interface IResultReceiver<T> where T : IResult
    {
        void OnResultReceived(T result);
    }
}