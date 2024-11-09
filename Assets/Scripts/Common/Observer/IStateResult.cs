using Asteroids.Common.Enums;

namespace Asteroids.Common.Observer
{
    public interface IStateResult : IResult
    {
        GameState State { get; }
    }

    public class StateResult : IStateResult
    {
        public GameState State { get; }

        public StateResult(GameState state)
        {
            State = state;
        }
    }
}