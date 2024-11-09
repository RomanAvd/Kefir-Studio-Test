using Asteroids.Common.Enums;
using Asteroids.Common.Observer;

namespace Asteroids.Controller.Game
{
    internal sealed class StartGameResult : StateResult
    {
        public StartGameResult() : base(GameState.Game)
        {
        }
    }
}