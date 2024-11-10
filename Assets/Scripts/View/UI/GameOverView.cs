using Asteroids.Common.Enums;

namespace Asteroids.View.UI
{
    internal sealed class GameOverView : UIStateView
    {
        protected override GameState State => GameState.GameOver;
    }
}