using Asteroids.Common.Observer;

namespace Asteroids.Controller
{
    public interface IObjectDestroyedResult : IResult
    {
        int Id { get; }
    }

    internal sealed class ObjectDestroyedResult : IObjectDestroyedResult
    {
        public int Id { get; }

        public ObjectDestroyedResult(int id)
        {
            Id = id;
        }
    }
}