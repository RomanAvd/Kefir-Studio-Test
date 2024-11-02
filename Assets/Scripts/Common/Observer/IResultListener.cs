namespace Asteroids.Common.Observer
{
    public interface IResultListener
    {
        void SendResult<T>(T result) where T : IResult;
    }
}